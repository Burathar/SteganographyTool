using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace SteganographyTool
{
    public class Steganographer
    {
        public void NewSteganograph(string sourceImagePath, string dataFilePath, string saveImagePath)
        {
            Bitmap image = FileStream.LoadImage(sourceImagePath);
            byte[] data = FileStream.LoadData(dataFilePath);
            float dataSpeadFactor = DetermineDataSpeadFactor(image.Size, data.Length);
            Encrypt(image, data, dataSpeadFactor);
            FileStream.SaveImage(image, saveImagePath);
            Console.WriteLine("Succeed!");
        }

        private Bitmap Encrypt(Bitmap sourceImage, byte[] data, float dataSpeadFactor)
        {
            int pixelAmount = sourceImage.Size.Height * sourceImage.Size.Width;
            
            /*for (int  i = 0; i < pixelAmount; i++) //floot dataspreadfactor, check if factor < 1 in de loop
            {
                int x = i %  sourceImage.Size.Width;
                int y = (int)Math.Floor((double)i / sourceImage.Size.Width);
                if(dataSpeadFactor < 1)
                {

                }
            }*/
            for(int y = 0; y < sourceImage.Size.Height; y++)
            {
                for(int x = 0; x < sourceImage.Size.Width; x++)
                {
                    int argb = sourceImage.GetPixel(x, y).ToArgb();
                    argb += 10;
                    sourceImage.SetPixel(x, y, Color.FromArgb(argb));
                }
            }
            return sourceImage;
        }

        private float DetermineDataSpeadFactor(Size size, int length)
        {
            int pixelAmount = size.Width * size.Height;
            float factor = pixelAmount / length;
            return factor;
        }

        public void DecryptSteganograph(string sourceImage, string saveData)
        {
            Bitmap source = FileStream.LoadImage(sourceImage);
        }
    }
}