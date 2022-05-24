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

        public static Vector3 Transform(Vector3 position, Matrix4 matrix)
        {
            return new Vector3(
                position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41,
                position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42,
                position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43);
        }


        public void CreateCollider(float[] vertices, Matrix4 transformMatrix)
        {
             float[] transformedVertecies = new float[vertices.Length];
            
            for (int i = 0; i < vertices.Length; i += 3)
            {
                Vector3 vertex = new Vector3(vertices[i], vertices[i + 1], vertices[i + 2]);
                Vector3 transformedVertex = Transform(vertex, transformMatrix);
                transformedVertecies[i] = transformedVertex.X;
                transformedVertecies[i + 1] = transformedVertex.Y;
                transformedVertecies[i + 2] = transformedVertex.Z;
            }
            
            bottomLeftBoundingBox = new Vector3(transformedVertecies[0], transformedVertecies[1], transformedVertecies[2]);
            bottomRightBoundingBox = new Vector3(transformedVertecies[3], transformedVertecies[4], transformedVertecies[5]);
            topLeftBoundingBox = new Vector3(transformedVertecies[6], transformedVertecies[7], transformedVertecies[8]);
            topRightBoundingBox = new Vector3(transformedVertecies[9], transformedVertecies[10], transformedVertecies[11]);
            
           // transform.
            
            
            //
            //
            // 0f, 0f, 0f, // Bottom-left vertex
            // 100f, 0f, 0f, // Bottom-right vertex
            // 0f, 100f, 0f, // Top-left vertex
            // 100f,100f, 0f,  // Top-right vertex
            //
            //
            
            // var transformedVerts = new float[vertecies.Length];
            // for (int i = 0; i < vertecies.Length; i += 3)
            // {
            //     var vertex = new Vector3(vertecies[i], vertecies[i + 1], vertecies[i + 2]);
            //     var transformedVertex = Vector3.Transform(vertex, transformMatrix);
            //     transformedVerts[i] = transformedVertex.X;
            //     transformedVerts[i + 1] = transformedVertex.Y;
            //     transformedVerts[i + 2] = transformedVertex.Z;
            // }
            //
            // var minX = transformedVerts[0];
            // var maxX = transformedVerts[0];
            // var minY = transformedVerts[1];
            // var maxY = transformedVerts[1];
            // var minZ = transformedVerts[2];
            // var maxZ = transformedVerts[2];
            //
            // for (int i = 0; i < transformedVerts.Length; i += 3)
            // {
            //     if (transformedVerts[i] < minX)
            //     {
            //         minX = transformedVerts[i];
            //     }
            //     if (transformedVerts[i] > maxX)
            //     {
            //         maxX = transformedVerts[i];
            //     }
            //     if (transformedVerts[i + 1] < minY)
            //     {
            //         minY = transformedVerts[i + 1];
            //     }
            //     if (transformedVerts[i + 1] > maxY)
            //     {
            //         maxY = transformedVerts[i + 1];
            //     }
            //     if (transformedVerts[i + 2] < minZ)
            //     {
            //         minZ = transformedVerts[i + 2];
            //     }
            //     if (transformedVerts[i + 2] > maxZ)
            //     {
            //         maxZ = transformedVerts[i + 2];
            //     }
            // }
            //
            // topLeftBoundingBox = new Vector3(minX, minY, minZ);
            // topRightBoundingBox = new Vector3(maxX, minY, minZ);
            // bottomLeftBoundingBox = new Vector3(min    
        }
        
        public Collider(float[] vertecies, Matrix4 transformMatrix)
        {
            CreateCollider(vertecies, transformMatrix);
        }



        public bool CheckForCollision( Collider collider, out Vector3 direction)
        {
            Collider A = this;
            Collider B = collider;

            if (A.bottomLeftBoundingBox.X > B.topRightBoundingBox.X)
            {
                if(B.topRightBoundingBox.Y > A.bottomLeftBoundingBox.Y)
                {
                    direction = new Vector3(-1, 0, 0);
                    return true;
                }

                if (A.topLeftBoundingBox.Y > B.bottomRightBoundingBox.Y)
                {
                    direction = new Vector3(1, 0, 0);
                    return true;
                }
            }
            
            direction = Vector3.Zero;
            return false;
        }
    }
}