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
    public partial class ThreadListNGCollectionEditDialog : Form
    {
        public ThreadListNGCollectionEditDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NGName))
            {
                MessageBox.Show("NGする文を空白にすることはできません");
                return;
            }
            this.Close();
        }

        public string NGName { get { return this.ngNameTextBox.Text; } set { this.ngNameTextBox.Text = value; } }
    }
}
