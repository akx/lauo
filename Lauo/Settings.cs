using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Lauo.Database;

namespace Lauo {
	class Settings {
		private static Settings _settings;
		public static Settings Instance { get { return _settings = _settings ?? new Settings(); } }
		
		
		private string configFilePath;
		public IList<string> SearchPaths { get; set; }

		// read/written from config -->
		private bool SearchInStartMenu = true;
		private IList<string> configSearchPaths;
		public IList<ScoreBoost> ScoreBoosts { get; private set; }
		public HashSet<string> DisallowedPathFragments { get; private set; }
		public HashSet<string> AllowedExtensions { get; private set; }

		private string hotkeyKeyString;
		private Keys hotkeyKey;
		private bool hotkeyShift, hotkeyCtrl, hotkeyAlt;
		// <--

		private Settings() {
			FindConfigurationPath();
			LoadDefaultConfiguration();
			ConfigurationPostSetup();
		}

		private void LoadDefaultConfiguration() {
			ScoreBoosts = new List<ScoreBoost> {
				new ScoreBoost("pdf|doc|chm|help", true, -15),
				new ScoreBoost("config", true, -1),
				new ScoreBoost("uninstall", true, -50),
			};
			SearchPaths = new List<string>(5);
			hotkeyKeyString = "Control+Shift+F1";
			DisallowedPathFragments = new HashSet<string>(new string[] {".svn", ".hg", ".git"});
			AllowedExtensions = new HashSet<string>(new string[] {".lnk", ".exe"});
			SearchInStartMenu = true;
			configSearchPaths = new List<string>();
		}

		private void FindConfigurationPath() {
			string[] ConfigPaths = new[] {
				Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
			};
			foreach (var configDirPath in ConfigPaths) {
				configFilePath = Path.Combine(configDirPath, "Lauo.xml");
				if (File.Exists(configFilePath)) break;
			}
		}

		internal void LoadConfiguration() {
			if (!File.Exists(configFilePath)) {
				LoadDefaultConfiguration();
			} else {
				XDocument config = XDocument.Load(configFilePath);
				var root = config.Root ?? new XElement("Config");
				UnserializeConfiguration(root);
			}

			ConfigurationPostSetup();
		}

		private void ConfigurationPostSetup() {
			SearchPaths.Clear();
			if (SearchInStartMenu) {
				SearchPaths.Add(Environment.GetFolderPath(Environment.SpecialFolder.Programs));
				SearchPaths.Add(ShHelp.GetSpecialFolderPath(ShHelp.Csidl.CommonStartmenu));
			}
			foreach (var sp in configSearchPaths) {
				if (Directory.Exists(sp)) SearchPaths.Add(sp);
			}
		}

		public void SaveConfiguration() {
			var doc = new XDocument(SerializeConfiguration());
			using (FileStream fs = new FileStream(configFilePath, FileMode.Create, FileAccess.Write)) {
				var xw = new XmlTextWriter(fs, Encoding.UTF8) {
					Formatting = Formatting.Indented,
					IndentChar = '\t',
					Indentation = 1
				};
				doc.WriteTo(xw);
				xw.Flush();
			}
		}

		private void UnserializeConfiguration(XElement root) {
			XDocument config;
			SearchInStartMenu = root.ReadBoolean("SearchInStartMenu", true);
			configSearchPaths = root.ReadStringList("SearchPaths");
			DisallowedPathFragments = new HashSet<string>(root.ReadStringList("DisallowedPathFragments"));
			AllowedExtensions = new HashSet<string>(root.ReadStringList("AllowedExtensions"));
			hotkeyKeyString = root.ReadString("Hotkey", "control+shift+3");
			ParseHotkeyString();

			ScoreBoosts = new List<ScoreBoost>();
			foreach (var el in root.Elements("ScoreBoost")) {
				var scoreValue = int.Parse(el.Attribute("Score").Value, CultureInfo.InvariantCulture);
				var isPlain = bool.Parse(el.Attribute("Plain").Value);
				ScoreBoosts.Add(new ScoreBoost(el.Value, isPlain, scoreValue));
			}

			LaunchDatabase.Instance.UnserializeFrecencyEntries(root.Element("Frecency"));
		}

		private XElement SerializeConfiguration() {
			var root = new XElement("Config");
			root.Add(new XComment(string.Format(CultureInfo.InvariantCulture, "Lauo configuration written at {0}", DateTime.Now)));
			root.WriteBoolean("SearchInStartMenu", SearchInStartMenu);
			root.WriteStringList("SearchPaths", configSearchPaths);
			root.WriteStringList("DisallowedPathFragments", DisallowedPathFragments.OrderBy(s => s));
			root.WriteStringList("AllowedExtensions", AllowedExtensions.OrderBy(s => s));
			root.WriteString("Hotkey", hotkeyKeyString);
			foreach (var scoreBoost in ScoreBoosts) root.Add(scoreBoost.Serialize());
			root.Add(LaunchDatabase.Instance.SerializeFrecencyEntries());

			return root;
		}


		public Hotkey GetConfiguredHotkey() {
			return new Hotkey(hotkeyKey, hotkeyShift, hotkeyCtrl, hotkeyAlt, false);
		}

		private void ParseHotkeyString() {
			foreach (var part in hotkeyKeyString.Split(new char[]{'+'}, StringSplitOptions.RemoveEmptyEntries)) {
				Keys kParsed = Keys.None;
				try {
					kParsed = (Keys) Enum.Parse(typeof (Keys), part.Trim());
				} catch(ArgumentException) {
					continue;
				}
				if (kParsed != Keys.None) {
					if (kParsed == Keys.Shift) hotkeyShift = true;
					else if (kParsed == Keys.Control) hotkeyCtrl = true;
					else if (kParsed == Keys.Alt) hotkeyAlt = true;
					else hotkeyKey = kParsed;
				}
			}
			Debug.Print("Parsed: {0}, shift {1}, ctrl {2}, alt {3}", hotkeyKey, hotkeyShift, hotkeyCtrl, hotkeyAlt);
		}




		public void Init() {
			throw new NotImplementedException();
		}
	}
}
