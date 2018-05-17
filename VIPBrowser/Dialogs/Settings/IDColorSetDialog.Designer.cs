namespace VIPBrowser.Dialogs.Settings
{
    partial class IDColorSetDialog
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
            this.minValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.maxValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.colorSelectButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.minValueNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxValueNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // minValueNumericUpDown
            // 
            this.minValueNumericUpDown.Location = new System.Drawing.Point(12, 25);
            this.minValueNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.minValueNumericUpDown.Name = "minValueNumericUpDown";
            this.minValueNumericUpDown.Size = new System.Drawing.Size(120, 23);
            this.minValueNumericUpDown.TabIndex = 0;
            // 
            // maxValueNumericUpDown
            // 
            this.maxValueNumericUpDown.Location = new System.Drawing.Point(138, 25);
            this.maxValueNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.maxValueNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxValueNumericUpDown.Name = "maxValueNumericUpDown";
            this.maxValueNumericUpDown.Size = new System.Drawing.Size(120, 23);
            this.maxValueNumericUpDown.TabIndex = 1;
            this.maxValueNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "最小値";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "最大値";
            // 
            // colorSelectButton
            // 
            this.colorSelectButton.Location = new System.Drawing.Point(101, 54);
            this.colorSelectButton.Name = "colorSelectButton";
            this.colorSelectButton.Size = new System.Drawing.Size(75, 23);
            this.colorSelectButton.TabIndex = 4;
            this.colorSelectButton.Text = "色を選択";
            this.colorSelectButton.UseVisualStyleBackColor = true;
            this.colorSelectButton.Click += new System.EventHandler(this.colorSelectButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(183, 54);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 5;
            this.acceptButton.Text = "完了";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // IDColorSetDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 91);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.colorSelectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxValueNumericUpDown);
            this.Controls.Add(this.minValueNumericUpDown);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "IDColorSetDialog";
            this.Text = "IDColorSetDialog";
            ((System.ComponentModel.ISupportInitialize)(this.minValueNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxValueNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NumericUpDown minValueNumericUpDown;
        private System.Windows.Forms.NumericUpDown maxValueNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button colorSelectButton;
        private System.Windows.Forms.Button acceptButton;
    }
}