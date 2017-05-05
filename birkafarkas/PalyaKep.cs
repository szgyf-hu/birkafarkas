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

        protected override void OnPaint(PaintEventArgs e)
        {
            bufferg.Clear(Color.DarkGreen);

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
