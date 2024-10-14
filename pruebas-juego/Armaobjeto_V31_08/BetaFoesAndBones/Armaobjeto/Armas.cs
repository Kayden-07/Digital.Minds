using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using BetaFoesAndBones.Vistas;
using BetaFoesAndBones.Personajes;


namespace BetaFoesAndBones.Armaobjeto
{
    internal class Armas : Componentes
    {
        private Texture2D garroteTextura;
        private Rectangle rArma;

        public Vector2 _position;
      
        public Armas(Game1 game, ContentManager contenedor)
        {
            _game = game;
            _content = contenedor;
            rArma = new Rectangle((int)_position.X, (int)_position.Y, 200, 150);
            garroteTextura = _content.Load<Texture2D>("garrote");
        }

        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            Garrote garrote = new Garrote(garroteTextura, new Vector2(500, 100));
            sprite.Draw(garrote.TexturaArma, garrote.PosicionArma, Color.White);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
