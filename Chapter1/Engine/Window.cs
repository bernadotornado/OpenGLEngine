using System;
using System.Collections.Generic;
using System.Numerics;
using OpenGLEngine.Common;
using OpenGLEngine.Components;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using Vector3 = OpenTK.Mathematics.Vector3;

namespace OpenGLEngine
{
    public class Window : GameWindow
    {
        
        public  static float[] vertices =
        {
            0f, 0f, 0f, // Bottom-left vertex
            100f, 0f, 0f, // Bottom-right vertex
            0f, 100f, 0f, // Top-left vertex
            100f,100f, 0f,  // Top-right vertex
        };


        public static Matrix4 ortho = Matrix4.Identity;
        public static List<Quad> quadRegistry = new List<Quad>();

        private double position= 0;
     
        private int _vertexBufferObject;

        private int _vertexArrayObject;
        
        private Shader _shader;
        private Player _player;
        Ball _ball;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.3f, 0.3f, 0.3f, 1f);
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
           
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);
            _vertexArrayObject = GL.GenVertexArray();
           
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
           
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();
            
            ortho = Matrix4.CreateOrthographic(Program.windowSize.X, Program.windowSize.Y, 1000, -1000);
            ortho.Transpose();
            _player = new Player(_shader, this);
            _player.id = 99;
            _player.Translate(0,-2000,0);
            _ball = new Ball(_shader, this);
            _ball.id = 100;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Quad _quad = new Quad(_shader);
                    _quad.id = i * 13 + j;
                    _quad.Translate(j *150 - Program.windowSize.X/2 , i*150 +100f , 0); 
                    _quad.Draw();
                    quadRegistry.Add(_quad);
                }
            }
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
           GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);
           GL.EnableVertexAttribArray(0);

           foreach (var q in quadRegistry)
           {    
               q.Draw();
           }
           _player.Draw();
           _ball.Draw();
           SwapBuffers();
             
        }
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
                Close();
            
            if (_ball.collider.CheckForCollision(_player.collider))
                _ball.OnCollision(_player);
            

            foreach (var q in quadRegistry)
            {
                var ax = _ball.collider.CheckForCollision(q.collider);
                if (ax)
                {
                    _ball.OnCollision(q.collider);
                    q.Translate(30000,0,0);
                }
            }
            _player.Update((float)e.Time);
            _ball.Update((float)e.Time);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            Program.windowSize.X = Size.X;
            Program.windowSize.Y = Size.Y;
        }
    }
}
