using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuInicio
{
    public abstract class Component
    {
        //Sirve para que las clases que lo hereden se dibujen
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        //Sirve para actualizar
        public abstract void Update(GameTime gameTime);
    }
}
