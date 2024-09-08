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


namespace BetaFoesAndBones.ArmasUniversal
{
    internal class Armas : Componentes
    {
        private Texture2D garroteTextura;
        public Armas(Game1 game, ContentManager contenedor)
        {
            _content = contenedor;
            _game = game;
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
