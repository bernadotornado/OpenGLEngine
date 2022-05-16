using System;
using System.Numerics;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace LearnOpenTK
{
    public class Window : GameWindow
    {
        
        private  float[] _vertices =
        {
            0.0f, 0f, 0f, // Bottom-left vertex
            1.0f, 0f, 0.0f, // Bottom-right vertex
            0.0f, 1.0f, 0.0f, // Top-left vertex
            1.0f,  1f, 0.0f,  // Top-right vertex
        };
        
        
        // private  float[] _vertices2 =
        // {
        //     0.0f, 0f, 0f, // Bottom-left vertex
        //     1.0f, 0f, 0.0f, // Bottom-right vertex
        //     1.0f,  -1f, 0.0f,  // Top vertex
        // };
        
        private Matrix4 transform = Matrix4.Identity;

        private double position= 0;
     
        private int _vertexBufferObject;

        private int _vertexArrayObject;
        private int _vertexArrayObject2;
        
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

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);
            position+= e.Time;
            
            
            
            transform = Matrix4.CreateTranslation((float) (position), 0.0f, 0.0f);
            transform.Transpose();
            Console.WriteLine(transform);
            _shader.SetMatrix4("uTransform", transform);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

            
            
            
            
            
            transform = Matrix4.CreateTranslation((float) (position), -1f, 0.0f);
            transform.Transpose();
            _shader.SetMatrix4("uTransform", transform);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            GL.EnableVertexAttribArray(0);
            
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
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
       
    }
}
