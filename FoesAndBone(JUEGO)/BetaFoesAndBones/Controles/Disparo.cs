using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using BetaFoesAndBones.Personajes;
using static BetaFoesAndBones.Personajes.Felix;
using BetaFoesAndBones.ArmasUniversal;

namespace BetaFoesAndBones.Controles
{

    internal class Disparo : Componentes
    {
        private int disparos;
        private int tiemposDisparos;
        public int Disparos { get { return disparos; } }
        public int TiempoDisparos { get { return tiemposDisparos; } }

        private Keys ultimaTecla = Keys.A;
        private Keys ultimaTeclaAntesDeLanzarElArma = Keys.A;
        private int tiempoCoolDown = 0;
        private bool terminoDeDisparar = false;

        public List<Arma> armasAlanzar;
        public string armalanzar;
        private int numArmaLanzar;
        private bool felixTieneArma;
        public bool estoyLanzandoElArma;
        private Vector2 posicionDelArmaAntesDeLanzar = Vector2.Zero;
        public Vector2 posicionPisoArma = Vector2.Zero;
        private bool elArmaColisiono = false;
        public bool FelixTieneArma { get { return felixTieneArma; } set { felixTieneArma = value; } }

        private Dictionary<Vector2, int> coli;
        private int tilesTamaño = 93;
        private List<Rectangle> intersections;
        private List<Rectangle> intersection;
        private int cambioVertical;
        private int cambioHorizontal;

        private Vector2 felix_posicion;
        public Vector2 Posicion { get { return felix_posicion; } set { felix_posicion = value; } }
        float felix_velocidad;

        public Texture2D magia_textura;
        public List<Magia> proyectiles;
        float magia_velocidad;

        MouseState estadoRaton;
        bool disparoRealizado = false;


        public Texture2D Textura { get { return magia_textura; } set { magia_textura = value; } }
        public Disparo(ContentManager contenedor, Vector2 posicion , Game1 game1, Dictionary<Vector2, int> _coli)
        {
            estoyLanzandoElArma = false;
            armalanzar = "a";
            armasAlanzar = new List<Arma>();
            disparos = 3;
            tiemposDisparos = 0;

            cambioHorizontal = 0;
            cambioVertical = 0;
            intersections = new List<Rectangle>();
            intersection = new List<Rectangle>();
            coli = _coli;
            _content = contenedor;
            _game = game1;
            magia_textura = _content.Load<Texture2D>("Disparos/Disparito");
            
            magia_velocidad = 400f;
            felix_posicion = posicion;

            proyectiles = new List<Magia>();
        }
        
        
        public override void Draw(GameTime gameTime, SpriteBatch sprite)
        {
            foreach (Magia proyectil in proyectiles)
            {
                //_spriteBatch.Draw(magia_textura, (proyectil.Posicion.X, proyectil.Posicion.Y), Color.White);
                sprite.Draw(magia_textura, new Rectangle((int)proyectil.Posicion.X + 25, (int)proyectil.Posicion.Y + 25, 50, 50), Color.White);
            }
        }
        public override void Update(GameTime gameTime)
        {
            elArmaColisiono = false;

            if (disparos != 3)
            {
                tiemposDisparos++;
                if(tiemposDisparos >= 70)
                {
                    disparos++;
                    tiemposDisparos = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W)) ultimaTecla = Keys.W;

            else if (Keyboard.GetState().IsKeyDown(Keys.S)) ultimaTecla = Keys.S;

            else if (Keyboard.GetState().IsKeyDown(Keys.A)) ultimaTecla = Keys.A;

            else if (Keyboard.GetState().IsKeyDown(Keys.D)) ultimaTecla = Keys.D;

            //------------------------------ lanzar arma todavia ver

            //foreach (Arma ta in armasAlanzar)
            //{
            //    ta.Actualizar(gameTime);
            //}

            if (disparos > 0 && !felixTieneArma) { 

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !disparoRealizado)
            {
                if (ultimaTecla == Keys.W)
                {
                    Vector2 direccion = new Vector2(felix_posicion.X, felix_posicion.Y - 10000) - felix_posicion;
                    direccion.Normalize();
                    Magia nuevoProyectil = new Magia(new Vector2(felix_posicion.X - 5, felix_posicion.Y - 65), direccion, magia_velocidad, magia_textura, 20); // Crea un nuevo objeto Magia
                    proyectiles.Add(nuevoProyectil);
                }
                else if (ultimaTecla == Keys.S)
                {
                    Vector2 direccion = new Vector2(felix_posicion.X, felix_posicion.Y + 10000) - felix_posicion;
                    direccion.Normalize();
                    Magia nuevoProyectil = new Magia(new Vector2(felix_posicion.X - 5, felix_posicion.Y + 100), direccion, magia_velocidad, magia_textura, 20); // Crea un nuevo objeto Magia
                    proyectiles.Add(nuevoProyectil);
                 }
                else if (ultimaTecla == Keys.A)
                {
                    Vector2 direccion = new Vector2(felix_posicion.X - 10000, felix_posicion.Y) - felix_posicion;
                    direccion.Normalize();
                    Magia nuevoProyectil = new Magia(new Vector2(felix_posicion.X - 50, felix_posicion.Y + 20), direccion, magia_velocidad, magia_textura, 20); // Crea un nuevo objeto Magia
                    proyectiles.Add(nuevoProyectil);
                 }
                else if (ultimaTecla == Keys.D)
                {
                    Vector2 direccion = new Vector2(felix_posicion.X + 10000, felix_posicion.Y) - felix_posicion;
                    direccion.Normalize();
                    Magia nuevoProyectil = new Magia(new Vector2(felix_posicion.X + 30, felix_posicion.Y + 20), direccion, magia_velocidad, magia_textura, 20); // Crea un nuevo objeto Magia
                    proyectiles.Add(nuevoProyectil);
                }
                disparoRealizado = true;
                    disparos--;
            }
            }

