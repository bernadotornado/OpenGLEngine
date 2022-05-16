using OpenGLEngine.Common;
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
        private Shader _shader;
        public Quad( Shader shader, string shaderTransformMatrix )
        {
            //this.vertices = vertices;
            this._shader = shader;
            this._shaderTransformMatrix = shaderTransformMatrix;
        }

        public void Translate(float x , float y, float z)
        {
            transform = Matrix4.CreateTranslation(x, y, z);
            transform.Transpose();
            _shader.SetMatrix4(_shaderTransformMatrix, transform);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            GL.EnableVertexAttribArray(0);
        }
    }
}