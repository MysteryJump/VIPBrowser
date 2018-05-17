using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Setting;

namespace VIPBrowser.ch2Browser.Dialogs
{
    public partial class Setting : Form
    {
        public SettingSerial SettingData { get; set; }
        private Serializer sz = new Serializer();


        public Setting()
        {
            InitializeComponent();
        }

        private void SettingSaveButton_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSetting();
            return;
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }


        private void SaveAndRestartbutton_Clicked(object sender, EventArgs e)
        {
            SaveSetting();
            Application.Restart();
        }
        /// <summary>
        /// 現在の設定を読み込みます
        /// </summary>
        private void LoadSetting()
        {
            this.isUseVisualStyle.Checked = this.SettingData.IsUseVisualStyle;
            this.defaultBoardListNameTextBox.Text = this.SettingData.DefaultBBSMenuAddress;
            this.isSaveFormLocation.Checked = this.SettingData.IsSaveFormLocation;
            this.isFormCloseWarning.Checked = this.SettingData.IsFormClosingWarning;
            this.defaultSearchEngineComboBox.SelectedItem = Enum.GetName(typeof(DefaultSearchEngine), this.SettingData.DefalutSearcher);
            this.isSaveWriteRecord.Checked = this.SettingData.IsSaveWriteRecord;
            this.isTimerGC.Checked = this.SettingData.IsPeriodicallyGC;
            this.isSaveThreadListTabCheckBox.Checked = this.SettingData.IsSaveThreadListView;
            this.isSaveThreadTabCheckBox.Checked = this.SettingData.IsSaveThreadView;
			this.defaultAddressbarTextBox.Text = this.SettingData.DefaultAddressBarText;
			this.isShowStartPageCheckBox.Checked = this.SettingData.IsShowStartPage;
			this.isMultiThreadingCheckBox.Checked = this.SettingData.IsMultiThreading;

            this.LoadSkinSetting();
        }

        private void LoadSkinSetting()
        {
            var skiDatas = VIPBrowserLibrary.Utility.SkinUtility.ReadSkinData();

            skinListBox.Items.Add("Default");
            int i = 0;
            int index = 0;

            string path = this.SettingData.UsingSkinPath;
            foreach (var item in skiDatas)
            {
                if (item.SkinPath == path)
                    index = i + 1;
                skinListBox.Items.Add(item.SkinName);
                i++;
            }
            this.skinListBox.SelectedIndex = index;

            skinListBox.Tag = skiDatas;
        }
        /// <summary>
        /// 現在の設定を保存します
        /// </summary>
        private void SaveSetting()
        {
            DefaultSearchEngine dse;
            this.SettingData.IsSaveWriteRecord = this.isSaveWriteRecord.Checked;
            this.SettingData.IsUseVisualStyle = this.isUseVisualStyle.Checked;
            this.SettingData.DefaultBBSMenuAddress = this.defaultBoardListNameTextBox.Text;
            this.SettingData.IsSaveFormLocation = this.isSaveFormLocation.Checked;
            this.SettingData.IsFormClosingWarning = this.isFormCloseWarning.Checked;
            this.SettingData.IsPeriodicallyGC = this.isTimerGC.Checked;
            this.SettingData.IsSaveThreadView = this.isSaveThreadTabCheckBox.Checked;
            this.SettingData.IsSaveThreadListView = this.isSaveThreadListTabCheckBox.Checked;
			this.SettingData.DefaultAddressBarText = this.defaultAddressbarTextBox.Text;
			this.SettingData.IsShowStartPage = this.isShowStartPageCheckBox.Checked;
			this.SettingData.IsMultiThreading = this.isMultiThreadingCheckBox.Checked;

            if (Enum.TryParse<DefaultSearchEngine>((string)this.defaultSearchEngineComboBox.SelectedItem, out dse))
                this.SettingData.DefalutSearcher = dse;
            else
                this.SettingData.DefalutSearcher = DefaultSearchEngine.Google;

            this.SaveSkinSetting();
            this.sz.Serialize(SettingData);
            global::VIPBrowser.Properties.Settings.Default.Save();
        }

        private void SaveSkinSetting()
        {
            int index = this.skinListBox.SelectedIndex;
            VIPBrowserLibrary.Chron.ThreadOrResData.SkinData[] sd = skinListBox.Tag as VIPBrowserLibrary.Chron.ThreadOrResData.SkinData[];

            if (index == 0)
            {
                this.SettingData.IsUsingSkin = false;
                this.SettingData.UsingSkinPath = String.Empty;
            }
            else
            {
                var s = sd[index - 1];
                this.SettingData.IsUsingSkin = true;
                this.SettingData.UsingSkinPath = s.SkinPath;
            }
        }

        private void skinListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = this.skinListBox.SelectedIndex;
            if (this.skinListBox.Tag == null || idx <= 0)
                return;

            VIPBrowserLibrary.Chron.ThreadOrResData.SkinData[] sd = this.skinListBox.Tag as VIPBrowserLibrary.Chron.ThreadOrResData.SkinData[];
            var data = sd[idx - 1];
            if (data.IsHaveImagePicture)
                this.skinImagePicutu.LoadAsync(data.ImagePath);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            fontDialog1.Font = this.Font;
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }

        private void settingIdColoringButton_Click(object sender, EventArgs e)
        {
            //やだ・・・global何か使っちゃって///
            global::VIPBrowser.Dialogs.Settings.IDColoringSettingDialog sy = new global::VIPBrowser.Dialogs.Settings.IDColoringSettingDialog();
            sy.ShowDialog();
        }

		private void button1_Click_2(object sender, EventArgs e)
		{
			VIPBrowser.Dialogs.Settings.BlueRushSettingForm brf = new VIPBrowser.Dialogs.Settings.BlueRushSettingForm();
			brf.ShowDialog();
		}
    }
}
