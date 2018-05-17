using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Other.UploadService;

namespace VIPBrowser.ch2Browser
{
    public partial class UploadDialog : Form
    {
        public UploadDialog()
        {
            InitializeComponent();
        }
        private byte[] uploadDataByte;
        private void loadClipBoardButton_Click(object sender, EventArgs e)
        {
            IDataObject ido = Clipboard.GetDataObject();
            var format = ido.GetFormats(false)[0];
            MessageBox.Show(format);
            if (format == DataFormats.Bitmap || format == DataFormats.Dib)
            {
                this.IsImage = true;
                MemoryStream ms = new MemoryStream();
                Clipboard.GetImage().Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                this.uploadDataByte = ms.ToArray();
            }
            else
            {
                MessageBox.Show("この形式のデータは読み込みできません");
            }
        }

        private void refFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "ファイルからデータを読み込む";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                FileInfo fi = new FileInfo(fileName);
                string ex = fi.Extension;
                if (ex.Contains("bmp") || ex.Contains("png") || ex.Contains("jpg") ||ex.Contains("jpeg"))
                {
                    this.IsImage = true;
                }
                else
                {
                    this.IsImage = false;
                }
                this.uploadDataByte = File.ReadAllBytes(fileName);
            }
        }

        public bool IsImage 
        {
            get 
            {
                return this.imgurRadioButton.Enabled;
            }
            set
            {
                this.imgurRadioButton.Enabled = value;
            }
        }

        private void UploadDialog_Load(object sender, EventArgs e)
        {
            this.axfcRadioButton.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UploadManagement um = new UploadManagement();
            Uploaders u;
            u = this.imgurRadioButton.Checked ? Uploaders.Imgur : Uploaders.Axfc;
            if (u == Uploaders.Axfc)
                if (!this.axfcRadioButton.Checked)
                    return;
            var data = um.UploadData(u, this.uploadDataByte);
            um.WriteUploadLog(u, dic: data);
            Clipboard.SetText(data["Link"]);
            MessageBox.Show("完了し、ダウンロードリンクをクリップボードにコピーしました。");
        }

        private void axfcRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var ch = sender as CheckBox;
            if (ch.Checked)
            {
                this.commentTextBox.Enabled = this.fileNameTextBox.Enabled = this.deletePassTextBox.Enabled = this.downloadPassTextBox.Enabled = this.checkBox1.Enabled = true;
            }
            else
            {
                this.commentTextBox.Enabled = this.fileNameTextBox.Enabled = this.deletePassTextBox.Enabled = this.downloadPassTextBox.Enabled = this.checkBox1.Enabled = false;
            }
        }

		private void button2_Click(object sender, EventArgs e)
		{
			MemoryStream ms = new MemoryStream();
			VIPBrowser.Dialogs.SimplePaintDialog spd = new VIPBrowser.Dialogs.SimplePaintDialog();
			spd.ImageStream = ms;
			spd.ShowDialog();
			this.uploadDataByte = ms.ToArray();
		}
    }
}
