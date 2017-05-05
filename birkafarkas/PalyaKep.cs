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

        int A;

        Bitmap buffer;
        Graphics bufferg;

        protected override void OnPaint(PaintEventArgs e)
        {
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            A = (int)(Math.Min(ClientSize.Width / 6.0, ClientSize.Height / 6.0) * 0.8);

        }
    }
}
