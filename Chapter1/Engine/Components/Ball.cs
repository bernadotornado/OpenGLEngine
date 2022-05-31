using System.Buffers.Text;
using OpenGLEngine.Common;
//using System.Numerics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGLEngine.Components
{
    public class Ball : Quad
    {
        private float _speed = 400f;
        Vector3 direction= new Vector3(1,-1,0);
        private float timer = 0;
        private GameWindow _window;
        
        public Ball(Shader shader, GameWindow window): base(shader)
        {
            _window = window;
        }
        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            float dot = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
            float tempX = normal.X * dot * 2f;
            float tempY = normal.Y * dot * 2f;
            float tempZ = normal.Z * dot * 2f;
            return new Vector3(vector.X - tempX, vector.Y - tempY, vector.Z - tempZ);
        }

        public void Update(float deltaTime)
        {
            bool hit = false;
            var a = direction;
            var b = positionVector + a * deltaTime * _speed;
            Translate(b.X, b.Y, b.Z);

            if (positionVector.X > Program.windowSize.X / 2)
            {
                direction = Reflect(direction, new Vector3(1,0,0));
            }
            if (positionVector.X < -1*Program.windowSize.X / 2)
            {
                direction = Reflect(direction, new Vector3(1,0,0));
            }
            if(positionVector.Y > Program.windowSize.Y / 2)
            {
                direction = Reflect(direction, new Vector3(0,1,0));
            }
            if(positionVector.Y < -1*Program.windowSize.Y / 2)
            {
                _window.Close();
            }
        }

         public void OnCollision(Collider other)
         {
                
              float yl = other.topLeftBoundingBox.Y - collider.topLeftBoundingBox.Y;
              float xr = collider.topRightBoundingBox.X - other.topRightBoundingBox.X;



              Vector3 normal;

              if (yl > xr)
              {
                  normal = new Vector3(0, 1, 0);
              }
              else
              {
                  normal = new Vector3(1, 0, 0);
              }
              
              
              
              direction =Reflect(direction, normal);
              Console.Beep(3000, 100);
         }

         public void OnCollision( Player player)
         {
             direction =Reflect(direction, new Vector3(0,1,0));
             Console.Beep(3000, 100);
         }
    }   
}