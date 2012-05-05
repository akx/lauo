using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Lauo.Database;


namespace Lauo {
	public partial class MainForm : Form {
		private NotifyIcon trayIcon;
		private Hotkey showHotkey;
		public MainForm() {

			InitializeComponent();
			DoubleBuffered = true;

			trayIcon = new NotifyIcon {Icon = Icon, Text = "Lauo", Visible = true};
			trayIcon.Click += TrayIconOnClick;

			try {
				Settings.Instance.LoadConfiguration();
			} catch(Exception exc) {
				if(MessageBox.Show("Lauo could not read its configuration file. The problem looks like this:\n" + exc.Message + "\n\nWould you like to exit now to fix this by hand?\nNot exiting means your configuration will be overwritten by defaults.", "Oops!", MessageBoxButtons.YesNo) == DialogResult.Yes) {
					Environment.Exit(1);
					return;
				}
			}
			LaunchDatabase.Instance.DatabaseUpdated += DbOnDatabaseUpdated;

			showHotkey = Settings.Instance.GetConfiguredHotkey();
			showHotkey.Pressed += ((sender, args) => ShowInteractive());

			/*foreach(var ent in LaunchDatabase.Instance.GetRecent()) {
				Debug.Print("{0}", ent);
			}
			 */
			
			Shown += OnShown;
			Closed += OnClosed;
		}

		private void OnClosed(object sender, EventArgs eventArgs) {
			trayIcon.Visible = false;
			trayIcon.Dispose();
			Settings.Instance.SaveConfiguration();
		}

		private void TrayIconOnClick(object sender, EventArgs eventArgs) {
			ShowInteractive();
		}

		private void ShowInteractive() {
			Show();
			BringToFront();
			inputBox.SelectionStart = 0;
			inputBox.SelectionLength = inputBox.Text.Length;
			inputBox.Focus();

		}

		private void DbOnDatabaseUpdated(object sender, EventArgs args) {
			this.XInvoke(delegate {
				statusLabel.Text = string.Format("{0} entries in database.", LaunchDatabase.Instance.Entries.Count);
				if (LaunchDatabase.Instance.Entries.Count > 0) {
					resultListBox.Enabled = true;
					DoSearch();
				} else {
					resultListBox.Enabled = false;
				}
			});
		}

		private void OnShown(object sender, EventArgs eventArgs) {
			if (showHotkey.KeyCode != Keys.None && !showHotkey.Register(this)) MessageBox.Show("Unable to register hotkey");
			DbOnDatabaseUpdated(null, null);
			StartBackgroundUpdate();
		}

		private static void StartBackgroundUpdate() {
			var th = new Thread(() => {
				var dbu = new DatabaseUpdater(LaunchDatabase.Instance);
				dbu.Run();
			}) {Name = "Background Database Update", Priority = ThreadPriority.Lowest};
			th.Start();
		}

		private void InputBoxTextChanged(object sender, EventArgs e) {
			DoSearch();
		}

		private void DoSearch() {
			var search = inputBox.Text.Trim().ToLowerInvariant().Replace(" ", "");
			var results = (!string.IsNullOrEmpty(search) ? LaunchDatabase.Instance.SearchDatabase(search) : Enumerable.Empty<ScoredSearchResult>());

			PopulateListbox(results);
		}

		private void PopulateListbox(IEnumerable<ScoredSearchResult> results) {
			resultListBox.BeginUpdate();
			resultListBox.Items.Clear();
			foreach (var result in results.Take(40)) {
				resultListBox.Items.Add(result);
			}
			if (resultListBox.Items.Count > 0) {
				resultListBox.SelectedIndex = 0;
				statusLabel.Text = "";
			}
			resultListBox.EndUpdate();
			resultListBox.CacheData();
		}

		private void inputBox_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Escape) {
				Hide();
				e.Handled = e.SuppressKeyPress = true;
				return;
			}

			var nItems = resultListBox.Items.Count;
			if (nItems > 0) {
				if(e.KeyCode == Keys.Return) {
					if (resultListBox.SelectedIndex < 0) resultListBox.SelectedIndex = 0;
					e.Handled = e.SuppressKeyPress = true;
					LaunchItem(resultListBox.SelectedItem as ScoredSearchResult);
					Hide();
					return;
				}
				if (e.KeyCode == Keys.Down) {
					resultListBox.SelectedIndex = (resultListBox.SelectedIndex + 1) % nItems;
					e.Handled = e.SuppressKeyPress = true;
					return;
				}
				if (e.KeyCode == Keys.Up) {
					resultListBox.SelectedIndex = ((resultListBox.SelectedIndex != 0) ? (resultListBox.SelectedIndex - 1) : nItems - 1);
					e.Handled = e.SuppressKeyPress = true;
					return;
				}
			}
			//Debug.Print("{0}", e.KeyCode);
		}

		private void LaunchItem(ScoredSearchResult scoredSearchResult) {
			if (scoredSearchResult != null) {
				scoredSearchResult.Entry.Launch("");
				
			}

		}

		private void resultListBox_SelectedIndexChanged(object sender, EventArgs e) {
			var item = resultListBox.SelectedItem as ScoredSearchResult;
			statusLabel.Text = item.Entry.CanonicalPath;
		}

		private void inputBox_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == '\r' || e.KeyChar == '\n' || e.KeyChar == 27) {
				e.Handled = true;
			}
		}

		private void showTargetToolStripMenuItem_Click(object sender, EventArgs e) {
			var item = resultListBox.SelectedItem as ScoredSearchResult;
			LaunchUtils.RevealTarget(item.Entry.CanonicalPath);
		}

		private void showLinkToolStripMenuItem_Click(object sender, EventArgs e) {
			var item = resultListBox.SelectedItem as ScoredSearchResult;
			LaunchUtils.RevealTarget(item.Entry.FullPath);
		}

		private void resultContextMenu_Opening(object sender, CancelEventArgs e) {
			var item = resultListBox.SelectedItem as ScoredSearchResult;
			bool isEnabled = (item != null);
			foreach (var cmItem in resultContextMenu.Items) {
				var toolStripMenuItem = cmItem as ToolStripMenuItem;
				if (toolStripMenuItem != null) {
					toolStripMenuItem.Enabled = isEnabled;
				}
			}
		}

		private void runToolStripMenuItem_Click(object sender, EventArgs e) {
			LaunchItem(resultListBox.SelectedItem as ScoredSearchResult);
		}

		private void resultListBox_MouseClick(object sender, MouseEventArgs e) {
			var idx = resultListBox.IndexFromPoint(e.X, e.Y);
			resultListBox.SelectedIndex = (idx >= 0 && idx < resultListBox.Items.Count ? idx : resultListBox.SelectedIndex);
		}

		private void resultListBox_DoubleClick(object sender, EventArgs e) {
			LaunchItem(resultListBox.SelectedItem as ScoredSearchResult);
		}

		private void createScoringRuleToolStripMenuItem_Click(object sender, EventArgs e) {
			var item = resultListBox.SelectedItem as ScoredSearchResult;
			var sbf = new ScoreBoostForm(item.Entry.NamePath, 0);
			if(sbf.ShowDialog() == DialogResult.OK) {
				Settings.Instance.ScoreBoosts.Add(sbf.MakeScoreBoost());
				DoSearch();
			}

		}

		private void refreshDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
			StartBackgroundUpdate();
			statusLabel.Text = "Started background update of database.";
		}
	}
}
