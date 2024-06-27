using BetaFoesAndBones.Vistas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BetaFoesAndBones
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vista _vistaActual;
        private Vista _proximaVista;
        public void ChangeState(Vista vista)
        {
            _proximaVista = vista;

        }
        public Game1()
        {
            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = w;
            _graphics.PreferredBackBufferHeight = h;
            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _vistaActual = new VistaMenu(this, GraphicsDevice, Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_proximaVista != null)
            {
                _vistaActual = _proximaVista;
                _proximaVista = null;
            };

            _vistaActual.Update(gameTime);

            _vistaActual.PosUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _vistaActual.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
