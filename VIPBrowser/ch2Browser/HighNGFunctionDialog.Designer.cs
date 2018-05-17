namespace VIPBrowser.ch2Browser
{
    partial class HighNGFunctionDialog
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
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.okButton = new System.Windows.Forms.Button();
            this.addMemberButton = new System.Windows.Forms.Button();
            this.deleteMemberButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(14, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(724, 222);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "種類";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "NGワード";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "正規表現";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "NG先のURL";
            this.columnHeader4.Width = 220;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "設定時間";
            this.columnHeader5.Width = 130;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "解除時間";
            this.columnHeader6.Width = 100;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(663, 245);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // addMemberButton
            // 
            this.addMemberButton.Location = new System.Drawing.Point(14, 240);
            this.addMemberButton.Name = "addMemberButton";
            this.addMemberButton.Size = new System.Drawing.Size(75, 23);
            this.addMemberButton.TabIndex = 2;
            this.addMemberButton.Text = "追加";
            this.addMemberButton.UseVisualStyleBackColor = true;
            this.addMemberButton.Click += new System.EventHandler(this.addMemberButton_Click);
            // 
            // deleteMemberButton
            // 
            this.deleteMemberButton.Location = new System.Drawing.Point(95, 240);
            this.deleteMemberButton.Name = "deleteMemberButton";
            this.deleteMemberButton.Size = new System.Drawing.Size(75, 23);
            this.deleteMemberButton.TabIndex = 3;
            this.deleteMemberButton.Text = "削除";
            this.deleteMemberButton.UseVisualStyleBackColor = true;
            this.deleteMemberButton.Click += new System.EventHandler(this.deleteMemberButton_Click);
            // 
            // HighNGFunctionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 275);
            this.Controls.Add(this.deleteMemberButton);
            this.Controls.Add(this.addMemberButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.listView1);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "HighNGFunctionDialog";
            this.Text = "HighNGFunction";
            this.Load += new System.EventHandler(this.HighNGFunctionDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button addMemberButton;
        private System.Windows.Forms.Button deleteMemberButton;
    }
}