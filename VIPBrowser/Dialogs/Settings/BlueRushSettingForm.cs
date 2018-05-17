using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowser.Dialogs.Settings
{
	public partial class BlueRushSettingForm : Form
	{
		public BlueRushSettingForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var s = VIPBrowserLibrary.Other.ServerService.UserData.Signin(this.textBox1.Text, textBox2.Text);
			if (s==null)
			{
				MessageBox.Show("ユーザー名かパスワードが異なります");
				return;
			}
			VIPBrowserLibrary.Other.ServerService.ShareUserData.SettingXml sx = new VIPBrowserLibrary.Other.ServerService.ShareUserData.SettingXml(ud: s);
			sx.SetXmlFile();
			this.IsLoging = true;
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Process.Start("http://vipbrowser.s601.xrea.com/bluerush/");
		}

		public bool IsLoging { get; set; }
	}
}
