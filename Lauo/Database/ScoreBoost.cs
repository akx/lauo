using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Lauo {
	class ScoreBoost {
		private string matchStr;
		private bool plain;
		public StringMatcher Matcher { get; private set; }
		public int Score { get; private set; }
		

		public ScoreBoost(string matchStr, bool plain, int score) {
			this.matchStr = matchStr;
			this.plain = plain;
			Matcher = new StringMatcher(matchStr, plain);
			Score = score;
		}

		public XElement Serialize() {
			var sbEl = new XElement("ScoreBoost");
			sbEl.SetAttributeValue("Score", Score.ToString(CultureInfo.InvariantCulture));
			sbEl.SetAttributeValue("Plain", plain.ToString(CultureInfo.InvariantCulture));
			sbEl.SetValue(matchStr);
			return sbEl;
		}
	}
}
