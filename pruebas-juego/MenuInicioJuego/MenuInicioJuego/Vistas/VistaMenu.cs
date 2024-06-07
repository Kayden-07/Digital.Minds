using MenuInicioJuego.Controles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuInicioJuego.Vistas
{
    public class VistaMenu : Vistas
    {
        List<Componentes> componentes;
        public VistaMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            var botonTexture = _content.Load<Texture2D>("Controles/boton");
            var botonFuente = _content.Load<SpriteFont>("Fuentes/fuente");

            var nuevoJuego = new Boton(botonTexture, botonFuente)
            {
                Posicion = new Vector2(300, 200),
                Texto = "New Game",
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void PosUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
