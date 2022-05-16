using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace LearnOpenTK.components
{
    public class Quad
    {
        float[] vertices = {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, 0.5f, 0.0f,
            0.5f, 0.5f, 0.0f
        };

        private Matrix4 transform;
        private Shader _shader;
        public Quad(float[] vertices, Shader shader )
        {
            this.vertices = vertices;
            this._shader = shader;
        }

        public void Translate(float x , float y, float z)
        {
            transform = Matrix4.CreateTranslation(x, y, z);
            transform.Transpose();
            _shader.SetMatrix4("uTransform", transform);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            GL.EnableVertexAttribArray(0);
        }
    }
}