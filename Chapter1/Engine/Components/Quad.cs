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
        private Matrix4 scale = Matrix4.Identity;
        private Matrix4 rotation = Matrix4.Identity;

        public Vector3 positionVector;
        public Vector3 rotationVector;
        public Vector3 scaleVector;
        
        
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
            positionVector = new Vector3(x, y, z);
           
        }

        public void Scale(float x, float y, float z)
        { 
            scale = Matrix4.CreateScale(x, y, z);
            
            transform.Transpose();
            scaleVector = new Vector3(x, y, z);
            
        }
        public void Rotate(float x, float y, float z)
        {
            rotation = Matrix4.CreateRotationX(x);
            rotation *= Matrix4.CreateRotationY(y);
            rotation *= Matrix4.CreateRotationZ(z);
                // transform.Transpose();
            rotationVector = new Vector3(x, y, z);
        }

        public void Draw()
        {
            _shader.SetMatrix4("uModelViewMatrix", transform);
            _shader.SetMatrix4("uProjectionMatrix", Matrix4.CreateOrthographic(Program.windowSize.X, Program.windowSize.Y, 1000, -1000));
            _shader.SetMatrix4("uScaleMatrix", scale);
            _shader.SetMatrix4("uRotationMatrix", rotation);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            GL.EnableVertexAttribArray(0);
        }
        
    }
}