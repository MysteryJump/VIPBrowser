namespace VIPBrowser.ch2Browser
{
    partial class UploadDialog
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
			this.loadClipBoardButton = new System.Windows.Forms.Button();
			this.refFileButton = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.imgurRadioButton = new System.Windows.Forms.RadioButton();
			this.axfcRadioButton = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.deletePassTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.downloadPassTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.commentTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// loadClipBoardButton
			// 
			this.loadClipBoardButton.Location = new System.Drawing.Point(12, 214);
			this.loadClipBoardButton.Name = "loadClipBoardButton";
			this.loadClipBoardButton.Size = new System.Drawing.Size(171, 23);
			this.loadClipBoardButton.TabIndex = 0;
			this.loadClipBoardButton.Text = "クリップボードから画像を読み込む";
			this.loadClipBoardButton.UseVisualStyleBackColor = true;
			this.loadClipBoardButton.Click += new System.EventHandler(this.loadClipBoardButton_Click);
			// 
			// refFileButton
			// 
			this.refFileButton.Location = new System.Drawing.Point(189, 4);
			this.refFileButton.Name = "refFileButton";
			this.refFileButton.Size = new System.Drawing.Size(70, 23);
			this.refFileButton.TabIndex = 1;
			this.refFileButton.Text = "参照";
			this.refFileButton.UseVisualStyleBackColor = true;
			this.refFileButton.Click += new System.EventHandler(this.refFileButton_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// imgurRadioButton
			// 
			this.imgurRadioButton.AutoSize = true;
			this.imgurRadioButton.Location = new System.Drawing.Point(12, 8);
			this.imgurRadioButton.Name = "imgurRadioButton";
			this.imgurRadioButton.Size = new System.Drawing.Size(61, 19);
			this.imgurRadioButton.TabIndex = 2;
			this.imgurRadioButton.TabStop = true;
			this.imgurRadioButton.Text = "Imgur";
			this.imgurRadioButton.UseVisualStyleBackColor = true;
			// 
			// axfcRadioButton
			// 
			this.axfcRadioButton.AutoSize = true;
			this.axfcRadioButton.Location = new System.Drawing.Point(79, 8);
			this.axfcRadioButton.Name = "axfcRadioButton";
			this.axfcRadioButton.Size = new System.Drawing.Size(50, 19);
			this.axfcRadioButton.TabIndex = 3;
			this.axfcRadioButton.TabStop = true;
			this.axfcRadioButton.Text = "Axfc";
			this.axfcRadioButton.UseVisualStyleBackColor = true;
			this.axfcRadioButton.CheckedChanged += new System.EventHandler(this.axfcRadioButton_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(11, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = " ";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(189, 214);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(70, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "アップロード";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// deletePassTextBox
			// 
			this.deletePassTextBox.Enabled = false;
			this.deletePassTextBox.Location = new System.Drawing.Point(12, 109);
			this.deletePassTextBox.Name = "deletePassTextBox";
			this.deletePassTextBox.Size = new System.Drawing.Size(117, 23);
			this.deletePassTextBox.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 91);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 15);
			this.label2.TabIndex = 7;
			this.label2.Text = "削除パス";
			// 
			// downloadPassTextBox
			// 
			this.downloadPassTextBox.Enabled = false;
			this.downloadPassTextBox.Location = new System.Drawing.Point(135, 109);
			this.downloadPassTextBox.Name = "downloadPassTextBox";
			this.downloadPassTextBox.Size = new System.Drawing.Size(123, 23);
			this.downloadPassTextBox.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(132, 91);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "ダウンロードパス";
			// 
			// commentTextBox
			// 
			this.commentTextBox.Enabled = false;
			this.commentTextBox.Location = new System.Drawing.Point(12, 150);
			this.commentTextBox.Multiline = true;
			this.commentTextBox.Name = "commentTextBox";
			this.commentTextBox.Size = new System.Drawing.Size(246, 58);
			this.commentTextBox.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 132);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 15);
			this.label4.TabIndex = 11;
			this.label4.Text = "コメント";
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Enabled = false;
			this.fileNameTextBox.Location = new System.Drawing.Point(12, 65);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(117, 23);
			this.fileNameTextBox.TabIndex = 12;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 15);
			this.label5.TabIndex = 13;
			this.label5.Text = "ファイル名";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new System.Drawing.Point(143, 59);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(115, 34);
			this.checkBox1.TabIndex = 14;
			this.checkBox1.Text = "ダウンロード件数は\r\n非常に少ない予定";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(189, 30);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(70, 23);
			this.button2.TabIndex = 15;
			this.button2.Text = "絵を描く";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// UploadDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(270, 249);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.fileNameTextBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.commentTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.downloadPassTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.deletePassTextBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.axfcRadioButton);
			this.Controls.Add(this.imgurRadioButton);
			this.Controls.Add(this.refFileButton);
			this.Controls.Add(this.loadClipBoardButton);
			this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "UploadDialog";
			this.Text = "UploadDialog";
			this.Load += new System.EventHandler(this.UploadDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadClipBoardButton;
        private System.Windows.Forms.Button refFileButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton imgurRadioButton;
        private System.Windows.Forms.RadioButton axfcRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox deletePassTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox downloadPassTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox commentTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button2;
    }
}