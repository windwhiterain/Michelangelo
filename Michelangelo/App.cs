using System.Numerics;
using Michelangelo;
using Michelangelo.Math;
using Michelangelo.Render;
using Michelangelo.UI;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
public struct RHI
{
    public GL gl;
}
public static class App
{
    public static GL gl;
    public static RHI Rhi => new() { gl = gl };
    public static Vector2D<int> windowSize => window.Size;
    static IUI ui;
    static IWindow window;
    static App()
    {
        ui = new DefualtUI();
        var options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);
        options.Title = ui.Title;
        window = Window.Create(options);
        window.Load += Initialize;
        window.Load += ui.OnLoad;
        window.Load += ForwardInput;
        window.Update += ui.OnUpdate;
        window.Render += ui.OnRender;
        window.FramebufferResize += ui.OnFramebufferResize;

    }
    static void Initialize()
    {
        gl = GL.GetApi(window);
    }
    static void Main()
    {
        window.Run();
        ui.Release();
        window.Dispose();
    }
    static void ForwardInput()
    {
        IInputContext input = window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
        {
            input.Keyboards[i].KeyDown += (keyboard, key, _) => { ui.OnInput(new KeyInputData(key, KeyInputType.Down)); };
            input.Keyboards[i].KeyUp += (keyboard, key, _) => { ui.OnInput(new KeyInputData(key, KeyInputType.Up)); };
            input.Mice[0].Click += (_, button, position) => { ui.OnInput(new MouseInputData(button, new Float2(position.X, position.Y) / new Float2(window.Size.X, window.Size.Y))); };
        }
    }
}