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
        private float dañoMelee;
        private float dañoDistancia;
        private int tiempoAntesDeDesaparecer;
        public int TiempoAntesDeDesaparecer
        {
            get { return tiempoAntesDeDesaparecer; }
            set { tiempoAntesDeDesaparecer = value; }
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

        public Arma(Texture2D texturaArma, Vector2 posicionArma, Vector2 tamañoArma, float dañoMelee, float dañoDistancia)
        {
            this.tamañoArma = tamañoArma;
            this.texturaArma = texturaArma;
            this.posicionArma = posicionArma;
            this.dañoMelee = dañoMelee;
            this.dañoDistancia = dañoDistancia;
            tiempoAntesDeDesaparecer = 0;
        }
    }

    public class Garrote : Arma
    {
        public Garrote(Texture2D texturaArma, Vector2 posicionArma) : base(texturaArma, posicionArma, new Vector2(150, 230), 3, 0)
        {

        }
    }
    public class LatigoSlime : Arma
    {
        public LatigoSlime(Texture2D texturaArma, Vector2 posicionArma) : base(texturaArma, posicionArma, new Vector2(80, 100), 10, 0)
        {

        }
    }
    public class BastonBacteriano : Arma
    {
        public BastonBacteriano(Texture2D texturaArma, Vector2 posicionArma) : base(texturaArma, posicionArma, new Vector2(50, 100), 30, 0)
        {

        }
    }
    public class PistolaElvira : Arma
    {
        public PistolaElvira(Texture2D texturaArma, Vector2 posicionArma) : base(texturaArma, posicionArma, new Vector2(90, 60), 30, 0)
        {
            {

            }
        }
    }
}
