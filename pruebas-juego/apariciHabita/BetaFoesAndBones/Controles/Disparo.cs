using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace BetaFoesAndBones.Controles
{
    internal class Disparo : Componentes
    {
        private Vector2 felix_posicion;
        public Vector2 Posicion { get { return felix_posicion; } set { felix_posicion = value; } }
        float felix_velocidad;

        public Texture2D magia_textura;
        public List<Magia> proyectiles;
        float magia_velocidad;

        MouseState estadoRaton;
        bool disparoRealizado = false;

        public Disparo(ContentManager contenedor, Vector2 posicion , Game1 game1)
        {
            _content = contenedor;
            _game = game1;
            magia_textura = _content.Load<Texture2D>("Disparos/Disparito");
            
            magia_velocidad = 400f;
            felix_posicion = posicion;

            proyectiles = new List<Magia>();
        }
        public Texture2D Textura { get { return magia_textura; } set { magia_textura = value; } }
        public class Magia
        {
            private Vector2 posicion;
            private Vector2 direccion;
            private float velocidad;
            private Texture2D textura;
            private float daño;

            public Vector2 Posicion
            {
                get { return posicion; }
                set { posicion = value; }
            }
            public Vector2 Direccion
            {
                get { return direccion; }
                set { direccion = value; }
            }
            public float Velocidad
            {
                get { return velocidad; }
                set { velocidad = value; }
            }
            public Texture2D Textura
            {
                get { return textura; }
                set { textura = value; }
            }
            public float Daño
            {
                get { return daño; }
                set { daño = value; }
            }
            public Magia(Vector2 posicion, Vector2 direccion, float velocidad, Texture2D textura, float daño)
            {
                Posicion = posicion;
                Direccion = direccion;
                Velocidad = velocidad;
                Textura = textura;
                Daño = daño;

            }

            public void Actualizar(GameTime gameTime)
            {
                Posicion += Direccion * Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }




        }
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            foreach (Magia proyectil in proyectiles)
            {
                //_spriteBatch.Draw(magia_textura, (proyectil.Posicion.X, proyectil.Posicion.Y), Color.White);
                sprite.Draw(magia_textura, new Rectangle((int)proyectil.Posicion.X + 25, (int)proyectil.Posicion.Y + 25, 50, 50), Color.White);
            }
        }
        public override void Update(GameTime gameTime)
        {
            
            estadoRaton = Mouse.GetState();
            if (estadoRaton.LeftButton == ButtonState.Pressed && !disparoRealizado)
            {
                Vector2 direccion = new Vector2(estadoRaton.X, estadoRaton.Y) - felix_posicion; // Calculo dirección de Felix hasta la posición del mouse
                direccion.Normalize();

                Magia nuevoProyectil = new Magia(felix_posicion, direccion, magia_velocidad,magia_textura, 20); // Crea un nuevo objeto Magia
                proyectiles.Add(nuevoProyectil);
                disparoRealizado = true;
            }

            if (estadoRaton.LeftButton == ButtonState.Released)
            {
                disparoRealizado = false;
            }

            foreach (Magia proyectil in proyectiles)
            {
                proyectil.Actualizar(gameTime);
            }
        }

    }
}
