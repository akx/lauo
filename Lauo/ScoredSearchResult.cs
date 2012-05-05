using System.Collections.Generic;
using System.Linq;
using Lauo.Database;

namespace Lauo {
	class ScoredSearchResult {
		public LaunchEntry Entry { get; private set;  }
		public int Score { get; set; }
		public bool RequiresPathLine { get; set; }
		public int[] HighlightPositions { get; private set; }
		public ScoredSearchResult(LaunchEntry entry, int score, IEnumerable<int> highlightPositions) {
			Entry = entry;
			Score = score;
			HighlightPositions = highlightPositions != null ? highlightPositions.ToArray() : new int[0]{};
		}

		public override string ToString() {
			return string.IsNullOrEmpty(Entry.LinkTarget) ? Entry.FullPath : "\u2192 " + Entry.LinkTarget;
		}
	}
}