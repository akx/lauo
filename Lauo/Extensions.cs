using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lauo {
	static class Extensions {
		public static void DrawStringRight(this Graphics g, string str, Font font, Brush brush, int x, int y) {
			var size = g.MeasureString(str, font);
			g.DrawString(str, font, brush, x - size.Width, y);
		}

		public static TV SetDefault<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV @default) {
			TV value;
			if (!dict.TryGetValue(key, out value)) {
				dict.Add(key, @default);
				return @default;
			} else {
				return value;
			}
		}

		public static TV SetDefault<TK, TV>(this IDictionary<TK, TV> dict, TK key) where TV: new() {
			TV value;
			if (!dict.TryGetValue(key, out value)) {
				value = new TV();
				dict.Add(key, value);
			}
			return value;
		}

		public static void XInvoke(this Control control, MethodInvoker meth) {
			if(control.InvokeRequired) control.BeginInvoke(meth);
			else meth();
		}
	}
}
