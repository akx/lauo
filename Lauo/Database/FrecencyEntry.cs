using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lauo.Database {
	class FrecencyEntry {
		public string CanonicalPath { get; private set; }
		public int NLaunched { get; private set; }
		public DateTime LastLaunch { get; private set; }

		public FrecencyEntry(string canonicalPath): this(canonicalPath, 0, DateTime.Now) {}

		public FrecencyEntry(string canonicalPath, int nLaunched, DateTime lastLaunch) {
			CanonicalPath = canonicalPath;
			NLaunched = nLaunched;
			LastLaunch = lastLaunch;
		}

		public void WasLaunched() {
			NLaunched ++;
			LastLaunch = DateTime.Now;
		}
	}
}
