namespace VIPBrowser.Dialogs
{
	partial class ReadProductWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadProductWindow));
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.CloseButton = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.ttttt = new System.Windows.Forms.TabPage();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.richTextBox3 = new System.Windows.Forms.RichTextBox();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.ttttt.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(526, 282);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(465, 340);
			this.CloseButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(87, 29);
			this.CloseButton.TabIndex = 1;
			this.CloseButton.Text = "終了";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.ttttt);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Location = new System.Drawing.Point(12, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(540, 316);
			this.tabControl.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.richTextBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(532, 288);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "SnowSweet";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// ttttt
			// 
			this.ttttt.Controls.Add(this.richTextBox2);
			this.ttttt.Location = new System.Drawing.Point(4, 24);
			this.ttttt.Name = "ttttt";
			this.ttttt.Padding = new System.Windows.Forms.Padding(3);
			this.ttttt.Size = new System.Drawing.Size(532, 288);
			this.ttttt.TabIndex = 1;
			this.ttttt.Text = "Twintail";
			this.ttttt.UseVisualStyleBackColor = true;
			// 
			// richTextBox2
			// 
			this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox2.Location = new System.Drawing.Point(3, 3);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.Size = new System.Drawing.Size(526, 282);
			this.richTextBox2.TabIndex = 0;
			this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.richTextBox3);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(532, 288);
			this.tabPage2.TabIndex = 2;
			this.tabPage2.Text = "HtmlAgilityPack";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// richTextBox3
			// 
			this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox3.Location = new System.Drawing.Point(3, 3);
			this.richTextBox3.Name = "richTextBox3";
			this.richTextBox3.Size = new System.Drawing.Size(526, 282);
			this.richTextBox3.TabIndex = 0;
			this.richTextBox3.Text = resources.GetString("richTextBox3.Text");
			// 
			// ReadProductWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(567, 379);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.CloseButton);
			this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ReadProductWindow";
			this.Text = "ReadProductWindow";
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ttttt.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage ttttt;
		private System.Windows.Forms.RichTextBox richTextBox2;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.RichTextBox richTextBox3;
	}
}