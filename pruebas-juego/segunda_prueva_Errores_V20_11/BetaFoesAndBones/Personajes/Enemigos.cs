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
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using static BetaFoesAndBones.Controles.Disparo;

namespace BetaFoesAndBones.Personajes
{
    internal class Enemigos : Componentes
    {
        private bool espacioPresionado;
        private bool finalJuego;
        public string numArma = "a";
        private int numArmaLanzar = 999;
        public List<Arma> armasPiso;

        private Rectangle areaAtaqueCortoAlcanze;
        public bool felixTieneArma;
        public bool felixLanzaArma;
        private int nose = 0;
        private bool hayEnemigos = true;

        private float daño;
        public Vector2 felix_posicion;
        private Vector2 muerte;
        private int murio = 0;
        public Random random = new Random();
        public List<Magia> proyectilesE;
        public int puntos;
        public List<Enemigo> enemigos = new List<Enemigo>();
        public List<Enemigo> enemigosMuertos = new List<Enemigo>(); //Para que suelten armas
        private Texture2D slimeTextura;
        private Texture2D bacterianoTextura;
        private Texture2D draconanioTextura;
        private Texture2D explosion;
        private Texture2D vulne_bacteriano;
        private Texture2D vulne_slime;
        private Texture2D vulne_elvira;

        private Texture2D balaMortero;
        private Texture2D radioMortero;

        private Texture2D cuerpoC;
        private Texture2D pinza1C;
        private Texture2D pinza1roC;
        private Texture2D pinza2C;
        private Texture2D pinza2roC;
        private Texture2D cuadrado;
        private AnimationManager ab, slime_ab, elvira_ab,vulne_bacte,vulne_slim,vulne_elvir;
    
        private float segundosVulnerable = 0;

        private Dictionary<int, List<Enemigo>> mapaHabitaciones; //creo diccionario 
        private HashSet<int> habitacionesVisitadas; //creo una colección de elementos únicos

        Slime slimeTuto;

        public Enemigos(Game1 game, ContentManager contenedor)
        {
            espacioPresionado = false;
            finalJuego = false;
            armasPiso = new List<Arma>();
            areaAtaqueCortoAlcanze = new Rectangle((int)felix_posicion.X - 45, (int)felix_posicion.Y - 25, 170, 180);
            felixLanzaArma = false;

            _game = game;
            _content = contenedor;
            slimeTextura = _content.Load<Texture2D>("Enemigos/Slime_movimiento");
            bacterianoTextura = _content.Load<Texture2D>("Enemigos/bacte_movimiento");
            draconanioTextura = _content.Load<Texture2D>("Enemigos/elvira_v1");
            explosion = _content.Load<Texture2D>("Enemigos/explosion");
            vulne_bacteriano = _content.Load<Texture2D>("Enemigos/Vulne_bacteriano");
            vulne_slime = _content.Load<Texture2D>("Enemigos/Vuln_slime");
            vulne_elvira = _content.Load<Texture2D>("Enemigos/Vulne_elvira");

            balaMortero = _content.Load<Texture2D>("Disparos/balaMortero");
            radioMortero = _content.Load<Texture2D>("Disparos/cuadrado");

            cuerpoC = _content.Load<Texture2D>("Enemigos/jefeCangrejo/cuerpo");
            pinza1C = _content.Load<Texture2D>("Enemigos/jefeCangrejo/pinza1");
            pinza1roC = _content.Load<Texture2D>("Enemigos/jefeCangrejo/Brazo_izquierdo_roto");
            pinza2C = _content.Load<Texture2D>("Enemigos/jefeCangrejo/pinza2");
            pinza2roC = _content.Load<Texture2D>("Enemigos/jefeCangrejo/Brazo_derecho_roto");
            cuadrado = _content.Load<Texture2D>("Controles/boton");

            felix_posicion = new Vector2(300, 300);
            puntos = 0;

            felixTieneArma = false;
            ab = new(5, 5, new System.Numerics.Vector2(165, 230));
            slime_ab = new(7, 7, new System.Numerics.Vector2(146, 190));
            elvira_ab = new(7, 7, new System.Numerics.Vector2(328, 400));
            vulne_bacte = new(7, 7, new System.Numerics.Vector2(200, 200));
            vulne_slim = new(7, 7, new System.Numerics.Vector2(200, 200));
            vulne_elvir = new(7, 7, new System.Numerics.Vector2(300, 300));
            mapaHabitaciones = new Dictionary<int, List<Enemigo>>(); //creo un diccionario que almacena relación entre las habitaciones y los enemigos 
            habitacionesVisitadas = new HashSet<int>(); //creo una colección de elementos únicos

            

            // Inicializar enemigos por habitación
            InicializarEnemigosPorHabitacion();
        }

