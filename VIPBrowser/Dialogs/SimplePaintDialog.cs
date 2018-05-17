using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.IO;

namespace VIPBrowser.Dialogs
{
	public partial class SimplePaintDialog : Form
	{
		public SimplePaintDialog()
		{
			InitializeComponent();
			
		}

		private void SimplePaintDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			var paint = this.elementHost1.Child as YoutubePlayer.Paint;
			var canvas = paint.CanvasData;

			RenderTargetBitmap render = new RenderTargetBitmap((Int32)canvas.ActualWidth, (Int32)canvas.ActualHeight, 96, 96, PixelFormats.Default);
			render.Render(canvas);

			var enc = new PngBitmapEncoder();
			enc.Frames.Add(BitmapFrame.Create(render));
			enc.Save(this.ImageStream);
		}

		public Stream ImageStream { get; set; }
	}
}
