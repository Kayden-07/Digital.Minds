using BetaFoesAndBones.Controles;
using BetaFoesAndBones.Vistas;
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
using static BetaFoesAndBones.Controles.Disparo;

namespace BetaFoesAndBones.Personajes
{
    internal class Felix : Componentes
    {
        private int w;
        private int b = 0;
        private bool centro = false;
        private bool centro2 = false;
        private bool izq = false;
        private bool der = false;

        public Rectangle cuadradoFelix;
        public int cambioH;
        private int cambioV;
        public  int Mapa;
        public int habitacion;
        private int cambioHab;
        public int habAnterior;
        private bool derecha;
        private bool izquierda;
        private bool arriba;
        private bool abajo;
        private float top;

        public Disparo disparo;
        private Texture2D[] felix;
        private Rectangle rFelix;
        private Rectangle rCuerpo;
        private Color colorF;

        public Vector2 _position;
        public Vector2 _velocity;
        public Vector2 _tamaño;

        public List<Enemigo> enemigoList;
        public int vida = 100;

        AnimationManager am;
        AnimationManager pm;
        AnimationManager pp;

        private int activo;
        private float daño;

        private List<Rectangle> intersections;

        private Texture2D cuadrado;

        private int tilesTamaño = 93;

        private Dictionary<Vector2, int> coli;
        private Dictionary<Vector2, int> habitacines;
        public Felix(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor, Dictionary<Vector2, int> _coli, Dictionary<Vector2, int> habitacines)
        {
            w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            cuadradoFelix = new Rectangle((int)_position.X + 15, (int)_position.Y, 50, 120);
            cambioH = 0;
            cambioV = 11;
            Mapa = 0;
            habitacion = 1;
            cambioHab = 0;
            habAnterior = 0;
            derecha = false;
            izquierda = false;
            arriba = false;
            abajo = false;
            top = 0;

            //chocar = _content.Load<Texture2D>("Controles/boton");

            _game = game;
            _graphicsDevice = graphicsDevice;
            coli = _coli;
            _content = contenedor;
            felix = new Texture2D[6];
            rFelix = new Rectangle((int)_position.X, (int)_position.Y, 200, 150);
            rCuerpo = new Rectangle((int)_position.X + 15, (int)_position.Y, 50, 120);

            felix[0] = _content.Load<Texture2D>("Felix/derecha_Felix_v2");
            felix[1] = _content.Load<Texture2D>("Felix/izquierda_felix_v2");
            felix[2] = _content.Load<Texture2D>("Felix/Animacion_caminata_arriba_V4");
            felix[3] = _content.Load<Texture2D>("Felix/Animacion_caminata_abajo_V4");
            felix[4] = _content.Load<Texture2D>("Felix/iddle_felix");
            felix[5] = _content.Load<Texture2D>("Felix/felix_v2");
            //_position = posicion;
            //_tamaño = tamaño;
            cuadrado = _content.Load<Texture2D>("Controles/boton");
            _position = new Vector2(240, 240);
            _velocity = new Vector2(40, 40);


            am = new(7, 7, new System.Numerics.Vector2(310, 215));
            pm = new(7, 7, new System.Numerics.Vector2(198, 215));
            pp = new(7, 7, new System.Numerics.Vector2(249, 225));

            intersections = new List<Rectangle>();

            disparo = new Disparo(contenedor, _position, _game, coli);

            colorF = Color.White;
            daño = 0f;
            this.habitacines = habitacines;
        }
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            //sprite.Draw(cuadrado, new Rectangle((int)_position.X + 15, (int)_position.Y, 50, 120), Color.Red);
            if (activo <= 0)
                sprite.Draw(felix[activo], new Rectangle(rFelix.X - 70, rFelix.Y, rFelix.Width, rFelix.Height), am.GetFrame(), colorF);
            else if (activo == 1)
                sprite.Draw(felix[activo], new Rectangle((int)_position.X, (int)_position.Y, 200, 150), am.GetFrame(), colorF);
            else if (activo == 2)
                sprite.Draw(felix[activo], new Rectangle((int)_position.X - 20, (int)_position.Y, 140, 150), pm.GetFrame(), colorF);
            else if (activo == 3)
                sprite.Draw(felix[activo], new Rectangle((int)_position.X - 20, (int)_position.Y, 120, 140), pm.GetFrame(), colorF);
            else if (activo == 4)
                sprite.Draw(felix[activo], new Rectangle((int)_position.X - 20, (int)_position.Y, 130, 140), pp.GetFrame(), colorF);
            else if (activo == 5)
                sprite.Draw(felix[activo], new Rectangle((int)_position.X, (int)_position.Y, 100, 130), colorF);

