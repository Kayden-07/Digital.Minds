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
using SharpDX.Direct2D1;


namespace BetaFoesAndBones.ArmasUniversal
{
    internal class Armas : Componentes
    {

        public List<Arma> ArmasLista = new List<Arma>();

        private Texture2D garroteTextura;
        private Texture2D ArmaBacteriano;
        private Texture2D ArmaElvira;
        public List<Enemigo> EnemigoLista;

        Garrote garrote;

        public Armas(Game1 game, ContentManager contenedor)
        {
            _content = contenedor;
            _game = game;
            garroteTextura = _content.Load<Texture2D>("espada");
            ArmaBacteriano = _content.Load<Texture2D>("Armas/Arma-bactereano");
            ArmaElvira = _content.Load<Texture2D>("Armas/Arma-elvira");
            //garrote = new Garrote(garroteTextura, new Vector2(200, 100), new Vector2(0, 0));
            //ArmasLista.Add(garrote);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            if(ArmasLista.Count > 0)
                foreach (var arma in ArmasLista)
                    sprite.Draw(arma.TexturaArma, new Rectangle((int)arma.PosicionArma.X, (int)arma.PosicionArma.Y, (int)arma.TamañoArma.X, (int)arma.TamañoArma.Y), Color.White);

            //sprite.Draw(ArmasLista[0].TexturaArma, new Rectangle((int)ArmasLista[0].PosicionArma.X, (int)ArmasLista[0].PosicionArma.Y, 130, 140), Color.White);

        }
        public override void Update(GameTime gameTime)
        {
        }
        public Arma SoltarArmaEnemy()
        {
            if (EnemigoLista.Count > 0)
            {
                int yatoco = 0;
                for (var i = EnemigoLista.Count - 1; i >= 0; i--)
                {
                    if (yatoco == 0)
                    {
                        yatoco = 1;
                        if (EnemigoLista[EnemigoLista.Count - 1] is Slime && yatoco == 1)
                        {
                            return new BastonBacteriano(ArmaBacteriano, EnemigoLista[EnemigoLista.Count - 1].Posicion);
                        }
                        else if (EnemigoLista[EnemigoLista.Count - 1] is Bacteriano && yatoco == 1)
                        {
                            return new BastonBacteriano(ArmaBacteriano, EnemigoLista[EnemigoLista.Count - 1].Posicion);
                        }
                        else if (EnemigoLista[EnemigoLista.Count - 1] is Draconario && yatoco == 1)
                        {
                            return new PistolaElvira(ArmaElvira, EnemigoLista[EnemigoLista.Count - 1].Posicion);
                        }
                    }
                }
                yatoco = 0;
            }
            return null; 
        }
    }
}
