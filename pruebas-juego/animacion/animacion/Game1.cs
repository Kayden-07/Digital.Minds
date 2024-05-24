using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Intrinsics.X86;

namespace animacion
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D[] imagenes_corriendo;
        private Vector2 _position;

        int contador;
        int activo;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
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

            _position = new Vector2(30, 6);
            imagenes_corriendo = new Texture2D[8];

            imagenes_corriendo[0] = Content.Load<Texture2D>("D31");
            imagenes_corriendo[1] = Content.Load<Texture2D>("D05");
            imagenes_corriendo[2] = Content.Load<Texture2D>("D07");
            imagenes_corriendo[3] = Content.Load<Texture2D>("D24");
            imagenes_corriendo[4] = Content.Load<Texture2D>("D26");
            imagenes_corriendo[5] = Content.Load<Texture2D>("D27");
            imagenes_corriendo[6] = Content.Load<Texture2D>("D28");
            imagenes_corriendo[7] = Content.Load<Texture2D>("D30");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            if (Keyboard.GetState().IsKeyDown(Keys.W)) { _position.Y--; }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { _position.Y++; }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) { _position.X--; }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { 
                _position.X++;
                contador++;
                if (contador > 5)
                {
                    contador = 0;
                    activo++;

                    if (activo > imagenes_corriendo.Length - 1)
                    {
                        activo = 0;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.W))) { _position.Y -= 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.S))) { _position.Y += 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.A))) { _position.X -= 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.D))) { _position.X += 3; }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(imagenes_corriendo[activo], new Rectangle((int)_position.X, (int)_position.Y, 300, 150), Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
