using BetaFoesAndBones.Controles;
using BetaFoesAndBones.Personajes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones.Vistas
{
    internal class VistaJuego : Vista
    {
        
        private Felix felix;
        private Mapa Mapa;
        public VistaJuego(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            Mapa = new Mapa(contenedor);
            felix = new Felix(game, graphicsDevice, contenedor, Mapa.coli);
        }

        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Mapa.Draw(gameTime, spriteBatch);
            felix.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            felix.Update(gameTime);
            Mapa.Update(gameTime);
        }
    }
}
