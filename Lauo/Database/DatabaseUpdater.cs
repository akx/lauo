using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lauo.Database {
	class DatabaseUpdater {
		private LaunchDatabase database;
		private IList<LaunchEntry> foundFiles;
		private Settings config;

		public DatabaseUpdater(LaunchDatabase database) {
			this.database = database;
			this.config = Settings.Instance;
			this.foundFiles = new List<LaunchEntry>();
		}

		public void Run() {
			Stopwatch sw = Stopwatch.StartNew();
			FindFiles();
			Debug.Print("Found {0} files; {1}", foundFiles.Count, sw.Elapsed);
			ParseLinks();
			Debug.Print("Parsed .lnks; {0}", sw.Elapsed);
			int nDropped = PruneDuplicates();
			Debug.Print("Pruned {0} duplicates; {1}", nDropped, sw.Elapsed);
			database.UpdateEntries(foundFiles, true);
			sw.Stop();
			Debug.Print("*** Finished. {0} elapsed total", sw.Elapsed);

		}

		private void ParseLinks() {
			foreach (var launchEntry in foundFiles) {
				var ext = Path.GetExtension(launchEntry.FullPath).ToLowerInvariant();
				if (ext == ".lnk") {
					using (var ss = new ShellShortcut(launchEntry.FullPath)) {
						if(!string.IsNullOrEmpty(ss.Path)) launchEntry.LinkTarget = ss.Path;
					}
				}
			}
			
		}

		private int PruneDuplicates() {
			var byPath = new Dictionary<string, List<LaunchEntry>>();
			foreach (var launchEntry in foundFiles) {
				byPath.SetDefault(launchEntry.FullPath).Add(launchEntry);
				if(!string.IsNullOrEmpty(launchEntry.LinkTarget)) byPath.SetDefault(launchEntry.LinkTarget).Add(launchEntry);
			}
			int dropped = 0;
			foreach (var byPathPair in byPath) {
				if(byPathPair.Value.Count <= 1) continue;
				foreach(var elToDrop in byPathPair.Value.OrderBy(entry => entry.NamePath.Length).Skip(1)) {
					foundFiles.Remove(elToDrop);
					dropped++;
				}
				
				
			}
			return dropped;
		}


		private void FindFiles() {
			var allowedExtensions = Settings.Instance.AllowedExtensions;
			var disallowedPathFragments = Settings.Instance.DisallowedPathFragments.ToArray();
			foreach (var basePath in config.SearchPaths) {
				
				var searchPaths = new Queue<string>();
				searchPaths.Enqueue(basePath);
				while (searchPaths.Count > 0) {
					var path = searchPaths.Dequeue();
					string[] entries = null;
					//Debug.Print(path);
					try {
						entries = Directory.GetFileSystemEntries(path);
					} catch {
						// XXX: Log exception!
						continue;
					}
					foreach (var dirent in entries) {
						if (disallowedPathFragments.Any(s => dirent.Contains(s))) continue;
						if (Directory.Exists(dirent)) {
							searchPaths.Enqueue(dirent);
							continue;
						}
						else {
							var ext = Path.GetExtension(dirent).ToLowerInvariant();
							if (allowedExtensions.Contains(ext)) {
								var namePath = Path.GetFileNameWithoutExtension(dirent); // .Replace(basePath, "")
								var le = new LaunchEntry {FullPath = dirent, NamePath = namePath};
								foundFiles.Add(le);
							}
						}
					}
				}
			}
		}
	}
}