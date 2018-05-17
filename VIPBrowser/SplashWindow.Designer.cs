namespace VIPBrowser
{
    partial class SplashWindow
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
            this.verLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // verLabel
            // 
            this.verLabel.BackColor = System.Drawing.Color.LightPink;
            this.verLabel.Location = new System.Drawing.Point(314, 167);
            this.verLabel.Name = "verLabel";
            this.verLabel.Size = new System.Drawing.Size(123, 23);
            this.verLabel.TabIndex = 0;
            this.verLabel.Text = "labeli";
            this.verLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SplashWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 202);
            this.Controls.Add(this.verLabel);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SplashWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashWindow";
            this.TransparencyKey = System.Drawing.Color.LightPink;
            this.Load += new System.EventHandler(this.SplashWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label verLabel;
    }
}