using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace disparos2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _esqueleto;
        Texture2D flecha;
        System.Numerics.Vector2 _positionEsqueleto;
        System.Numerics.Vector2 _positionFlecha;
        int widthEsqueleto = 100;
        int heightEsqueleto = 100;

        private float _rotation;
        public float LinearVelocity = 4f;
        public float RotationVelocity = 3f;
        public System.Numerics.Vector2 Origin;

        public System.Numerics.Vector2 Direction;

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
            
            _esqueleto = Content.Load<Texture2D>("esqueleto");
            flecha = Content.Load<Texture2D>("flecha1");
            Origin = new System.Numerics.Vector2(flecha.Width / 2, flecha.Height / 2);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            if (Keyboard.GetState().IsKeyDown(Keys.W)) { _positionEsqueleto.Y -= 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { _positionEsqueleto.Y += 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) { _positionEsqueleto.X -= 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { _positionEsqueleto.X += 3; }
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _rotation += MathHelper.ToRadians(RotationVelocity);


            Direction = new System.Numerics.Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
            _positionFlecha = new System.Numerics.Vector2(_positionEsqueleto.X + widthEsqueleto / 2, _positionEsqueleto.Y + heightEsqueleto / 2);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(flecha, _positionFlecha, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
            _spriteBatch.Draw(_esqueleto, new Rectangle((int)_positionEsqueleto.X, (int)_positionEsqueleto.Y, widthEsqueleto, heightEsqueleto), Color.White);

            _spriteBatch.End(); 
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
