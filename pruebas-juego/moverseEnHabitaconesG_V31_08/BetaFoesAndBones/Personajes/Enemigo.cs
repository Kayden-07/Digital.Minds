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
        private float hp;
        private int puntos;
        private Color colorE;
        public Vector2 temp;
        private Vector2 tamaño;

        public Vector2 Tamaño
        {
            get { return tamaño; }
            set { tamaño = value; }
        }
        public Color ColorE
        {
            get { return colorE; }
            set { colorE = value; }
        }
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
        public int Puntos
        {
            get { return puntos; }
            set { puntos = value; }
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
        public float HP
        {
            get { return hp; }
            set { hp = value; }
        }

        public Enemigo(Texture2D textura, Vector2 posicion, float velocidad, float tiempoAparicion, float hp, int puntos, Vector2 tamaño)
        {
            ColorE = Color.White;
            Textura = textura;
            Posicion = posicion;
            Velocidad = velocidad;
            TiempoAparicion = tiempoAparicion;
            HP = hp;
            Puntos = puntos;
            Tamaño = tamaño;
        }

        public void Update(GameTime gameTime, Vector2 felix_posicion, int windowWidth, int windowHeight)
        {
            var distancia = felix_posicion - Posicion;
            Vector2 direccion = Vector2.Normalize(distancia);
            Posicion += direccion * Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            temp = Posicion - (direccion * Velocidad * (float)0.5);
            Posicion = new Vector2(
                MathHelper.Clamp(Posicion.X, 150, windowWidth - 150 - Tamaño.X),
                MathHelper.Clamp(Posicion.Y, 50, windowHeight - 100 - Tamaño.Y)
            );
        }
    }


    public class Slime : Enemigo
    {
        public Slime(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 160f, 3f, 20, 100,new Vector2(100,100))
        {
        }
    }

    public class Bacteriano : Enemigo
    {
        public Bacteriano(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 100f, 9f, 60, 300, new Vector2(150, 230))
        {

        }
    }

    public class Draconario : Enemigo
    {
        public Draconario(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 80f, 20f, 100, 500, new Vector2(165, 230))
        {
        }
    }
}
