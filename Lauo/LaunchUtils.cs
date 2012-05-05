using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lauo {
	class LaunchUtils {
		public static void RevealTarget(string target) {
			string argument = "/select, \"" + target + "\"";
			System.Diagnostics.Process.Start("explorer.exe", argument);
		}
	}
}
