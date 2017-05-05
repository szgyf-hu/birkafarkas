using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace birkafarkas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void palyaKep1_MouseMove(object sender, MouseEventArgs e)
        {
            int x, y;
            palyaKep1.PixelToLogic(e.X, e.Y, out x, out y);
            Text = String.Format("px:{0} py:{1} x:{2} y:{3}", e.X, e.Y, x, y);
            palyaKep1.select(x, y);
        }
    }
}
