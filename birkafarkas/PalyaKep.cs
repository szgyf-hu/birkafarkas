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

        Image sheep;
        Image wolf;

        public PalyaKep() : base()
        {
            sheep = Image.FromFile(@".\Media\sheep.png");
            wolf = Image.FromFile(@".\Media\wolf.png");
            Redraw();
        }

        public void setPalya(Palya palya)
        {
            this.palya = palya;
            Redraw();
            Invalidate();
        }

        protected void Redraw()
        {
            if (buffer == null)
                return;

            if (palya == null)
                return;

            bufferg.Clear(Color.DarkGreen);

            Pen p = new Pen(Color.Blue, 5);

            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 7; x++)
                {
                    Csomopont cs = palya.Csomopontok[x, y];
                    if (cs != null)
                    {
                        int px = ox + A * x;
                        int py = oy + A * y;

                        int A2 = A;// (int)((A / 2)*0.8);

                        /*
                        if (cs.Ellista[(int)EIrany.Fel] != null)
                            bufferg.DrawLine(p, px, py, px, py - A2);
                        if (cs.Ellista[(int)EIrany.JobbraFel] != null)
                            bufferg.DrawLine(p, px, py, px + A2, py - A2);*/
                        if (cs.Ellista[(int)EIrany.Jobbra] != null)
                            bufferg.DrawLine(p, px, py, px + A2, py);
                        if (cs.Ellista[(int)EIrany.JobbraLe] != null)
                            bufferg.DrawLine(p, px, py, px + A2, py + A2);
                        if (cs.Ellista[(int)EIrany.Le] != null)
                            bufferg.DrawLine(p, px, py, px, py + A2);
                        if (cs.Ellista[(int)EIrany.BalraLe] != null)
                            bufferg.DrawLine(p, px, py, px - A2, py + A2);
                        /*if (cs.Ellista[(int)EIrany.Balra] != null)
                            bufferg.DrawLine(p, px, py, px - A2, py);
                        if (cs.Ellista[(int)EIrany.BalraFel] != null)
                            bufferg.DrawLine(p, px, py, px - A2, py - A2);*/

                        switch (cs.Tipus)
                        {
                            case ECsomopontTipus.Üres:
                                if (cs.LepesTipp)
                                    bufferg.FillEllipse(Brushes.Lime, px - 10, py - 10, 20, 20);
                                else
                                    bufferg.FillEllipse(Brushes.Red, px - 10, py - 10, 20, 20);
                                break;
                            case ECsomopontTipus.Birka:
                                bufferg.DrawImage(sheep, px - 64 / 2, py - 64 / 2, 64, 64); break;
                            case ECsomopontTipus.Farkas:
                                bufferg.DrawImage(wolf, px - 64 / 2, py - 64 / 2, 64, 64); break;
                        }
                    }
                }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (buffer != null)
                e.Graphics.DrawImage(buffer, ClientRectangle.Left, ClientRectangle.Top);

            if (selectedX >= 0 && selectedX <= 7
                &&
                selectedY >= 0 && selectedY <= 7)
                e.Graphics.DrawRectangle(Pens.Yellow,
                    ox + A * selectedX - A / 2,
                    oy + A * selectedY - A / 2,
                    A,
                    A);
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

            Redraw();

            Invalidate();
        }

        public void PixelToLogic(int px, int py, out int x, out int y)
        {
            px -= ox;
            py -= oy;

            px += A / 2;
            py += A / 2;

            x = px / A;
            y = py / A;
        }

        int selectedX, selectedY;

        public void select(int x, int y)
        {
            selectedX = x;
            selectedY = y;
            palya.TippekBeallitasa(x, y);
            Redraw();
            Invalidate();
        }
    }
}
