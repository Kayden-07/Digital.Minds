using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuInicio.States
{
    public class GameState : State
    {

        Texture2D texture;
        
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            texture = _content.Load<Texture2D>("Controls/boton");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(texture,new Rectangle(200, 200, 150, 50),Color.Aqua);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
