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
	public partial class FirstStartUpDialog : Form
	{
		public FirstStartUpDialog()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Settings.BlueRushSettingForm brsf = new Settings.BlueRushSettingForm();
			brsf.ShowDialog();
			if (brsf.IsLoging)
			{
				this.Close();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MigratedFromOriginalDialog mfod = new MigratedFromOriginalDialog();
			mfod.ShowDialog();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
