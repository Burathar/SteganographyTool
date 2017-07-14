using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteganographyTool
{
    public class RGB
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public Color Color
        {
            get
            {
                return Color.FromArgb(R, G, B);
            }
            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        public RGB(int r, int g, int b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public RGB(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }
    }
}
