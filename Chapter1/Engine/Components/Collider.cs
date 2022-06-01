using System;
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
        public Quad owner;

        public static Vector3 Transform(Vector3 position, Matrix4 matrix)
        {
            return new Vector3(
                position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41,
                position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42,
                position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43);
        }


        public void CreateCollider(float[] vertices, Matrix4 transformMatrix, Quad owner)
        {
            this.owner = owner;
             float[] transformedVertecies = new float[vertices.Length];
            transformMatrix.Transpose();
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
        }
        
        public Collider(float[] vertecies, Matrix4 transformMatrix, Quad owner)
        {
            CreateCollider(vertecies, transformMatrix, owner);
        }

        public override string ToString()
        {
            return $"{owner} Collider: {bottomLeftBoundingBox} {bottomRightBoundingBox} {topLeftBoundingBox} {topRightBoundingBox}";
        }

        public bool CheckForCollision(Collider other)
        {
            float x1 = other.topLeftBoundingBox.X - topRightBoundingBox.X;
            float x2 = topLeftBoundingBox.X - other.topRightBoundingBox.X;
            
            float y1 = other.topLeftBoundingBox.Y - bottomRightBoundingBox.Y;
            float y2 = topLeftBoundingBox.Y - other.bottomRightBoundingBox.Y;
            
            if (other.topLeftBoundingBox.X > topRightBoundingBox.X || other.topRightBoundingBox.X < topLeftBoundingBox.X)
            {
                return false;
            }
            if (other.bottomLeftBoundingBox.Y > topRightBoundingBox.Y || other.topRightBoundingBox.Y < bottomLeftBoundingBox.Y)
            {
                return false;
            }
                
        
            OnCollisionEnter(other);
            return true;
            
        }

        public virtual void OnCollisionEnter(Collider collider)
        {
            
        }
        
        public virtual void OnCollisionExit(Collider collider)
        {
            
        }

        public virtual void OnCollisionStay(Collider collider)
        {



        }


    }
}