            try
            {

            
            if (armalanzar != "a" && armasAlanzar[int.Parse(armalanzar)] is PistolaElvira)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !disparoRealizado)
                {
                    if (ultimaTecla == Keys.W)
                    {
                        Vector2 direccion = new Vector2(felix_posicion.X, felix_posicion.Y - 10000) - felix_posicion;
                        direccion.Normalize();
                        Magia nuevoProyectil = new Magia(new Vector2(felix_posicion.X - 5, felix_posicion.Y - 65), direccion, magia_velocidad, magia_textura, 30); // Crea un nuevo objeto Magia
                        proyectiles.Add(nuevoProyectil);
                    }
                    else if (ultimaTecla == Keys.S)
                    {
                        Vector2 direccion = new Vector2(felix_posicion.X, felix_posicion.Y + 10000) - felix_posicion;
                        direccion.Normalize();
                        Magia nuevoProyectil = new Magia(new Vector2(felix_posicion.X - 5, felix_posicion.Y + 100), direccion, magia_velocidad, magia_textura, 30); // Crea un nuevo objeto Magia
                        proyectiles.Add(nuevoProyectil);
                    }
                    else if (ultimaTecla == Keys.A)
                    {
                        Vector2 direccion = new Vector2(felix_posicion.X - 10000, felix_posicion.Y) - felix_posicion;
                        direccion.Normalize();
                        Magia nuevoProyectil = new Magia(new Vector2(felix_posicion.X - 50, felix_posicion.Y + 20), direccion, magia_velocidad, magia_textura, 30); // Crea un nuevo objeto Magia
                        proyectiles.Add(nuevoProyectil);
                    }
                    else if (ultimaTecla == Keys.D)
                    {
                        Vector2 direccion = new Vector2(felix_posicion.X + 10000, felix_posicion.Y) - felix_posicion;
                        direccion.Normalize();
                        Magia nuevoProyectil = new Magia(new Vector2(felix_posicion.X + 30, felix_posicion.Y + 20), direccion, magia_velocidad, magia_textura, 30); // Crea un nuevo objeto Magia
                        proyectiles.Add(nuevoProyectil);
                    }
                    disparoRealizado = true;
                    disparos--;
                }
            }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                armalanzar = (int.Parse(armalanzar) - 1).ToString();
            }

            if (armalanzar != "a") numArmaLanzar = int.Parse(armalanzar);
            if (felixTieneArma)
            {
                if(armalanzar != "a") numArmaLanzar = int.Parse(armalanzar);
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        ultimaTeclaAntesDeLanzarElArma = ultimaTecla;
                        posicionDelArmaAntesDeLanzar = armasAlanzar[numArmaLanzar].PosicionArma;
                        estoyLanzandoElArma = true;
                    }
                
            }
            
            if (estoyLanzandoElArma)
            {
                elArmaColisiono = false;
                foreach (var reac in intersection)
                {
                    if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioHorizontal, reac.Y + cambioVertical), out int _val))
                    {
                        elArmaColisiono = true;
                    }
                }

                if (elArmaColisiono)
                    estoyLanzandoElArma = false;
                if(numArmaLanzar < armalanzar.Count())
                    intersection = getIntersectingTilesHorizontal(new Rectangle((int)armasAlanzar[numArmaLanzar].PosicionArma.X, (int)armasAlanzar[numArmaLanzar].PosicionArma.Y, 190, 150));
                else
                {
                    elArmaColisiono = true;
                }

                foreach (var reac in intersection)
                {
                    if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioHorizontal, reac.Y + cambioVertical), out int _val))
                    {
                        elArmaColisiono = true;
                        return;
                    }
                    else elArmaColisiono = false;
                }
                intersection = getIntersectingTilesVertical(new Rectangle((int)armasAlanzar[numArmaLanzar].PosicionArma.X, (int)armasAlanzar[numArmaLanzar].PosicionArma.Y, 190, 150));

                
                felixTieneArma = false;
                if (!elArmaColisiono)
                {
                    if (ultimaTeclaAntesDeLanzarElArma == Keys.D)
                    {
                        if (armasAlanzar[numArmaLanzar].PosicionArma.X < posicionDelArmaAntesDeLanzar.X + 600)
                            armasAlanzar[numArmaLanzar].PosicionArma = new Vector2(armasAlanzar[numArmaLanzar].PosicionArma.X + 10, armasAlanzar[numArmaLanzar].PosicionArma.Y);
                        else estoyLanzandoElArma = false;
                    }
                    else if (ultimaTeclaAntesDeLanzarElArma == Keys.A)
                    {
                        if (armasAlanzar[numArmaLanzar].PosicionArma.X > posicionDelArmaAntesDeLanzar.X - 600)
                            armasAlanzar[numArmaLanzar].PosicionArma = new Vector2(armasAlanzar[numArmaLanzar].PosicionArma.X - 10, armasAlanzar[numArmaLanzar].PosicionArma.Y);
                        else estoyLanzandoElArma = false;
                    }
                    else if (ultimaTeclaAntesDeLanzarElArma == Keys.W)
                    {
                        if (armasAlanzar[numArmaLanzar].PosicionArma.Y > posicionDelArmaAntesDeLanzar.Y - 600)
                            armasAlanzar[numArmaLanzar].PosicionArma = new Vector2(armasAlanzar[numArmaLanzar].PosicionArma.X, armasAlanzar[numArmaLanzar].PosicionArma.Y - 10);
                        else estoyLanzandoElArma = false;
                    }
                    else if (ultimaTeclaAntesDeLanzarElArma == Keys.S)
                    {
                        if (armasAlanzar[numArmaLanzar].PosicionArma.Y < posicionDelArmaAntesDeLanzar.Y + 600)
                            armasAlanzar[numArmaLanzar].PosicionArma = new Vector2(armasAlanzar[numArmaLanzar].PosicionArma.X, armasAlanzar[numArmaLanzar].PosicionArma.Y + 10);
                        else estoyLanzandoElArma = false;
                    }
                    if(estoyLanzandoElArma == false)
                    {
                        posicionPisoArma = armasAlanzar[numArmaLanzar].PosicionArma;
                    }
                }
            }

            if (disparoRealizado)
            {
                tiempoCoolDown += 1;
                if(tiempoCoolDown > 30)
                {
                    disparoRealizado = false;
                    terminoDeDisparar = false;
                    tiempoCoolDown = 0;
                }
            }

            foreach (Magia proyectil in proyectiles)
            {
                proyectil.Actualizar(gameTime);
            }
            for(int i = proyectiles.Count - 1;  i >= 0; i--) {

                intersections = getIntersectingTilesHorizontal(new Rectangle((int)proyectiles[i].Posicion.X, (int)proyectiles[i].Posicion.Y, 50, 50));

                foreach (var reac in intersections)
                {
                    if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioHorizontal, reac.Y + cambioVertical), out int _val))
                    {
                        proyectiles.RemoveAt(i);
                        return;
                    }
                }
            }
            for (int i = proyectiles.Count - 1; i >= 0; i--)
            {

                intersections = getIntersectingTilesVertical(new Rectangle((int)proyectiles[i].Posicion.X, (int)proyectiles[i].Posicion.Y, 50, 50));

                foreach (var reac in intersections)
                {
                    if (coli.TryGetValue(new Vector2(reac.X - 1 + cambioHorizontal, reac.Y + cambioVertical), out int _val))
                    {
                        proyectiles.RemoveAt(i);
                        return;
                    }
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

        public void Colisiones(int cambioH, int cambioV)
        {
            cambioHorizontal = cambioH;
            cambioVertical = cambioV;
        }
        public void Borrar()
        {
            proyectiles.Clear();
        }
    }
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
}