        private void InicializarEnemigosPorHabitacion()
        {
            mapaHabitaciones[2] = new List<Enemigo> //accedo a la entrada del diccionario 1 y creo lista vacía de tipo Enemigo
            {
                new Slime(slimeTextura, new Vector2(1400, 500)),
            };
            mapaHabitaciones[3] = new List<Enemigo>
            {
                new Slime(slimeTextura, new Vector2(700, 350)),
                new Slime(slimeTextura, new Vector2(1000, 800)),
                new Bacteriano(bacterianoTextura, new Vector2(1500, 500)),
                new Slime(slimeTextura, new Vector2(800, 800)),
                new Slime(slimeTextura, new Vector2(1500, 200)),


                 new Slime(slimeTextura, new Vector2(1950, 350)),

                new Bacteriano(bacterianoTextura, new Vector2(2500, 100)),
                new Slime(slimeTextura, new Vector2(2400, 100)),
                new Draconario(draconanioTextura, new Vector2(2100, 700))
            };

            mapaHabitaciones[4] = new List<Enemigo>
            {
                new Slime(slimeTextura, new Vector2(500, 100)),
                new Slime(slimeTextura, new Vector2(1000, 100)),
                new Slime(slimeTextura, new Vector2(1400, 100)),

                new Slime(slimeTextura, new Vector2(750, 350)),

                new Slime(slimeTextura, new Vector2(500, 800)),
                new Slime(slimeTextura, new Vector2(1000, 800)),
                new Slime(slimeTextura, new Vector2(1400, 800)),
            };

            mapaHabitaciones[5] = new List<Enemigo>
            {
                new Draconario(draconanioTextura, new Vector2(750, 350)),

                new Bacteriano(bacterianoTextura, new Vector2(500, 800)),
                new Slime(slimeTextura, new Vector2(1000, 800)),
                new Bacteriano(bacterianoTextura, new Vector2(1400, 800)),
            };
            mapaHabitaciones[7] = new List<Enemigo>
            {
                new Draconario(draconanioTextura, new Vector2(700, 400)),
                new Draconario(draconanioTextura, new Vector2(900, 600)),
            };
            mapaHabitaciones[9] = new List<Enemigo>
            {
                new Draconario(draconanioTextura, new Vector2(900, 350)),
                new Slime(slimeTextura, new Vector2(500, 800)),
                new Bacteriano(bacterianoTextura, new Vector2(1300, 500)),
                new Slime(slimeTextura, new Vector2(600, 350)),
                new Slime(slimeTextura, new Vector2(1400, 200)),
            };
            mapaHabitaciones[10] = new List<Enemigo>
            {
                new Draconario(draconanioTextura, new Vector2(750, 500)),
                new Draconario(draconanioTextura, new Vector2(750, 350)),
                new Bacteriano(bacterianoTextura, new Vector2(500, 700)),
                new Slime(slimeTextura, new Vector2(1000, 800)),
                new Bacteriano(bacterianoTextura, new Vector2(1400, 700)),
            };
            mapaHabitaciones[11] = new List<Enemigo>
            {
                new Slime(slimeTextura, new Vector2(1000, 800)),

                new JefeCangrejo(cuerpoC,pinza1C, pinza1roC,pinza2C,pinza2roC,cuadrado, new Vector2(150, 100), balaMortero, radioMortero)
            };

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
                if(enemigo is not JefeCangrejo)
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
                            sprite.Draw(draconanioTextura, new Rectangle((int)enemigo.Posicion.X, (int)enemigo.Posicion.Y, 250, 250), elvira_ab.GetFrame(), enemigo.ColorE);
                        }
                    }
                    else {
                        if (enemigo.ID == 1)
                        { sprite.Draw(vulne_slime, new Rectangle((int)enemigo.Posicion.X, (int)enemigo.Posicion.Y, 130, 130), vulne_slim.GetFrame(), enemigo.ColorE); }
                        if (enemigo.ID == 2)
                        {sprite.Draw(vulne_bacteriano, new Rectangle((int)enemigo.Posicion.X, (int)enemigo.Posicion.Y, 200, 200), vulne_bacte.GetFrame(), enemigo.ColorE);}
                        if (enemigo.ID == 3)
                        { sprite.Draw(vulne_elvira, new Rectangle((int)enemigo.Posicion.X, (int)enemigo.Posicion.Y, 250, 250), vulne_elvir.GetFrame(), enemigo.ColorE); }
                    }
                }
                else
                {
                    JefeCangrejo jC = (JefeCangrejo)enemigo;
                    jC.Draw(gameTime, sprite);
                }
                
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
            if (numArma != "a") numArmaLanzar = int.Parse(numArma);
            ab.Update();
            slime_ab.Update();
            elvira_ab.Update();
            vulne_slim.Update();
            vulne_bacte.Update();
            vulne_elvir.Update();
            daño += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var enemigo in enemigos)
            {
                enemigo.Update(gameTime, felix_posicion, _game.w, _game.h);
                if (enemigo is JefeCangrejo)
                {
                    finalJuego = true;
                    JefeCangrejo j = (JefeCangrejo)enemigo;
                    j.Actualizar(gameTime, felix_posicion);
                    if (finalJuego)
                    {
                        if (j.HP<= 0)
                        {
                            _game.ChangeState(new VistaGanaste(_game, _graphicsDevice, _content));
                        }
                    }
                }
            }
            

            foreach (Enemigo enemy in enemigos.ToList())
            {
                if(enemy is not JefeCangrejo)
                {
                if (daño >= 0.2 && enemy.ColorE == Color.Red)
                {
                    daño = 0f;
                    enemy.ColorE = Color.White;
                }

                foreach (Magia p in proyectilesE.ToList())
                {
                    if (new Rectangle((int)p.Posicion.X, (int)p.Posicion.Y, (int)p.Textura.Width, (int)p.Textura.Height).Intersects(new Rectangle((int)enemy.Posicion.X, (int)enemy.Posicion.Y, (int)enemy.Tamaño.X, (int)enemy.Tamaño.Y)))
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
                if (enemy.TiempoVulnerable > 0 && enemy.TiempoVulnerable < 3)
                {
                    enemigosMuertos.Add(enemy);
                }
                if (enemy.TiempoVulnerable >= 3 && enemy.EnemigoVulnerable == true)
                {
                    murio = 1;
                    puntos += (int)enemy.Puntos;
                    muerte = enemy.Posicion;
                    enemigos.Remove(enemy);
                }
                }
                else
                {
                    if (enemy.HP <= 0) enemigos.Remove(enemy);
                    JefeCangrejo c = (JefeCangrejo)enemy;
                    foreach(Enemigo pC in c.partesCangrejo)
                    {

                        if (daño >= 0.2 && pC.ColorE == Color.Red)
                        {
                            daño = 0f;
                            pC.ColorE = Color.White;
                        }
                        foreach (Magia p in proyectilesE.ToList())
                        {
                            if(pC is CuerpoC)
                            {
                                if (new Rectangle((int)p.Posicion.X, (int)p.Posicion.Y, (int)p.Textura.Width, (int)p.Textura.Height).Intersects(new Rectangle((int)pC.Posicion.X, (int)pC.Posicion.Y - (int)enemy.Posicion.Y, (int)pC.Tamaño.X, (int)pC.Tamaño.Y)))
                                {
                                    proyectilesE.Remove(p);
                                    daño = 0f;
                                    if(c.pinza1.HP > 0 || c.pinza2.HP > 0)
                                        pC.HP -= p.Daño / 4;
                                    else
                                        pC.HP -= p.Daño;
                                    pC.ColorE = Color.Red;
                                }
                            }
                            else if(pC is Pinza) 
                            {
                                Pinza pinza = (Pinza)pC;
                                if (pinza.HP > 0f) 
                                {
                                    foreach (Rectangle rPinza in pinza.colisions)
                                    {
                                        if (new Rectangle((int)p.Posicion.X, (int)p.Posicion.Y, (int)p.Textura.Width, (int)p.Textura.Height).Intersects(rPinza))
                                        {
                                            proyectilesE.Remove(p);
                                            daño = 0f;
                                            pC.HP -= p.Daño;
                                            pC.ColorE = Color.Red;
                                        }
                                    }
                                }
                            }
                            
                        }
                    }
                    
                }
            }
            AtacarEnemigosConArmaCortoAlcance();
            AtacarEnemigosAlLanzarArma();

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
                espacioPresionado = true;
            if (espacioPresionado && Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                espacioPresionado = false;
                foreach (Enemigo enemy in enemigos.ToList())
                {
                    if (enemy is not JefeCangrejo)
                    {
                        if (areaAtaqueCortoAlcanze.Intersects(new Rectangle((int)enemy.Posicion.X, (int)enemy.Posicion.Y, (int)enemy.Tamaño.X, (int)enemy.Tamaño.Y)))
                        {
                            // Recordar despues cambiar el daño del arma a una variable
                            if(numArma != "a")
                            {
                                enemy.HP -= armasPiso[int.Parse(numArma)].DañoMelee;
                                enemy.Posicion = enemy.temp;
                                enemy.ColorE = Color.Red;
                            }
                        }
                    }
                    else
                    {
                        JefeCangrejo c = (JefeCangrejo)enemy;
                        foreach (Enemigo pC in c.partesCangrejo)
                        {
                            if (pC is CuerpoC)
                            {
                                if (areaAtaqueCortoAlcanze.Intersects(new Rectangle((int)pC.Posicion.X, (int)pC.Posicion.Y - (int)enemy.Posicion.Y, (int)pC.Tamaño.X, (int)pC.Tamaño.Y)))
                                {
                                    pC.HP -= 10;
                                    pC.ColorE = Color.Red;
                                }
                            }
                            else
                            {
                                Pinza pinza = (Pinza)pC;
                                foreach (Rectangle rPinza in pinza.colisions)
                                {
                                    if (areaAtaqueCortoAlcanze.Intersects(rPinza) && pC.HP > 0)
                                    {
                                        pC.HP -= 10;
                                        pC.ColorE = Color.Red;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            }
        private void AtacarEnemigosAlLanzarArma()
        {
            Rectangle cuerpoEnemigo;
            if (felixLanzaArma)
            {
                foreach (Enemigo enemy in enemigos.ToList())
                {
                    cuerpoEnemigo = new Rectangle((int)enemy.Posicion.X, (int)enemy.Posicion.Y, (int)enemy.Tamaño.X, (int)enemy.Tamaño.X);
                    if (!enemy.meAtacaron)
                    {
                        if (cuerpoEnemigo.Intersects(new Rectangle((int)armasPiso[numArmaLanzar].PosicionArma.X, (int)armasPiso[numArmaLanzar].PosicionArma.Y, 100, 100)))
                        {
                            // Recordar despues cambiar el daño del arma a una variable
                            enemy.HP -= 60;
                            enemy.ColorE = Color.Red;
                            enemy.meAtacaron = true;
                        }
                    }
                    else if (enemy.meAtacaron && !cuerpoEnemigo.Intersects(new Rectangle((int)armasPiso[numArmaLanzar].PosicionArma.X, (int)armasPiso[numArmaLanzar].PosicionArma.Y, 100, 100)))
                    {
                        enemy.meAtacaron = false;
                    }
                }
            }
        }

        public void RecibirEnemigosMuertos(List<Enemigo> ene)
        {
            if(ene.Count > 0)
            {
                foreach (Enemigo enemy in enemigos.ToList()) 
                { 
                    foreach(Enemigo enemy2 in ene.ToList())
                    {
                        if (enemy == enemy2)
                        {
                            murio = 1;
                            puntos += (int)enemy.Puntos;
                            muerte = enemy.Posicion;
                            enemigos.Remove(enemy);
                        }
                    }
                }
            }
        }
    }
}