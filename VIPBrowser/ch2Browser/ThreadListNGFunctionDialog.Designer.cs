namespace VIPBrowser.ch2Browser
{
    partial class ThreadListNGFunctionDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.addNGMemberButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Location = new System.Drawing.Point(12, 13);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(554, 205);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "NGするワード";
            this.columnHeader1.Width = 300;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(491, 225);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // addNGMemberButton
            // 
            this.addNGMemberButton.Location = new System.Drawing.Point(12, 225);
            this.addNGMemberButton.Name = "addNGMemberButton";
            this.addNGMemberButton.Size = new System.Drawing.Size(75, 23);
            this.addNGMemberButton.TabIndex = 2;
            this.addNGMemberButton.Text = "追加";
            this.addNGMemberButton.UseVisualStyleBackColor = true;
            this.addNGMemberButton.Click += new System.EventHandler(this.addNGMemberButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(93, 225);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "削除";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // ThreadListNGFunctionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 260);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addNGMemberButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.listView1);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ThreadListNGFunctionDialog";
            this.Text = "スレッドリストのNGを編集";
            this.Load += new System.EventHandler(this.ThreadListNGFunctionDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button addNGMemberButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}