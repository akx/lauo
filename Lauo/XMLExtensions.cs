using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Lauo {
	static class XMLExtensions {
		public static bool ReadBoolean(this XElement element, string key, bool @default = false) {
			var child = element.Element(key);
			return (child != null ? Boolean.Parse(child.Value) : @default);
		}

		public static void WriteBoolean(this XElement element, string key, bool value) {
			element.SetElementValue(key, value.ToString(CultureInfo.InvariantCulture));
		}

		public static List<string> ReadStringList(this XElement element, string key) {
			var child = element.Element(key);
			List<string> results = new List<string>();
			if(child != null) foreach(var el in child.Elements("s")) {
				results.Add(el.Value);
			}
			return results;
		}

		public static void WriteStringList(this XElement element, string key, IEnumerable<string> values) {
			var root = new XElement(key);
			foreach (var el in values.Select(s => new XElement("s", s))) root.Add(el);
			element.Add(root);
		}

		public static string ReadString(this XElement element, string key, string @default = "") {
			var child = element.Element(key);
			return (child != null ? child.Value : @default);
		}

		public static void WriteString(this XElement element, string key, string value) {
			element.SetElementValue(key, value);
		}
	}
}
