using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BetaFoesAndBones.Personajes
{
    public abstract class Enemigo 
    {
        private Texture2D textura;
        private Vector2 posicion;
        private float velocidad;
        private float tiempoAparicion;
       
        public Texture2D Textura
        {
            get { return textura; }
            set { textura = value; }
        }
        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }
        public float Velocidad
        {
            get { return velocidad; }
            set { velocidad = value; }
        }
        public float TiempoAparicion
        {
            get { return tiempoAparicion; }
            set { tiempoAparicion = value; }
        }

        public Enemigo(Texture2D textura, Vector2 posicion, float velocidad, float tiempoAparicion)
        {
            Textura = textura;
            Posicion = posicion;
            Velocidad = velocidad;
            TiempoAparicion = tiempoAparicion;
        }

        public void Update(GameTime gameTime, Vector2 felix_posicion, int windowWidth, int windowHeight)
        {
            var distancia = felix_posicion - Posicion;
            Vector2 direccion = Vector2.Normalize(distancia);
            Posicion += direccion * Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Posicion = new Vector2(
                MathHelper.Clamp(Posicion.X, 0, windowWidth - Textura.Width),
                MathHelper.Clamp(Posicion.Y, 0, windowHeight - Textura.Height)
            );
        }
    }


    public class Slime : Enemigo
    {
        public Slime(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 120f, 3f)
        {
        }
    }

    public class Bacteriano : Enemigo
    {
        public Bacteriano(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 100f, 9f)
        {

        }
    }

    public class Draconario : Enemigo
    {
        public Draconario(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 80f, 20f)
        {
        }
    }
}
