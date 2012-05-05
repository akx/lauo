using System.Text.RegularExpressions;

namespace Lauo {
	internal class StringMatcher {
		private Regex regex;
		public StringMatcher(string value): this(value, false) {}

		public StringMatcher(string value, bool isRegexp) {
			if (!isRegexp) value = Regex.Escape(value);
			regex = new Regex(value, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}
		public bool Match(string target) {
			return regex.Match(target).Success;
		}
	}
}