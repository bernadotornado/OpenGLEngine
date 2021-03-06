using OpenGLEngine.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGLEngine.Components
{
    public class Player: Quad
    {
        private float _position;
        private float _speed = 800f;
        private GameWindow _window;
        
        
        public Player(Shader shader, GameWindow window) : base(shader)
        {
            _window = window;
        }

        public void Update(float deltaTime )
        {
            var input = _window.KeyboardState;

            if(input.IsKeyDown(Keys.A))
            {
                _position -= _speed * deltaTime;
            }
            if(input.IsKeyDown(Keys.D))
            {
                _position += _speed * deltaTime;
            }
            
            Scale(2.5f, 0.6f, 1f);;
            Translate((_position), -400f, 0.0f);
        }
    }
}