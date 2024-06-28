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
        Vector2 felix_posicion;
        float felix_velocidad;

        Texture2D magia_textura;
        List<Magia> proyectiles;
        float magia_velocidad;

        MouseState estadoRaton;
        bool disparoRealizado = false;

        public Disparo(ContentManager contenedor, Vector2 posicion)
        {
            _content = contenedor;

            magia_textura = _content.Load<Texture2D>("Controles/boton");
        }
        public class Magia
        {
            private Vector2 posicion;
            public Vector2 Posicion { get { return posicion; } set { posicion = value; } } //"set" permite que otro codigo actualice la variable
            public Vector2 Direccion { get; set; }
            public float Velocidad { get; set; }

            public Magia(Vector2 posicion, Vector2 direccion, float velocidad)
            {
                Posicion = posicion;
                Direccion = direccion;
                Velocidad = velocidad;
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
                sprite.Draw(magia_textura, new Rectangle((int)proyectil.Posicion.X, (int)proyectil.Posicion.Y + 25, 50, 50), Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            estadoRaton = Mouse.GetState();
            if (estadoRaton.LeftButton == ButtonState.Pressed && !disparoRealizado)
            {
                Vector2 direccion = new Vector2(estadoRaton.X, estadoRaton.Y) - felix_posicion; // Calculo dirección de Felix hasta la posición del mouse
                direccion.Normalize();

                Magia nuevoProyectil = new Magia(felix_posicion, direccion, magia_velocidad); // Crea un nuevo objeto Magia
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
