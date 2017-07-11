using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteganographyTool
{
    public static class FileStream
    {
        public static Bitmap LoadImage(string sourceImagePath)
        {
            Image imgInput = Image.FromFile(sourceImagePath);
            ImageFormat thisFormat = imgInput.RawFormat;
            return new Bitmap(imgInput);
        }

        public static void SaveData(byte[] bytes, string savePath)
        {
            string extension = Path.GetExtension(savePath);
            switch (extension)
            {
                case ".txt":
                    UTF8Encoding utf8 = new UTF8Encoding(true, true);
                    string text = utf8.GetString(bytes, 0, bytes.Length);
                    File.WriteAllText(savePath, text);
                    break;
                case ".png":
                    break;
                case ".jpg":
                    break;
                default:
                    throw new ArgumentException("Unsupported extention", savePath);
            }
        }

        public static void SaveImage(Bitmap image, string saveImagePath)
        {
            image.Save(saveImagePath);
        }

        public static byte[] LoadData(string dataFilePath)
        {
            string text = File.ReadAllText(dataFilePath);

            UTF8Encoding utf8 = new UTF8Encoding(true, true);

            // We need to dimension the array, since we'll populate it with 2 method calls.
            Byte[] bytes = new Byte[utf8.GetByteCount(text) + utf8.GetPreamble().Length];
            // Encode the string.
            Array.Copy(utf8.GetPreamble(), bytes, utf8.GetPreamble().Length);
            utf8.GetBytes(text, 0, text.Length, bytes, utf8.GetPreamble().Length);
            return bytes;

            //System.Text.Encoder utf8Encoder = Encoding.UTF8.GetEncoder();

            //int byteCount = utf8Encoder.GetByteCount(chars, 0, chars.Length, true);
            //byte[] bytes = new Byte[byteCount];
            //int bytesEncodedCount = utf8Encoder.GetBytes(chars, 0, chars.Length, bytes, 0, true);
            //return new BitArray(bytes);
        }

        public static byte[] ToByteArray(this BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }
    }
}