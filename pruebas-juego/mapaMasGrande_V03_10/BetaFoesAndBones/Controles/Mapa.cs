using BetaFoesAndBones.Personajes;
using Microsoft.Xna.Framework;
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
        public int posicionParedes;
        public bool cam;
        public int cambio;
        public int numCambioH;
        public int numCambioV;
        public int H;
        public int V;
        public bool mostrarPuertas;

        private Dictionary<Vector2, int> tilemap;

        public Dictionary<Vector2, int> coli;

        public Dictionary<Vector2, int> habitaciones;

        private List<Rectangle> textureStore;

        private Texture2D textureAtlas;

        private Vector2 _position;

        private Vector2 _velocity;
        
        public Mapa(ContentManager contenedor) 
        {
            cambio = 0;
            cam = false;
            numCambioH = 0;
            numCambioV = 0;
            H = 0;
            V = 0;
            mostrarPuertas = true;

            _content = contenedor;
            textureAtlas = _content.Load<Texture2D>("Tiles-SandstoneDungeons");
            tilemap = loadMap("../../../Data/colis_piso.csv");
            coli = loadMap("../../../Data/colis_colis.csv");
            habitaciones = loadMap("../../../Data/colis_hab.csv");
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

                    ((int)item.Key.X * 93) + 93 - numCambioH - H,
                    ((int)item.Key.Y * 97) + numCambioV - (11 * 97),
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
            //foreach (var item in habitaciones)
            //{
            //    Rectangle dest = new(

            //        ((int)item.Key.X * 93) + 93 - numCambioH - H,
            //        ((int)item.Key.Y * 97) + numCambioV - (11 * 97),
            //        93,
            //        97
            //     );
            //    int x = item.Value % num_tiles_per_row;
            //    int y = item.Value / num_tiles_per_row;
            //    Rectangle src = new(
            //        x * pixel_tilesize,
            //        y * pixel_tilesize,
            //        pixel_tilesize,
            //        pixel_tilesize
            //    );

            //    sprite.Draw(textureAtlas, dest, src, Color.White);
            //}
            if (mostrarPuertas)
            {
                foreach (var item in coli)
                {
                    Rectangle dest = new(

                        ((int)item.Key.X * 93) + 93 - numCambioH - H,
                        ((int)item.Key.Y * 97) + numCambioV - (11 * 97),
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

        }

        public override void Update(GameTime gameTime)
        {
            // Cambio de habitaciones
            if (cambio == 1)
            {
                numCambioH += 21 * 93;
                cam = true;
            }
            if (cambio == 2)
            {
                numCambioH -= 21 * 93;
                cam = true;
            }
            if (cambio == 3)
            {
                numCambioV -= 13 * 97;
                cam = true;
            }
            if (cambio == 4)
            {
                numCambioV += 13 * 97;
                cam = true;
            }
            if (cambio == 5)
            {
                
                
                if (posicionParedes * 93 != numCambioH)
                {
                    numCambioH = posicionParedes * 93;
                    H = 0;
                }
                else if(H <= 100) H += 7;
                if (H >= 90)
                {
                    numCambioH = posicionParedes * 93;
                    H = 93;
                }
                cam = true;
            }
            if (cambio == 6)
            {
                
                if (posicionParedes * 93 != numCambioH)
                {
                    numCambioH = posicionParedes*93;
                    H = 93;
                }
                else H -= 7;
                if (H < 0)
                {
                    numCambioH = posicionParedes * 93;
                    H = 0;
                }
                cam = true;
            }
            if (cambio == 9)
            {
                H = 0;
                if ((numCambioH / 93) != posicionParedes)
                {
                    numCambioH = posicionParedes * 93;
                }
            }
            //if (cambio == 12)
            //{
            //    numCambioH = posicionParedes;
            //    H = 93;
            //    cam = true;

            //}
            //if (cambio == 11)
            //{
            //    numCambioH += 93;
            //    H = 0;
            //    cam = true;
            //}
            if (cambio == 10)
            {
                    H = 0;
                    if ((numCambioH / 93) != posicionParedes)
                    {
                        numCambioH = posicionParedes * 93;
                    }
            }


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
