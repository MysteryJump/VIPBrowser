namespace VIPBrowser.ch2Browser
{
    partial class UploadManagementDialog
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
            this.components = new System.ComponentModel.Container();
            this.listViewExtension1 = new VIPBrowserLibrary.Other.MyExtensions.GUIExtension.ListViewExtension();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewExtension1
            // 
            this.listViewExtension1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewExtension1.FullRowSelect = true;
            this.listViewExtension1.Location = new System.Drawing.Point(12, 12);
            this.listViewExtension1.MaxLength = -1;
            this.listViewExtension1.MultiSelect = false;
            this.listViewExtension1.Name = "listViewExtension1";
            this.listViewExtension1.Size = new System.Drawing.Size(500, 115);
            this.listViewExtension1.TabIndex = 0;
            this.listViewExtension1.UseCompatibleStateImageBehavior = false;
            this.listViewExtension1.View = System.Windows.Forms.View.Details;
            this.listViewExtension1.DoubleClick += new System.EventHandler(this.listViewExtension1_DoubleClick);
            this.listViewExtension1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewExtension1_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ダウンロードリンク";
            this.columnHeader1.Width = 122;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "アップローダー";
            this.columnHeader2.Width = 79;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ダウンロードパス";
            this.columnHeader3.Width = 87;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "削除リンク/パス";
            this.columnHeader4.Width = 86;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "時刻";
            this.columnHeader5.Width = 100;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "完了";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteItemToolStripMenuItem,
            this.linkCopyToolStripMenuItem,
            this.downloadItemToolStripMenuItem,
            this.deleteLogToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 114);
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteItemToolStripMenuItem.Text = "削除する";
            // 
            // linkCopyToolStripMenuItem
            // 
            this.linkCopyToolStripMenuItem.Name = "linkCopyToolStripMenuItem";
            this.linkCopyToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.linkCopyToolStripMenuItem.Text = "リンクをコピーする";
            this.linkCopyToolStripMenuItem.Click += new System.EventHandler(this.linkCopyToolStripMenuItem_Click);
            // 
            // downloadItemToolStripMenuItem
            // 
            this.downloadItemToolStripMenuItem.Name = "downloadItemToolStripMenuItem";
            this.downloadItemToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.downloadItemToolStripMenuItem.Text = "ダウンロードする";
            // 
            // deleteLogToolStripMenuItem
            // 
            this.deleteLogToolStripMenuItem.Name = "deleteLogToolStripMenuItem";
            this.deleteLogToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteLogToolStripMenuItem.Text = "ログから削除する";
            this.deleteLogToolStripMenuItem.Click += new System.EventHandler(this.deleteLogToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "アップロードする";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // UploadManagementDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 167);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewExtension1);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UploadManagementDialog";
            this.Text = "UploadManagementDialog";
            this.Load += new System.EventHandler(this.UploadManagementDialog_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VIPBrowserLibrary.Other.MyExtensions.GUIExtension.ListViewExtension listViewExtension1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteLogToolStripMenuItem;
        private System.Windows.Forms.Button button2;
    }
}