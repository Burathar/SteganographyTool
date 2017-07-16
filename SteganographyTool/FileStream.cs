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
            Byte[] bytes = new Byte[1 + utf8.GetByteCount(text) + utf8.GetPreamble().Length + Steganographer.Terminator.Length];//first byte is used for storing the dataDensity in its first 3 bits.
            Array.Copy(utf8.GetPreamble(), 0, bytes, 1, utf8.GetPreamble().Length);
            utf8.GetBytes(text, 0, text.Length, bytes, utf8.GetPreamble().Length + 1);
            TerminateData(bytes);
            return bytes;
        }

        private static byte[] TerminateData(byte[] bytes)
        {
            for (int i = 0; i < 100000; i++)//change occurences of the terminator in data.(timeout after 100000 occurrences)
            {
                int? terminatorIndex = Steganographer.FindSequence(bytes, Steganographer.Terminator);
                if (terminatorIndex != null)
                {
                    bytes[(int)terminatorIndex] = 0xba;
                }
                else break;
            }
            //Array.Resize(ref bytes, bytes.Length + Terminator.Length); //add terminator at the end of data
            int terminatorI = 0;
            for (int i = bytes.Length - Steganographer.Terminator.Length; i < bytes.Length; i++)
            {
                bytes[i] = Steganographer.Terminator[terminatorI++];
            }
            return bytes;
        }
    }
}