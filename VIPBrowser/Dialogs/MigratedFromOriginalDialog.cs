using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Setting.ConvertOtherBrowserData;

namespace VIPBrowser.Dialogs
{
    public partial class MigratedFromOriginalDialog : Form
    {
        private string folderPath;

        public MigratedFromOriginalDialog()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
			IOtherDataConverter iodc;
			ConvertFrom cf;
			if (this.v2cRadioBox.Checked)
			{
				cf = ConvertFrom.V2C;
			}
			else if (this.janeStyleRadioBox.Checked)
			{
				cf = ConvertFrom.JaneStyle;
			}
			else
			{
				throw new NotImplementedException();
			}
			
			var chc = new CheckCanConvert(cf);
			if (!chc.TryConvertTest(this.textBox1.Text))
			{
				MessageBox.Show("このフォルダーからの移行はできません");
				return;
			}
			var cht = chc.CanConvertType;
			switch (cf)
			{
				case ConvertFrom.JaneStyle:
					iodc = new JaneConverter(this.textBox1.Text);
					if (cht != ConvertFrom.JaneStyle)
					{
						if (MessageBox.Show("このフォルダーからはJaneStyleのデータが発見されましたが続行しますか","続行", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) 
						{
							return;
						}
					}
					break;
				case ConvertFrom.V2C:
					iodc = new V2CConverter(this.textBox1.Text);
					if (cht != ConvertFrom.JaneStyle)
					{
						if (MessageBox.Show("このフォルダーからはV2Cのデータが発見されましたが続行しますか","続行", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) 
						{
							return;
						}
					}
					break;
				default:
					throw new ApplicationException();
			}
			if (this.cookieCheckBox.Checked)
			{
				iodc.ConvertCookie();
			}
			if (this.pastLogCheckBox.Checked)
			{
				iodc.ConvertLog();
			} 
			if (this.writeRecodeCheckBox.Checked)
			{
				var cb = (ConverterBase)iodc;
				cb.ConvertKakikomi();
			}

		}

        private void button3_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            this.folderBrowserDialog1.Description = "フォルダーを選択する";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.folderPath = this.folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
