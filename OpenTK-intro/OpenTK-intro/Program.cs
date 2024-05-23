using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Net.Mail;

namespace OpenTK_intro
{
    public class Program
    {
        static void Main(string[] args)
        {
            // create Game Window inside using -> automatic disposal after exit
            using (Window window = new Window(2300, 1400, "OpenTK window"))
            {
                window.Run();
            }
        }
    }
}


