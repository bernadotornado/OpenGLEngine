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
        Vector3 direction= new Vector3(1,1,0);
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
            Vector3 dir = Vector3.Zero;
            // foreach (var VARIABLE in Window.quadRegistry)
            // {
            //     hit = collider.CheckForCollision(VARIABLE.collider);
            //
            //
            //        if (hit)
            //        {
            //            Console.WriteLine("HIT!!!!");
            //        }
            //        else
            //        {
            //            Console.WriteLine("NO HIT");
            //        }
            //        
            //                        
            //        
            // }
            var a = direction;

            var b = positionVector + a * deltaTime * _speed;
          
            
            Translate(b.X, b.Y, b.Z);
            timer += deltaTime;
          //  if (timer > 0.5f)
            {
                timer = 0;
                Random rnd = new Random();  
                //direction = new Vector3(-direction.X, direction.Y, direction.Z);
                var input = _window.KeyboardState;
                if(input.IsKeyDown(Keys.D1))
                {
                    direction = new Vector3(1, 1, 0);
                }
                if(input.IsKeyDown(Keys.D2))
                {
                    direction = new Vector3(-1, 1, 0);
                }
                if(input.IsKeyDown(Keys.D3))
                {
                    direction = new Vector3(1, -1, 0);
                }
                if(input.IsKeyDown(Keys.D4))
                {
                    direction = new Vector3(-1, -1, 0);
                }
                //direction= Reflect(direction, new Vector3(0, 1, 0));
                
                
            }
            //Console.WriteLine(collider);

        }

         public void OnCollision(Quad quad) 
         {
           //  System.Numerics.Vector3 n = quad.positionVector - positionVector;
                 var n = Vector3.One;
              direction =Reflect(direction, n);
         }
    }   
}