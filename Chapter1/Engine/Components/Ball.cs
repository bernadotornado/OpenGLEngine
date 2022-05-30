using System.Buffers.Text;
using OpenGLEngine.Common;
using System.Numerics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using System;

namespace OpenGLEngine.Components
{
    public class Ball : Quad
    {
        private float _speed = 400f;
        Vector3 direction= new Vector3(1,1,0);
        private float timer = 0;
        
        
        public Ball(Shader shader, GameWindow window): base(shader)
        {
            
        }

        public void Update(float deltaTime)
        {
            bool hit = false;
            Vector3 dir = Vector3.Zero;
            foreach (var VARIABLE in Window.quadRegistry)
            {
                hit = collider.CheckForCollision(VARIABLE.collider, out dir);
            }
            //
            // if (hit)
            // {
            //     Console.WriteLine("HIT!!!!");
            // }
            // else
            // {
            //     Console.WriteLine("NO HIT");
            // }
            var a = new OpenTK.Mathematics.Vector3(direction.X, direction.Y, direction.Z);

            var b = positionVector + a * deltaTime * _speed;
          
            
            Translate(b.X, b.Y, b.Z);
            timer += deltaTime;
            if (timer > 0.2f)
            {
                timer = 0;
                Random rnd = new Random();  
                //direction = new Vector3(-direction.X, direction.Y, direction.Z);
               direction= Vector3.Reflect(direction, new Vector3(0, 1, 0));

            }

        }

        public void OnCollision(Quad quad)
        {
          //  System.Numerics.Vector3 n = quad.positionVector - positionVector;
                var n = Vector3.One;
             direction = System.Numerics.Vector3.Reflect(direction, n);
        }
    }   
}