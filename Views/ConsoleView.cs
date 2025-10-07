using System;

namespace FileFragmentationMVC.Views
{
    public class ConsoleView
    {
        public string GetParagraph()
        {
            Console.WriteLine("Enter Your Paragraph:");
            return Console.ReadLine();
        }

        public int GetNumberOfWords(int maxWords)
        {
            int numWords;
            while (true)
            {
                Console.Write($"Enter number of words per file (max {maxWords}): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out numWords) && numWords > 0 && numWords <= maxWords)
                    break;
                else
                    Console.WriteLine("Invalid input! Enter a positive number not greater than paragraph length.");
            }
            return numWords;
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        // Display all fragment files
        public void ShowFragmentFiles(string[] fragmentFiles)
        {
            Console.WriteLine("\nCreated Fragment Files:");
            for (int i = 0; i < fragmentFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {fragmentFiles[i]}");
            }
        }

        // Ask user to pick a file number
        public int GetFileNumber(string prompt, int maxNumber)
        {
            int fileNum;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out fileNum) && fileNum > 0 && fileNum <= maxNumber)
                    break;
                else
                    Console.WriteLine($"Invalid input! Enter a number between 1 and {maxNumber}.");
            }
            return fileNum;
        }

        // Display file content
        public void ShowFileContent(string fileName, string content)
        {
            Console.WriteLine($"\nContent of {fileName}:\n");
            Console.WriteLine(content);
            Console.WriteLine("\n------------------------------------\n");
        }


        public bool ConfirmDeletion()
        {
            Console.Write("Do you want to delete all fragment files for next run? (y/n): ");
            string choice = Console.ReadLine().ToLower();
            return choice == "y";
        }

        
    }
}
