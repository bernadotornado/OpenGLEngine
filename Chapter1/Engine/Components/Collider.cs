using OpenTK.Mathematics;
using Vector3 = System.Numerics.Vector3;

namespace OpenGLEngine.Components
{
    public class Collider
    {
        public Vector3 topLeftBoundingBox;
        public Vector3 topRightBoundingBox;
        public Vector3 bottomLeftBoundingBox;
        public Vector3 bottomRightBoundingBox;


        public Collider(float[] vertecies, Matrix4 transformMatrix)
        {
            
        }


    }
}