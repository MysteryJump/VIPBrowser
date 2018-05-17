using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowser.Dialogs.Settings
{
    public partial class IDColorSetDialog : Form
    {
        public IDColorSetDialog()
        {
            InitializeComponent();
        }
        public Color IdColor
        {
            get;
            set;
        }
        public int MinValue 
        {
            get { return (int)this.minValueNumericUpDown.Value; }
            set { this.minValueNumericUpDown.Value = (decimal)value; }
        }
        public int MaxValue 
        {
            get { return (int)this.maxValueNumericUpDown.Value; }
            set { this.maxValueNumericUpDown.Value = (decimal)value; }
        }
        private void colorSelectButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.IdColor = this.colorDialog1.Color;
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
