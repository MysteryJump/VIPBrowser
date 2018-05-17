using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Chron.ThreadOrResData;

namespace SkinCreateAndParser
{
    public partial class BaseCreateSkin : UserControl
    {
        public BaseCreateSkin()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
            this.IsRealTimePreview = true;
        }
        public EditType Edit { get; set; }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Edit == EditType.Res)
            {
                Res r = new Res(1, "名無し", "sage", "こんにち", "ID:nsrvetng", DateTime.Now.ToString(), "", true);
                StringBuilder sb = new StringBuilder();
                string id = r.ID;
                sb.Append(this.textBox3.Text);
                string idx = r.Index.ToString();
                sb.Replace("<PLAINNUMBER/>", @"<a href=""menu:" + idx + @""" name=""" + idx + @""" target=""_blank"">" + idx + "</a>");
                sb.Replace("<NUMBER/>", @"<a href=""menu:" + idx + @""" name=""" + idx + @""" target=""_blank"">" + idx + "</a>");
                sb.Replace("<MAILNAME/>", r.Mail);
                sb.Replace("<SKINPATH/>", "SkinPath");
                sb.Replace("<ID/>", r.ID);
                sb.Replace("<BE/>", r.BE);
                sb.Replace("<NAME/>", r.Name);
                sb.Replace("<MAIL/>", r.Mail);
                sb.Replace("<DATE/>", r.Date);
                sb.Replace("<DATEONLY/>", r.Date);
                sb.Replace("<MESSAGE/>", r.Sentence);
                if (this.IsRealTimePreview)
                    webBrowser1.DocumentText = sb.ToString();
            }
            else if (Edit == EditType.Header)
            {
                string data = this.textBox3.Text;
                data.Replace("<THREADNAME/>", "きゃわわ");
                data.Replace("<SKINPATH/>", "");
                if (this.IsRealTimePreview)
                    webBrowser1.DocumentText = data;
            }
            else
            {
                if (this.IsRealTimePreview)
                    webBrowser1.DocumentText = this.textBox3.Text;
            }
        }

        public string NowString
        {
            get { return this.textBox3.Text; }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            this.IsRealTimePreview = c.Checked;
        }


        public bool IsRealTimePreview { get; set; }
    }
    public enum EditType
    {
        Res,
        Header,
        Footer
    }
}
