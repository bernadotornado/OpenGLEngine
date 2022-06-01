using OpenGLEngine.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace OpenGLEngine.Components
{
    public class Quad
    {
        private Matrix4 position = Matrix4.Identity;
        private Matrix4 scale = Matrix4.Identity;
        private Matrix4 rotation = Matrix4.Identity;

        public Vector3 positionVector;
        public Vector3 rotationVector;
        public Vector3 scaleVector;
        public int id;

        public Collider collider = new Collider(new float[12], Matrix4.Identity, null);
        
        private Shader _shader;
        public Quad( Shader shader)
        {
            this._shader = shader;
            coll();
        }

        public  override  string ToString()
        {
            return $"{id}";
        }
        
        void coll()=> collider.CreateCollider(Window.vertices, position*rotation*scale, this);
        public void Translate(float x , float y, float z)
        {
            position = Matrix4.CreateTranslation(x, y, z);  
            position.Transpose();
            positionVector = new Vector3(x, y, z);
            coll();
        }

        public void Scale(float x, float y, float z)
        { 
            scale = Matrix4.CreateScale(x, y, z);
            
            position.Transpose();
            scaleVector = new Vector3(x, y, z);
            coll();
        }
        public void Rotate(float x, float y, float z)
        {
            rotation = Matrix4.CreateRotationX(x);
            rotation *= Matrix4.CreateRotationY(y);
            rotation *= Matrix4.CreateRotationZ(z);
            rotationVector = new Vector3(x, y, z);
            coll();
        }

        public void Draw()
        {
            _shader.SetMatrix4("uModelViewMatrix", position *scale * rotation);
            _shader.SetMatrix4("uProjectionMatrix", Matrix4.CreateOrthographic(Program.windowSize.X, Program.windowSize.Y, 1000, -1000));
           
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            GL.EnableVertexAttribArray(0);
        }
    }
}