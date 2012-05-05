namespace Lauo {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.inputBox = new System.Windows.Forms.TextBox();
			this.resultContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.createScoringRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createAliasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.systemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resultListBox = new Lauo.ResultListBox();
			this.statusStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.resultContextMenu.SuspendLayout();
			this.systemContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 355);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(518, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.statusLabel.Size = new System.Drawing.Size(503, 17);
			this.statusLabel.Spring = true;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.inputBox, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.resultListBox, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 355);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// inputBox
			// 
			this.inputBox.AcceptsReturn = true;
			this.inputBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.inputBox.Location = new System.Drawing.Point(3, 3);
			this.inputBox.Name = "inputBox";
			this.inputBox.Size = new System.Drawing.Size(512, 29);
			this.inputBox.TabIndex = 0;
			this.inputBox.TextChanged += new System.EventHandler(this.InputBoxTextChanged);
			this.inputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputBox_KeyPress);
			this.inputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.inputBox_KeyUp);
			// 
			// resultContextMenu
			// 
			this.resultContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.showTargetToolStripMenuItem,
            this.showLinkToolStripMenuItem,
            this.toolStripSeparator1,
            this.createScoringRuleToolStripMenuItem,
            this.createAliasToolStripMenuItem});
			this.resultContextMenu.Name = "contextMenuStrip1";
			this.resultContextMenu.ShowImageMargin = false;
			this.resultContextMenu.Size = new System.Drawing.Size(162, 120);
			this.resultContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.resultContextMenu_Opening);
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.runToolStripMenuItem.Text = "Run";
			this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
			// 
			// showTargetToolStripMenuItem
			// 
			this.showTargetToolStripMenuItem.Name = "showTargetToolStripMenuItem";
			this.showTargetToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.showTargetToolStripMenuItem.Text = "Show Target...";
			this.showTargetToolStripMenuItem.Click += new System.EventHandler(this.showTargetToolStripMenuItem_Click);
			// 
			// showLinkToolStripMenuItem
			// 
			this.showLinkToolStripMenuItem.Name = "showLinkToolStripMenuItem";
			this.showLinkToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.showLinkToolStripMenuItem.Text = "Show Link...";
			this.showLinkToolStripMenuItem.Click += new System.EventHandler(this.showLinkToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
			// 
			// createScoringRuleToolStripMenuItem
			// 
			this.createScoringRuleToolStripMenuItem.Name = "createScoringRuleToolStripMenuItem";
			this.createScoringRuleToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.createScoringRuleToolStripMenuItem.Text = "Create Scoring Rule...";
			this.createScoringRuleToolStripMenuItem.Click += new System.EventHandler(this.createScoringRuleToolStripMenuItem_Click);
			// 
			// createAliasToolStripMenuItem
			// 
			this.createAliasToolStripMenuItem.Name = "createAliasToolStripMenuItem";
			this.createAliasToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.createAliasToolStripMenuItem.Text = "Create Alias...";
			// 
			// systemContextMenu
			// 
			this.systemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshDatabaseToolStripMenuItem});
			this.systemContextMenu.Name = "systemContextMenu";
			this.systemContextMenu.Size = new System.Drawing.Size(165, 26);
			// 
			// refreshDatabaseToolStripMenuItem
			// 
			this.refreshDatabaseToolStripMenuItem.Name = "refreshDatabaseToolStripMenuItem";
			this.refreshDatabaseToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.refreshDatabaseToolStripMenuItem.Text = "Refresh Database";
			this.refreshDatabaseToolStripMenuItem.Click += new System.EventHandler(this.refreshDatabaseToolStripMenuItem_Click);
			// 
			// resultListBox
			// 
			this.resultListBox.ContextMenuStrip = this.resultContextMenu;
			this.resultListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.resultListBox.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.resultListBox.FormattingEnabled = true;
			this.resultListBox.Location = new System.Drawing.Point(3, 38);
			this.resultListBox.Name = "resultListBox";
			this.resultListBox.Size = new System.Drawing.Size(512, 314);
			this.resultListBox.TabIndex = 1;
			this.resultListBox.SelectedIndexChanged += new System.EventHandler(this.resultListBox_SelectedIndexChanged);
			this.resultListBox.DoubleClick += new System.EventHandler(this.resultListBox_DoubleClick);
			this.resultListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resultListBox_MouseClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(518, 377);
			this.ContextMenuStrip = this.systemContextMenu;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.statusStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "MainForm";
			this.Text = "Lauo";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.resultContextMenu.ResumeLayout(false);
			this.systemContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox inputBox;
		private ResultListBox resultListBox;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.ContextMenuStrip resultContextMenu;
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showTargetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showLinkToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem createScoringRuleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createAliasToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip systemContextMenu;
		private System.Windows.Forms.ToolStripMenuItem refreshDatabaseToolStripMenuItem;

	}
}

