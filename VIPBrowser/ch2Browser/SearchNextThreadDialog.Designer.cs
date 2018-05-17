namespace VIPBrowser.ch2Browser
{
	partial class SearchNextThreadDialog
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.label1 = new System.Windows.Forms.Label();
			this.Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ThreadName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ResCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Speed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Count,
            this.ThreadName,
            this.ResCount,
            this.Time,
            this.Speed});
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(12, 28);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(547, 207);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "o";
			// 
			// Count
			// 
			this.Count.Text = "#";
			this.Count.Width = 25;
			// 
			// ThreadName
			// 
			this.ThreadName.Text = "スレタイ";
			this.ThreadName.Width = 189;
			// 
			// ResCount
			// 
			this.ResCount.Text = "レス数";
			this.ResCount.Width = 48;
			// 
			// Time
			// 
			this.Time.Text = "スレ立て";
			this.Time.Width = 116;
			// 
			// Speed
			// 
			this.Speed.Text = "勢い";
			this.Speed.Width = 59;
			// 
			// SearchNextThreadDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(571, 247);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listView1);
			this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SearchNextThreadDialog";
			this.Text = "SearchNextThreadDialog";
			this.Load += new System.EventHandler(this.SearchNextThreadDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader Count;
		private System.Windows.Forms.ColumnHeader ThreadName;
		private System.Windows.Forms.ColumnHeader ResCount;
		private System.Windows.Forms.ColumnHeader Time;
		private System.Windows.Forms.ColumnHeader Speed;
	}
}