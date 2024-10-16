﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using static BetaFoesAndBones.Controles.Disparo;

namespace BetaFoesAndBones.Personajes
{
    internal class Enemigos : Componentes
    {
        private Rectangle areaAtaqueCortoAlcanze;
        public bool felixTieneArma;
        private int nose = 0;
        private bool hayEnemigos = true;
        private bool vulne = false;

        private float daño;
        public Vector2 felix_posicion;
        private Vector2 muerte;
        private int murio = 0;
        public Random random = new Random();
        public List<Magia> proyectilesE;
        public int puntos;
        public List<Enemigo> enemigos = new List<Enemigo>();
        private Texture2D slimeTextura;
        private Texture2D bacterianoTextura;
        private Texture2D draconanioTextura;
        private Texture2D explosion;
        private Texture2D vulne_bacteriano;
        private AnimationManager ab, slime_ab, vulne_bacte,elvira_ab;
        private float segundosVulnerable = 0;

        private Dictionary<int, List<Enemigo>> mapaHabitaciones; //creo diccionario 
        private HashSet<int> habitacionesVisitadas; //creo una colección de elementos únicos

        public Enemigos(Game1 game, ContentManager contenedor)
        {
            areaAtaqueCortoAlcanze = new Rectangle((int)felix_posicion.X - 45, (int)felix_posicion.Y - 25, 170, 180);

            _game = game;
            _content = contenedor;
            slimeTextura = _content.Load<Texture2D>("Enemigos/Slime_movimiento");
            bacterianoTextura = _content.Load<Texture2D>("Enemigos/bacte_movimiento");
            draconanioTextura = _content.Load<Texture2D>("Enemigos/elvira_v1");
            explosion = _content.Load<Texture2D>("Enemigos/explosion");
            vulne_bacteriano = _content.Load<Texture2D>("Enemigos/elvira_v1");
            felix_posicion = new Vector2(300, 300);
            puntos = 0;

            felixTieneArma = false;

            ab = new(5, 5, new System.Numerics.Vector2(165, 230));
            slime_ab = new(7, 7, new System.Numerics.Vector2(146, 190));
            vulne_bacte = new(7, 7, new System.Numerics.Vector2(330, 350));
            elvira_ab = new(7, 7, new System.Numerics.Vector2(200, 400));

            mapaHabitaciones = new Dictionary<int, List<Enemigo>>(); //creo un diccionario que almacena relación entre las habitaciones y los enemigos 
            habitacionesVisitadas = new HashSet<int>(); //creo una colección de elementos únicos

            // Inicializar enemigos por habitación
            InicializarEnemigosPorHabitacion();
        }

        private void InicializarEnemigosPorHabitacion()
        {
            mapaHabitaciones[2] = new List<Enemigo> //accedo a la entrada del diccionario 1 y creo lista vacía de tipo Enemigo
            {
                new Slime(slimeTextura, new Vector2(500, 100)),
                new Bacteriano(bacterianoTextura, new Vector2(1000, 100)),
                new Slime(slimeTextura, new Vector2(1400, 100)),

                new Slime(slimeTextura, new Vector2(750, 350)),

                new Slime(slimeTextura, new Vector2(500, 800)),
                new Slime(slimeTextura, new Vector2(1000, 800)),
                new Bacteriano(bacterianoTextura, new Vector2(1400, 800)),
            };

            mapaHabitaciones[3] = new List<Enemigo>
            {
                new Slime(slimeTextura, new Vector2(500, 100)),
                new Slime(slimeTextura, new Vector2(1000, 100)),
                new Slime(slimeTextura, new Vector2(1400, 100)),

                new Slime(slimeTextura, new Vector2(750, 350)),

                new Slime(slimeTextura, new Vector2(500, 800)),
                new Slime(slimeTextura, new Vector2(1000, 800)),
                new Slime(slimeTextura, new Vector2(1400, 800)),
            };

            mapaHabitaciones[4] = new List<Enemigo>
            {


                new Draconario(draconanioTextura, new Vector2(750, 350)),

                new Bacteriano(bacterianoTextura, new Vector2(500, 800)),
                new Bacteriano(bacterianoTextura, new Vector2(1000, 800)),
                new Bacteriano(bacterianoTextura, new Vector2(1400, 800)),
            };
            //mapaHabitaciones[2] = new List<Enemigo>();
            //mapaHabitaciones[3] = new List<Enemigo>();
            //mapaHabitaciones[4] = new List<Enemigo>();
        }

