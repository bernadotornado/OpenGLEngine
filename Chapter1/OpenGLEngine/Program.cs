using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace OpenGLEngine
{
    public static class Program
    {
        static Vector2i windowSize = new Vector2i(1920, 1080);
        private static void Main()
        {
            
            
            
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(windowSize.Y, windowSize.Y),
                Title = "OpenGLEngine",
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
