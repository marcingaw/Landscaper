using System;
using System.Drawing;
using System.Windows.Forms;

namespace Landscaper {

    public partial class LandscaperForm : Form {

        private Landscape Land;
        private bool Draw3DMap = false;

        public LandscaperForm() {
            InitializeComponent();
        }

        private void LandscaperForm_Load(object sender, EventArgs e) {
            Land = new Landscape(512, 512);
        }

        private void LandscaperForm_Paint(object sender, PaintEventArgs e) {
            Graphics gr = e.Graphics;
            RectangleF vcb = gr.VisibleClipBounds;
            int w = Land.Width;//(int)vcb.Width;
            int h = Land.Height;//(int)vcb.Height;
            Bitmap flatMap = new Bitmap(w, h);
            Bitmap map3D = Draw3DMap ? new Bitmap(w, h) : null;
            for (int x = 0; x < Land.Width && x < w; x++) {
                for (int y = 0; y < Land.Height && y < h; y++) {
                    int ph = Land.GetPointElev(x, y);
                    Color c = ph < SeaLevelBar.Value ?
                              Color.FromArgb(0, 0, 192) :
                              Color.FromArgb(0, ph, 0);
                    flatMap.SetPixel(x, y, c);
                    if (Draw3DMap) {
                        int ph3D = ph < SeaLevelBar.Value ?
                                   SeaLevelBar.Value - 1 :
                                   ph;
                        int y3D = (y + h) / 2;
                        int x3D = x;  // TODO: Nice 3D effect on the X axis.
                        int yPh3D = y3D - (ph3D * h) / (1024 + (h - y));
                        for (int ypx = y3D; ypx >= yPh3D; ypx--) {
                            map3D.SetPixel(x3D, ypx, c);
                        }
                    }
                }
            }
            gr.DrawImage(flatMap, ReloadBtn.Width, 0);
            if (Draw3DMap) {
                gr.DrawImage(map3D, ReloadBtn.Width + w + 10, 0);
                Draw3DMap = false;
            }
        }

        private void ReloadBtn_Click(object sender, EventArgs e) {
            Land = new Landscape(512, 512);
            Invalidate();
        }

        private void SeaLevelBar_ValueChanged(object sender, EventArgs e) {
            Invalidate();
        }

        private void Show3DBtn_Click(object sender, EventArgs e) {
            Draw3DMap = true;
            Invalidate();
        }
    }

}
