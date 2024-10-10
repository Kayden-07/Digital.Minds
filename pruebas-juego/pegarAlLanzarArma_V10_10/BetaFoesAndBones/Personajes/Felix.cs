using BetaFoesAndBones.ArmasUniversal;
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

        private Rectangle areaAtaqueCortoAlcanze;

        public string numArma = "a";
        public bool tieneArma = false;
        public bool lanzaArma = false;
        private Keys ultimaTecla;

        private bool temp = false;
        private bool temp2 = false;
        public int posicionParedesH = 0;
        public int posicionParedesV = 0;
        private int w;
        private int h;
        private int b = 0;
        private int c = 0;
        private bool centro = false;
        private bool centro2 = false;
        private bool centroArriba = false;
        private bool centroAbajo = false;
        private bool arr = false;
        private bool abaj = false;
        private bool izq = false;
        private bool der = false;
        private bool estabaEnElMedio = false;

        public Rectangle cuadradoFelix;
        public int cambioH;
        private int cambioV;
        public  int Mapa;
        public int MapaHorizontal;
        public int MapaVertical;

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
        public List<Arma> armasPiso;
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

            areaAtaqueCortoAlcanze = new Rectangle((int)_position.X + 15, (int)_position.Y, 50, 120);

            ultimaTecla = Keys.A;

            w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //Pendiente: Revisar en versiones anteriores cuando cree cuadradoFelix
            cuadradoFelix = new Rectangle((int)_position.X + 15, (int)_position.Y, 150, 150);
            cambioH = 0;
            cambioV = 11;
            Mapa = 0;
            MapaHorizontal = 0;
            MapaVertical = 0;
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
            _position = new Vector2(740, 440);
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
            //sprite.Draw(cuadrado, areaAtaqueCortoAlcanze, Color.Blue);
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
            lanzaArma = disparo.estoyLanzandoElArma;

            areaAtaqueCortoAlcanze = new Rectangle((int)_position.X - 45, (int)_position.Y - 25, 170, 180);

            AgarrarArmaPiso();
            izq = false;
            der = false;
            arr = false;
            abaj = false;
            cuadradoFelix = new Rectangle((int)_position.X + 15, (int)_position.Y, 50, 120);
            
            rFelix = new Rectangle((int)_position.X, (int)_position.Y, 200, 150);
            rCuerpo = new Rectangle((int)_position.X + 15, (int)_position.Y, 50, 120);
            daño += (float)gameTime.ElapsedGameTime.TotalSeconds;

            _velocity = Vector2.Zero;

            //---------------Aca esta la función que hace que felix se mueva --------------
            movimientoYanimaciones(gameTime);

            enemigoHabitacionesGrandes();

            // Intersicciones
            Vector2 temp = _position;
             _position.X += _velocity.X;
            intersections = getIntersectingTilesHorizontal(new Rectangle((int)_position.X, (int)_position.Y, 120, 120));

            moverseHabitacionesHorrizontal();
            moverseHabitacionesVerticales();

            foreach (var reac in intersections)
            {
                if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV), out int _val))
                {
                    if((enemigoList.Count > 0 || (enemigoList.Count <= 0 && _val != 5 && _val != 4 && _val != 57)) && _val != 0)
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
                    if ((enemigoList.Count > 0 || (enemigoList.Count <= 0 && _val != 5 && _val != 4 && _val != 57)) && _val != 0) 
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
            disparo.FelixTieneArma = tieneArma;
            disparo.armasAlanzar = armasPiso;
            disparo.armalanzar = numArma;
            //---------------Aca esta la función que hace que felix pierda vida si lo golpean los enemigos --------------

            dañoEnemigos();
        }
        private void movimientoYanimaciones(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                numArma = "a";
                tieneArma = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (!centroArriba) _velocity.Y = -5;
                activo = 2;
                top = 0;
                am.Update();
                pm.Update();
                arr = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (!centroAbajo) _velocity.Y = 5;
                activo = 3;
                top = 0;
                am.Update();
                pm.Update();
                abaj = true;
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
            else if (Keyboard.GetState().IsKeyDown(Keys.J))
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
        private void enemigoHabitacionesGrandes()
        {
            foreach (Enemigo enemy in enemigoList.ToList())
            {
                if (centro) enemy.Posicion = new Vector2 (enemy.Posicion.X - 3,enemy.Posicion.Y);
                else if(centro2) enemy.Posicion = new Vector2(enemy.Posicion.X + 3, enemy.Posicion.Y);
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
                    vida -= enemy.DañoEnemigo;
                    enemy.Posicion = enemy.temp;
                    colorF = Color.Red;
                }
                if (vida <= 0)
                {
                    _game.ChangeState(new VistaPerdiste(_game, _graphicsDevice, _content));
                }
            }
        }
        private void AgarrarArmaPiso()
        {
            if (numArma != "a")
                armasPiso[int.Parse(numArma)].PosicionArma = new Vector2(rCuerpo.X, rCuerpo.Y);
            for (int i = 0; i < armasPiso.Count; i++)
            {
                if (rCuerpo.Intersects(new Rectangle((int)armasPiso[i].PosicionArma.X, (int)armasPiso[i].PosicionArma.Y, 100, 100)) && Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    numArma = i.ToString();
                    tieneArma = true;
                }
            }
        }
        private void moverseHabitacionesHorrizontal()
        {
            int a = 0;
            posicionParedesH = cambioH;
            foreach (var reac in intersections)
                {
                    for(int i = 8 ;i> 0; i--) { 
                        if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH + i , reac.Y + cambioV), out int _val))
                        {
                            a = 1;
                        }
                    }
                    if(a != 1 && _position.X >= ((w/2) / 93) * 93 && izq)
                    {
                    centro = true;
                    b += 3;
                    MapaHorizontal = 5;
                    if(b >= 323)
                    {
                        cambioH++;
                            b = 0;
                    }
                    }
                    else if(a == 1 && _position.X >= (((w / 2) / 93) * 93) - 5 )
                    {
                    MapaHorizontal = 9;
                    temp = true;
                    centro = false;
                    }
                    else
                    centro=false;
                        
                }
            a = 0;
            foreach (var reac in intersections)
            {
                
                for (int i = 7; i >= 0; i--)
                {
                    if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH - i, reac.Y + cambioV), out int _val))
                    {
                        a = 2;
                    }
                }
                if (a != 2 && _position.X <= ((w / 2) / 93) * 93 - 93 && der)
                {
                    centro2 = true;
                    b += 3;
                    if (temp == true)
                    {
                        temp = false;
                        cambioH--;
                        b = 0;
                        MapaHorizontal = 13;
                    }
                    if (b >= 323)
                    {
                        cambioH--;
                        b = 0;
                        MapaHorizontal = 13;
                    }
                    MapaHorizontal = 6;
                }
                else if (a == 2 && _position.X <= (((w / 2) / 93) * 93) - 93 + 5)
                {
                    MapaHorizontal = 10;
                    centro2 = false;
                    temp = true;
                }
                else
                    centro2 = false;
                

            }
        }
        private void moverseHabitacionesVerticales()
        {
            int a = 0;
            posicionParedesV = cambioV;
            foreach (var reac in intersections)
            {
                for (int i = 4; i > 0; i--)
                {
                    if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV + i), out int _val))
                    {
                        a = 1;
                    }
                }
                if (a != 1 && _position.Y >= ((h / 2) / 97) * 97 && abaj)
                {
                    centroAbajo = true;
                    c += 3;
                    MapaVertical = 7;
                    if (c >= 323)
                    {
                        cambioV++;
                        c = 0;
                    }
                }
                else if (a == 1 && _position.Y >= (((h / 2) / 97) * 97) - 5)
                {
                    MapaVertical = 11;
                    temp2 = true;
                    centroAbajo = false;
                }
                else
                    centroAbajo = false;

            }
            a = 0;
            foreach (var reac in intersections)
            {

                for (int i = 4; i >= 0; i--)
                {
                    if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioH, reac.Y + cambioV - i), out int _val))
                    {
                        a = 2;
                    }
                }
                if (a != 2 && _position.Y <= ((h / 2) / 97) * 97 - 97 && arr)
                {
                    centroArriba = true;
                    b += 3;
                    if (temp2 == true)
                    {
                        temp2 = false;
                        cambioV--;
                        b = 0;
                        MapaVertical = 13;
                    }
                    if (b >= 323)
                    {
                        cambioV--;
                        b = 0;
                        MapaVertical = 13;
                    }
                    MapaVertical = 8;
                }
                else if (a == 2 && _position.Y <= (((h / 2) / 97) * 97) - 97 + 5)
                {
                    MapaVertical = 12;
                    centroArriba = false;
                    temp2 = true;
                }
                else
                    centroArriba = false;


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
                                _position.X = 1650;
                                Mapa = 2;
                            }
                            if (hab.Value == 0)
                            {
                                derecha = true;
                                _position.X = 200;
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
                    cambioV += 13;
                    abajo = false;
                }
                if (arriba)
                {
                    cambioV -= 13;
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