            disparo.Draw(gameTime, sprite);
        }

        public override void Update(GameTime gameTime)
        {
            izq = false;
            der = false;
            cuadradoFelix = new Rectangle((int)_position.X + 15, (int)_position.Y, 50, 120);
            
            rFelix = new Rectangle((int)_position.X, (int)_position.Y, 200, 150);
            rCuerpo = new Rectangle((int)_position.X + 15, (int)_position.Y, 50, 120);
            daño += (float)gameTime.ElapsedGameTime.TotalSeconds;

            _velocity = Vector2.Zero;

            //---------------Aca esta la función que hace que felix se mueva --------------
            movimientoYanimaciones(gameTime);

            // Intersicciones
            Vector2 temp = _position;
             _position.X += _velocity.X;
            intersections = getIntersectingTilesHorizontal(new Rectangle((int)_position.X, (int)_position.Y, 120, 120));

            moverseHabitacionesHorrizontal();

            foreach (var reac in intersections)
            {
                if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV), out int _val))
                {
                    if(enemigoList.Count > 0 || (enemigoList.Count <= 0 && _val != 5 && _val != 4 && _val != 57))
                    {
                        _position = temp;
                    }
                }
            }

            habAnterior = habitacion;
            //---------------Aca esta la función que permite el cambio de habitacion de forma horizontal --------------

            CambioDeHabitacionesHorrizontal();


            _position.Y += _velocity.Y;
            intersections = getIntersectingTilesVertical(new Rectangle((int)_position.X, (int)_position.Y, 120, 120));

            foreach (var reac in intersections)
            {
                if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV), out int _val))
                {
                    if (enemigoList.Count > 0 || (enemigoList.Count <= 0 && _val != 5 && _val != 4 && _val != 57)) 
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

            //---------------Aca esta la función que permite el cambio de habitacion de forma Vertical --------------

            CambioDeHabitacionesVertical();

            cambioHab = habitacion;
            if (cambioHab != habAnterior) 
            {
                disparo.Borrar();
                habAnterior = cambioHab;
            }
            disparo.Update(gameTime);
            disparo.Posicion = _position;
            disparo.Colisiones(cambioH, cambioV);
            //---------------Aca esta la función que hace que felix pierda vida si lo golpean los enemigos --------------

            dañoEnemigos();
        }
        private void movimientoYanimaciones(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _velocity.Y = -5;
                activo = 2;
                top = 0;
                am.Update();
                pm.Update();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _velocity.Y = 5;
                activo = 3;
                top = 0;
                am.Update();
                pm.Update();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (!centro2) _velocity.X = -5;
                activo = 1;
                top = 0;
                pm.Update();
                am.Update();
                der = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (!centro) _velocity.X = 5;
                activo = 0;
                top = 0;
                pm.Update();
                am.Update();
                izq = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                activo = 4;
                top = 0;
                pm.Update();
                am.Update();
            }
            else
            {
                activo = 5;
                top += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (top > 5 && top < 10)
                {

                    activo = 4;
                    pp.Update();


                }
                if (top > 10)
                {
                    top = 0;
                }
            }
        }
        private void dañoEnemigos()
        {
            if (daño >= 0.2 && colorF == Color.Red)
            {
                daño = 0;
                colorF = Color.White;
            }
            foreach (Enemigo enemy in enemigoList.ToList())
            {
                if (rCuerpo.Intersects(new Rectangle((int)enemy.Posicion.X, (int)enemy.Posicion.Y, 100, 100)))
                {
                    daño = 0f;
                    vida -= 10;
                    enemy.Posicion = enemy.temp;
                    colorF = Color.Red;
                }
                if (vida <= 0)
                {
                    _game.ChangeState(new VistaPerdiste(_game, _graphicsDevice, _content));
                }
            }
        }

        private void moverseHabitacionesHorrizontal()
        {
            int a = 0;
            
                foreach (var reac in intersections)
                {
                    for(int i = 9 ;i> 0; i--) { 
                        if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH + i , reac.Y + cambioV), out int _val))
                        {
                            a = 1;
                        }
                    }
                    if(a != 1 && _position.X >= ((w/2) / 93) * 93 && izq)
                    {
                    centro = true;
                    Mapa = 5;
                    b+= 3;
                        if (b >= 300)
                        {
                            cambioH++;
                            b = 0;
                        }
                    }
                    else
                        centro = false;
                }
            foreach (var reac in intersections)
            {
                for (int i = 9; i > 0; i--)
                {
                    if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH - i, reac.Y + cambioV), out int _val))
                    {
                        a = 2;
                    }
                }
                if (a != 2 && _position.X >= ((w / 2) / 93) * 93 - 93 && der)
                {
                    centro2 = true;
                    Mapa = 6;
                    b += 3;
                    if (b >= 300)
                    {
                        cambioH--;
                        b = 0;
                    }
                }
                else
                    centro2 = false;
            }
        }
        private void CambioDeHabitacionesHorrizontal()
        {
            if (enemigoList.Count <= 0)
            {
                foreach (var reac in intersections)
                {
                if (habitacines.TryGetValue(new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV), out int _val))
                {
                    foreach (var hab in habitacines)
                    {
                        if (hab.Key == new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV))
                        {
                            if (hab.Value == 1)
                            {
                                izquierda = true;
                                _position.X = 1750;
                                Mapa = 2;
                            }
                            if (hab.Value == 0)
                            {
                                derecha = true;
                                _position.X = 100;
                                Mapa = 1;
                            }
                                habitacion = hab.Value - 3;
                        }
                    }
                }
                }
                if (derecha)
            {
                cambioH += 21;
                derecha = false;

            }
            if (izquierda)
            {
                cambioH -= 21;
                izquierda = false;
            }
           }
        }
        private void CambioDeHabitacionesVertical()
        {
            if (enemigoList.Count <= 0)
            {

                foreach (var reac in intersections)
                {
                    if (habitacines.TryGetValue(new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV), out int _val))
                    {
                        foreach (var hab in habitacines)
                        {
                            if (hab.Key == new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV))
                            {
                                if (hab.Value == 2)
                                {
                                    abajo = true;

                                    _position.Y = 200;
                                    Mapa = 3;
                                }
                                if (hab.Value == 3)
                                {
                                    arriba = true;

                                    _position.Y = 800;
                                    Mapa = 4;
                                }
                                habitacion = hab.Value - 3;
                            }
                        }
                    }
                }
                // Esto sirve para que felix se pueda mover entre habitaciones (no lo hago dentro de foreach ya que como interesciona mas de una vez
                // cada vez que toca una puerta hace que mueva el mapa por lo menos dos veces)
                if (abajo)
                {
                    cambioV += 12;
                    abajo = false;
                }
                if (arriba)
                {
                    cambioV -= 12;
                    arriba = false;
                }
            }
        }
        private List<Rectangle> getIntersectingTilesHorizontal(Rectangle target)
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
        private List<Rectangle> getIntersectingTilesVertical(Rectangle target)
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
