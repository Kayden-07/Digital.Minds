using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones.Controles
{
    public class Boton : Componentes
    {
        private bool yaTOco;
        private bool presionoEspacio;
        //----------Variables-----------
        private MouseState _mouseActual;

        private SpriteFont _fuente;

        private bool _sobreBtn;

        private MouseState _mouseAnterior;

        private Texture2D _textura;

        public EventHandler Click;

        private int _valor;

        //------------Propiedades--------------
        public int Valor { get { return _valor; } }
        public bool SobreBtn { get { return _sobreBtn; } set {  _sobreBtn = value; } }
        public bool Clicked { get; private set; }

        public Color LetraColor { get; set; }

        public Vector2 Posicion {  get; set; }
        
        public String Texto { get; set; }
        public Rectangle Rectangulo
        {
            get { return new Rectangle((int)Posicion.X, (int)Posicion.Y, (Texto.Length > 15) ? Texto.Length * 20 : 300, 100); }
        }


        // ------------Metodos-------------------

        public Boton(Texture2D texture, SpriteFont fuente, int valor)
        {
            yaTOco = false;
            presionoEspacio = false;

            _textura = texture;
            _fuente = fuente;
            LetraColor = Color.White;
            _valor = valor;
            _sobreBtn = false;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.Gray;

            //
            if (_sobreBtn)
                color = Color.Yellow;

            spriteBatch.Draw(_textura, Rectangulo, color);

            //Si el texto del boton no esta vacio
            if (!string.IsNullOrEmpty(Texto))
            {
                // La pocicion digo que sea la misma que el recangulo pero le sumo la mitad del ancho del rectangulo asi esta centrada
                // y despues le resto la mitad del ancho del texto, lo mosmo con la altura.
                var x = (Rectangulo.X + (Rectangulo.Width / 2)) -(_fuente.MeasureString(Texto).X / 2);
                var y = (Rectangulo.Y + (Rectangulo.Height / 2)) -(_fuente.MeasureString(Texto).Y / 2);
            
                spriteBatch.DrawString(_fuente,Texto, new Vector2(x, y), LetraColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            
            _mouseAnterior = _mouseActual;
            _mouseActual = Mouse.GetState();

            // Creo un objeto que es el mouse
            var mouse = new Rectangle(_mouseActual.X, _mouseActual.Y, 1, 1);

            //_sobreBtn = false;

            // Pregunto si el mouse toca al rectangulo del boton
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && _sobreBtn)
            {
                presionoEspacio = true;
                
            }
            if (presionoEspacio) 
            {
                if (Keyboard.GetState().IsKeyUp(Keys.Space) && _sobreBtn)
                {
                    Click?.Invoke(this, new EventArgs());
                    presionoEspacio = false;
                }
            }
            //if (mouse.Intersects(Rectangulo))
            //{
            //    //_sobreBtn = true;
            //    // Hace que cuando suelto click izquiedo se active algo
            //    //if (_mouseActual.LeftButton == ButtonState.Released && _mouseAnterior.LeftButton == ButtonState.Pressed)
            //    //{
            //    //    Click?.Invoke(this, new EventArgs());
            //    //}
                
            //}

        }
    }
}
