namespace VIPBrowser.Dialogs
{
    partial class MigratedFromOriginalDialog
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
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.v2cRadioBox = new System.Windows.Forms.RadioButton();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.janeStyleRadioBox = new System.Windows.Forms.RadioButton();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pastLogCheckBox = new System.Windows.Forms.CheckBox();
			this.cookieCheckBox = new System.Windows.Forms.CheckBox();
			this.aaListCheckBox = new System.Windows.Forms.CheckBox();
			this.writeRecodeCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Enabled = false;
			this.radioButton3.Location = new System.Drawing.Point(150, 57);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(100, 19);
			this.radioButton3.TabIndex = 12;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "radioButton3";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// v2cRadioBox
			// 
			this.v2cRadioBox.AutoSize = true;
			this.v2cRadioBox.Location = new System.Drawing.Point(96, 57);
			this.v2cRadioBox.Name = "v2cRadioBox";
			this.v2cRadioBox.Size = new System.Drawing.Size(48, 19);
			this.v2cRadioBox.TabIndex = 11;
			this.v2cRadioBox.TabStop = true;
			this.v2cRadioBox.Text = "V2C";
			this.v2cRadioBox.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 27);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(239, 23);
			this.textBox1.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 15);
			this.label1.TabIndex = 9;
			this.label1.Text = "移行元のフォルダー";
			// 
			// janeStyleRadioBox
			// 
			this.janeStyleRadioBox.AutoSize = true;
			this.janeStyleRadioBox.Location = new System.Drawing.Point(9, 57);
			this.janeStyleRadioBox.Name = "janeStyleRadioBox";
			this.janeStyleRadioBox.Size = new System.Drawing.Size(81, 19);
			this.janeStyleRadioBox.TabIndex = 8;
			this.janeStyleRadioBox.TabStop = true;
			this.janeStyleRadioBox.Text = "JaneStyle";
			this.janeStyleRadioBox.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(257, 24);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 7;
			this.button3.Text = "参照";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(257, 53);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 13;
			this.button4.Text = "移行する";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.writeRecodeCheckBox);
			this.groupBox1.Controls.Add(this.aaListCheckBox);
			this.groupBox1.Controls.Add(this.cookieCheckBox);
			this.groupBox1.Controls.Add(this.pastLogCheckBox);
			this.groupBox1.Location = new System.Drawing.Point(15, 82);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(317, 122);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "移行するもの";
			// 
			// pastLogCheckBox
			// 
			this.pastLogCheckBox.AutoSize = true;
			this.pastLogCheckBox.Location = new System.Drawing.Point(7, 22);
			this.pastLogCheckBox.Name = "pastLogCheckBox";
			this.pastLogCheckBox.Size = new System.Drawing.Size(68, 19);
			this.pastLogCheckBox.TabIndex = 15;
			this.pastLogCheckBox.Text = "過去ログ";
			this.pastLogCheckBox.UseVisualStyleBackColor = true;
			// 
			// cookieCheckBox
			// 
			this.cookieCheckBox.AutoSize = true;
			this.cookieCheckBox.Location = new System.Drawing.Point(81, 22);
			this.cookieCheckBox.Name = "cookieCheckBox";
			this.cookieCheckBox.Size = new System.Drawing.Size(65, 19);
			this.cookieCheckBox.TabIndex = 16;
			this.cookieCheckBox.Text = "Cookie";
			this.cookieCheckBox.UseVisualStyleBackColor = true;
			// 
			// aaListCheckBox
			// 
			this.aaListCheckBox.AutoSize = true;
			this.aaListCheckBox.Enabled = false;
			this.aaListCheckBox.Location = new System.Drawing.Point(7, 47);
			this.aaListCheckBox.Name = "aaListCheckBox";
			this.aaListCheckBox.Size = new System.Drawing.Size(66, 19);
			this.aaListCheckBox.TabIndex = 17;
			this.aaListCheckBox.Text = "AAリスト";
			this.aaListCheckBox.UseVisualStyleBackColor = true;
			// 
			// writeRecodeCheckBox
			// 
			this.writeRecodeCheckBox.AutoSize = true;
			this.writeRecodeCheckBox.Location = new System.Drawing.Point(152, 22);
			this.writeRecodeCheckBox.Name = "writeRecodeCheckBox";
			this.writeRecodeCheckBox.Size = new System.Drawing.Size(94, 19);
			this.writeRecodeCheckBox.TabIndex = 18;
			this.writeRecodeCheckBox.Text = "書き込み履歴";
			this.writeRecodeCheckBox.UseVisualStyleBackColor = true;
			// 
			// MigratedFromOriginalDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(346, 222);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.radioButton3);
			this.Controls.Add(this.v2cRadioBox);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.janeStyleRadioBox);
			this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MigratedFromOriginalDialog";
			this.Text = "移行作業 -移行元選択 ";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton v2cRadioBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton janeStyleRadioBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox writeRecodeCheckBox;
		private System.Windows.Forms.CheckBox aaListCheckBox;
		private System.Windows.Forms.CheckBox cookieCheckBox;
		private System.Windows.Forms.CheckBox pastLogCheckBox;

    }
}