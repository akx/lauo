using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lauo {
	class ResultListBox: ListBox {

		private int ItemMargin = 2;
		private ToolTip toolTip = new ToolTip {AutoPopDelay = 100, AutomaticDelay = 500};


		public ResultListBox() {
			DrawMode = DrawMode.OwnerDrawVariable;
			DoubleBuffered = true;
			ResizeRedraw = true;
			Font = new Font("Segoe UI", 9);
			MouseMove += MouseMoveTooltip;
		}

		public void CacheData() {
			var items = Items.Cast<ScoredSearchResult>().ToList();
			var ambiguous = items.GroupBy(result => result.Entry.NamePath.ToLowerInvariant()).Where(results => results.Count() > 1).ToDictionary(results => results.Key);


			foreach(var item in items) {
				if (ambiguous.ContainsKey(item.Entry.NamePath.ToLowerInvariant())) {
					item.RequiresPathLine = true;
				}
			}
			RefreshItems();
		}



		protected override void OnDrawItem(DrawItemEventArgs e) {
			var item = (e.Index >= 0 && e.Index < Items.Count ? Items[e.Index] : null) as ScoredSearchResult;
			e.DrawBackground();
			if (item == null) return;
			bool isSelected = SelectedIndices.Contains(e.Index);
			int lh = e.Font.Height;
			int top = e.Bounds.Top + ItemMargin;
			int left = e.Bounds.Left + ItemMargin;
			int right= e.Bounds.Right- ItemMargin;
			

			using(var brush = new SolidBrush(e.ForeColor)) {
				e.Graphics.DrawString(item.Entry.NamePath, e.Font, brush, left, top);
				e.Graphics.DrawStringRight(item.Score.ToString(), e.Font, brush, right, top);
				if (item.RequiresPathLine) {
					e.Graphics.DrawString(item.Entry.FullPath, e.Font, (isSelected ? brush : Brushes.Gray), left, top + lh, StringFormat.GenericTypographic);
				}
			}
			e.DrawFocusRectangle();
		}

		protected override void OnMeasureItem(MeasureItemEventArgs e) {
			if (e.Index >= 0 && e.Index < Items.Count) {
				var item = Items[e.Index] as ScoredSearchResult;
				int h = Font.Height;
				if (item.RequiresPathLine) h *= 2;
				h += ItemMargin * 2;
				e.ItemHeight = h;
			}
		}

		private void MouseMoveTooltip(object sender, MouseEventArgs e) {
			if (sender is ListBox) {
				ListBox listBox = (ListBox)sender;
				Point point = new Point(e.X, e.Y);
				int hoverIndex = listBox.IndexFromPoint(point);

				if (hoverIndex >= 0 && hoverIndex < listBox.Items.Count) {
					var item = listBox.Items[hoverIndex];
					if (item != toolTip.Tag) {
						toolTip.SetToolTip(listBox, item.ToString());
						toolTip.Tag = item;
					}
				}
			}
		}
	}
}
