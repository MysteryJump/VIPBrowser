using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowser.Dialogs
{
    public partial class AddNewBoardDialog : Form
    {
        public AddNewBoardDialog()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
            await VIPBrowserLibrary.Utility.TextUtility.WriteAsync(gs.CurrentDirectory + "\\userboard.bor", textBox2.Text + "\t" + textBox1.Text + "\n", true);
            this.Close();
        }
    }
}
