using MenuInicio.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MenuInicio
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private State _currentState;
        private State _nextState;
        
        public void ChangeState(State state)
        {
            _nextState = state;

        }
        public Game1()
        {
            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
           _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            _currentState = new MenuState(this, GraphicsDevice, Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            };

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, _spriteBatch);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
