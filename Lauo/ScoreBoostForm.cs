using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lauo {
	internal partial class ScoreBoostForm : Form {
		public ScoreBoostForm() {
			InitializeComponent();
		}

		public ScoreBoostForm(string boostText, int amount): this() {
			boostTermBox.Text = boostText;
			boostAmountBox.Value = amount;
		}

		public ScoreBoost MakeScoreBoost() {
			return new ScoreBoost(boostTermBox.Text, !regexCheckBox.Checked,(int)boostAmountBox.Value);
		}

		private void okButton_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
		}
	}
}
