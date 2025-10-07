using System.IO;

namespace FileFragmentationMVC.Models
{
    public class FileManager
    {
        public static bool CompareFiles(string inputFile, string outputFile)
        {
            string original = File.ReadAllText(inputFile).Trim();
            string combined = File.ReadAllText(outputFile).Trim();
            return original == combined;
        }

        public static void DeleteFiles(string[] fragmentFiles, string outputFile)
        {
            foreach (var file in fragmentFiles)
                File.Delete(file);

            if (File.Exists(outputFile))
                File.Delete(outputFile);
        }
    }
}
