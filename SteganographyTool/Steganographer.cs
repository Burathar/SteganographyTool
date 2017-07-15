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
        private static byte[] Terminator = new byte[] { 0xaa, 0xaa, 0x0f, 0xaa};//make ba
        public void NewSteganograph(string sourceImagePath, string dataFilePath, string saveImagePath, bool grayValue = false)
        {
            Bitmap image = FileStream.LoadImage(sourceImagePath);
            byte[] data = FileStream.LoadData(dataFilePath);
            TerminateData(data);
            Encrypt(image, data, grayValue);// remove exception
            FileStream.SaveImage(image, saveImagePath);
            Console.WriteLine("Succeed!");
        }

        private void TerminateData(byte[] data)
        {
            for(int i = 0; i < 100000; i++)//change occurences of the terminator in data.
            {
                int? terminatorIndex = ContainsSequence(data, Terminator);
                if (terminatorIndex != null)
                {
                    data[(int)terminatorIndex] = 0xba;
                }
                else break;
            }
            Array.Resize(ref data, data.Length + Terminator.Length); //add terminator at the end of data
            for(int i = data.Length - Terminator.Length - 1; i < data.Length; i++)
            {
                data[i] = Terminator[i - data.Length];
            }
        }

        public int? ContainsSequence(byte[] toSearch, byte[] toFind)
        {
            for (int i = 0; i + toFind.Length < toSearch.Length; i++)
            {
                bool allEqual = true;
                for (int j = 0; j < toFind.Length; j++)
                {
                    if (toSearch[i + j] != toFind[j])
                    {
                        allEqual = false;
                        break;
                    }
                }
                if (allEqual) return i;
            }
            return null;
        }

        private void Encrypt(Bitmap sourceImage, byte[] data, bool grayValue)
        {
            int pixelAmount = sourceImage.Size.Height * sourceImage.Size.Width;
            if (data.Length > pixelAmount * 8) throw new ExcessiveDataException();
            if (grayValue)
            {
                if (data.Length <= pixelAmount)
                {
                    Mutate1Bit(sourceImage, data, true);
                }
                if (data.Length > pixelAmount)
                {
                    MutateMoreBits(sourceImage, data, pixelAmount, true);
                }
            }
            else
            {
                if (data.Length <= pixelAmount * 3)
                {
                    Mutate1Bit(sourceImage, data, false);
                }
                if (data.Length > pixelAmount * 3)
                {
                    MutateMoreBits(sourceImage, data, pixelAmount, false);
                }
            }
        }

        private void MutateMoreBits(Bitmap sourceImage, byte[] data, int pixelAmount, bool grayValue)
        {
            int dataDensity = (int)Math.Ceiling(((double)data.Length * 8) / pixelAmount);
            int dataCounter = 0;
            for (int y = 0; y < sourceImage.Size.Height; y++)
            {
                for (int x = 0; x < sourceImage.Size.Width; x++)
                {
                    RGB rgb = new RGB(sourceImage.GetPixel(x, y));
                    rgb.R = MutateChannel(data, rgb.R, ref dataCounter, dataDensity, !grayValue);
                    rgb.G = MutateChannel(data, rgb.G, ref dataCounter, dataDensity, !grayValue);
                    rgb.B = MutateChannel(data, rgb.B, ref dataCounter, dataDensity, true);
                    sourceImage.SetPixel(x, y, rgb.Color);
                    if (dataCounter >= data.Length * 8) return;
                }
            }
        }

        private void Mutate1Bit(Bitmap sourceImage, byte[] data, bool grayValue)
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

        private int MutateChannel(byte[] data, int channel, ref int dataCounter, int dataDensity, bool increaseCounter)
        {
            int startCounter = dataCounter;
            for(int i = 0; i < dataDensity; i++)
            {
                bool? bit = GetDataBit(data, dataCounter);
                if (bit == null) return channel;
                channel = (bool)bit ? channel | (int)Math.Pow(2, i) : channel % (int)Math.Pow(2, i);
                dataCounter++;
            }
            if (!increaseCounter) dataCounter = startCounter;
            return channel;
        }

        private int MutateChannel(byte[] data, int channel, ref int dataCounter, bool increaseCounter)
        {
            bool? bit = GetDataBit(data, dataCounter);
            if (bit == null) return channel;
            channel = (bool)bit ? channel | 0b0000_0001 : channel & 0b1111_1110;
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

        public void DecryptSteganograph(string sourceImage, string saveData, bool grayValue = false)
        {
            Bitmap source = FileStream.LoadImage(sourceImage);
            //Decrypt(source) < haal bytes uit de afbeelding tot de terminator is gevonden
        }
    }
}