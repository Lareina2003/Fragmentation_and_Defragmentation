using System;
using System.IO;
class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Your Paragraph");
        string paragraph = Console.ReadLine();
        File.WriteAllText("input.txt", paragraph);
        Console.WriteLine("paragraph Save in input.txt");
        string text = File.ReadAllText("input.txt");//read paragraph from file
        string[] words = text.Split(new char[] { ' ', '\n', '\r', 't' }, StringSplitOptions.RemoveEmptyEntries);//Split paragraph into words
        Console.Write("Enter number of Words per File : ");
        int numWordsPerFile = int.Parse(Console.ReadLine());

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


    }
    
}