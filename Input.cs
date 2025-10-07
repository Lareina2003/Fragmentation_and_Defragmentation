using System;
using System.IO;
using System.Linq;

namespace FileFragmentationApp
{
    // Class responsible for fragmentation
    class Fragmenter
    {
        public string InputFile { get; private set; } = "input.txt";
        public string[] Words { get; private set; }

        public Fragmenter(string paragraph)
        {
            File.WriteAllText(InputFile, paragraph);
            Console.WriteLine("Paragraph saved in input.txt");

            string text = File.ReadAllText(InputFile);
            Words = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] Fragment(int numWordsPerFile)
        {
            int totalWords = Words.Length;
            int numFiles = (int)Math.Ceiling((double)totalWords / numWordsPerFile);
            int digits = numFiles.ToString().Length;
            string[] fragmentFiles = new string[numFiles];

            for (int i = 0; i < numFiles; i++)
            {
                int start = i * numWordsPerFile;
                int end = Math.Min(start + numWordsPerFile, totalWords);
                string[] chunk = new string[end - start];
                Array.Copy(Words, start, chunk, 0, end - start);

                string filename = $"{(i + 1).ToString().PadLeft(digits, '0')}.txt";
                File.WriteAllText(filename, string.Join(" ", chunk));
                fragmentFiles[i] = filename;
            }

            Console.WriteLine($"Paragraph fragmented into {numFiles} files successfully.");
            return fragmentFiles;
        }
    }

    // Class responsible for defragmentation
    class Defragmenter
    {
        public string OutputFile { get; private set; } = "output.txt";

        public void CombineFragments(string[] fragmentFiles)
        {
            using (StreamWriter sw = new StreamWriter(OutputFile))
            {
                foreach (var file in fragmentFiles)
                {
                    string content = File.ReadAllText(file);
                    sw.Write(content + " ");
                }
            }
            Console.WriteLine("All fragments combined into output.txt successfully!");
        }
    }

    // Class responsible for comparing and cleaning files
    class FileManager
    {
        public static void CompareFiles(string inputFile, string outputFile)
        {
            string original = File.ReadAllText(inputFile).Trim();
            string combined = File.ReadAllText(outputFile).Trim();

            if (original == combined)
                Console.WriteLine("SUCCESS: Input and Output files are equal.");
            else
                Console.WriteLine("FAILURE: Something went wrong! Input and Output do not match.");
        }

        public static void DeleteFiles(string[] fragmentFiles, string outputFile)
        {
            foreach (var file in fragmentFiles)
                File.Delete(file);

            if (File.Exists(outputFile))
                File.Delete(outputFile);

            Console.WriteLine("All fragment files deleted. Ready for next run!");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter Your Paragraph:");
            string paragraph = Console.ReadLine();

            // Create Fragmenter object
            Fragmenter fragmenter = new Fragmenter(paragraph);

            // Get valid number of words per file
            int numWordsPerFile;
            while (true)
            {
                Console.Write("Enter number of Words per File: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out numWordsPerFile) && numWordsPerFile > 0)
                {
                    if (numWordsPerFile <= fragmenter.Words.Length)
                        break;
                    else
                        Console.WriteLine($"Number too large! Paragraph only has {fragmenter.Words.Length} words.");
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter only positive numbers.");
                }
            }

            // Fragment the paragraph
            string[] fragmentFiles = fragmenter.Fragment(numWordsPerFile);

            // Defragmentation
            Defragmenter defragmenter = new Defragmenter();
            defragmenter.CombineFragments(fragmentFiles);

            // Compare Input and Output
            FileManager.CompareFiles("input.txt", "output.txt");

            // delete files
            Console.Write("Do you want to delete all fragment files for next run? (y/n): ");
            string choice = Console.ReadLine().ToLower();
            if (choice == "y")
            {
                FileManager.DeleteFiles(fragmentFiles, "output.txt");
            }
        }
    }
}
