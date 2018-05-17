using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms.Design.Behavior;
using System.Windows.Forms.Design;
using System.Windows.Forms.ComponentModel;
using System.Data;
using System.Net;
using System.Windows.Markup;
using System.Windows.Input;
using System.Windows.Forms.Layout;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Configuration;

namespace VIPBrowser.ch2Browser
{
    public class OriginalComponentThreadViewer : RichTextBox
    {
        public OriginalComponentThreadViewer(string name)
        {
            this.Name = name;

        }
        public OriginalComponentThreadViewer()
        {
            
        }
        private new ControlCollection Controls { get; set; }
    }
}
