using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SteganographyTool
{
    public class Steganographer
    {
        public void NewSteganograph(string sourceImage, string data, string saveImage)
        {
            Bitmap source = FileStream.LoadImage(sourceImage);
        }

        public void DecryptSteganograph(string sourceImage, string saveData)
        {
            Bitmap source = FileStream.LoadImage(sourceImage);
        }
    }
}