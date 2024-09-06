using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BetaFoesAndBones.Armas
{
    public abstract class Arma
    {
        private Texture2D texturaArma;
        private Vector2 posicionArma;
        private float dañoMelee;
        private float dañoDistancia;
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

        public Arma(Texture2D texturaArma, Vector2 posicionArma, float dañoMelee, float dañoDistancia)
        {
            this.TexturaArma = texturaArma;
            this.PosicionArma = posicionArma;
            this.DañoMelee = dañoMelee;
            this.DañoDistancia = dañoDistancia;
        }
    }

    public class Garrote : Arma
    {
        public Garrote(Texture2D texturaArma, Vector2 posicionArma) : base(texturaArma, posicionArma, 3, 0)
        {

        }
    }

}
