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
        public void NewSteganograph(string sourceImagePath, string dataFilePath, string saveImagePath, bool grayValue)
        {
            Bitmap image = FileStream.LoadImage(sourceImagePath);
            byte[] data = FileStream.LoadData(dataFilePath);
            Encrypt(image, data, grayValue);// catch excessivedataexception
            FileStream.SaveImage(image, saveImagePath);
            Console.WriteLine("Succeed!");
        }

        private void Encrypt(Bitmap sourceImage, byte[] data, bool grayValue)
        {
            int pixelAmount = sourceImage.Size.Height * sourceImage.Size.Width;
            if (data.Length > pixelAmount * 8) throw new ExcessiveDataException();
            if (grayValue)
            {
                if (data.Length <= pixelAmount)
                {
                    Mutate1BitRGB(sourceImage, data, true);
                }
                if (data.Length > pixelAmount)
                {

                }
            }
            else
            {
                if (data.Length <= pixelAmount * 3)
                {
                    Mutate1BitRGB(sourceImage, data, false);
                }
                if (data.Length > pixelAmount * 3)
                {

                }
            }
        }

        private void Mutate1BitRGB(Bitmap sourceImage, byte[] data, bool grayValue)
        {
            int dataCounter = 0;
            for (int y = 0; y < sourceImage.Size.Height; y++)
            {
                for (int x = 0; x < sourceImage.Size.Width; x++)
                {
                    RGB rgb = new RGB(sourceImage.GetPixel(x, y));
                    rgb.R = MutateChannel(data, rgb.R, ref dataCounter, !grayValue);
                    rgb.G = MutateChannel(data, rgb.G, ref dataCounter, !grayValue);
                    rgb.B = MutateChannel(data, rgb.B, ref dataCounter, true);
                    sourceImage.SetPixel(x, y, rgb.Color);
                    if (dataCounter >= data.Length * 8) return;
                }
            }
        }

        private int MutateChannel(byte[] data, int channel, ref int dataCounter, bool increaseCounter)
        {
            bool? bit = GetDataBit(data, dataCounter);
            if (bit == null) return channel;
            channel = (bool)bit ? channel | 0b00000001 : channel & 0b11111110;
            if(increaseCounter) dataCounter++;
            return channel;
        }

        private bool? GetDataBit(byte[] data, int dataCounter)
        {
            if (dataCounter >= data.Length * 8) return null;
            int byteIndex = (int)Math.Floor(dataCounter / 8d);
            byte b = data[byteIndex];
            int bitIndex = dataCounter % 8;
            return GetBit(b, bitIndex);
        }

        private bool GetBit(byte b, int bitIndex)
        {//Source: https://stackoverflow.com/questions/4854207/get-a-specific-bit-from-byte
            return (b & (1 << bitIndex - 1)) != 0;
        }

        public void DecryptSteganograph(string sourceImage, string saveData)
        {
            Bitmap source = FileStream.LoadImage(sourceImage);
        }
    }
}