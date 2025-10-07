using System;
using System.IO;

namespace FileFragmentationMVC.Models
{
    public class Fragmenter
    {
        public string InputFile { get; private set; } = "input.txt";
        public string[] Words { get; private set; }

        public Fragmenter(string paragraph)
        {
            File.WriteAllText(InputFile, paragraph);
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
                int start = i * numWordsPerFile;//starting word index for this fragment file
                int end = Math.Min(start + numWordsPerFile, totalWords);//ending word index, making sure it doesn’t go past the last word.
                string[] chunk = new string[end - start];//Creates a temporary array to store the words for this fragment.
                Array.Copy(Words, start, chunk, 0, end - start);//Copies the correct range of words from the full paragraph into chunk

                string filename = $"{(i + 1).ToString().PadLeft(digits, '0')}.txt";
                File.WriteAllText(filename, string.Join(" ", chunk));
                fragmentFiles[i] = filename;
            }
            return fragmentFiles;
        }
    }
}
