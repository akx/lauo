using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Lauo {
	static class ShHelp {
		[DllImport("shell32.dll")]
		static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

		public enum Csidl {
			CommonStartmenu = 0x16
		};

		public static string GetSpecialFolderPath(Csidl csidl) {
			var path = new StringBuilder(260);
			return SHGetSpecialFolderPath(IntPtr.Zero, path, (int)csidl, false) ? path.ToString() : null;
		}
	}
}
