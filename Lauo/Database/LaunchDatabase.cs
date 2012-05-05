using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Lauo.Database {
	internal delegate void DatabaseUpdatedDelegate(object sender, EventArgs args);

	class LaunchDatabase {
		private static LaunchDatabase _db;
		public static LaunchDatabase Instance { get { return _db = _db ?? new LaunchDatabase(); } }

		private readonly IList<LaunchEntry> entries;
		public ReadOnlyCollection<LaunchEntry> Entries { get { return new ReadOnlyCollection<LaunchEntry>(entries); } }
		public event DatabaseUpdatedDelegate DatabaseUpdated;
		private readonly Dictionary<string, FrecencyEntry> frecencyEntries;

		private LaunchDatabase() {
			entries = new List<LaunchEntry>();
			frecencyEntries = new Dictionary<string, FrecencyEntry>();
		}

		#region Frecency

		public void UnserializeFrecencyEntries(XElement frecencyRoot) {
			frecencyEntries.Clear();
			if (frecencyRoot == null) return;
			foreach(var frecEntry in frecencyRoot.Elements("Entry")) {
				var nLaunches = int.Parse(frecEntry.Attribute("N").Value, CultureInfo.InvariantCulture);
				var lastLaunch = DateTime.Parse(frecEntry.Attribute("LL").Value, CultureInfo.InvariantCulture);
				var path = frecEntry.Attribute("Path").Value;
				frecencyEntries[path] = new FrecencyEntry(path, nLaunches, lastLaunch);
			}
		}

		public XElement SerializeFrecencyEntries() {
			XElement frecencyRoot = new XElement("Frecency");
			foreach(var entry in frecencyEntries.Values.OrderByDescending(entry => entry.NLaunched)) {
				var xEntry = new XElement("Entry");
				xEntry.SetAttributeValue("N", entry.NLaunched.ToString(CultureInfo.InvariantCulture));
				xEntry.SetAttributeValue("LL", entry.LastLaunch.ToString(CultureInfo.InvariantCulture));
				xEntry.SetAttributeValue("Path", entry.CanonicalPath);
				frecencyRoot.Add(xEntry);
			}
			return frecencyRoot;
		}

		public void AddFrecency(string canonicalPath) {
			FrecencyEntry frecencyEntry = null;
			if (!frecencyEntries.TryGetValue(canonicalPath, out frecencyEntry)) {
				frecencyEntry = frecencyEntries[canonicalPath] = new FrecencyEntry(canonicalPath);
			} 
			frecencyEntry.WasLaunched();
		}

		#endregion

		public void UpdateEntries(IList<LaunchEntry> newEntries, bool deleteNotFound) {

			var newLookup = newEntries.ToDictionary(le => le.FullPath);
			var currLookup = entries.ToDictionary(le => le.FullPath);

			if(deleteNotFound) {
				for (int i = 0; i < entries.Count; i++) {
					var le = entries[i];
					if(!newLookup.ContainsKey(le.FullPath)) {
						entries.RemoveAt(i);
						i--;
						continue;
					}
				}
			}

			foreach(var le in newEntries) {
				LaunchEntry existing = null;
				if(currLookup.TryGetValue(le.FullPath, out existing)) {
					existing.UpdateFrom(le);
				} else { 
					entries.Add(le);
				}
			}
			if (DatabaseUpdated != null) DatabaseUpdated(this, new EventArgs());
		}

		private void ApplyScoringModifiers(string search, ScoredSearchResult ssr) {
			var canonicalPath = ssr.Entry.CanonicalPath;
			var namePath = ssr.Entry.NamePath;
			foreach (var scoreBoost in Settings.Instance.ScoreBoosts) {
				if (scoreBoost.Matcher.Match(canonicalPath) || scoreBoost.Matcher.Match(namePath)) ssr.Score += scoreBoost.Score;
			}
			FrecencyEntry frecencyEntry = null;
			if(frecencyEntries.TryGetValue(canonicalPath, out frecencyEntry)) {
				ssr.Score += (int)Math.Ceiling(Math.Sqrt(frecencyEntry.NLaunched) * 15);
			}
			if (namePath.ToLowerInvariant().StartsWith(search.ToLowerInvariant())) ssr.Score += 15;
		}

		public IEnumerable<ScoredSearchResult> SearchDatabase(string search) {
			var results = new List<ScoredSearchResult>();
			int minDeltasum = search.Length - 1;
			foreach (var ent in Entries) {
				var np = ent.NamePath.ToLowerInvariant();
				int pos = 0;
				var positions = new List<int>();
				foreach (var ch in search) {
					var idx = np.IndexOf(ch, pos);
					if (idx == -1) {
						positions.Clear();
						break;
					}
					positions.Add(idx); // - (positions.Count > 0 ? positions[positions.Count - 1] : idx));
					pos = idx + 1;
				}
				if (positions.Count == 0) continue;

				int deltaSum = 0;
				for (int i = 0; i < positions.Count - 1; i++) deltaSum += positions[i + 1] - positions[i];
				//Debug.Print("'{0}' <{1}> ({2}) {3} <= {4}?", search, np, String.Join(", ", positions.Select(i => i.ToString()).ToArray()), deltaSum, minDeltasum);
				if (deltaSum < minDeltasum) continue;
				int score = deltaSum;
				results.Add(new ScoredSearchResult(ent, score, positions));
			}
			if (results.Count == 0) return Enumerable.Empty<ScoredSearchResult>();

			double maxScore = results.Max(result => result.Score) + 1;
			
			foreach (var ssr in results) {
				ssr.Score = (int) Math.Round((1.0 - (ssr.Score / maxScore)) * 1000);
				ApplyScoringModifiers(search, ssr);
			}
			
			return results.Where(result => result.Score > 0).OrderByDescending(result => result.Score).ThenBy(result => result.Entry.NamePath);

		}

		public IEnumerable<ScoredSearchResult> GetRecent() {
			var lookup = entries.ToLookup(entry => entry.CanonicalPath);
			var launchEntries = frecencyEntries.OrderByDescending(pair => pair.Value.LastLaunch).Select(pair => lookup[pair.Value.CanonicalPath].FirstOrDefault());
			return launchEntries.Select((entry, i) => new ScoredSearchResult(entry, 1000 - i, null));
		}
	}
}

