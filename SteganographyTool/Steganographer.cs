using System;
using System.Drawing;

namespace SteganographyTool
{
    public class Steganographer
    {
        public static byte[] Terminator = new byte[] { 0xaa, 0xaa, 0x0f, 0xab };

        public void NewSteganograph(string sourceImagePath, string dataFilePath, string saveImagePath, bool grayScale = false, bool fillRandom = true, EncodingType encoding = EncodingType.UTF8)
        {
            Bitmap sourceImage = FileStream.LoadImage(sourceImagePath);
            Console.WriteLine("Loaded source image");
            byte[] data = FileStream.LoadData(dataFilePath, encoding);
            Console.WriteLine("Loaded data file");
            if (!CheckDataLength(data, sourceImage, grayScale)) return;
            MutateBits(sourceImage, data, sourceImage.Height * sourceImage.Width, grayScale, fillRandom);
            Console.WriteLine("Encrypted data into the image");
            FileStream.SaveImage(sourceImage, saveImagePath);
            Console.WriteLine($"Mutated image saved to {saveImagePath}");
        }

        private bool CheckDataLength(byte[] data, Bitmap sourceImage, bool grayScale)
        {
            if ((((data.Length - 1) * 8) + 3 > sourceImage.Width * sourceImage.Height * 3 * 8 && !grayScale) || (((data.Length - 1) * 8) + 3 > sourceImage.Width * sourceImage.Height * 8 && grayScale))
            {
                Console.WriteLine($"The image is not big enough to contain the encryption data. The image would have to be {(grayScale ? (((data.Length - 1) * 8) + 3 / (double)sourceImage.Width * sourceImage.Height * 3 * 8) : ((data.Length - 1) * 8) + 3 / (double)sourceImage.Width * sourceImage.Height * 8)} times bigger");
                if (((data.Length - 1) * 8) + 3 < sourceImage.Width * sourceImage.Height * 3 * 8) Console.WriteLine("The image would be big enough if not passed as grayscale image");
                return false;
            }
            return true;
        }

        private void MutateBits(Bitmap sourceImage, byte[] data, int pixelAmount, bool grayScale, bool fillRandom)
        {
            int dataDensity = (int)Math.Ceiling(((double)data.Length * 8) / (grayScale ? pixelAmount : pixelAmount * 3));
            Console.WriteLine($"{dataDensity} bits were used per channel to store the data");
            if (dataDensity > 4) Console.WriteLine("Warning: when using more than 4 bits per channel, the output image might suffer from major artifacting. Use a smaller data:image ratio to resolve this problem");
            data[0] = (byte)((dataDensity - 1) << 5); //only first 3 bits are read;
            int dataCounter = 0;
            MutateDataDensityBits(data, sourceImage, ref dataCounter, grayScale);
            for (int y = 0; y < sourceImage.Size.Height; y++)
            {
                for (int x = (y == 0) ? grayScale ? 4 : 1 : 0; x < sourceImage.Size.Width; x++)
                {
                    RGB rgb = new RGB(sourceImage.GetPixel(x, y));
                    rgb.R = MutateChannel(data, rgb.R, ref dataCounter, dataDensity, !grayScale);
                    rgb.G = MutateChannel(data, rgb.G, ref dataCounter, dataDensity, !grayScale);
                    rgb.B = MutateChannel(data, rgb.B, ref dataCounter, dataDensity, true);
                    sourceImage.SetPixel(x, y, rgb.Color);
                    if (dataCounter >= data.Length * 8)
                    {
                        if (fillRandom)
                        {
                            FillRandom(sourceImage, dataDensity, grayScale, x + 1, y);
                            return;
                        }
                        else
                            return;
                    }
                }
            }
        }

        private void FillRandom(Bitmap sourceImage, int dataDensity, bool grayScale, int startX, int startY)
        {
            Random r = new Random();
            byte[] data = new byte[3];
            for (int y = startY; y < sourceImage.Size.Height; y++)
            {
                for (int x = (y == startY) ? startX : 0; x < sourceImage.Size.Width; x++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        data[i] = (byte)r.Next(255);
                    }
                    int dataCounter = 0;
                    RGB rgb = new RGB(sourceImage.GetPixel(x, y));
                    rgb.R = MutateChannel(data, rgb.R, ref dataCounter, dataDensity, !grayScale);
                    rgb.G = MutateChannel(data, rgb.G, ref dataCounter, dataDensity, !grayScale);
                    rgb.B = MutateChannel(data, rgb.B, ref dataCounter, dataDensity, true);
                    sourceImage.SetPixel(x, y, rgb.Color);
                }
            }
        }

        private void MutateDataDensityBits(byte[] data, Bitmap sourceImage, ref int dataCounter, bool grayScale)
        {
            if (grayScale)
            {
                for (int i = 0; i <= 3; i++)
                {
                    RGB rgb = new RGB(sourceImage.GetPixel(i, 0));
                    rgb.R = MutateChannel(data, rgb.R, ref dataCounter, 1, false);
                    rgb.G = MutateChannel(data, rgb.G, ref dataCounter, 1, false);
                    rgb.B = MutateChannel(data, rgb.B, ref dataCounter, 1, true);
                    sourceImage.SetPixel(i, 0, rgb.Color);
                }
            }
            else
            {
                RGB rgb = new RGB(sourceImage.GetPixel(0, 0));
                rgb.R = MutateChannel(data, rgb.R, ref dataCounter, 1, true);
                rgb.G = MutateChannel(data, rgb.G, ref dataCounter, 1, true);
                rgb.B = MutateChannel(data, rgb.B, ref dataCounter, 1, true);
                sourceImage.SetPixel(0, 0, rgb.Color);
            }
            dataCounter = 8;
        }

