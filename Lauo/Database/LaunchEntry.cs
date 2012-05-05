using System.Diagnostics;

namespace Lauo.Database {
	class LaunchEntry {
		public string FullPath { get; set; }
		public string NamePath { get; set; }
		public string LinkTarget { get; set; }
		public string CanonicalPath { get { return LinkTarget ?? FullPath; } }


		public void UpdateFrom(LaunchEntry other) {
			FullPath = other.FullPath;
			NamePath = other.NamePath;
			LinkTarget = other.LinkTarget;
		}

		public void Launch(string arguments) {
			arguments = (arguments ?? "").Trim();
			Process.Start(new ProcessStartInfo {
				FileName = CanonicalPath,
				Arguments = arguments,
				UseShellExecute = true,
				ErrorDialog = true
			});
			LaunchDatabase.Instance.AddFrecency(CanonicalPath);
		}
	}
}