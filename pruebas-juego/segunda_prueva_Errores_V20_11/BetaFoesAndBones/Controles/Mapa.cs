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
        public int posicionParedesH;
        public int posicionParedesV;
        public bool cam;
        public int cambio;
        public int cambioV;
        public int cambioH;
        public int numCambioH;
        public int numCambioV;
        public int numCambioVNegativo;
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
            cambioV = 0;
            cambioH = 0;
            cam = false;
            numCambioH = 0;
            numCambioV = (11 * 97);
            numCambioVNegativo = -numCambioV;
            H = 0;
            V = 0;
            mostrarPuertas = true;

            _content = contenedor;
            textureAtlas = _content.Load<Texture2D>("Tiles-SandstoneDungeons");
            tilemap = loadMap("Data/jefe_piso.csv");
            coli = loadMap("Data/jefe_colis.csv");
            habitaciones = loadMap("Data/jefe_hab.csv");
            textureStore = new() {
                new Rectangle(0,0,8,8),
                new Rectangle(0,8,8,8),
            };
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            numCambioVNegativo = -numCambioV;
            int num_tiles_per_row = 9;
            int pixel_tilesize = 32;
            
            foreach (var item in tilemap)
            {
                Rectangle dest = new(

                    ((int)item.Key.X * 93) + 93 - numCambioH - H,
                    ((int)item.Key.Y * 97) + numCambioVNegativo - V,
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
           
            if (mostrarPuertas)
            {
                foreach (var item in coli)
                {
                    Rectangle dest = new(

                        ((int)item.Key.X * 93) + 93 - numCambioH - H,
                        ((int)item.Key.Y * 97) + numCambioVNegativo - V,
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
            if (cambioH == 5)
            {
                if (posicionParedesH * 93 != numCambioH)
                {
                    numCambioH = posicionParedesH * 93;
                    H = 0;
                }
                else if(H <= 100) H += 7;
                if (H >= 90)
                {
                    numCambioH = posicionParedesH * 93;
                    H = 93;
                }
                cam = true;
            }
            if (cambioH == 6)
            {
                
                if (posicionParedesH * 93 != numCambioH)
                {
                    numCambioH = posicionParedesH*93;
                    H = 93;
                }
                else H -= 7;
                if (H < 0)
                {
                    numCambioH = posicionParedesH * 93;
                    H = 0;
                }
                cam = true;
            }
            if (cambioV == 7)
            {
                if (posicionParedesV * 97 != numCambioV)
                {
                    numCambioV = posicionParedesV * 97;
                    V = 0;
                }
                else if (H <= 100) V += 7;
                if (V >= 92)
                {
                    numCambioV = posicionParedesV * 97;
                    V = 97;
                }
                cam = true;
            }
            if (cambioV == 8)
            {

                if (posicionParedesV * 97 != numCambioV)
                {
                    numCambioV = posicionParedesV * 97;
                    V = 97;
                }
                else V -= 7;
                if (V < 0)
                {
                    numCambioV = posicionParedesV * 97;
                    V = 0;
                }
                cam = true;
            }
            if (cambioH == 9)
            {
                H = 0;
                if ((numCambioH / 93) != posicionParedesH)
                {
                    numCambioH = posicionParedesH * 93;
                }
            }
            if (cambioH == 10)
            {
                    H = 0;
                    if ((numCambioH / 93) != posicionParedesH)
                    {
                        numCambioH = posicionParedesH * 93;
                    }
            }
            if (cambioV == 11)
            {
                V = 0;
                if ((numCambioV / 97) != posicionParedesV)
                {
                    numCambioV = posicionParedesV * 97;
                }
            }
            if (cambioV == 12)
            {
                V = 0;
                if ((numCambioV / 97) != posicionParedesV)
                {
                    numCambioV = posicionParedesV * 97;
                }
            }

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
