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
    }
}