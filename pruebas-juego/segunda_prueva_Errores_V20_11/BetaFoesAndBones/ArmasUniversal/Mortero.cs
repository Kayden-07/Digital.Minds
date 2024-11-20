using BetaFoesAndBones.Personajes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones.ArmasUniversal
{
    internal class Mortero : Enemigo
    {
        private Vector2 posicion;
        private Vector2 objetivo;
        private float velocidad;
        private Texture2D textura;
        private float daño;

        public Mortero(Vector2 posicionObjetivo, Texture2D textura, Vector2 posicion, float velocidad, float tiempoAparicion, float hp, int puntos, Vector2 tamaño, int dañoEnemigo, int id) : base(textura, posicion, velocidad, tiempoAparicion, hp, puntos, tamaño, dañoEnemigo, id)
        {
            this.posicion = posicion;
            this.objetivo = posicionObjetivo;
            this.textura = textura;
            this.velocidad = 200;
        }

        public bool AlcanzoDestino => Vector2.Distance(posicion, objetivo) < 50f; // Verifica si alcanzó el objetivo

        //public Mortero(Vector2 posicionInicial, Vector2 posicionObjetivo, float velocidad, Texture2D textura, float daño)
        //{
        //    this.posicion = posicionInicial;
        //    this.objetivo = posicionObjetivo;
        //    this.velocidad = velocidad;
        //    this.textura = textura;
        //    this.daño = daño;
        //}

        public void Actualizar(GameTime gameTime)
        {
            Vector2 direccion = Vector2.Normalize(objetivo - posicion);
            posicion += direccion * velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, Color.White);
        }
    }
}
