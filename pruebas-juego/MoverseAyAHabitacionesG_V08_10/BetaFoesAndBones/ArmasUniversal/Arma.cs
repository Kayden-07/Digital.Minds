using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BetaFoesAndBones.ArmasUniversal
{
    public abstract class Arma
    {
        private Texture2D texturaArma;
        private Vector2 posicionArma;
        private Vector2 tamañoArma;
        private Vector2 direccionArma;
        private float dañoMelee;
        private float dañoDistancia;
        public Vector2 DireccionArma
        {
            get { return direccionArma; }
            set { direccionArma = value; }
        }
        public Vector2 TamañoArma
        {
            get { return tamañoArma; }
            set { tamañoArma = value; }
        }
        public Texture2D TexturaArma
        {
            get { return texturaArma; }
            set { texturaArma = value; }
        }
        public Vector2 PosicionArma
        {
            get { return posicionArma; }
            set { posicionArma = value; }
        }
        public float DañoMelee
        {
            get { return dañoMelee; }
            set { dañoMelee = value; }
        }
        public float DañoDistancia
        {
            get { return dañoMelee; }
            set { dañoDistancia = value; }
        }

        public Arma(Texture2D texturaArma, Vector2 posicionArma, Vector2 tamañoArma, Vector2 direccionArma,float dañoMelee, float dañoDistancia)
        {
            this.tamañoArma = tamañoArma;
            this.texturaArma = texturaArma;
            this.posicionArma = posicionArma;
            this.dañoMelee = dañoMelee;
            this.dañoDistancia = dañoDistancia;
            this.direccionArma = direccionArma;
        }

        public void Actualizar(GameTime gameTime)
        {
            posicionArma += direccionArma * 10 * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }
    }

    public class Garrote : Arma
    {
        public Garrote(Texture2D texturaArma, Vector2 posicionArma, Vector2 direccionArma) : base(texturaArma, posicionArma, new Vector2(0,0), new Vector2(150, 230), 3, 0)
        {

        }
    }

}
