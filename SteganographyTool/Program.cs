using System;
using System.Collections.Generic;
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
            Welcome();
            Loop();
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
                Console.WriteLine("Please enter a valid option (N/D/I/Q)");
                return;
            }
            switch (input)
            {
                case "N":
                    steganographer.NewSteganograph();
                    break;

                case "D":
                    steganographer.DecryptSteganograph();
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
            Console.WriteLine("To make a new Steganograph,\t\tenter 'N'");
            Console.WriteLine("To decrypt an exsiting steganograph,\tenter 'D'");
            Console.WriteLine("For more info on steganographs,\t\tenter 'I'");
            Console.WriteLine("To quit,\t\t\t\tenter 'Q'");
        }
    }
}