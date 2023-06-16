using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Diagnostics;
using TetrisGameOpenGL.Snake;

namespace TetrisGameOpenGL.Display
{
    public class DisplayManager : GameWindow
    {
        static int GLOBAL_WIDTH;
        static int GLOBAL_HEIGHT;
        static int DELAY;
        static bool PAUSE = false;
        static bool MULTIPLAYER_MODE = false;
        static Field Field;
        static SnakePlayer Snake;
        static SnakePlayer Snake2;

        Stopwatch sw = new Stopwatch();

        public DisplayManager(int _width, int _height, string _title, int _globalwidth, int _globalheight, int _delay, bool _multiplayerMode) : base(_width, _height, GraphicsMode.Default, _title,
            GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.ForwardCompatible)
        {
            GLOBAL_WIDTH = _globalwidth;
            GLOBAL_HEIGHT = _globalheight;
            MULTIPLAYER_MODE = _multiplayerMode;
            DELAY = _delay;
            Field = new Field(GLOBAL_WIDTH, GLOBAL_HEIGHT);
            Snake = new SnakePlayer(GLOBAL_WIDTH / 2, GLOBAL_HEIGHT / 2, 1, Field, new float[]{ 1.0F, 0.65F, 0}, new float[] { 1.0F, 0.85F, 0 }, EnumDirection.Right);
            if (MULTIPLAYER_MODE) Snake2 = new SnakePlayer(GLOBAL_WIDTH / 4, GLOBAL_HEIGHT / 4, 1, Field, new float[] { 0.55F, 0.0F, 1.0F }, new float[] { 0.85F, 0.0F, 1.0F }, EnumDirection.Left);
            GL.Viewport(0, 0, _width, _height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, GLOBAL_WIDTH, 0, GLOBAL_HEIGHT, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        protected void Restart()
        {
            Field = new Field(GLOBAL_WIDTH, GLOBAL_HEIGHT);
            Snake = new SnakePlayer(GLOBAL_WIDTH / 2, GLOBAL_HEIGHT / 2, 1, Field, new float[] { 1.0F, 0.65F, 0 }, new float[] { 1.0F, 0.85F, 0 }, EnumDirection.Right);
            if (MULTIPLAYER_MODE) Snake2 = new SnakePlayer(GLOBAL_WIDTH / 4, GLOBAL_HEIGHT / 4, 1, Field, new float[] { 0.55F, 0.0F, 1.0F }, new float[] { 0.85F, 0.0F, 1.0F }, EnumDirection.Left);
            sw.Restart();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            sw.Start();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if (!PAUSE && sw.ElapsedMilliseconds >= DELAY)
            {
                sw.Restart();
                GL.Clear(ClearBufferMask.ColorBufferBit);
                if (!Field.containFood) Field.generateFood();
                if (Snake.Alive)
                {
                    Snake.Move(Field);
                    Snake.DirectionChanged = false;
                }
                if (MULTIPLAYER_MODE && Snake2.Alive)
                {
                    Snake2.Move(Field);
                    Snake2.DirectionChanged = false;
                }
                Field.DrawBlocks();
                Field.DrawFieldGrid();
                this.SwapBuffers();
            }
            if (!Snake.Alive)
            {
                if (!MULTIPLAYER_MODE || !Snake2.Alive)
                {
                    Console.WriteLine("Game end!");
                    Console.WriteLine("Orange snake score: " + Snake.Score);
                    if (MULTIPLAYER_MODE) Console.WriteLine("Purple snake score: " + Snake2.Score);
                    Console.ReadKey();
                    Restart();
                }
            }
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            Console.WriteLine("Pressed:" + e.Key);
            switch (e.Key)
            {
                case Key.Left: if (Snake.CurrentDirection != EnumDirection.Right && !Snake.DirectionChanged) Snake.CurrentDirection = EnumDirection.Left; Snake.DirectionChanged = true; break;
                case Key.Right: if (Snake.CurrentDirection != EnumDirection.Left && !Snake.DirectionChanged) Snake.CurrentDirection = EnumDirection.Right; Snake.DirectionChanged = true; break;
                case Key.Down: if (Snake.CurrentDirection != EnumDirection.Up && !Snake.DirectionChanged) Snake.CurrentDirection = EnumDirection.Down; Snake.DirectionChanged = true; break;
                case Key.Up: if (Snake.CurrentDirection != EnumDirection.Down && !Snake.DirectionChanged) Snake.CurrentDirection = EnumDirection.Up; Snake.DirectionChanged = true; break;
                case Key.Space: PAUSE = !PAUSE; break;
                default: break;
            }
            if (MULTIPLAYER_MODE)
            {
                switch (e.Key)
                {
                    case Key.A: if (Snake2.CurrentDirection != EnumDirection.Right && !Snake2.DirectionChanged) Snake2.CurrentDirection = EnumDirection.Left; Snake2.DirectionChanged = true; break;
                    case Key.D: if (Snake2.CurrentDirection != EnumDirection.Left && !Snake2.DirectionChanged) Snake2.CurrentDirection = EnumDirection.Right; Snake2.DirectionChanged = true; break;
                    case Key.S: if (Snake2.CurrentDirection != EnumDirection.Up && !Snake2.DirectionChanged) Snake2.CurrentDirection = EnumDirection.Down; Snake2.DirectionChanged = true; break;
                    case Key.W: if (Snake2.CurrentDirection != EnumDirection.Down && !Snake2.DirectionChanged) Snake2.CurrentDirection = EnumDirection.Up; Snake2.DirectionChanged = true; break;
                    default: break;
                }
            }
        }

    }
}
