namespace VIPBrowser.ch2Browser
{
	partial class NewThreadWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.threadTitleTextBox = new System.Windows.Forms.TextBox();
			this.sentenceTextBox = new System.Windows.Forms.TextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.mailTextBox = new System.Windows.Forms.TextBox();
			this.threadWriteButton = new System.Windows.Forms.Button();
			this.canselButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.MainTabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTabControl
			// 
			this.MainTabControl.Controls.Add(this.tabPage1);
			this.MainTabControl.Controls.Add(this.tabPage2);
			this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTabControl.Location = new System.Drawing.Point(0, 0);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(541, 330);
			this.MainTabControl.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tableLayoutPanel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(533, 302);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "書き込み";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
			this.tableLayoutPanel1.Controls.Add(this.threadTitleTextBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.sentenceTextBox, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.mailTextBox, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.threadWriteButton, 4, 4);
			this.tableLayoutPanel1.Controls.Add(this.canselButton, 3, 4);
			this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(527, 296);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// threadTitleTextBox
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.threadTitleTextBox, 4);
			this.threadTitleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.threadTitleTextBox.Location = new System.Drawing.Point(53, 3);
			this.threadTitleTextBox.Name = "threadTitleTextBox";
			this.threadTitleTextBox.Size = new System.Drawing.Size(471, 23);
			this.threadTitleTextBox.TabIndex = 17;
			// 
			// sentenceTextBox
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.sentenceTextBox, 5);
			this.sentenceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sentenceTextBox.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F);
			this.sentenceTextBox.Location = new System.Drawing.Point(3, 60);
			this.sentenceTextBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.sentenceTextBox.Multiline = true;
			this.sentenceTextBox.Name = "sentenceTextBox";
			this.tableLayoutPanel1.SetRowSpan(this.sentenceTextBox, 2);
			this.sentenceTextBox.Size = new System.Drawing.Size(521, 207);
			this.sentenceTextBox.TabIndex = 10;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nameTextBox.Location = new System.Drawing.Point(53, 33);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(177, 23);
			this.nameTextBox.TabIndex = 12;
			// 
			// mailTextBox
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.mailTextBox, 2);
			this.mailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mailTextBox.Location = new System.Drawing.Point(290, 33);
			this.mailTextBox.Name = "mailTextBox";
			this.mailTextBox.Size = new System.Drawing.Size(234, 23);
			this.mailTextBox.TabIndex = 11;
			// 
			// threadWriteButton
			// 
			this.threadWriteButton.Location = new System.Drawing.Point(440, 270);
			this.threadWriteButton.Name = "threadWriteButton";
			this.threadWriteButton.Size = new System.Drawing.Size(75, 23);
			this.threadWriteButton.TabIndex = 9;
			this.threadWriteButton.Text = "スレ立て";
			this.threadWriteButton.UseVisualStyleBackColor = true;
			this.threadWriteButton.Click += new System.EventHandler(this.threadWriteButton_Click);
			// 
			// canselButton
			// 
			this.canselButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.canselButton.Location = new System.Drawing.Point(359, 270);
			this.canselButton.Name = "canselButton";
			this.canselButton.Size = new System.Drawing.Size(75, 23);
			this.canselButton.TabIndex = 15;
			this.canselButton.Text = "キャンセル";
			this.canselButton.UseVisualStyleBackColor = true;
			this.canselButton.Click += new System.EventHandler(this.canselButton_Click);
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(237, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 15);
			this.label2.TabIndex = 14;
			this.label2.Text = "メール欄";
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 15);
			this.label1.TabIndex = 13;
			this.label1.Text = "名前";
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 15);
			this.label3.TabIndex = 16;
			this.label3.Text = "スレタイ";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.webBrowser1);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(533, 302);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "プレビュー";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// webBrowser1
			// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(3, 3);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(527, 296);
			this.webBrowser1.TabIndex = 0;
			// 
			// NewThreadWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(541, 330);
			this.Controls.Add(this.MainTabControl);
			this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "NewThreadWindow";
			this.Text = "NewThreadWindow";
			this.MainTabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox threadTitleTextBox;
		private System.Windows.Forms.TextBox sentenceTextBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.TextBox mailTextBox;
		private System.Windows.Forms.Button threadWriteButton;
		private System.Windows.Forms.Button canselButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.WebBrowser webBrowser1;
	}
}