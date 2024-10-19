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
        private int dañoEnemigo;
        private Color colorE;
        public Vector2 temp;
        public bool meAtacaron;
        private Vector2 tamaño;
        private bool enemigoVulnerable = false;
        private float tiempoVulnerable;

        public bool EnemigoVulnerable
        {
            get { return enemigoVulnerable; }
            set { enemigoVulnerable = value; }
        }
        public float TiempoVulnerable
        {
            get { return tiempoVulnerable; }
            set { tiempoVulnerable = value; }
        }
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
        public int DañoEnemigo
        {
            get { return dañoEnemigo; }
            set { dañoEnemigo = value; }
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

        public Enemigo(Texture2D textura, Vector2 posicion, float velocidad, float tiempoAparicion, float hp, int puntos, Vector2 tamaño, int dañoEnemigo)
        {
            meAtacaron = false;
            ColorE = Color.White;
            Textura = textura;
            Posicion = posicion;
            Velocidad = velocidad;
            TiempoAparicion = tiempoAparicion;
            HP = hp;
            Puntos = puntos;
            Tamaño = tamaño;
            DañoEnemigo = dañoEnemigo;
            EnemigoVulnerable = enemigoVulnerable;
            this.tiempoVulnerable = 0;
            this.enemigoVulnerable = false;
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
            : base(textura, posicion, 160f, 3f, 20, 100,new Vector2(100,100), 5)
        {
        }
    }

    public class Bacteriano : Enemigo
    {
        public Bacteriano(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 100f, 9f, 60, 300, new Vector2(150, 230), 10)
        {

        }
    }

    public class Draconario : Enemigo
    {
        public Draconario(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 80f, 20f, 100, 500, new Vector2(165, 230), 13)
        {
        }
    }
    public class Pinza : Enemigo
    {
        public Pinza(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 80f, 2f, 200, 500, new Vector2(700, 300), 30)
        {
        }
    }
    public class CuerpoC : Enemigo
    {
        public CuerpoC(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 80f, 2f, 400, 500, new Vector2(400, 900), 30)
        {
        }
    }

    public class JefeCangrejo : Enemigo
    {
        Texture2D texCuerpo;
        Texture2D texPinza1;
        Texture2D texPinza2;
        Pinza pinza1;
        Pinza pinza2;
        CuerpoC cuerpo;
        public JefeCangrejo(Texture2D cuerpo, Texture2D pinza1, Texture2D pinza2, Vector2 posicion) : base(cuerpo, posicion, 0f, 5f,500, 2000, new Vector2(565, 630), 20)
        {
            this.pinza1 = new Pinza(pinza1, new Vector2((int)posicion.X + 100, (int)posicion.Y + 40));
            this.pinza2 = new Pinza(pinza2, new Vector2((int)posicion.X + 100, (int)posicion.Y + 500));
            this.cuerpo = new CuerpoC(cuerpo, new Vector2((int)posicion.X, (int)posicion.Y));
            this.texCuerpo = cuerpo;
            this.texPinza1 = pinza1;
            this.texPinza2 = pinza2;
        }
        public void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            sprite.Draw(pinza1.Textura, new Rectangle((int)pinza1.Posicion.X, (int)pinza1.Posicion.Y, (int)pinza1.Tamaño.X, (int)pinza1.Tamaño.Y),ColorE);
            sprite.Draw(pinza2.Textura, new Rectangle((int)pinza2.Posicion.X, (int)pinza2.Posicion.Y, (int)pinza2.Tamaño.X, (int)pinza2.Tamaño.Y),ColorE);
            sprite.Draw(cuerpo.Textura, new Rectangle((int)cuerpo.Posicion.X, (int)cuerpo.Posicion.Y, (int)cuerpo.Tamaño.X, (int)cuerpo.Tamaño.Y),ColorE);
        }
    }
}
