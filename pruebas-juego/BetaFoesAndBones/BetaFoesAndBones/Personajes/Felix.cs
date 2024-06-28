using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones.Personajes
{
    internal class Felix : Componentes
    {

        private Texture2D[] felix;

        public Vector2 _position;
        public Vector2 _velocity;
        public Vector2 _tamaño;
        AnimationManager am;
        AnimationManager pm;
        private int activo;

        private List<Rectangle> intersections;

        private Texture2D cuadrado;

        private int tilesTamaño = 93;

        private Dictionary<Vector2, int> coli;
        public Felix(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor, Dictionary<Vector2, int> _coli) 
        {
            coli = _coli;
            _content = contenedor;
            felix = new Texture2D[5];

            felix[0] = _content.Load<Texture2D>("Felix/derecha_Felix_v2");
            felix[1] = _content.Load<Texture2D>("Felix/izquierda_felix_v2");
            felix[2] = _content.Load<Texture2D>("Felix/arriba_felix_v2");
            felix[3] = _content.Load<Texture2D>("Felix/abajo_v2");
            felix[4] = _content.Load<Texture2D>("Felix/Risas_felix");
            //_position = posicion;
            //_tamaño = tamaño;
            cuadrado = _content.Load<Texture2D>("Controles/boton");
            _position = new Vector2(240, 240);
            _velocity = new Vector2(40, 40);

            am = new(7, 7, new System.Numerics.Vector2(310, 215));
            pm = new(7, 7, new System.Numerics.Vector2(315, 215));

            intersections = new List<Rectangle>();
        }
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            //foreach (var reac in intersections)
            //{
            //    sprite.Draw(cuadrado, new Rectangle(
            //        reac.X * tilesTamaño,
            //        reac.Y * tilesTamaño,
            //        tilesTamaño,
            //        tilesTamaño
            //        ), Color.Red
            //        );
            //}
            //sprite.Draw(cuadrado,new Rectangle((int)_position.X, (int)_position.Y, 200, 150),Color.White);
            if (activo <= 1) { sprite.Draw(felix[activo],
                new Rectangle((int)_position.X, (int)_position.Y, 200, 150),
                am.GetFrame(),
                Color.White);
            }
            else
            {
                sprite.Draw(felix[activo],
                new Rectangle((int)_position.X, (int)_position.Y, 200, 150),
                pm.GetFrame(),
                Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _velocity = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _velocity.Y = -5;
                activo = 2;
                am.Update();
                pm.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _velocity.Y = 5;
                activo = 3;
                am.Update();
                pm.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _velocity.X = -5;
                activo = 1;
                pm.Update();
                am.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _velocity.X = 5;
                activo = 0;
                pm.Update();
                am.Update();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                activo = 4;
                pm.Update();
                am.Update();
            }
            // Intersicciones
            Vector2 temp = _position;

            _position.X += _velocity.X;
            intersections = getIntersectingTilesHorizontal(new Rectangle((int)_position.X, (int)_position.Y, 200, 150));

            
            foreach (var reac in intersections)
            {
                if (coli.TryGetValue(new Vector2(reac.X -1, reac.Y), out int _val))
                {
                    _position = temp; 
                }
            }
            _position.Y += _velocity.Y;
            intersections = getIntersectingTilesVertical(new Rectangle((int)_position.X, (int)_position.Y, 200, 150));

            foreach (var reac in intersections)
            {
                if (coli.TryGetValue(new Vector2(reac.X -1, reac.Y), out int _val))
                {
                    Rectangle colisions = new Rectangle(
                        reac.X * tilesTamaño,
                        reac.Y * tilesTamaño,
                        tilesTamaño,
                        tilesTamaño
                        );

                    if (_velocity.Y > 0.0f)
                    {
                        _position.Y = colisions.Top - 93;
                    }
                    else if (_velocity.Y < 0.0f)
                    {
                        _position.Y = colisions.Bottom;
                    }
                }
            }
        }
        public List<Rectangle> getIntersectingTilesHorizontal(Rectangle target)
        {

            List<Rectangle> intersections = new List<Rectangle>();

            int withTiles = (target.Width - (target.Width % tilesTamaño)) / tilesTamaño;
            int heightTiles = (target.Height - (target.Height % tilesTamaño)) / tilesTamaño;

            for (int x = 0; x <= withTiles; x++)
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
        public List<Rectangle> getIntersectingTilesVertical(Rectangle target)
        {

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
    }
}
