using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Landscaper {
    public partial class LandscaperForm : Form {

        private Landscape Land;

        public LandscaperForm() {
            InitializeComponent();
        }

        private void LandscaperForm_Load(object sender, EventArgs e) {
            Land = new Landscape(512, 384, 4, 255);
        }

        private void LandscaperForm_Paint(object sender, PaintEventArgs e) {
            Graphics gr = e.Graphics;
            RectangleF vcb = gr.VisibleClipBounds;
            int w = (int)vcb.Width, h = (int)vcb.Height;
            Bitmap bm = new Bitmap(w, h);
            for (int x = 0; x < Land.Width && x < w; x++) {
                for (int y = 0; y < Land.Height && y < h; y++) {
                    int ph = Land.GetPointElev(x, y);
                    if (ph < 64) {
                        bm.SetPixel(x, y, Color.FromArgb(0, 0, 64 + 2 * ph));
                    } else if (ph < 192) {
                        bm.SetPixel(x, y, Color.FromArgb(0, ph, 0));
                    } else {
                        bm.SetPixel(x, y, Color.FromArgb(4 * (ph - 192), ph, 4 * (ph - 192)));
                    }
                }
            }
            gr.DrawImage(bm, 0, 0);
        }

        private void ReloadBtn_Click(object sender, EventArgs e) {
            Land = new Landscape(512, 384, 4, 255);
            Invalidate();
        }
    }
}
