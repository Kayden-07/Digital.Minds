using BetaFoesAndBones.Controles;
using BetaFoesAndBones.Personajes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones.Vistas
{
    internal class VistaGanaste : Vista
    {
        SpriteFont _fuen;
        private Texture2D cuadrado;
        private Texture2D felix;
        private Texture2D fondo;
        private string texto;
        private int w;
        private int h;
        private int y;
        private int x;
        private int felixX;
        private int felixY;
        private Boton botonRestart;

        public VistaGanaste(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            _fuen = _content.Load<SpriteFont>("Fuentes/arial");
            cuadrado = _content.Load<Texture2D>("Controles/boton");
            fondo = _content.Load<Texture2D>("portada_ganaste");
            var botonFuente = _content.Load<SpriteFont>("Fuentes/fuente");
            felix = _content.Load<Texture2D>("Controles/felix_sticker");
            texto = "GANASTE!";
            w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            felixX = (w / 2) - (felix.Width / 2);
            felixY = (h / 2) - (felix.Height / 2) + 100;
            x = (w / 2) - texto.Length - 100;
            y = (h / 2) - 150;
            botonRestart = new Boton(cuadrado, botonFuente, 1)
            {
                Posicion = new Vector2(x - 50, y + 450),
                Texto = "Volver al menu",
            };
            botonRestart.Click += BotonRestart_Click;
        }

        private void BotonRestart_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new VistaMenu(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(fondo, new Rectangle(0, 0, w, h), Color.White);
            botonRestart.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(felix, new Rectangle(felixX, felixY, 500, 300), Color.White);
            spriteBatch.DrawString(_fuen, texto, new Vector2(x, y), Color.Black);
            spriteBatch.Draw(cuadrado, new Rectangle(-20, 80, w + 50, 150), Color.BurlyWood);
            spriteBatch.DrawString(_fuen, "Felicidades, ahora felix va a poder tener una vida feliz y tranquila fuera de la mazmorra", new Vector2(50, 100), Color.Black);
            spriteBatch.DrawString(_fuen, "gracias a ti", new Vector2(50, 150), Color.Black);
            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            botonRestart.Update(gameTime);
            botonRestart.SobreBtn = true;
        }
    }
}
