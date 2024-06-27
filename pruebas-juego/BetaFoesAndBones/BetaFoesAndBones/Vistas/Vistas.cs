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
    public abstract class Vista
    {
        // ---------Variables----------
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected Game1 _game;

        //----------Metodos-----------

        public abstract void Draw(GameTime  gameTime, SpriteBatch spriteBatch);

        public abstract void PosUpdate(GameTime gameTime);

        public Vista(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor)
        {
            _content = contenedor;
            _graphicsDevice = graphicsDevice;
            _game = game;
        }

        public abstract void Update(GameTime gameTime);


    }
}
