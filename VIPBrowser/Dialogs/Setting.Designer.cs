namespace VIPBrowser.ch2Browser.Dialogs
{
    partial class Setting
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.isShowStartPageCheckBox = new System.Windows.Forms.CheckBox();
			this.isUseVisualStyle = new System.Windows.Forms.CheckBox();
			this.isTimerGC = new System.Windows.Forms.CheckBox();
			this.isFormCloseWarning = new System.Windows.Forms.CheckBox();
			this.isSaveFormLocation = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.isMultiThreadingCheckBox = new System.Windows.Forms.CheckBox();
			this.defaultAddressbarTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.isSaveThreadTabCheckBox = new System.Windows.Forms.CheckBox();
			this.isSaveThreadListTabCheckBox = new System.Windows.Forms.CheckBox();
			this.normalBoardLabel = new System.Windows.Forms.Label();
			this.defaultBoardListNameTextBox = new System.Windows.Forms.TextBox();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.settingIdColoringButton = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.skinImagePicutu = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.skinListBox = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.defaultSearchEngineComboBox = new System.Windows.Forms.ComboBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.isSaveWriteRecord = new System.Windows.Forms.CheckBox();
			this.SettingSaveButton = new System.Windows.Forms.Button();
			this.SaveButton = new System.Windows.Forms.Button();
			this.saveAndRestartButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.button1 = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.skinImagePicutu)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(671, 321);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox2);
			this.tabPage2.Controls.Add(this.groupBox1);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(663, 293);
			this.tabPage2.TabIndex = 2;
			this.tabPage2.Text = "全般";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.isShowStartPageCheckBox);
			this.groupBox2.Controls.Add(this.isUseVisualStyle);
			this.groupBox2.Controls.Add(this.isTimerGC);
			this.groupBox2.Controls.Add(this.isFormCloseWarning);
			this.groupBox2.Controls.Add(this.isSaveFormLocation);
			this.groupBox2.Location = new System.Drawing.Point(440, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(215, 281);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "全般";
			// 
			// isShowStartPageCheckBox
			// 
			this.isShowStartPageCheckBox.AutoSize = true;
			this.isShowStartPageCheckBox.Location = new System.Drawing.Point(6, 136);
			this.isShowStartPageCheckBox.Name = "isShowStartPageCheckBox";
			this.isShowStartPageCheckBox.Size = new System.Drawing.Size(143, 19);
			this.isShowStartPageCheckBox.TabIndex = 4;
			this.isShowStartPageCheckBox.Text = "スタートページを表示する";
			this.isShowStartPageCheckBox.UseVisualStyleBackColor = true;
			// 
			// isUseVisualStyle
			// 
			this.isUseVisualStyle.AutoSize = true;
			this.isUseVisualStyle.Checked = true;
			this.isUseVisualStyle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.isUseVisualStyle.Location = new System.Drawing.Point(6, 111);
			this.isUseVisualStyle.Name = "isUseVisualStyle";
			this.isUseVisualStyle.Size = new System.Drawing.Size(173, 19);
			this.isUseVisualStyle.TabIndex = 3;
			this.isUseVisualStyle.Text = "VisualStyleを用いて描画する";
			this.isUseVisualStyle.UseVisualStyleBackColor = true;
			// 
			// isTimerGC
			// 
			this.isTimerGC.AutoSize = true;
			this.isTimerGC.Location = new System.Drawing.Point(6, 71);
			this.isTimerGC.Name = "isTimerGC";
			this.isTimerGC.Size = new System.Drawing.Size(184, 34);
			this.isTimerGC.TabIndex = 2;
			this.isTimerGC.Text = "GCを定期的に行う\r\n(性能が低下する場合があります)";
			this.isTimerGC.UseVisualStyleBackColor = true;
			// 
			// isFormCloseWarning
			// 
			this.isFormCloseWarning.AutoSize = true;
			this.isFormCloseWarning.Location = new System.Drawing.Point(6, 46);
			this.isFormCloseWarning.Name = "isFormCloseWarning";
			this.isFormCloseWarning.Size = new System.Drawing.Size(203, 19);
			this.isFormCloseWarning.TabIndex = 1;
			this.isFormCloseWarning.Text = "アプリケーションの終了時に警告を行う";
			this.isFormCloseWarning.UseVisualStyleBackColor = true;
			// 
			// isSaveFormLocation
			// 
			this.isSaveFormLocation.AutoSize = true;
			this.isSaveFormLocation.Checked = true;
			this.isSaveFormLocation.CheckState = System.Windows.Forms.CheckState.Checked;
			this.isSaveFormLocation.Location = new System.Drawing.Point(6, 21);
			this.isSaveFormLocation.Name = "isSaveFormLocation";
			this.isSaveFormLocation.Size = new System.Drawing.Size(193, 19);
			this.isSaveFormLocation.TabIndex = 0;
			this.isSaveFormLocation.Text = "終了時にフォームの位置を保存する";
			this.isSaveFormLocation.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.isMultiThreadingCheckBox);
			this.groupBox1.Controls.Add(this.defaultAddressbarTextBox);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.isSaveThreadTabCheckBox);
			this.groupBox1.Controls.Add(this.isSaveThreadListTabCheckBox);
			this.groupBox1.Controls.Add(this.normalBoardLabel);
			this.groupBox1.Controls.Add(this.defaultBoardListNameTextBox);
			this.groupBox1.Location = new System.Drawing.Point(8, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(426, 284);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "専用ブラウザ";
			// 
			// isMultiThreadingCheckBox
			// 
			this.isMultiThreadingCheckBox.AutoSize = true;
			this.isMultiThreadingCheckBox.Location = new System.Drawing.Point(9, 129);
			this.isMultiThreadingCheckBox.Name = "isMultiThreadingCheckBox";
			this.isMultiThreadingCheckBox.Size = new System.Drawing.Size(191, 19);
			this.isMultiThreadingCheckBox.TabIndex = 6;
			this.isMultiThreadingCheckBox.Text = "スレッド読み込み時に並列化を行う";
			this.isMultiThreadingCheckBox.UseVisualStyleBackColor = true;
			// 
			// defaultAddressbarTextBox
			// 
			this.defaultAddressbarTextBox.Location = new System.Drawing.Point(184, 50);
			this.defaultAddressbarTextBox.Name = "defaultAddressbarTextBox";
			this.defaultAddressbarTextBox.Size = new System.Drawing.Size(236, 23);
			this.defaultAddressbarTextBox.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 53);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(172, 15);
			this.label6.TabIndex = 4;
			this.label6.Text = "アドレスバーに標準で表示するURL";
			// 
			// isSaveThreadTabCheckBox
			// 
			this.isSaveThreadTabCheckBox.AutoSize = true;
			this.isSaveThreadTabCheckBox.Location = new System.Drawing.Point(9, 104);
			this.isSaveThreadTabCheckBox.Name = "isSaveThreadTabCheckBox";
			this.isSaveThreadTabCheckBox.Size = new System.Drawing.Size(217, 19);
			this.isSaveThreadTabCheckBox.TabIndex = 3;
			this.isSaveThreadTabCheckBox.Text = "終了時にスレッドのタブの状態を保存する";
			this.isSaveThreadTabCheckBox.UseVisualStyleBackColor = true;
			// 
			// isSaveThreadListTabCheckBox
			// 
			this.isSaveThreadListTabCheckBox.AutoSize = true;
			this.isSaveThreadListTabCheckBox.Location = new System.Drawing.Point(9, 79);
			this.isSaveThreadListTabCheckBox.Name = "isSaveThreadListTabCheckBox";
			this.isSaveThreadListTabCheckBox.Size = new System.Drawing.Size(241, 19);
			this.isSaveThreadListTabCheckBox.TabIndex = 2;
			this.isSaveThreadListTabCheckBox.Text = "終了時にスレッドリストのタブの状態を保存する";
			this.isSaveThreadListTabCheckBox.UseVisualStyleBackColor = true;
			// 
			// normalBoardLabel
			// 
			this.normalBoardLabel.AutoSize = true;
			this.normalBoardLabel.Location = new System.Drawing.Point(6, 25);
			this.normalBoardLabel.Name = "normalBoardLabel";
			this.normalBoardLabel.Size = new System.Drawing.Size(154, 15);
			this.normalBoardLabel.TabIndex = 1;
			this.normalBoardLabel.Text = "標準で更新する板一覧のURL";
			// 
			// defaultBoardListNameTextBox
			// 
			this.defaultBoardListNameTextBox.Location = new System.Drawing.Point(184, 22);
			this.defaultBoardListNameTextBox.Name = "defaultBoardListNameTextBox";
			this.defaultBoardListNameTextBox.Size = new System.Drawing.Size(236, 23);
			this.defaultBoardListNameTextBox.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.settingIdColoringButton);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.defaultSearchEngineComboBox);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(663, 293);
			this.tabPage1.TabIndex = 4;
			this.tabPage1.Text = "スレッド";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(221, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 15);
			this.label5.TabIndex = 4;
			this.label5.Text = "IDの色付け";
			// 
			// settingIdColoringButton
			// 
			this.settingIdColoringButton.Location = new System.Drawing.Point(224, 29);
			this.settingIdColoringButton.Name = "settingIdColoringButton";
			this.settingIdColoringButton.Size = new System.Drawing.Size(75, 23);
			this.settingIdColoringButton.TabIndex = 3;
			this.settingIdColoringButton.Text = "設定する";
			this.settingIdColoringButton.UseVisualStyleBackColor = true;
			this.settingIdColoringButton.Click += new System.EventHandler(this.settingIdColoringButton_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.skinImagePicutu);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.skinListBox);
			this.groupBox3.Location = new System.Drawing.Point(429, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(226, 281);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "スキン";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 15);
			this.label4.TabIndex = 3;
			this.label4.Text = "プレビュー";
			// 
			// skinImagePicutu
			// 
			this.skinImagePicutu.Location = new System.Drawing.Point(9, 154);
			this.skinImagePicutu.Name = "skinImagePicutu";
			this.skinImagePicutu.Size = new System.Drawing.Size(211, 121);
			this.skinImagePicutu.TabIndex = 2;
			this.skinImagePicutu.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(117, 15);
			this.label3.TabIndex = 1;
			this.label3.Text = "使用可能なスキン一覧";
			// 
			// skinListBox
			// 
			this.skinListBox.FormattingEnabled = true;
			this.skinListBox.ItemHeight = 15;
			this.skinListBox.Location = new System.Drawing.Point(6, 37);
			this.skinListBox.Name = "skinListBox";
			this.skinListBox.Size = new System.Drawing.Size(214, 79);
			this.skinListBox.TabIndex = 0;
			this.skinListBox.SelectedIndexChanged += new System.EventHandler(this.skinListBox_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "デフォルトの検索エンジン";
			// 
			// defaultSearchEngineComboBox
			// 
			this.defaultSearchEngineComboBox.FormattingEnabled = true;
			this.defaultSearchEngineComboBox.Items.AddRange(new object[] {
            "Google",
            "Yahoo",
            "Bing",
            "Amazon"});
			this.defaultSearchEngineComboBox.Location = new System.Drawing.Point(18, 30);
			this.defaultSearchEngineComboBox.Name = "defaultSearchEngineComboBox";
			this.defaultSearchEngineComboBox.Size = new System.Drawing.Size(189, 23);
			this.defaultSearchEngineComboBox.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.isSaveWriteRecord);
			this.tabPage3.Location = new System.Drawing.Point(4, 24);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(663, 293);
			this.tabPage3.TabIndex = 5;
			this.tabPage3.Text = "レス";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// isSaveWriteRecord
			// 
			this.isSaveWriteRecord.AutoSize = true;
			this.isSaveWriteRecord.Location = new System.Drawing.Point(8, 6);
			this.isSaveWriteRecord.Name = "isSaveWriteRecord";
			this.isSaveWriteRecord.Size = new System.Drawing.Size(146, 19);
			this.isSaveWriteRecord.TabIndex = 0;
			this.isSaveWriteRecord.Text = "書き込み履歴を保存する";
			this.isSaveWriteRecord.UseVisualStyleBackColor = true;
			// 
			// SettingSaveButton
			// 
			this.SettingSaveButton.Location = new System.Drawing.Point(592, 327);
			this.SettingSaveButton.Name = "SettingSaveButton";
			this.SettingSaveButton.Size = new System.Drawing.Size(67, 23);
			this.SettingSaveButton.TabIndex = 1;
			this.SettingSaveButton.Text = "OK";
			this.SettingSaveButton.UseVisualStyleBackColor = true;
			this.SettingSaveButton.Click += new System.EventHandler(this.SettingSaveButton_Click);
			// 
			// SaveButton
			// 
			this.SaveButton.Location = new System.Drawing.Point(511, 327);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(75, 23);
			this.SaveButton.TabIndex = 2;
			this.SaveButton.Text = "適用";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// saveAndRestartButton
			// 
			this.saveAndRestartButton.Location = new System.Drawing.Point(12, 327);
			this.saveAndRestartButton.Name = "saveAndRestartButton";
			this.saveAndRestartButton.Size = new System.Drawing.Size(156, 23);
			this.saveAndRestartButton.TabIndex = 3;
			this.saveAndRestartButton.Text = "設定を適用後再起動する";
			this.saveAndRestartButton.UseVisualStyleBackColor = true;
			this.saveAndRestartButton.Click += new System.EventHandler(this.SaveAndRestartbutton_Clicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(174, 331);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(249, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "一部の設定はプログラムの再起動後に適用されます";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(6, 161);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(173, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "BlueRushに関する設定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_2);
			// 
			// Setting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(671, 359);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.saveAndRestartButton);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.SettingSaveButton);
			this.Controls.Add(this.tabControl1);
			this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "Setting";
			this.Text = "設定";
			this.Load += new System.EventHandler(this.Setting_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.skinImagePicutu)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button SettingSaveButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button saveAndRestartButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label normalBoardLabel;
        private System.Windows.Forms.TextBox defaultBoardListNameTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox isSaveFormLocation;
        private System.Windows.Forms.CheckBox isFormCloseWarning;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox defaultSearchEngineComboBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox isSaveWriteRecord;
        private System.Windows.Forms.CheckBox isTimerGC;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox skinImagePicutu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox skinListBox;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.CheckBox isUseVisualStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button settingIdColoringButton;
        private System.Windows.Forms.CheckBox isSaveThreadTabCheckBox;
        private System.Windows.Forms.CheckBox isSaveThreadListTabCheckBox;
		private System.Windows.Forms.TextBox defaultAddressbarTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox isShowStartPageCheckBox;
		private System.Windows.Forms.CheckBox isMultiThreadingCheckBox;
		private System.Windows.Forms.Button button1;
    }
}