using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace animacion2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D[] felix;

        private Vector2 _position;
        AnimationManager am;

        int activo;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
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
            felix = new Texture2D[5];

            felix[0] = Content.Load<Texture2D>("derecha");
            felix[1] = Content.Load<Texture2D>("izquierda_felix");
            felix[2] = Content.Load<Texture2D>("arriba_felix");
            felix[3] = Content.Load<Texture2D>("abajo_felix");
            felix[4] = Content.Load<Texture2D>("Risas_felix");
            _position = new Vector2(30, 6);

            am = new(11, 4, new System.Numerics.Vector2(325, 215));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.W)) { 
                _position.Y--;
                activo = 2;
                am.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                _position.Y++;
                activo = 3;
                am.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) { 
                _position.X--;
                activo = 1;
                am.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { 
                _position.X++;
                activo = 0;
                am.Update();

            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.W))) { _position.Y -= 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.S))) { _position.Y += 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.A))) { _position.X -= 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.D))) { _position.X += 3; }


           
            // TODO: Add your update logic here

            base.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(felix[activo],
                new Rectangle((int)_position.X, (int)_position.Y, 450, 350),
                am.GetFrame(),
                Color.White);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
