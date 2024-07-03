using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace BetaFoesAndBones.Personajes
{
    internal class Enemigos : Componentes
    {
        float tiempoTranscurridoSlime;
        float tiempoTranscurridoBacteriano;
        float tiempoTranscurridoDraconiano;
        public Vector2 felix_posicion;
        public Random random = new Random();

        public List<Enemigo> enemigos = new List<Enemigo>();
        Texture2D slimeTextura;
        Texture2D bacterianoTextura;
        public Enemigos(Game1 game,ContentManager contenedor) {
            _game = game;
            _content = contenedor;
            slimeTextura = _content.Load<Texture2D>("Controles/boton");
            bacterianoTextura = _content.Load<Texture2D>("Controles/boton");
            tiempoTranscurridoSlime = 0f;
            tiempoTranscurridoBacteriano = 0f;
            tiempoTranscurridoDraconiano = 0f;
            felix_posicion = new Vector2(300,300);
        }
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            foreach (var enemigo in enemigos)
            {
                sprite.Draw(enemigo.Textura, enemigo.Posicion, Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            tiempoTranscurridoSlime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tiempoTranscurridoBacteriano += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tiempoTranscurridoDraconiano += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (tiempoTranscurridoSlime >= 5f) 
            {
                Enemigo nuevoSlime = null;
                do
                {
                    nuevoSlime = new Slime(slimeTextura, new Vector2(random.Next(0, _game.w), random.Next(0, _game.h)));
                } while (Vector2.Distance(nuevoSlime.Posicion, felix_posicion) < 250);
                enemigos.Add(nuevoSlime);
                tiempoTranscurridoSlime = 0f;
            }        
            if (tiempoTranscurridoBacteriano >= 10f) 
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
                tiempoTranscurridoDraconiano = 0;
            }
            foreach (var enemigo in enemigos)
            {
                enemigo.Update(gameTime, felix_posicion, _game.w, _game.h);
            }
        }
    }
}
