using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SteganographyTool
{
    internal class Program
    {
        private static Steganographer steganographer;

        private static void Main(string[] args)
        {
            string[] testArgs = { "d", @"F:\imageOut.png", @"F:\dataOut.txt", "-f" };
            //string[] testArgs = { "e", @"F:\image.jpg", @"F:\imageOut.png", @"f:\data.txt", "-f", "r" };
            args = testArgs;
            steganographer = new Steganographer();
            ReadArgs(args);
            //Console.WriteLine("SteganographyTool Copyright ©Marijn Kuypers under GNU General Public License v3.0");
            Console.ReadLine();
        }

        private static void ReadArgs(string[] args)
        {
            List<string> path = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains(@":\")) path.Add(args[i]);
                if (args[i].Length == 2 && args[i][0] == '-')
                    args[i] = args[i].Substring(1);
                args[i].ToLower();
            }
            EncodingType encoding = (args.Contains("-ascii") || args.Contains("ascii") || args.Contains("a")) ? EncodingType.ASCII : EncodingType.UTF8;

            if (args.Contains("h") || args.Contains("help"))
            {
                Help();
                return;
            }
            if (args.Contains("i"))
            {
                Info();
                return;
            }
            if (args.Contains("e"))
            {
                if (!CheckPaths(path, args.Contains("f"), 3))
                    return;
                steganographer.NewSteganograph(path[0], path[2], path[1], args.Contains("g"), args.Contains("r"), encoding);
                return;
            }
            if (args.Contains("d"))
            {
                if (!CheckPaths(path, args.Contains("f"), 2))
                    return;
                steganographer.DecryptSteganograph(path[0], path[1], args.Contains("g"), encoding);
                return;
            }
            Help();
        }

        private static bool CheckPaths(List<string> path, bool forceOverwrite, int expectedPaths)
        {
            if (path.Count < expectedPaths)
            {
                Console.WriteLine("Too few file paths were passed");
                return false;
            }
            if (!File.Exists(path[0]))
            {
                Console.WriteLine($"{path[0]} is not a valid image file path.");
                return false;
            }
            if (!Directory.Exists(Path.GetDirectoryName(path[1])))
            {
                Console.WriteLine($"{Path.GetDirectoryName(path[0])} is not a valid directory path.");
                return false;
            }
            if (File.Exists(path[1]) && !forceOverwrite)
            {
                Console.WriteLine($"{Path.GetFileName(path[1])} already exists. If you want to overwrite this file use -f");
                return false;
            }
            if (expectedPaths == 2) return true;
            if (false)//(Path.GetExtension(path[1]) != ".png" && Path.GetExtension(path[1]) != ".bmp")
            {
                Console.WriteLine($"You must choose either .png or .bmp as save format instead of {Path.GetExtension(path[1])} for your savefile");
                return false;
            }
            if (!File.Exists(path[2]))
            {
                Console.WriteLine($"{path[2]} is not a valid file path.");
                return false;
            }
            return true;
        }

        private static void Help()
        {
            Console.WriteLine("usage: SteganographyTool.exe [options] C:\\image_path C:\\output_path [C:\\data_path]");
            Console.WriteLine("\toptions:");
            Console.WriteLine("\t-h\t--help.");
            Console.WriteLine("\t-e\t--enrypt, expects image, output path, and data path.");
            Console.WriteLine("\t-d\t--decrypt, expects image, and output path.");
            Console.WriteLine("\t-f\t--force overwrite exsiting output file.");
            Console.WriteLine("\t-g\t--the image is a grayscale image(prevents colorartefacts). This reduces the datacapacity by 3 times.");
            Console.WriteLine("\t-a\t--the datafile should be read/written with ASCII encoding. Otherwise UTF-8 is used.");
            Console.WriteLine("\t-r\t--fill the writable bytes after the data with random noise. This can make the artifacting less obvious.");
            Console.WriteLine("\t-i\t--info about steganography.");
        }

        private static void Info()
        {
            Console.WriteLine("Steganography is the practice of concealing a file, message, image, or video within another file, message, image, or video. The word steganography combines the Greek words steganos (στεγανός), meaning \"covered, concealed, or protected\", and graphein (γράφειν) meaning \"writing\".");
            Console.WriteLine("The first recorded use of the term was in 1499 by Johannes Trithemius in his Steganographia, a treatise on cryptography and steganography, disguised as a book on magic. Generally, the hidden messages appear to be (or be part of) something else: images, articles, shopping lists, or some other cover text. For example, the hidden message may be in invisible ink between the visible lines of a private letter. Some implementations of steganography that lack a shared secret are forms of security through obscurity, whereas key-dependent steganographic schemes adhere to Kerckhoffs's principle.");
            Console.WriteLine("The advantage of steganography over cryptography alone is that the intended secret message does not attract attention to itself as an object of scrutiny. Plainly visible encrypted messages—no matter how unbreakable—arouse interest, and may in themselves be incriminating in countries where encryption is illegal.[2] Thus, whereas cryptography is the practice of protecting the contents of a message alone, steganography is concerned with concealing the fact that a secret message is being sent, as well as concealing the contents of the message.");
            Console.WriteLine("Steganography includes the concealment of information within computer files. In digital steganography, electronic communications may include steganographic coding inside of a transport layer, such as a document file, image file, program or protocol. Media files are ideal for steganographic transmission because of their large size. For example, a sender might start with an innocuous image file and adjust the color of every 100th pixel to correspond to a letter in the alphabet, a change so subtle that someone not specifically looking for it is unlikely to notice it.");
            Console.ReadLine();
        }
    }
}