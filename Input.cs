using System;
using System.IO;
using System.Linq;
class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Your Paragraph");
        string paragraph = Console.ReadLine();
        File.WriteAllText("input.txt", paragraph);
        Console.WriteLine("paragraph Save in input.txt");
        string text = File.ReadAllText("input.txt");//read paragraph from file
        string[] words = text.Split(new char[] { ' ', '\n'}, StringSplitOptions.RemoveEmptyEntries);//Split paragraph into words
        Console.Write("Enter number of Words per File : ");
        int numWordsPerFile;
        while (true)
        {
            Console.Write("Enter number of Words per File: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out numWordsPerFile) && numWordsPerFile > 0)
            {
                break; // valid number, exit the loop
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter only positive numbers.");
            }
        }


        //Fragment text into Multiple files
        int totalWords = words.Length;
        int numFiles = (int)Math.Ceiling((double)totalWords / numWordsPerFile);
        int digits = numFiles.ToString().Length;

        for (int i =0; i<numFiles; i++)
        {
            int start = i * numWordsPerFile;
            int end = Math.Min(start + numWordsPerFile, totalWords);
            string[] chunk = new string[end - start];
            Array.Copy(words, start, chunk, 0, end - start);
            string filename = $"{(i + 1).ToString().PadLeft(digits, '0')}.txt"; // 01.txt, 02.txt, etc.
            File.WriteAllText(filename, string.Join(" ", chunk));
        }
        Console.WriteLine($"Paragraph fragmented into {numFiles} files successfully");


        string[] fragmentFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt")
                                          .Where(f => Path.GetFileName(f) != "input.txt") // exclude input.txt
                                          .OrderBy(f => f) // sort properly: 001.txt, 002.txt...
                                          .ToArray();

        using (StreamWriter sw = new StreamWriter("output.txt"))
        {
            foreach (string file in fragmentFiles)
            {
                string content = File.ReadAllText(file);
                sw.Write(content + " "); // add space between fragments
            }
        }

        Console.WriteLine("All fragments combined into output.txt successfully!");

    }
    
}