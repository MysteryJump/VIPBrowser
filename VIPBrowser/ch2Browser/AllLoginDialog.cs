using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.BBS.X2ch;

namespace VIPBrowser.ch2Browser
{
    public partial class AllLoginDialog : Form
    {
        /// <summary>
        /// プログラムの設定情報を表します
        /// </summary>
        public VIPBrowserLibrary.Setting.SettingSerial SettingData { get; set; }

        public AllLoginDialog()
        {
            InitializeComponent();
            this.Load += AllLoginDialog_Load;
        }

        void AllLoginDialog_Load(object sender, EventArgs e)
        {
            this.isBeLoginLabel.Text = this.SettingData.IsBeLogin.ToString();
            this.isRokkaLoginLabel.Text = this.SettingData.IsRoninLogined.ToString();
            this.isP2LoginLabel.Text = this.SettingData.IsP2Login.ToString();
            if (this.SettingData.IsBeLogin)
            {
                this.beLoginoroutButton.Text = "ログアウト";
            } 
            if (this.SettingData.IsRoninLogined)
            {
                this.rokkaLoginoroutButton.Text = "ログアウト";
            } 
            if (this.SettingData.IsP2Login)
            {
                this.p2LoginoroutButton.Text = "ログアウト";
            }
        }

        private async void beLoginoroutButton_Click(object sender, EventArgs e)
        {
            BeLogin bl = new BeLogin();
            if (!Boolean.Parse(this.isBeLoginLabel.Text))
            {
                if (String.IsNullOrWhiteSpace(this.user.Text) || String.IsNullOrWhiteSpace(this.password.Text))
                    return;
                bool b = await bl.Login(this.user.Text, this.password.Text);
                if (b)
                {
                    MessageBox.Show("ログインに成功しました");
                    this.isBeLoginLabel.Text = "True";
                    this.SettingData.IsBeLogin = true;
                    this.beLoginoroutButton.Text = "ログアウト";
                }
                else
                {
                    MessageBox.Show("ログインに失敗しました");
                    this.beLoginoroutButton.Text = "ログイン";
                    this.SettingData.IsBeLogin = false;
                }
            }
            else
            {
                bl.Logout();
                this.isBeLoginLabel.Text = "False";
                MessageBox.Show("ログアウトに成功しました");
                this.SettingData.IsBeLogin = false;
                this.beLoginoroutButton.Text = "ログイン";
            }
        }

        private void rokkaLoginoroutButton_Click(object sender, EventArgs e)
        {
			if (!Boolean.Parse(this.isRokkaLoginLabel.Text))
			{
				if (String.IsNullOrWhiteSpace(this.user.Text) || String.IsNullOrWhiteSpace(this.password.Text))
					return;
				bool b = Ronin.Login(this.user.Text, this.password.Text);
				if (b)
				{
					MessageBox.Show("ログインに成功しました");
					this.isRokkaLoginLabel.Text = "True";
					this.SettingData.IsRoninLogined = true;
					this.rokkaLoginoroutButton.Text = "ログアウト";
				}
				else
				{
					MessageBox.Show("ログインに失敗しました");
					this.rokkaLoginoroutButton.Text = "ログイン";
					this.SettingData.IsRoninLogined = false;
				}
			}
			else
			{
				Ronin.Logout();
				this.isRokkaLoginLabel.Text = "False";
				MessageBox.Show("ログアウトに成功しました");
				this.SettingData.IsRoninLogined = false;
				this.rokkaLoginoroutButton.Text = "ログイン";
			}
        }

        private void p2LoginoroutButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
