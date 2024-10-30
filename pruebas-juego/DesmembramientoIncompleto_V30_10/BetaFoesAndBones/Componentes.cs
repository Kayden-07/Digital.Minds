using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones
{
    
    public abstract class Componentes
    {
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected Game1 _game;
        public abstract void Draw(GameTime gameTime, SpriteBatch sprite);

        public abstract void Update(GameTime gameTime);
    }
}
