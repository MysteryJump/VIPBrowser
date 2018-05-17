namespace VIPBrowser.ch2Browser
{
    partial class ThreadListStyleColorChangeForm
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.conditionsTextBox = new System.Windows.Forms.TextBox();
            this.typeSelectComboBox = new System.Windows.Forms.ComboBox();
            this.colorSelectButton = new System.Windows.Forms.Button();
            this.enterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // conditionsTextBox
            // 
            this.conditionsTextBox.Location = new System.Drawing.Point(192, 12);
            this.conditionsTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.conditionsTextBox.Name = "conditionsTextBox";
            this.conditionsTextBox.Size = new System.Drawing.Size(146, 23);
            this.conditionsTextBox.TabIndex = 0;
            this.conditionsTextBox.TextChanged += new System.EventHandler(this.conditionstextBox_TextChanged);
            // 
            // typeSelectComboBox
            // 
            this.typeSelectComboBox.FormattingEnabled = true;
            this.typeSelectComboBox.Items.AddRange(new object[] {
            "勢い",
            "レス数",
            "スレタイ"});
            this.typeSelectComboBox.Location = new System.Drawing.Point(12, 12);
            this.typeSelectComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.typeSelectComboBox.Name = "typeSelectComboBox";
            this.typeSelectComboBox.Size = new System.Drawing.Size(174, 23);
            this.typeSelectComboBox.TabIndex = 2;
            // 
            // colorSelectButton
            // 
            this.colorSelectButton.Location = new System.Drawing.Point(344, 12);
            this.colorSelectButton.Name = "colorSelectButton";
            this.colorSelectButton.Size = new System.Drawing.Size(75, 23);
            this.colorSelectButton.TabIndex = 3;
            this.colorSelectButton.Text = "色選択";
            this.colorSelectButton.UseVisualStyleBackColor = true;
            this.colorSelectButton.Click += new System.EventHandler(this.colorSelectButton_Click);
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(344, 39);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(75, 23);
            this.enterButton.TabIndex = 4;
            this.enterButton.Text = "適用";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // ThreadListStyleColorChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 74);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.colorSelectButton);
            this.Controls.Add(this.typeSelectComboBox);
            this.Controls.Add(this.conditionsTextBox);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ThreadListStyleColorChangeForm";
            this.Text = "スタイル変更";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox conditionsTextBox;
        private System.Windows.Forms.ComboBox typeSelectComboBox;
        private System.Windows.Forms.Button colorSelectButton;
        private System.Windows.Forms.Button enterButton;
    }
}