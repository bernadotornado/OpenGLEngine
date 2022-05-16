﻿using OpenGLEngine.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenGLEngine.Components
{
    public class Quad
    {
        float[] vertices = {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, 0.5f, 0.0f,
            0.5f, 0.5f, 0.0f
        };

        private string _shaderTransformMatrix;
        private Matrix4 transform;
        private Matrix4 scale = Matrix4.Identity;
        private Shader _shader;
        public Quad( Shader shader)
        {
            //this.vertices = vertices;
            this._shader = shader;
  
        }

        public void Translate(float x , float y, float z)
        {
            transform = Matrix4.CreateTranslation(x, y, z);
            transform.Transpose();
           
        }

        public void Scale(float x, float y, float z)
        { 
            scale = Matrix4.CreateScale(x, y, z);
            
            transform.Transpose();
            
        }

        public void Draw()
        {
            _shader.SetMatrix4("uModelViewMatrix", transform);
            _shader.SetMatrix4("uProjectionMatrix", Matrix4.CreateOrthographic(Program.windowSize.X, Program.windowSize.Y, 1000, -1000));
            _shader.SetMatrix4("uScaleMatrix", scale);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            GL.EnableVertexAttribArray(0);
        }
        
    }
}