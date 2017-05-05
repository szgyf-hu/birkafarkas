using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace birkafarkas
{
    class PalyaKep : Control
    {

        int A, ox, oy;

        Bitmap buffer;
        Graphics bufferg;

        Palya palya = new Palya();

        public void setPalya(Palya palya)
        {
            this.palya = palya;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            bufferg.Clear(Color.DarkGreen);

            if (palya != null)
            {
                for (int y = 0; y < 7; y++)
                    for (int x = 0; x < 7; x++)
                        if (palya.Csomopontok[x, y] != null)
                        {
                            int px = ox + A * x;
                            int py = oy + A * y;
                            bufferg.FillEllipse(Brushes.Red, px-10, py-10, 20, 20);
                        }
            }
            e.Graphics.DrawImage(buffer, ClientRectangle.Left, ClientRectangle.Top);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            A = (int)(Math.Min(ClientSize.Width / 6.0, ClientSize.Height / 6.0) * 0.8);
            ox = (ClientSize.Width - A * 6) / 2;
            oy = (ClientSize.Height - A * 6) / 2;

            buffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            bufferg = Graphics.FromImage(buffer);

            Invalidate();
        }
    }
}
