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


namespace BetaFoesAndBones.ArmasUniversal
{
    internal class Armas : Componentes
    {

        public List<Arma> ArmasLista = new List<Arma>();

        private Texture2D garroteTextura;

        Garrote garrote;

        public Armas(Game1 game, ContentManager contenedor)
        {
            _content = contenedor;
            _game = game;
            garroteTextura = _content.Load<Texture2D>("espada");
            garrote = new Garrote(garroteTextura, new Vector2(500, 100), new Vector2(0, 0));
            ArmasLista.Add(garrote);

        }

        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {

            sprite.Draw(ArmasLista[0].TexturaArma, new Rectangle((int)ArmasLista[0].PosicionArma.X, (int)ArmasLista[0].PosicionArma.Y, 130, 140), Color.White);

        }
        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
