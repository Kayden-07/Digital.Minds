using MenuInicioJuego.Personajes;
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

namespace MenuInicioJuego.Vistas
{
    internal class VistaJuego : Vista
    {
        Texture2D texture;

        private Dictionary<Vector2, int> tilemap;

        private List<Rectangle> textureStore;

        private Texture2D textureAtlas;

        private int tilesTamaño = 32;
        private Felix felix;
        public VistaJuego(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            textureAtlas = _content.Load<Texture2D>("Tiles-SandstoneDungeons");
            texture = _content.Load<Texture2D>("Controles/boton");
            tilemap = loadMap("../../../Data/habitacionMazmorra.csv");
            textureStore = new() {
                new Rectangle(0,0,8,8),
                new Rectangle(0,8,8,8),
            };
            felix = new Felix(game, graphicsDevice, contenedor);

        }

        private Dictionary<Vector2, int> loadMap(string filepath)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > -1)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
                y++;
            }
            return result;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            int num_tiles_per_row = 9;
            int pixel_tilesize = 32;
            foreach (var item in tilemap)
            {
                Rectangle dest = new(

                     ((int)item.Key.X * 96) + 30,
                     ((int)item.Key.Y * 110) + 80,
                     96,
                     110
                 );
                int x = item.Value % num_tiles_per_row;
                int y = item.Value / num_tiles_per_row;
                Rectangle src = new(
                    x * pixel_tilesize,
                    y * pixel_tilesize,
                    pixel_tilesize,
                    pixel_tilesize
                    );

                spriteBatch.Draw(textureAtlas, dest, src, Color.White);
            }
            felix.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            felix.Update(gameTime);
        }
    }
}
