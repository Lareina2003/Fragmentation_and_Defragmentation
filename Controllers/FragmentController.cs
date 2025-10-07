using FileFragmentationMVC.Models;
using FileFragmentationMVC.Views;

namespace FileFragmentationMVC.Controllers
{
    public class FragmentController
    {
        private readonly ConsoleView view;

        public FragmentController(ConsoleView view)
        {
            this.view = view;
        }

        public void Run()
        {
            // 1. Get paragraph
            string paragraph = view.GetParagraph();

            // 2. Fragment
            Fragmenter fragmenter = new Fragmenter(paragraph);
            int numWordsPerFile = view.GetNumberOfWords(fragmenter.Words.Length);
            string[] fragments = fragmenter.Fragment(numWordsPerFile);
            view.ShowMessage($"Paragraph fragmented into {fragments.Length} files successfully.");

            // 3. Defragment
            Defragmenter defragmenter = new Defragmenter();
            defragmenter.CombineFragments(fragments);
            view.ShowMessage("All fragments combined into output.txt successfully!");

            // 4. Compare
            bool isEqual = FileManager.CompareFiles("input.txt", "output.txt");
            view.ShowMessage(isEqual ? "SUCCESS: Input and Output files are equal."
                                     : "FAILURE: Something went wrong!");

            // 5. Cleanup
            if (view.ConfirmDeletion())
            {
                FileManager.DeleteFiles(fragments, "output.txt");
                view.ShowMessage("All fragment files deleted. Ready for next run!");
            }
        }
    }
}
