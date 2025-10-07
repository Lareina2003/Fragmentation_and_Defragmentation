using FileFragmentationMVC.Controllers;
using FileFragmentationMVC.Views;

namespace FileFragmentationMVC
{
    class Program
    {
        static void Main()
        {
            ConsoleView view = new ConsoleView();
            FragmentController controller = new FragmentController(view);
            controller.Run();
        }
    }
}
