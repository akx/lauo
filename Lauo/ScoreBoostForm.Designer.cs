namespace Lauo {
	partial class ScoreBoostForm {
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
			this.label1 = new System.Windows.Forms.Label();
			this.boostTermBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.boostAmountBox = new System.Windows.Forms.NumericUpDown();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.regexCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.boostAmountBox)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Search Term";
			// 
			// boostTermBox
			// 
			this.boostTermBox.Location = new System.Drawing.Point(16, 30);
			this.boostTermBox.Name = "boostTermBox";
			this.boostTermBox.Size = new System.Drawing.Size(355, 20);
			this.boostTermBox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Boost Amount";
			// 
			// boostAmountBox
			// 
			this.boostAmountBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.boostAmountBox.Location = new System.Drawing.Point(16, 73);
			this.boostAmountBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.boostAmountBox.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.boostAmountBox.Name = "boostAmountBox";
			this.boostAmountBox.Size = new System.Drawing.Size(83, 20);
			this.boostAmountBox.TabIndex = 3;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(16, 115);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(296, 115);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// regexCheckBox
			// 
			this.regexCheckBox.AutoSize = true;
			this.regexCheckBox.Location = new System.Drawing.Point(254, 53);
			this.regexCheckBox.Name = "regexCheckBox";
			this.regexCheckBox.Size = new System.Drawing.Size(117, 17);
			this.regexCheckBox.TabIndex = 5;
			this.regexCheckBox.Text = "Regular Expression";
			this.regexCheckBox.UseVisualStyleBackColor = true;
			// 
			// ScoreBoostForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(383, 150);
			this.ControlBox = false;
			this.Controls.Add(this.regexCheckBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.boostAmountBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.boostTermBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ScoreBoostForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Score Boost";
			((System.ComponentModel.ISupportInitialize)(this.boostAmountBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox boostTermBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown boostAmountBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckBox regexCheckBox;
	}
}