namespace VIPBrowser
{
    partial class Form1 : VIPBrowserPlugin.IPluginHost
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pluginSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TabCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifiedToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.aboutBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.StartPage = new System.Windows.Forms.TabPage();
            this._2chBrowser = new System.Windows.Forms.TabPage();
            this.ch2BrowserControl1 = new VIPBrowser.ch2BrowserControl(this);
            this.WebBrowserPage = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.startPageWebPage = new System.Windows.Forms.WebBrowser();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this._2chBrowser.SuspendLayout();
            this.WebBrowserPage.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TabCloseMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(124, 26);
            // 
            // TabCloseMenuItem
            // 
            this.TabCloseMenuItem.Name = "TabCloseMenuItem";
            this.TabCloseMenuItem.Size = new System.Drawing.Size(123, 22);
            this.TabCloseMenuItem.Text = "タブを閉じる";
            this.TabCloseMenuItem.Click += new System.EventHandler(this.TabCloseMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.modifiedToolStripDropDownButton,
            this.otherToolStripDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(830, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(69, 22);
            this.toolStripDropDownButton1.Text = "ファイル";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.closeToolStripMenuItem.Text = "終了";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Clicked);
            // 
            // modifiedToolStripDropDownButton
            // 
            this.modifiedToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.modifiedToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.pluginSetupToolStripMenuItem});
            this.modifiedToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("modifiedToolStripDropDownButton.Image")));
            this.modifiedToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modifiedToolStripDropDownButton.Name = "modifiedToolStripDropDownButton";
            this.modifiedToolStripDropDownButton.Size = new System.Drawing.Size(45, 22);
            this.modifiedToolStripDropDownButton.Text = "編集";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.settingToolStripMenuItem.Text = "設定";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Clicked);
            //
            // pluginSetupToolStripMenuItem
            //
            this.pluginSetupToolStripMenuItem.Name = "pluginToolStripMenuItem";
            this.pluginSetupToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.pluginSetupToolStripMenuItem.Text = "プラグイン設定";
            this.pluginSetupToolStripMenuItem.Click += new System.EventHandler(this.PluginSetupToolStripMenuItem_Clicked);
            // 
            // otherToolStripDropDownButton
            // 
            this.otherToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.otherToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutBoxToolStripMenuItem});
            this.otherToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("otherToolStripDropDownButton.Image")));
            this.otherToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.otherToolStripDropDownButton.Name = "otherToolStripDropDownButton";
            this.otherToolStripDropDownButton.Size = new System.Drawing.Size(57, 22);
            this.otherToolStripDropDownButton.Text = "その他";
            // 
            // aboutBoxToolStripMenuItem
            // 
            this.aboutBoxToolStripMenuItem.Name = "aboutBoxToolStripMenuItem";
            this.aboutBoxToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.aboutBoxToolStripMenuItem.Text = "バージョン情報";
            this.aboutBoxToolStripMenuItem.Click += new System.EventHandler(this.AboutBoxShow_Cliked);
            //
            // startPageWebPage
            //
            this.startPageWebPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPageWebPage.Name = "startPageWebPage";
            // 
            // tabControl1
            // 
            this.MainTabControl.Controls.Add(this.StartPage);
            this.MainTabControl.Controls.Add(this._2chBrowser);
            this.MainTabControl.Controls.Add(this.WebBrowserPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 25);
            this.MainTabControl.Name = "tabControl1";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(830, 421);
            this.MainTabControl.TabIndex = 2;
            // 
            // StartPage
            // 
            this.StartPage.Location = new System.Drawing.Point(4, 24);
            this.StartPage.Name = "StartPage";
            this.StartPage.Padding = new System.Windows.Forms.Padding(3);
            this.StartPage.Size = new System.Drawing.Size(822, 393);
            this.StartPage.TabIndex = 0;
            this.StartPage.Text = "スタートページ";
            this.StartPage.Controls.Add(this.startPageWebPage);
            this.StartPage.UseVisualStyleBackColor = true;
            // 
            // _2chBrowser
            // 
            this._2chBrowser.Controls.Add(this.ch2BrowserControl1);
            this._2chBrowser.Location = new System.Drawing.Point(4, 24);
            this._2chBrowser.Name = "_2chBrowser";
            this._2chBrowser.Padding = new System.Windows.Forms.Padding(3);
            this._2chBrowser.Size = new System.Drawing.Size(822, 393);
            this._2chBrowser.TabIndex = 1;
            this._2chBrowser.Text = "2ch専ブラ";
            this._2chBrowser.UseVisualStyleBackColor = true;
            // 
            // ch2BrowserControl1
            // 
            this.ch2BrowserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ch2BrowserControl1.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.ch2BrowserControl1.Location = new System.Drawing.Point(3, 3);
            this.ch2BrowserControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ch2BrowserControl1.Name = "ch2BrowserControl1";
            this.ch2BrowserControl1.Size = new System.Drawing.Size(816, 387);
            this.ch2BrowserControl1.TabIndex = 0;
            // 
            // WebBrowserPage
            // 
            this.WebBrowserPage.Controls.Add(this.tabControl2);
            this.WebBrowserPage.Controls.Add(this.toolStrip2);
            this.WebBrowserPage.Location = new System.Drawing.Point(4, 24);
            this.WebBrowserPage.Name = "WebBrowserPage";
            this.WebBrowserPage.Padding = new System.Windows.Forms.Padding(3);
            this.WebBrowserPage.Size = new System.Drawing.Size(822, 393);
            this.WebBrowserPage.TabIndex = 2;
            this.WebBrowserPage.Text = "ウェブブラウザ";
            this.WebBrowserPage.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 28);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(816, 362);
            this.tabControl2.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(816, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(830, 421);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(830, 446);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 446);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MainTabControl.ResumeLayout(false);
            this._2chBrowser.ResumeLayout(false);
            this.WebBrowserPage.ResumeLayout(false);
            this.WebBrowserPage.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TabCloseMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.TabControl MainTabControl { get; set; }
        private System.Windows.Forms.TabPage StartPage;
        private System.Windows.Forms.TabPage _2chBrowser;
        private ch2BrowserControl ch2BrowserControl1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton modifiedToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.TabPage WebBrowserPage;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        public System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.ToolStripDropDownButton otherToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem aboutBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pluginSetupToolStripMenuItem;
        private System.Windows.Forms.WebBrowser startPageWebPage;
    }
}

