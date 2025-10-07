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

        public bool ConfirmDeletion()
        {
            Console.Write("Do you want to delete all fragment files for next run? (y/n): ");
            string choice = Console.ReadLine().ToLower();
            return choice == "y";
        }
    }
}
