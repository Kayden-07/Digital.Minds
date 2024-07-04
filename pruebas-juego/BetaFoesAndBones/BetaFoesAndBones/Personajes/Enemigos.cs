using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BetaFoesAndBones.Controles.Disparo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace BetaFoesAndBones.Personajes
{
    internal class Enemigos : Componentes
    {
        float tiempoTranscurridoSlime;
        float tiempoTranscurridoBacteriano;
        float tiempoTranscurridoDraconiano;
        float daño;
        public Vector2 felix_posicion;
        private Vector2 muerte;
        private int murio = 0;
        public Random random = new Random();
        public List<Magia> proyectilesE;
        public int puntos;
        public List<Enemigo> enemigos = new List<Enemigo>();
        Texture2D slimeTextura;
        Texture2D bacterianoTextura;
        Texture2D draconanioTextura;
        Texture2D explosion;
        AnimationManager ab;

        public Enemigos(Game1 game,ContentManager contenedor) {
            _game = game;
            _content = contenedor;
            slimeTextura = _content.Load<Texture2D>("Enemigos/Slimep");
            bacterianoTextura = _content.Load<Texture2D>("Enemigos/bacte_movimiento");
            draconanioTextura = _content.Load<Texture2D>("Enemigos/Draconariop");
            explosion = _content.Load<Texture2D>("Enemigos/explosion");
            tiempoTranscurridoSlime = 0f;
            tiempoTranscurridoBacteriano = 0f;
            tiempoTranscurridoDraconiano = 0f;
            felix_posicion = new Vector2(300,300);
            puntos = 0;

            ab = new(5, 5, new System.Numerics.Vector2(165, 230));
        }
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            foreach (var enemigo in enemigos)
            {
                if (enemigo.HP == 60) 
                    sprite.Draw(enemigo.Textura, enemigo.Posicion, ab.GetFrame(), enemigo.ColorE);
                else 
                {
                    sprite.Draw(enemigo.Textura, enemigo.Posicion, enemigo.ColorE);
                }
            }
            if(murio == 1)
            {
                sprite.Draw(explosion,new Rectangle((int)muerte.X, (int)muerte.Y, 100,100), Color.White);
            }
            if(daño >= 0.2)
            {
                daño = 0;
                murio = 0;
            }
        }

        public override void Update(GameTime gameTime)
        {
            ab.Update();
            tiempoTranscurridoSlime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tiempoTranscurridoBacteriano += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tiempoTranscurridoDraconiano += (float)gameTime.ElapsedGameTime.TotalSeconds;
            daño += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (tiempoTranscurridoSlime >= 3f) 
            {
                Enemigo nuevoSlime = null;
                do
                {
                    nuevoSlime = new Slime(slimeTextura, new Vector2(random.Next(0, _game.w), random.Next(0, _game.h)));
                } while (Vector2.Distance(nuevoSlime.Posicion, felix_posicion) < 250);
                enemigos.Add(nuevoSlime);
                tiempoTranscurridoSlime = 0f;
            }        
            if (tiempoTranscurridoBacteriano >= 7f) 
            {
                Enemigo nuevoBacteriano = null;
                do
                {
                    nuevoBacteriano = new Bacteriano(bacterianoTextura, new Vector2(random.Next(0, _game.w), random.Next(0, _game.h)));
                } while (Vector2.Distance(nuevoBacteriano.Posicion, felix_posicion) < 250);
                enemigos.Add(nuevoBacteriano);
                tiempoTranscurridoBacteriano = 0f;
            }        
            if (tiempoTranscurridoDraconiano >= 20f) 
            {
                Enemigo nuevoDraconario = null;
                do
                {
                    nuevoDraconario = new Draconario(draconanioTextura, new Vector2(random.Next(0, _game.w), random.Next(0, _game.h)));
                } while (Vector2.Distance(nuevoDraconario.Posicion, felix_posicion) < 300);
                enemigos.Add(nuevoDraconario);
                tiempoTranscurridoDraconiano = 0f;
            }
            foreach (var enemigo in enemigos)
            {
                enemigo.Update(gameTime, felix_posicion, _game.w, _game.h);
            }
            foreach (Enemigo enemy in enemigos.ToList())
            {
                if (daño >= 0.2 && enemy.ColorE == Color.Red)
                {
                    daño = 0f;
                    enemy.ColorE = Color.White;
                }
                foreach (Magia p in proyectilesE.ToList())
                {
                    if (new Rectangle((int)p.Posicion.X, (int)p.Posicion.Y, (int)p.Textura.Width, (int)p.Textura.Height).Intersects(new Rectangle((int)enemy.Posicion.X, (int)enemy.Posicion.Y, (int)enemy.Tamaño.X, (int)enemy.Tamaño.X)))
                    {
                        proyectilesE.Remove(p);
                        daño = 0f;
                        enemy.HP -= p.Daño;
                        enemy.ColorE = Color.Red;
                    }
                    if (enemy.HP < 0 || enemy.HP == 0)
                    {
                        murio = 1;
                        puntos += (int)enemy.Puntos;
                        muerte = enemy.Posicion;
                        enemigos.Remove(enemy);
                    }
                }
            }

        }
    }
}
