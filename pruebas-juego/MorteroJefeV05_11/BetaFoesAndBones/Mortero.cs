using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones
{
    public class Mortero
    {
        private Vector2 posicion;
        private Vector2 objetivo;
        private float velocidad;
        private Texture2D textura;
        private float daño;

        public bool AlcanzoDestino => Vector2.Distance(posicion, objetivo) < 5f; // Verifica si alcanzó el objetivo

        public Mortero(Vector2 posicionInicial, Vector2 posicionObjetivo, float velocidad, Texture2D textura, float daño)
        {
            this.posicion = posicionInicial;
            this.objetivo = posicionObjetivo;
            this.velocidad = velocidad;
            this.textura = textura;
            this.daño = daño;
        }

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