        private int MutateChannel(byte[] data, int channel, ref int dataCounter, int dataDensity, bool increaseDataCounter)
        {
            int startCounter = dataCounter;
            for (int i = 0; i < dataDensity; i++)
            {
                bool? bit = GetDataBit(data, dataCounter);
                if (bit == null) return channel;
                channel = (bool)bit ? channel | (int)Math.Pow(2, i) : channel & 255 - (int)Math.Pow(2, i);
                dataCounter++;
            }
            if (!increaseDataCounter) dataCounter = startCounter;
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
        {
            return (b & (1 << 7 - bitIndex)) != 0;
        }

        public void DecryptSteganograph(string sourceImage, string saveData, bool grayScale = false, EncodingType encoding = EncodingType.UTF8)
        {
            Bitmap source = FileStream.LoadImage(sourceImage);
            Console.WriteLine("Loaded source image");
            byte[] bytes;
            bytes = ReadImage(source, grayScale);
            Console.WriteLine("Extracted data from image");
            //bytes = FileStream.LoadDataRaw(sourceImage);
            byte[] data = RemoveTerminator(bytes);
            if (data == null) { Console.WriteLine("No data terminator was found, cannot extract data"); return; }//Overwrite this?
            FileStream.SaveData(data, saveData, encoding);
            Console.WriteLine($"Extracted data saved to {saveData}");
        }

        private byte[] RemoveTerminator(byte[] bytes)
        {
            int? terminatorIndex = FindSequence(bytes, Terminator);
            if (terminatorIndex == null) return null;
            byte[] data = new byte[(int)terminatorIndex];
            Array.Copy(bytes, data, (int)terminatorIndex);
            return data;
        }

        private byte[] ReadImage(Bitmap sourceImage, bool grayScale)
        {
            int dataDensity = GetDataDensity(sourceImage, grayScale);
            byte[] data = new byte[sourceImage.Width * sourceImage.Height * dataDensity];
            int dataCounter = 0;
            for (int y = 0; y < sourceImage.Size.Height; y++)
            {
                for (int x = (y == 0) ? grayScale ? 4 : 1 : 0; x < sourceImage.Size.Width; x++)
                {
                    Color color = sourceImage.GetPixel(x, y);
                    if (ReadChannel(color.R, data, dataDensity, ref dataCounter, !grayScale)) return data;
                    if (ReadChannel(color.G, data, dataDensity, ref dataCounter, !grayScale)) return data;
                    if (ReadChannel(color.B, data, dataDensity, ref dataCounter, true)) return data;
                }
            }
            return data;
        }

        private bool CheckForTerminator(byte[] data, int dataCounter)
        {
            if (dataCounter < Terminator.Length * 8) return false;
            byte[] newBytes = new byte[Terminator.Length];
            Array.Copy(data, (int)Math.Floor((double)dataCounter / 8), newBytes, 0, Terminator.Length);
            if (newBytes.Equals(Terminator))
                return true;
            return false;
        }

        private bool ReadChannel(byte channel, byte[] data, int dataDensity, ref int dataCounter, bool increaseDataCounter)
        {
            int startCounter = dataCounter;
            for (int i = 0; i < dataDensity; i++)
            {
                bool bit = (channel >> i & 1) == 1;
                int byteIndex = (int)Math.Floor(dataCounter / 8d);
                int bitIndex = dataCounter % 8;
                data[byteIndex] = (byte)(bit ? data[byteIndex] | (1 << 7 - bitIndex) : data[byteIndex] & ~(1 << 7 - bitIndex));//if the bit is false, nothing has to happen, becouse the byte is 00 by defoult
                if (bitIndex == 0)
                    if (CheckForTerminator(data, dataCounter))
                        return true;
                dataCounter++;
            }
            if (!increaseDataCounter) dataCounter = startCounter;
            return false;
        }

        private int GetDataDensity(Bitmap sourceImage, bool grayScale)
        {
            int dataDensity = 0;
            if (grayScale)
            {
                for (int i = 0; i <= 3; i++)
                {
                    Color c = sourceImage.GetPixel(i, 0);
                    dataDensity = dataDensity | (c.R & 1) << i | (c.G & 1) << i | (c.B & 1) << i;
                }
            }
            else
            {
                Color c = sourceImage.GetPixel(0, 0);
                dataDensity = dataDensity | (c.R & 1) << 2 | (c.G & 1) << 1 | (c.B & 1);
            }
            return dataDensity + 1;
        }

        public static int? FindSequence(byte[] toSearch, byte[] toFind)
        {
            for (int i = 0; i + toFind.Length <= toSearch.Length; i++)
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
    }
}