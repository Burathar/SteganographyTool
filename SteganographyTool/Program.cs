using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteganographyTool
{
    internal class Program
    {
        private static Steganographer steganographer;

        private static void Main(string[] args)
        {
            steganographer = new Steganographer();
            if (args.Length == 0)
            {
                Welcome();
                Loop();
            }
            else
            {
                ReadArgs(args);
            }
        }

        private static void ReadArgs(string[] args)
        {
            string[] path;
            path = args.Where(s => s.Contains(@":\")).ToArray();
            args = args.Select(x => x.ToLower()).ToArray();

            if (!File.Exists(path[0]))
            {
                Console.WriteLine($"{path[0]} is not a valid image file path.");
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(path[1])))
            {
                Console.WriteLine($"{Path.GetDirectoryName(path[0])} is not a valid directory path.");
                return;
            }
            if (File.Exists(path[1]) && !args.Contains("-f"))
            {
                Console.WriteLine($"{Path.GetFileName(path[1])} already exists. If you want to overwrite this file use -f");
            }
            //check if given file extention is png or bmp
            if (!File.Exists(path[2]))
            {
                Console.WriteLine($"{path[2]} is not a valid file path.");
                return;
            }

            if (args.Contains("-h") || args.Contains("h") || args.Contains("help"))
            {
                Help();
                return;
            }
            if (args.Contains("-i"))
            {
                Info();
                return;
            }
            if (args.Contains("-e"))
            {
                steganographer.NewSteganograph(path[0], path[2], path[1]);
                return;
            }
            if (args.Contains("-d"))
            {
                steganographer.DecryptSteganograph(path[0], path[1]);
                return;
            }
            Help();
        }

        private static void Help()
        {
            Console.WriteLine("usage: SteganographyTool.exe [options] C:\\image_path C:\\output_path [C:\\data_path]");
            Console.WriteLine("\toptions:");
            Console.WriteLine("\t-h\t--help");
            Console.WriteLine("\t-e\t--enrypt, expects image, output path, and data path");
            Console.WriteLine("\t-d\t--decrypt, expects image, and output path");
            Console.WriteLine("\t-i\t--info about steganography");
            Console.WriteLine("\t-f\t--force overwrite exsiting output file");
        }

        private static void Welcome()
        {
            Console.WriteLine("Copyright ©Marijn Kuypers under GNU General Public License v3.0");
            Console.WriteLine();
            Console.WriteLine("Welcome to this Steganography tool!");
            Console.WriteLine();
        }

        private static void Loop()
        {
            bool quit = false;
            while (!quit)
            {
                Menu();
                ChooseFunction(Console.ReadLine(), ref quit);
            }
        }

        private static void ChooseFunction(string input, ref bool quit)
        {
            input = RemoveWhitespace.Clean(input);
            Console.WriteLine();
            input = input.ToUpper();
            if (input.Length != 1)
            {
                Console.WriteLine("Please enter a valid option (E/D/I/Q)");
                return;
            }
            switch (input)
            {
                case "E":
                    if (!Encrypt())
                        return;
                    break;

                case "D":
                    if (!Decrypt())
                        return;
                    break;

                case "I":
                    Info();
                    break;

                case "Q":
                    quit = true;
                    break;

                default:
                    break;
            }
        }

        private static bool Encrypt()
        {
            Console.WriteLine("Please specify the source image path.");
            string sourceImage = Console.ReadLine();
            if (!File.Exists(sourceImage))
            {
                Console.WriteLine("The given file does not exist.");
                return false;
            }
            Console.WriteLine("Please specify the data file path.");
            string data = Console.ReadLine();
            if (!File.Exists(data))
            {
                Console.WriteLine("The given file does not exist.");
                return false;
            }
            Console.WriteLine("Please specify the save image path.");
            string saveImage = Console.ReadLine();
            if (!Directory.Exists(Path.GetDirectoryName(saveImage)))
            {
                Console.WriteLine("The given directory does not exist.");
                return false;
            }
            if (!File.Exists(saveImage))
            {
                Console.WriteLine("The given file already exists.");
                return false;
            }
            steganographer.NewSteganograph(sourceImage, data, saveImage);
            return true;
        }

        private static bool Decrypt()
        {
            Console.WriteLine("Please specify the source image path.");
            string sourceImage = Console.ReadLine();
            if (!File.Exists(sourceImage))
            {
                Console.WriteLine("The given file does not exist.");
                return false;
            }
            Console.WriteLine("Please specify the save data path.");
            string saveData = Console.ReadLine();
            if (!Directory.Exists(Path.GetDirectoryName(saveData)))
            {
                Console.WriteLine("The given directory does not exist.");
                return false;
            }
            if (File.Exists(saveData))
            {
                Console.WriteLine("The given file already exists.");
                return false;
            }
            steganographer.DecryptSteganograph(sourceImage, saveData);
            return true;
        }

        private static void Info()
        {
            Console.WriteLine("Steganography is the practice of concealing a file, message, image, or video within another file, message, image, or video. The word steganography combines the Greek words steganos (στεγανός), meaning \"covered, concealed, or protected\", and graphein (γράφειν) meaning \"writing\".");
            Console.WriteLine("The first recorded use of the term was in 1499 by Johannes Trithemius in his Steganographia, a treatise on cryptography and steganography, disguised as a book on magic. Generally, the hidden messages appear to be (or be part of) something else: images, articles, shopping lists, or some other cover text. For example, the hidden message may be in invisible ink between the visible lines of a private letter. Some implementations of steganography that lack a shared secret are forms of security through obscurity, whereas key-dependent steganographic schemes adhere to Kerckhoffs's principle.");
            Console.WriteLine("The advantage of steganography over cryptography alone is that the intended secret message does not attract attention to itself as an object of scrutiny. Plainly visible encrypted messages—no matter how unbreakable—arouse interest, and may in themselves be incriminating in countries where encryption is illegal.[2] Thus, whereas cryptography is the practice of protecting the contents of a message alone, steganography is concerned with concealing the fact that a secret message is being sent, as well as concealing the contents of the message.");
            Console.WriteLine("Steganography includes the concealment of information within computer files. In digital steganography, electronic communications may include steganographic coding inside of a transport layer, such as a document file, image file, program or protocol. Media files are ideal for steganographic transmission because of their large size. For example, a sender might start with an innocuous image file and adjust the color of every 100th pixel to correspond to a letter in the alphabet, a change so subtle that someone not specifically looking for it is unlikely to notice it.");
            Console.ReadLine();
        }

        private static void Menu()
        {
            Console.WriteLine("To make encrypt new Steganograph,\tenter 'E'");
            Console.WriteLine("To decrypt an exsiting steganograph,\tenter 'D'");
            Console.WriteLine("For more info on steganographs,\t\tenter 'I'");
            Console.WriteLine("To quit,\t\t\t\tenter 'Q'");
        }
    }
}