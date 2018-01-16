using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

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

        public static void SaveData(byte[] bytes, string savePath, EncodingType encoding)
        {
            string extension = Path.GetExtension(savePath);
            switch (extension)
            {
                case ".txt":
                    UTF8Encoding utf8 = new UTF8Encoding(true, true);//was 8
                    string text = utf8.GetString(bytes, 0, bytes.Length);
                    switch (encoding)
                    {
                        case EncodingType.Utf8:
                            File.WriteAllText(savePath, text, Encoding.UTF8);
                            break;

                        case EncodingType.Ascii:
                            File.WriteAllText(savePath, text, Encoding.ASCII);
                            break;

                        default:
                            File.WriteAllText(savePath, text);
                            break;
                    }
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

        public static byte[] LoadData(string dataFilePath, EncodingType encoding)
        {
            Byte[] bytes = null;
            string extension = Path.GetExtension(dataFilePath);
            switch (extension)
            {
                case ".txt":
                    string text;
                    switch (encoding)
                    {
                        case EncodingType.Utf8:
                            text = File.ReadAllText(dataFilePath, Encoding.UTF8);
                            break;

                        case EncodingType.Ascii:
                            text = File.ReadAllText(dataFilePath, Encoding.ASCII);
                            break;

                        default:
                            text = File.ReadAllText(dataFilePath);
                            break;
                    }
                    UTF8Encoding utf8 = new UTF8Encoding(true, true);
                    bytes = new Byte[1 + utf8.GetByteCount(text) + utf8.GetPreamble().Length + Steganographer.Terminator.Length];//first byte is used for storing the dataDensity in its first 3 bits.
                    Array.Copy(utf8.GetPreamble(), 0, bytes, 1, utf8.GetPreamble().Length);
                    utf8.GetBytes(text, 0, text.Length, bytes, utf8.GetPreamble().Length + 1);
                    break;

                case ".png":
                    break;
            }

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