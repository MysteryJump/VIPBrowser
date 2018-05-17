using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowser.ch2Browser
{
    public partial class ThreadListStyleColorChangeForm : Form
    {

        public Color SelectColor 
        {
            set { this.colorSelectButton.BackColor = value; }
            get { return this.colorSelectButton.BackColor; }
        }
        public string Conditions
        {
            set { this.conditionsTextBox.Text = value; }
            get { return this.conditionsTextBox.Text; }
        }

        public VIPBrowserLibrary.Chron.ThreadOrResData.ChangeColorTypeConditions NowType 
        {
            set 
            {
                string se = value.ToString();
                if (se == "Speed")
                    se = "勢い";
                else if (se == "ResCount")
                    se = "レス数";
                else
                    se = "スレタイ";
                this.typeSelectComboBox.SelectedItem = se;
            }
            get 
            {
                string se = this.typeSelectComboBox.SelectedItem.ToString();
                if (se == "勢い")
                    se = "Speed";
                else if (se == "レス数")
                    se = "ResCount";
                else
                    se = "ThreadName";
                return (VIPBrowserLibrary.Chron.ThreadOrResData.ChangeColorTypeConditions)
                    Enum.Parse(typeof(VIPBrowserLibrary.Chron.ThreadOrResData.ChangeColorTypeConditions),
                    se);
            }
        }


        
        public ThreadListStyleColorChangeForm()
        {
            InitializeComponent();
        }

        private void conditionstextBox_TextChanged(object sender, EventArgs e)
        {


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                this.conditionsTextBox.Enabled = true;
                this.typeSelectComboBox.Enabled = true;
                this.colorSelectButton.Enabled = true;
            }
            else
            {
                this.colorSelectButton.Enabled = false;
                this.typeSelectComboBox.Enabled = false;
                this.colorSelectButton.Enabled = false;
            }
        }

        private void colorSelectButton_Click(object sender, EventArgs e)
        {
            while (true)
            {
                this.colorDialog1.AnyColor = true;
                this.colorDialog1.SolidColorOnly = true;
                if (DialogResult.OK == this.colorDialog1.ShowDialog())
                {
                    Color c = this.colorDialog1.Color;
                    if (!c.IsKnownColor)
                    {
                        MessageBox.Show("この色は使用することができません");
                    }
                    else
                    {
                        this.colorSelectButton.BackColor = c;
                        break;
                    }
                }
                else
                    break;
            }
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
