using BetaFoesAndBones.ArmasUniversal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace BetaFoesAndBones.Personajes
{
    public abstract class Enemigo
    {
        private Texture2D textura;
        private Vector2 posicion;
        private float velocidad;
        private float tiempoAparicion;
        private float hp;
        private int id;
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
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Enemigo(Texture2D textura, Vector2 posicion, float velocidad, float tiempoAparicion, float hp, int puntos, Vector2 tamaño, int dañoEnemigo, int id)
        {
            meAtacaron = false;
            ColorE = Color.White;
            Textura = textura;
            Posicion = posicion;
            Velocidad = velocidad;
            TiempoAparicion = tiempoAparicion;
            HP = hp;
            ID = id;
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
            //Posicion = new Vector2(
            //    MathHelper.Clamp(Posicion.X, 150, windowWidth - 150 - Tamaño.X),
            //    MathHelper.Clamp(Posicion.Y, 50, windowHeight - 100 - Tamaño.Y)
            //);
        }
    }


    public class Slime : Enemigo
    {
        public Slime(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 160f, 3f, 20, 100, new Vector2(100, 100), 5, 1)
        {

        }
    }

    public class Bacteriano : Enemigo
    {
        public Bacteriano(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 100f, 9f, 60, 300, new Vector2(150, 230), 10, 2)
        {

        }
    }

    public class Draconario : Enemigo
    {
        public Draconario(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 80f, 20f, 100, 500, new Vector2(165, 230), 13, 3)
        {
        }
    }
    public class Pinza : Enemigo
    {
        private Texture2D pinzaTex;
        private Texture2D pinzaRotaTex;
        public System.Numerics.Vector2 Origin;
        public float rotation;
        public float colisionX, colisionY;
        public Rectangle colision1;
        public float radious;
        public List<Rectangle> colisions;
        private bool giroArriba;
        public bool reversa;

        public Pinza(Texture2D textura, Texture2D texturaRota, Vector2 posicion, float _rotation, bool _giroArriba)
            : base(textura, posicion, 80f, 2f, 200, 500, new Vector2(500, 200), 3, 4)
        {
            pinzaTex = textura;
            pinzaRotaTex = texturaRota;
            colisions = new List<Rectangle>();
            reversa = false;
            Origin = new System.Numerics.Vector2(0, textura.Height / 2);
            rotation = _rotation;
            giroArriba = _giroArriba;

            for (int i = 1; i <= 4; i++)
            {
                radious = i * 100 + 100;
                colisionX = posicion.X + (float)Math.Cos(rotation) * radious;
                colisionY = posicion.Y - 40 + (float)Math.Sin(rotation) * radious;
                colision1 = new Rectangle((int)colisionX, (int)colisionY, 100, 100);
                colisions.Add(colision1);
            }
        }
        public void Actualizar(GameTime gameTime)
        {
            Textura = (HP <= 0) ? pinzaRotaTex : pinzaTex;
            Tamaño = (HP <= 0) ? new Vector2(200, 100) : new Vector2(500, 200);


            if (giroArriba)
            {
                if (rotation >= 1.5) reversa = false;
                else if (rotation <= 0.2) reversa = true;
                if (reversa) rotation += 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else rotation -= 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                for (int i = 0; i <= 3; i++)
                {
                    radious = i * 100 + 100;
                    colisionX = Posicion.X + (float)Math.Cos(rotation) * radious;
                    colisionY = Posicion.Y - 40 + (float)Math.Sin(rotation) * radious;
                    colision1 = new Rectangle((int)colisionX, (int)colisionY, 100, 100);
                    colisions[i] = colision1;
                }

            }
            else
            {
                if (rotation <= -1.5) reversa = false;
                else if (rotation >= -0.2) reversa = true;

                if (reversa) rotation -= 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else rotation += 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                for (int i = 0; i <= 3; i++)
                {
                    radious = i * 100 + 150;
                    colisionX = Posicion.X + (float)Math.Cos(rotation) * radious;
                    colisionY = Posicion.Y - 40 + (float)Math.Sin(rotation) * radious;
                    colision1 = new Rectangle((int)colisionX, (int)colisionY, 100, 100);
                    colisions[i] = colision1;
                }
            }


        }
    }
    public class CuerpoC : Enemigo
    {
        public CuerpoC(Texture2D textura, Vector2 posicion)
            : base(textura, posicion, 80f, 2f, 400, 500, new Vector2(400, 900), 30, 5)
        {
        }
    }

    public class JefeCangrejo : Enemigo
    {
        public bool DañoExplosion;
        List<Rectangle> areaImpact = new List<Rectangle>();
        private List<Mortero> morteros;
        private Texture2D morteroTextura;
        private Texture2D radioMortero;
        private float tiempoTranscurrido;
        private const float intervaloDisparo = 3f;
        private Vector2 posicionFelix;

        float rotacionPinza1;
        float rotacionPinza2;
        Texture2D colCuerpo;
        public List<Enemigo> partesCangrejo;
        public Pinza pinza1;
        public Pinza pinza2;
        public CuerpoC cuerpo;
        private bool reversa1;
        private bool reversa2;
        public JefeCangrejo(Texture2D cuerpo, Texture2D pinza1, Texture2D pinza1ro, Texture2D pinza2, Texture2D pinza2ro, Texture2D colisionCuadrado, Vector2 posicion, Texture2D texturaMortero, Texture2D radioMortero) : base(cuerpo, posicion, 0f, 5f, 500, 2000, new Vector2(565, 630), 20, 6)
        {
            DañoExplosion = false;
            morteroTextura = texturaMortero;
            rotacionPinza1 = MathHelper.ToRadians(15);
            rotacionPinza2 = MathHelper.ToRadians(-80);
            this.pinza1 = new Pinza(pinza1, pinza1ro, new Vector2((int)posicion.X + 410, (int)posicion.Y + 10), rotacionPinza1, true);
            this.pinza2 = new Pinza(pinza2, pinza2ro, new Vector2((int)posicion.X + 410, (int)posicion.Y + 850), rotacionPinza2, false);
            this.cuerpo = new CuerpoC(cuerpo, new Vector2((int)posicion.X, (int)posicion.Y));
            partesCangrejo = new List<Enemigo>() { this.pinza1, this.pinza2, this.cuerpo };
            colCuerpo = colisionCuadrado;
            reversa1 = false;
            reversa2 = false;

            morteros = new List<Mortero>();
            areaImpact = new List<Rectangle>();
            tiempoTranscurrido = 0f;
            posicionFelix = new Vector2(0, 0);
            this.radioMortero = radioMortero;
        }
        public void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            HP = pinza1.HP + pinza2.HP + cuerpo.HP;
            sprite.Draw(pinza1.Textura, new Rectangle((int)pinza1.Posicion.X, (int)pinza1.Posicion.Y, (int)pinza1.Tamaño.X, (int)pinza1.Tamaño.Y), null, pinza1.ColorE, pinza1.rotation, pinza1.Origin, SpriteEffects.None, 0);
            sprite.Draw(pinza2.Textura, new Rectangle((int)pinza2.Posicion.X, (int)pinza2.Posicion.Y, (int)pinza2.Tamaño.X, (int)pinza2.Tamaño.Y), null, pinza2.ColorE, pinza2.rotation, pinza2.Origin, SpriteEffects.None, 0);
            sprite.Draw(cuerpo.Textura, new Rectangle((int)cuerpo.Posicion.X, (int)cuerpo.Posicion.Y, (int)cuerpo.Tamaño.X, (int)cuerpo.Tamaño.Y), cuerpo.ColorE);
            //foreach (Rectangle colision in pinza1.colisions)
            //    sprite.Draw(colCuerpo, colision, Color.White);
            //foreach (Rectangle colision in pinza2.colisions)
            //    sprite.Draw(colCuerpo, colision, Color.White);
            foreach (var area in areaImpact)
                sprite.Draw(radioMortero, area, Color.Red * 0.30f);
            foreach (var mortero in morteros)
                mortero.Draw(sprite);
           
        }
        public void Actualizar(GameTime gameTime, Vector2 felixPosicion)
        {
            if (DañoExplosion)
                DañoExplosion = false;
            posicionFelix = felixPosicion;

            pinza1.Actualizar(gameTime);
            pinza2.Actualizar(gameTime);

            tiempoTranscurrido += (float)gameTime.ElapsedGameTime.TotalSeconds; //mortero-------------

            if (tiempoTranscurrido >= intervaloDisparo)
            {
                Mortero nuevoMortero = new Mortero(posicionFelix, morteroTextura, new Vector2(posicionFelix.X, -10), 50, 10, 1, 0, new Vector2(50, 50), 20, 6);
                morteros.Add(nuevoMortero);
                areaImpact.Add(new Rectangle((int)posicionFelix.X - 50, (int)posicionFelix.Y - 30, 200, 200));
                tiempoTranscurrido = 0f;
            }
            foreach (var mortero in morteros)
                mortero.Actualizar(gameTime);

            if (morteros.Count != 0)
            {
                for (int i = 0; i < morteros.Count; i++)
                {
                    if (morteros[i].AlcanzoDestino)
                    {
                        if (areaImpact[i].Intersects(new Rectangle((int)posicionFelix.X + 15, (int)posicionFelix.Y, 50, 120)))
                        {
                            DañoExplosion = true;
                        }
                    }
                }
            }

            for (int i = morteros.Count - 1; i >= 0; i--)
            {
                if (morteros[i].AlcanzoDestino)
                    areaImpact.RemoveAt(i);
            }
            morteros.RemoveAll(m => m.AlcanzoDestino); //mortero-------------
        }
    }
}
