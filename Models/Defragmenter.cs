using System.IO;

namespace FileFragmentationMVC.Models
{
    public class Defragmenter
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
        }
    }
}