        public void PonerEnemigos(int habitacion)
        {
            nose = 0;
            if (mapaHabitaciones.ContainsKey(habitacion)) //verificar si existe una clave específica dentro del diccionario
            {
                if (habitacionesVisitadas.Count > 0) { 
                    foreach(var hab in habitacionesVisitadas) { 
                        if (habitacion == hab) {
                            nose = 1;
                        }
                    }
                    if (nose != 1)
                    {
                        enemigos.AddRange(mapaHabitaciones[habitacion]); //agrega los enemigos de la habitación específica a la lista de enemigos
                        habitacionesVisitadas.Add(habitacion); //la habitacion ingresada se mete en las ya visitadas 
                    }
                }
                else
                {
                    enemigos.AddRange(mapaHabitaciones[habitacion]); //agrega los enemigos de la habitación específica a la lista de enemigos
                    habitacionesVisitadas.Add(habitacion); //la habitacion ingresada se mete en las ya visitadas  
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            foreach (var enemigo in enemigos)
            {
                if (!enemigo.EnemigoVulnerable)
                {
                    if (enemigo.TiempoAparicion == 9f)
                        sprite.Draw(enemigo.Textura, enemigo.Posicion, ab.GetFrame(), enemigo.ColorE);
                    else if (enemigo.TiempoAparicion == 3f)
                    {
                        sprite.Draw(enemigo.Textura, enemigo.Posicion, slime_ab.GetFrame(), enemigo.ColorE);
                    }
                    else if (enemigo.TiempoAparicion == 20f)
                    {
                        sprite.Draw(vulne_bacteriano, new Rectangle((int)enemigo.Posicion.X, (int)enemigo.Posicion.Y, 200, 180), vulne_bacte.GetFrame(), enemigo.ColorE);
                    }
                }
                else sprite.Draw(vulne_bacteriano, new Rectangle((int)enemigo.Posicion.X, (int)enemigo.Posicion.Y,150,120), vulne_bacte.GetFrame(), enemigo.ColorE);


            }

            
            if (murio == 1)
            {
                sprite.Draw(explosion, new Rectangle((int)muerte.X, (int)muerte.Y, 100, 100), Color.White);
            }
            if (daño >= 0.2)
            {
                daño = 0;
                murio = 0;
            }
           
        }

        public override void Update(GameTime gameTime)
        {
            areaAtaqueCortoAlcanze = new Rectangle((int)felix_posicion.X - 45, (int)felix_posicion.Y - 25, 170, 180);

            ab.Update();
            slime_ab.Update();
            vulne_bacte.Update();
            daño += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var enemigo in enemigos)
            {
                enemigo.Update(gameTime, felix_posicion, _game.w, _game.h);
            }

            foreach (Enemigo enemy in enemigos.ToList())
            {
                if (daño >= 0.2 && enemy.ColorE == Color.Red)
                {
                    daño = 0f;
                    enemy.ColorE = Color.White;
                }

                foreach (Magia p in proyectilesE.ToList())
                {
                    if (new Rectangle((int)p.Posicion.X, (int)p.Posicion.Y, (int)p.Textura.Width, (int)p.Textura.Height).Intersects(new Rectangle((int)enemy.Posicion.X, (int)enemy.Posicion.Y, (int)enemy.Tamaño.X, (int)enemy.Tamaño.X)))
                    {
                        proyectilesE.Remove(p);
                        daño = 0f;
                        enemy.HP -= p.Daño;
                        enemy.ColorE = Color.Red;
                    }

                }
                if (enemy.HP <= 0)
                {
                    
                    enemy.Velocidad = 0f;
                    enemy.DañoEnemigo = 0;
                    enemy.EnemigoVulnerable = true;
                   
                    
                    
                }
                if (enemy.EnemigoVulnerable)
                {
                    enemy.TiempoVulnerable += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (enemy.TiempoVulnerable >= 3 && enemy.EnemigoVulnerable == true)
                {
                    murio = 1;
                    puntos += (int)enemy.Puntos;
                    muerte = enemy.Posicion;
                    enemigos.Remove(enemy);
                }
            }
            AtacarEnemigosConArmaCortoAlcance();

           /*foreach (Enemigo e in enemigos.ToList())
            {
                foreach (Enemigo enemy in enemigos.ToList())
                {
                    if (e != enemy)
                    {
                        if (new Rectangle((int)e.Posicion.X, (int)e.Posicion.Y, (int)enemy.Tamaño.X, (int)enemy.Tamaño.Y).Intersects(new Rectangle((int)enemy.Posicion.X, (int)enemy.Posicion.Y, (int)enemy.Tamaño.X, (int)enemy.Tamaño.Y)))
                        {
                            e.Posicion = e.temp;
                        }
                    }
                }
            }*/
        }
        private void AtacarEnemigosConArmaCortoAlcance()
        {
            if (felixTieneArma && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                foreach (Enemigo enemy in enemigos.ToList())
                {
                    if (areaAtaqueCortoAlcanze.Intersects(new Rectangle((int)enemy.Posicion.X, (int)enemy.Posicion.Y, (int)enemy.Tamaño.X, (int)enemy.Tamaño.X)))
                    {
                        // Recordar despues cambiar el daño del arma a una variable
                        enemy.HP -= 10;
                        enemy.Posicion = enemy.temp;
                        enemy.ColorE = Color.Red;
                    }
                }
            }
        }
    }
}