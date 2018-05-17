using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Chron.ThreadOrResData.Abone;

namespace VIPBrowser.ch2Browser
{
    class NGCollectionEditDialog : Form
    {
        private Button okButton;
        private TextBox textBox1;
        private ComboBox comboBox1;

        public string Conditions { get { return textBox1.Text; } set { textBox1.Text = value; } }

        public AboneType Type
        {
            get 
            {
                string data = comboBox1.SelectedItem.ToString();
                if (data == "ID")
                    data = "ID";
                else if (data == "名前欄")
                    data = "Name";
                else if (data == "メール欄")
                    data = "Mail";
                else
                    data = "Sentence";
                return (AboneType)Enum.Parse(typeof(AboneType), data);
            }
            set 
            {
                string data = value.ToString();
                if (data == "ID")
                    data = "ID";
                else if (data == "Name")
                    data = "名前欄";
                else if (data == "Mail")
                    data = "メール欄";
                else
                    data = "本文";
                this.comboBox1.SelectedItem = data;
            }
        }
        public string ComboBoxSet 
        {
            set 
            {
                string data = value.ToString();
                if (data == "ID")
                    data = "ID";
                else if (data == "Name")
                    data = "名前欄";
                else if (data == "Mail")
                    data = "メール欄";
                else if(data == "Sentence")
                    data = "本文";
                this.comboBox1.SelectedItem = data;
            }
        }
    
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ID",
            "名前欄",
            "メール欄",
            "本文"});
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(327, 12);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "保存";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(148, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 23);
            this.textBox1.TabIndex = 2;
            // 
            // NGCollectionEditDialog
            // 
            this.ClientSize = new System.Drawing.Size(426, 50);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.comboBox1);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NGCollectionEditDialog";
            this.Text = "NGワードを編集";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public NGCollectionEditDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
