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

namespace OpenGLEngine
{
    public class Window : GameWindow
    {
        
        private  float[] _vertices =
        {
            0f, 0f, 0f, // Bottom-left vertex
            100f, 0f, 0f, // Bottom-right vertex
            0f, 100f, 0f, // Top-left vertex
            100f,100f, 0f,  // Top-right vertex
        };
        
        

        private double position= 0;
     
        private int _vertexBufferObject;

        private int _vertexArrayObject;
        
        private Shader _shader;

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
           
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw);
            _vertexArrayObject = GL.GenVertexArray();
           
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
           
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();
            
            Matrix4 m = Matrix4.CreateOrthographic(Program.windowSize.X, Program.windowSize.Y, 1000, -1000);
            m.Transpose();
            Console.WriteLine((m));
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
           GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);
           GL.EnableVertexAttribArray(0);

           for (int i = 0; i < 3; i++)
           {
               for (int j = 0; j < 13; j++)
               {
                   Quad _quad = new Quad(_shader);
                   _quad.Translate(j *150 - Program.windowSize.X/2 , i*150 +100f , 0); 
                   _quad.Draw();
               }
           }
            
            Quad player = new Quad(_shader);
            
            player.Scale(2.5f, 0.6f, 1f);
            player.Translate((float) (position), -400f, 0.0f);
            player.Draw();
            
            Quad ball = new Quad(_shader);
            ball.Scale(0.5f, 0.5f, 1f);
            ball.Translate(0, -400f, 0.0f);
            ball.Draw();
            
            
            SwapBuffers();
        }
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if(input.IsKeyDown(Keys.A))
            {
                position -= e.Time*400f;
            }
            if(input.IsKeyDown(Keys.D))
            {
                position += e.Time*400f;
            }
            
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
