using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using static enemigos_siguen.Game1;

namespace enemigos_siguen
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D felix_textura;
        Texture2D slimeTextura;
        Texture2D bacterianoTextura;
        Texture2D draconanioTextura;
        Texture2D magia_textura;
        Texture2D magia_fuerte_textura;

        Vector2 felix_posicion;

        float felix_velocidad;

        float tiempoTranscurridoSlime;
        float tiempoTranscurridoBacteriano;
        float tiempoTranscurridoDraconiano;

        List<Enemigo> enemigos = new List<Enemigo>();
        List<Magia> proyectiles = new List<Magia>();
       
        MouseState estadoRaton;
        bool disparoRealizado = false;

        public class Magia
        {
            private Vector2 posicion;
            private Vector2 direccion;
            private float velocidad;
            private Texture2D textura;
            private float daño;
            
            public Vector2 Posicion
            {
                get { return posicion; }
                set { posicion = value; }
            }
            public Vector2 Direccion
            {
                get { return direccion; }
                set { direccion = value; }
            }
            public float Velocidad
            {
                get { return velocidad; }
                set { velocidad = value; }
            }
            public Texture2D Textura
            {
                get { return textura; }
                set { textura = value; }
            }
            public float Daño
            {
                get { return daño; }
                set { daño = value; }
            }
    public Magia(Vector2 posicion, Vector2 direccion, float velocidad, Texture2D textura, float daño)
            {
                Posicion = posicion;
                Direccion = direccion;
                Velocidad = velocidad;
                Textura = textura;
                Daño = daño;
               
            }

            public void Actualizar(GameTime gameTime)
            {
                Posicion += Direccion * Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
                
            }

          
            

        }

        public abstract class Enemigo
        {
            private Texture2D textura;
            private Vector2 posicion;
            private float velocidad;
            private float tiempoAparicion;
            private float hp; 
            
            public Texture2D Textura
            {
                get { return textura; }
                set { textura = value; }
            }
            public Vector2 Posicion
            {
                get { return posicion; }
                set { posicion = value; }
            }
            public float Velocidad
            {
                get { return velocidad; }
                set { velocidad = value; }
            }
            public float TiempoAparicion
            {
                get { return tiempoAparicion; }
                set { tiempoAparicion = value; }
            }
            public float HP
            {
                get { return hp; }
                set { hp = value; }
            }

          

            public Enemigo(Texture2D textura, Vector2 posicion, float velocidad, float tiempoAparicion,float hp)
            {
                Textura = textura;
                Posicion = posicion;
                Velocidad = velocidad;
                TiempoAparicion = tiempoAparicion;
                HP = hp;
              
            }
          
            public void Update(GameTime gameTime, Vector2 felix_posicion, int windowWidth, int windowHeight)
            {
                var distancia = felix_posicion - Posicion;
                Vector2 direccion = Vector2.Normalize(distancia);
                Posicion += direccion * Velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Posicion = new Vector2(
                    MathHelper.Clamp(Posicion.X, 0, windowWidth - Textura.Width),
                    MathHelper.Clamp(Posicion.Y, 0, windowHeight - Textura.Height)


                );
            }
            
        }
       

        public class Slime : Enemigo
        {
            public Slime(Texture2D textura, Vector2 posicion)
                : base(textura, posicion, 120f, 3f, 20)
            {
            }
        }

        public class Bacteriano : Enemigo
        {
            public Bacteriano(Texture2D textura, Vector2 posicion)
                : base(textura, posicion, 100f, 9f,60)
            {
            }
        }

        public class Draconario : Enemigo
        {
            public Draconario(Texture2D textura, Vector2 posicion)
                : base(textura, posicion, 80f, 20f, 100)
            {
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            felix_posicion = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            felix_velocidad = 350f;
            tiempoTranscurridoSlime = 0f;
            tiempoTranscurridoBacteriano = 0f;
            tiempoTranscurridoDraconiano = 0f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            felix_textura = Content.Load<Texture2D>("Felixp");
            magia_textura = Content.Load<Texture2D>("Disparito");
            magia_fuerte_textura = Content.Load<Texture2D>("magia");
            slimeTextura = Content.Load<Texture2D>("Slimep");
            bacterianoTextura = Content.Load<Texture2D>("Viruseanop");
            draconanioTextura = Content.Load<Texture2D>("Draconariop");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var tecla = Keyboard.GetState();

            if (tecla.IsKeyDown(Keys.W))
            {
                felix_posicion.Y -= felix_velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (tecla.IsKeyDown(Keys.S))
            {
                felix_posicion.Y += felix_velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (tecla.IsKeyDown(Keys.A))
            {
                felix_posicion.X -= felix_velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (tecla.IsKeyDown(Keys.D))
            {
                felix_posicion.X += felix_velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
          /*  if (tecla.IsKeyDown(Keys.E))
            {
                Vector2 direccion = new Vector2(estadoRaton.X, estadoRaton.Y) - felix_posicion;
                direccion.Normalize();

                Magia nuevoProyectil = new Magia(felix_posicion, direccion, 80f, magia_textura, /*daño*//*100);
                proyectiles.Add(nuevoProyectil);
                disparoRealizado = true;
        }
        */
            tiempoTranscurridoSlime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tiempoTranscurridoBacteriano += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tiempoTranscurridoDraconiano += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Random random = new Random();

            if (tiempoTranscurridoSlime >= 3f)
            {
                Enemigo nuevoSlime = null;
                do
                {
                    nuevoSlime = new Slime(slimeTextura, new Vector2(random.Next(0, Window.ClientBounds.Width), random.Next(0, Window.ClientBounds.Height)));
                } while (Vector2.Distance(nuevoSlime.Posicion, felix_posicion) < 250);
                enemigos.Add(nuevoSlime);
                tiempoTranscurridoSlime = 0f;
            }

            if (tiempoTranscurridoBacteriano >= 9f)
            {
                Enemigo nuevoBacteriano = null;
                do
                {
                    nuevoBacteriano = new Bacteriano(bacterianoTextura, new Vector2(random.Next(0, Window.ClientBounds.Width), random.Next(0, Window.ClientBounds.Height)));
                } while (Vector2.Distance(nuevoBacteriano.Posicion, felix_posicion) < 250);
                enemigos.Add(nuevoBacteriano);
                tiempoTranscurridoBacteriano = 0f;
            }

            if (tiempoTranscurridoDraconiano >= 20f)
            {
                Enemigo nuevoDraconario = null;
                do
                {
                    nuevoDraconario = new Draconario(draconanioTextura, new Vector2(random.Next(0, Window.ClientBounds.Width), random.Next(0, Window.ClientBounds.Height)));
                } while (Vector2.Distance(nuevoDraconario.Posicion, felix_posicion) < 300);
                enemigos.Add(nuevoDraconario);
                tiempoTranscurridoDraconiano = 0f;
            }

            int windowWidth = Window.ClientBounds.Width;
            int windowHeight = Window.ClientBounds.Height;

            foreach (var enemigo in enemigos)
            {
                enemigo.Update(gameTime, felix_posicion, windowWidth, windowHeight);
            }

            estadoRaton = Mouse.GetState();
            if (estadoRaton.LeftButton == ButtonState.Pressed && !disparoRealizado)
            {
                Vector2 direccion = new Vector2(estadoRaton.X, estadoRaton.Y) - felix_posicion;
                direccion.Normalize();

                Magia nuevoProyectil = new Magia(felix_posicion, direccion, 200f, magia_textura, /*daño*/20);
                proyectiles.Add(nuevoProyectil);
                disparoRealizado = true;
            }

            if (estadoRaton.LeftButton == ButtonState.Released)
            {
                disparoRealizado = false;
            }

            foreach (Magia proyectil in proyectiles.ToList())
            {
                proyectil.Actualizar(gameTime);

                if (proyectil.Posicion.X < 0 || proyectil.Posicion.X > Window.ClientBounds.Width ||
                    proyectil.Posicion.Y < 0 || proyectil.Posicion.Y > Window.ClientBounds.Height)
                {
                    proyectiles.Remove(proyectil);
                }
                
            }
          
            List<Enemigo> muertos = new List<Enemigo>(); 
            List<Magia> impacto = new List<Magia>();


        foreach ( Enemigo enemy in enemigos.ToList())
        {
                foreach (Magia p in proyectiles.ToList())
                {
                    if (enemy.Posicion.X < p.Posicion.X + p.Textura.Width &&
                        enemy.Posicion.X + enemy.Textura.Width > p.Posicion.X &&
                        enemy.Posicion.Y < p.Posicion.Y + p.Textura.Height &&
                        enemy.Posicion.Y + enemy.Textura.Height > p.Posicion.Y)
                    {
                        proyectiles.Remove(p);
                        enemy.HP -= p.Daño;
                    }
                    if ( enemy.HP < 0 || enemy.HP == 0) {
                     
                       enemigos.Remove(enemy);
                    }
                }
            }


            base.Update(gameTime) ;
            


        }
        
      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(felix_textura, felix_posicion, Color.White);

            foreach (var enemigo in enemigos)
            {
                _spriteBatch.Draw(enemigo.Textura, enemigo.Posicion, Color.White);
            }

            foreach (var proyectil in proyectiles)
            {
                _spriteBatch.Draw(proyectil.Textura, proyectil.Posicion, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        
    }
}