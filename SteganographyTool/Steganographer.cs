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
            Bitmap source = FileStream.LoadImage(sourceImagePath);
            byte[] data = FileStream.LoadData(dataFilePath);
            FileStream.SaveData(data, saveImagePath);
            Console.WriteLine("Succeed!");
        }

        public void DecryptSteganograph(string sourceImage, string saveData)
        {
            Bitmap source = FileStream.LoadImage(sourceImage);
        }
    }
}