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
    public class VistaPerdiste : Vista
    {
        private SpriteFont _fuen;
        private string texto;
        private int y;
        private int x;
        public VistaPerdiste(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            _fuen = _content.Load<SpriteFont>("Fuentes/arial");
            texto = "Perdiste";
            x = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - texto.Length ;
            y = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(_fuen, texto, new Vector2(x, y), Color.White);
            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
