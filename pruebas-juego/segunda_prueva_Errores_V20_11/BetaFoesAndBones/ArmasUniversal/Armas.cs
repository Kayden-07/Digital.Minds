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
using SharpDX.Direct3D9;
using System.Security.Policy;
using Microsoft.Xna.Framework.Input;


namespace BetaFoesAndBones.ArmasUniversal
{
    internal class Armas : Componentes
    {
        private bool presionoEspacio;
        private float rotacion;
        private bool atacando;
        private float t;
        private float duracionGolpe;

        public int ejecucion;
        public List<Arma> ArmasLista = new List<Arma>();
        public string numArma;
        
        private Texture2D garroteTextura;
        private Texture2D ArmaBacteriano;
        private Texture2D ArmaElvira;
        private Texture2D Arma_slime;
        public List<Enemigo> EnemigoLista;

        Garrote garrote;

        public Armas(Game1 game, ContentManager contenedor)
        {
            presionoEspacio = false;
            _content = contenedor;
            _game = game;
            
            garroteTextura = _content.Load<Texture2D>("espada");
            ArmaBacteriano = _content.Load<Texture2D>("Armas/Arma-bactereano");
            ArmaElvira = _content.Load<Texture2D>("Armas/Arma-elvira");
            Arma_slime = _content.Load<Texture2D>("Armas/Arma-slime");

            ejecucion = 0;
            numArma = "a";

            rotacion = 0;
            atacando = false;
            t = 0;
            duracionGolpe = 0.3f;

        }

        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            if(ArmasLista.Count > 0)
                foreach (var arma in ArmasLista)
                {
                    Rectangle posArma = new Rectangle((int)arma.PosicionArma.X, (int)arma.PosicionArma.Y, (int)arma.TamañoArma.X, (int)arma.TamañoArma.Y);
                    if (numArma != "a" && int.Parse(numArma) < ArmasLista.Count && arma != ArmasLista[int.Parse(numArma)])
                    {
                        if ((arma != ArmasLista[ArmasLista.Count - 1]) || ejecucion == 0)
                            sprite.Draw(arma.TexturaArma,posArma , Color.White);
                    }
                    else if (ejecucion == 0)
                    {
                        if(arma is not PistolaElvira)
                        {
                            posArma = new Rectangle((int)arma.PosicionArma.X + 50, (int)arma.PosicionArma.Y + 100, (int)arma.TamañoArma.X, (int)arma.TamañoArma.Y);
                            Vector2 origenRotacion = new Vector2(arma.TexturaArma.Width / 2, arma.TexturaArma.Height);
                            sprite.Draw(arma.TexturaArma, posArma, null, Color.White, rotacion, origenRotacion, SpriteEffects.None, 0f);
                        }
                        else
                        {
                            sprite.Draw(arma.TexturaArma, posArma, Color.White);
                        }
                    }
                }

            //sprite.Draw(ArmasLista[0].TexturaArma, new Rectangle((int)ArmasLista[0].PosicionArma.X, (int)ArmasLista[0].PosicionArma.Y, 130, 140), Color.White);

        }
        public override void Update(GameTime gameTime)
        {
            float tiempo = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !atacando)
            {
                presionoEspacio = true;
            }
            if (presionoEspacio && Keyboard.GetState().IsKeyUp(Keys.Space)) 
            {
                atacando = true;
                t = 0f;
            }
            if (atacando && numArma != "a")
            {
                t += tiempo;
                if (t < duracionGolpe / 2)
                {
                    rotacion = MathHelper.Lerp(0f, MathHelper.ToRadians(30), t / (duracionGolpe / 2));
                }
                else if (t < duracionGolpe)
                {
                    rotacion = MathHelper.Lerp(MathHelper.ToRadians(30), 0f, (t - (duracionGolpe / 2)) / (duracionGolpe / 2));
                }
                else
                {
                    atacando = false;
                    rotacion = 0f;
                }
            }
        }
        public Arma SoltarArmaEnemy(Enemigo enemy)
        {
            if (enemy is Slime)
                return new LatigoSlime(Arma_slime, enemy.Posicion);
            else if (enemy is Bacteriano)
                return new BastonBacteriano(ArmaBacteriano, enemy.Posicion);
            else if (enemy is Draconario)
                return new PistolaElvira(ArmaElvira, enemy.Posicion);
            return null;
        }
    }
}
