using System.Drawing;

namespace SteganographyTool
{
    public class Rgb
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public Color Color
        {
            get => Color.FromArgb(R, G, B);
            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        public Rgb(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Rgb(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }
    }
}