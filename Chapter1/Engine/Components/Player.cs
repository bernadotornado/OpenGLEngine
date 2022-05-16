using OpenGLEngine.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGLEngine.Components
{
    public class Player: Quad
    {
        private float _position;
        private float _speed = 400f;
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
        }
    }
}