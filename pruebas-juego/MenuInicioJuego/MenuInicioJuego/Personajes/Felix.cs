using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MenuInicioJuego.Personajes
{
    internal class Felix : Componentes
    {

        private Texture2D[] felix;

        private Vector2 _position;
        private Vector2 _tamaño;
        AnimationManager am;

        private int activo;
        public Felix(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) 
        {
            _content = contenedor;
            felix = new Texture2D[5];

            felix[0] = _content.Load<Texture2D>("Felix/derecha");
            felix[1] = _content.Load<Texture2D>("Felix/izquierda_felix");
            felix[2] = _content.Load<Texture2D>("Felix/arriba_felix");
            felix[3] = _content.Load<Texture2D>("Felix/abajo_felix");
            felix[4] = _content.Load<Texture2D>("Felix/Risas_felix");
            //_position = posicion;
            //_tamaño = tamaño;
            _position = new Vector2(30, 6);

            am = new(11, 4, new System.Numerics.Vector2(325, 215));
        }
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            sprite.Draw(felix[activo],
                new Rectangle((int)_position.X, (int)_position.Y, 200, 150),
                am.GetFrame(),
                Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _position.Y--;
                activo = 2;
                am.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _position.Y++;
                activo = 3;
                am.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _position.X--;
                activo = 1;
                am.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _position.X++;
                activo = 0;
                am.Update();

            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.W))) { _position.Y -= 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.S))) { _position.Y += 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.A))) { _position.X -= 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && (Keyboard.GetState().IsKeyDown(Keys.D))) { _position.X += 3; }
        }
    }
}
