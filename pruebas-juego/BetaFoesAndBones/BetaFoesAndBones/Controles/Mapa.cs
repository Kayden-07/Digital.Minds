﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace BetaFoesAndBones.Controles
{
    internal class Mapa : Componentes
    {

        private Dictionary<Vector2, int> tilemap;

        public Dictionary<Vector2, int> coli;

        private List<Rectangle> textureStore;

        private Texture2D textureAtlas;

        private Vector2 _position;

        private Vector2 _velocity;


        
        public Mapa(ContentManager contenedor) 
        {
            _content = contenedor;
            textureAtlas = _content.Load<Texture2D>("Tiles-SandstoneDungeons");
            tilemap = loadMap("../../../Data/habitacionMazmorra.csv");
            coli = loadMap("../../../Data/colisiones.csv");
            textureStore = new() {
                new Rectangle(0,0,8,8),
                new Rectangle(0,8,8,8),
            };
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            int num_tiles_per_row = 9;
            int pixel_tilesize = 32;
            foreach (var item in tilemap)
            {
                Rectangle dest = new(

                    ((int)item.Key.X * 93) + 93,
                    ((int)item.Key.Y * 97),
                    93,
                    97
                 );
                int x = item.Value % num_tiles_per_row;
                int y = item.Value / num_tiles_per_row;
                Rectangle src = new(
                    x * pixel_tilesize,
                    y * pixel_tilesize,
                    pixel_tilesize,
                    pixel_tilesize
                );

                sprite.Draw(textureAtlas, dest, src, Color.White);
            }
            foreach (var item in coli)
            {
                Rectangle dest = new(

                    ((int)item.Key.X * 93) + 93,
                    ((int)item.Key.Y * 97),
                    93,
                    97
                );
                int x = item.Value % num_tiles_per_row;
                int y = item.Value / num_tiles_per_row;
                Rectangle src = new(
                    x * pixel_tilesize,
                    y * pixel_tilesize,
                    pixel_tilesize,
                    pixel_tilesize
                    );

                sprite.Draw(textureAtlas, dest, src, Color.White);
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            //_position = posicion;
            //_velocity = velocidad;
            
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
        
    }
}
