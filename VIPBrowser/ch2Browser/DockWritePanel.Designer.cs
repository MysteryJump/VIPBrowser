namespace VIPBrowser.ch2Browser
{
    partial class DockWritePanel
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.threadTitleTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mailTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.AAButton = new System.Windows.Forms.Button();
            this.uploaderButton = new System.Windows.Forms.Button();
            this.IsThreadCheckBox = new System.Windows.Forms.CheckBox();
            this.sentenceTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.10101F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.35353F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.050505F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.10101F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.13131F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.13131F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.13131F));
            this.tableLayoutPanel1.Controls.Add(this.threadTitleTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mailTextBox, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.acceptButton, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.AAButton, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.uploaderButton, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.IsThreadCheckBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.sentenceTextBox, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(639, 167);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // threadTitleTextBox
            // 
            this.threadTitleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.threadTitleTextBox.Enabled = false;
            this.threadTitleTextBox.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.threadTitleTextBox.Location = new System.Drawing.Point(67, 3);
            this.threadTitleTextBox.Name = "threadTitleTextBox";
            this.threadTitleTextBox.Size = new System.Drawing.Size(219, 19);
            this.threadTitleTextBox.TabIndex = 17;
            // 
            // nameTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.nameTextBox, 2);
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextBox.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.nameTextBox.Location = new System.Drawing.Point(67, 32);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(251, 19);
            this.nameTextBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 29);
            this.label3.TabIndex = 18;
            this.label3.Text = "スレタイ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 29);
            this.label1.TabIndex = 14;
            this.label1.Text = "名前";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mailTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.mailTextBox, 3);
            this.mailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mailTextBox.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.mailTextBox.Location = new System.Drawing.Point(388, 32);
            this.mailTextBox.Name = "mailTextBox";
            this.mailTextBox.Size = new System.Drawing.Size(248, 19);
            this.mailTextBox.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.label2.Location = new System.Drawing.Point(324, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 29);
            this.label2.TabIndex = 15;
            this.label2.Text = "メール";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // acceptButton
            // 
            this.acceptButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acceptButton.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.acceptButton.Location = new System.Drawing.Point(554, 3);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(82, 23);
            this.acceptButton.TabIndex = 19;
            this.acceptButton.Text = "送信";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // AAButton
            // 
            this.AAButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AAButton.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.AAButton.Location = new System.Drawing.Point(471, 3);
            this.AAButton.Name = "AAButton";
            this.AAButton.Size = new System.Drawing.Size(77, 23);
            this.AAButton.TabIndex = 21;
            this.AAButton.Text = "AA";
            this.AAButton.UseVisualStyleBackColor = true;
            this.AAButton.Click += new System.EventHandler(this.AAButton_Click);
            // 
            // uploaderButton
            // 
            this.uploaderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploaderButton.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.uploaderButton.Location = new System.Drawing.Point(388, 3);
            this.uploaderButton.Name = "uploaderButton";
            this.uploaderButton.Size = new System.Drawing.Size(77, 23);
            this.uploaderButton.TabIndex = 20;
            this.uploaderButton.Text = "アップローダー";
            this.uploaderButton.UseVisualStyleBackColor = true;
            this.uploaderButton.Click += new System.EventHandler(this.uploaderButton_Click);
            // 
            // IsThreadCheckBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.IsThreadCheckBox, 2);
            this.IsThreadCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IsThreadCheckBox.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.IsThreadCheckBox.Location = new System.Drawing.Point(292, 3);
            this.IsThreadCheckBox.Name = "IsThreadCheckBox";
            this.IsThreadCheckBox.Size = new System.Drawing.Size(90, 23);
            this.IsThreadCheckBox.TabIndex = 12;
            this.IsThreadCheckBox.Text = "スレ立て";
            this.IsThreadCheckBox.UseVisualStyleBackColor = true;
            this.IsThreadCheckBox.CheckedChanged += new System.EventHandler(this.IsThreadCheckBox_CheckedChanged);
            // 
            // sentenceTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.sentenceTextBox, 7);
            this.sentenceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sentenceTextBox.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sentenceTextBox.Location = new System.Drawing.Point(3, 61);
            this.sentenceTextBox.Multiline = true;
            this.sentenceTextBox.Name = "sentenceTextBox";
            this.sentenceTextBox.Size = new System.Drawing.Size(633, 103);
            this.sentenceTextBox.TabIndex = 22;
            // 
            // DockWritePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DockWritePanel";
            this.Size = new System.Drawing.Size(639, 167);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox threadTitleTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mailTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button AAButton;
        private System.Windows.Forms.Button uploaderButton;
        private System.Windows.Forms.CheckBox IsThreadCheckBox;
        private System.Windows.Forms.TextBox sentenceTextBox;

    }
}
