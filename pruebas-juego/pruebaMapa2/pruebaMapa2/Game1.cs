using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace pruebaMapa2
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<Vector2, int> tilemap;

        private Dictionary<Vector2, int> coli;

        private List<Rectangle> textureStore;

        private Texture2D textureAtlas;

        private Texture2D felix;
        private Texture2D cuadrado;

        private Vector2 _position;

        private Vector2 _velocity;

        private int tilesTamaño = 32;

        private List<Rectangle> intersections;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            tilemap = loadMap("../../../Data/mapa_Cap1.csv");
            coli = loadMap("../../../Data/mapa_colligions.csv");
            textureStore = new() {
                new Rectangle(0,0,8,8),
                new Rectangle(0,8,8,8),
            };
            intersections = new List<Rectangle>();
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

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textureAtlas = Content.Load<Texture2D>("mapa");

            felix = Content.Load<Texture2D>("Skeletuschiquito");

            cuadrado = Content.Load<Texture2D>("cuadraro");

            _position = new Vector2(40, 40);
            _velocity = new Vector2(40, 40);

            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            _velocity = Vector2.Zero;
            //if (Keyboard.GetState().IsKeyDown(Keys.W)) { _position.Y -= 3; }
            //if (Keyboard.GetState().IsKeyDown(Keys.S)) { _position.Y += 3; }
            //if (Keyboard.GetState().IsKeyDown(Keys.A)) { _position.X -= 3; }
            //if (Keyboard.GetState().IsKeyDown(Keys.D)) { _position.X += 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.W)) { _velocity.Y = -3; }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { _velocity.Y = 3; }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) { _velocity.X = -3; }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) { _velocity.X = 3; }

            _position.X += _velocity.X;
            intersections = getIntersectingTilesHorizontal(new Rectangle((int)_position.X, (int)_position.Y, 40, 40));

            foreach (var reac in intersections)
            {
                if (coli.TryGetValue(new Vector2(reac.X, reac.Y), out int _val))
                {
                    Rectangle colisions = new Rectangle(
                        reac.X * tilesTamaño,
                        reac.Y * tilesTamaño,
                        tilesTamaño,
                        tilesTamaño
                        );
                
                    if(_velocity.X > 0.0f)
                    {
                        _position.X = colisions.Left - 32;
                    }
                    else if( _velocity.X < 0.0f)
                    {
                        _position.X = colisions.Right;
                    }
                }
            }

            _position.Y += _velocity.Y;
            intersections = getIntersectingTilesVertical(new Rectangle((int)_position.X, (int)_position.Y, 40, 40));
            
            foreach (var reac in intersections)
            {
                if (coli.TryGetValue(new Vector2(reac.X, reac.Y), out int _val))
                {
                    Rectangle colisions = new Rectangle(
                        reac.X * tilesTamaño,
                        reac.Y * tilesTamaño,
                        tilesTamaño,
                        tilesTamaño
                        );

                    if (_velocity.Y > 0.0f)
                    {
                        _position.Y = colisions.Top - 32;
                    }
                    else if (_velocity.Y < 0.0f)
                    {
                        _position.Y = colisions.Bottom;
                    }
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            

            base.Update(gameTime);
        }

        public List<Rectangle> getIntersectingTilesHorizontal(Rectangle target) { 
        
            List<Rectangle> intersections = new List<Rectangle>();

            int withTiles = (target.Width - (target.Width % tilesTamaño)) / tilesTamaño;
            int heightTiles = (target.Height - (target.Height % tilesTamaño)) / tilesTamaño;
            
            for(int x = 0; x <=withTiles; x++)
            {
                for (int y = 0; y <= heightTiles; y++)
                {
                    intersections.Add(new Rectangle(
                        (target.X + x * tilesTamaño) / tilesTamaño,
                        (target.Y + y * (tilesTamaño - 1)) / tilesTamaño,
                        tilesTamaño,
                        tilesTamaño
                        )
                        );
                }
            }

            return intersections;
        }
        public List<Rectangle> getIntersectingTilesVertical(Rectangle target) {

            List<Rectangle> intersections = new List<Rectangle>();

            int withTiles = (target.Width - (target.Width % tilesTamaño)) / tilesTamaño;
            int heightTiles = (target.Height - (target.Height % tilesTamaño)) / tilesTamaño;

            for (int x = 0; x <= withTiles; x++)
            {
                for (int y = 0; y <= heightTiles; y++)
                {
                    intersections.Add(new Rectangle(
                        (target.X + x * (tilesTamaño - 1)) / tilesTamaño,
                        (target.Y + y * tilesTamaño) / tilesTamaño,
                        tilesTamaño,
                        tilesTamaño
                        )
                        );
                }
            }

            return intersections;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            int num_tiles_per_row = 16;
            int pixel_tilesize = 16;
            foreach (var item in tilemap)
            {
                Rectangle dest = new(

                    (int)item.Key.X * 32,
                    (int)item.Key.Y * 32,
                    32,
                    32
                );
                int x = item.Value % num_tiles_per_row;
                int y = item.Value / num_tiles_per_row;
                Rectangle src = new(
                    x * pixel_tilesize,
                    y * pixel_tilesize,
                    pixel_tilesize,
                    pixel_tilesize
                    );

                _spriteBatch.Draw(textureAtlas, dest, src, Color.White);
            }

            foreach (var item in coli)
            {
                Rectangle dest = new(

                    (int)item.Key.X * tilesTamaño,
                    (int)item.Key.Y * tilesTamaño,
                    tilesTamaño,
                    tilesTamaño
                );
                int x = item.Value % num_tiles_per_row;
                int y = item.Value / num_tiles_per_row;
                Rectangle src = new(
                    x * pixel_tilesize,
                    y * pixel_tilesize,
                    pixel_tilesize,
                    pixel_tilesize
                    );

                _spriteBatch.Draw(textureAtlas, dest, src, Color.White);
            }

            foreach(var reac in intersections)
            {
                _spriteBatch.Draw(cuadrado, new Rectangle(
                    reac.X * tilesTamaño,
                    reac.Y * tilesTamaño,
                    tilesTamaño,
                    tilesTamaño
                    ), Color.Red
                    );
            }
            _spriteBatch.Draw(felix, new Rectangle((int)_position.X, (int)_position.Y, 40,40), Color.White);
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
