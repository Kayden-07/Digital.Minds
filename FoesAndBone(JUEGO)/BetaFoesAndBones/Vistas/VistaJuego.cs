using BetaFoesAndBones.ArmasUniversal;
using BetaFoesAndBones.Controles;
using BetaFoesAndBones.Personajes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones.Vistas
{
    internal class VistaJuego : Vista
    {
        private float posicionSlie;
        private float vidaCangrejo;
        private JefeCangrejo jC;
        
        private Felix felix;
        private Mapa Mapa;
        private Enemigos enemigo;
        private Armas arma;
        public int puntos;
        public string pt;


        // --------HUD----------

        private SpriteFont _fuente;
        private SpriteFont _fuen;
        private Texture2D CirculoUlti;
        private Texture2D cuadro;
        private Texture2D cuadroVidaVacio;
        private Texture2D cuadroHabilidad;
        private Texture2D disparoMagia;
        private Texture2D disparoMagia1;
        private Texture2D disparoMagia2;
        private Texture2D disparoMagia0;

        private Texture2D Hab1Tuto;
        private Texture2D Hab2Tuto;
        private Texture2D TirarArmaTuto;

        private Texture2D _guita;

        private Texture2D chocar;
        private Rectangle rChocar;
        public VistaJuego(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            posicionSlie = 0;
            vidaCangrejo = 0;
            Mapa = new Mapa(contenedor);
            felix = new Felix(game, graphicsDevice, contenedor, Mapa.coli, Mapa.habitaciones);
            enemigo = new Enemigos(game, contenedor);
            arma = new Armas(game, contenedor);
            _fuente = _content.Load<SpriteFont>("Fuentes/fuente");
            _fuen = _content.Load<SpriteFont>("Fuentes/arial");
            CirculoUlti = _content.Load<Texture2D>("HUD/UltInactivo");
            disparoMagia = _content.Load<Texture2D>("HUD/MagiaCompleta");
            disparoMagia2 = _content.Load<Texture2D>("HUD/CargaEs1");
            disparoMagia1 = _content.Load<Texture2D>("HUD/CargaEs2");
            disparoMagia0 = _content.Load<Texture2D>("HUD/CargaEs3");

            Hab1Tuto = _content.Load<Texture2D>("Tutorial/Hab1Tuto1");
            Hab2Tuto = _content.Load<Texture2D>("Tutorial/DesmemAgarrar1");
            TirarArmaTuto = _content.Load<Texture2D>("Tutorial/LanzaeArmaTuto1");

            cuadro = _content.Load<Texture2D>("HUD/VidaFelix");
            cuadroVidaVacio = _content.Load<Texture2D>("HUD/VidaIncompleta");
            cuadroHabilidad = _content.Load<Texture2D>("HUD/Habilidad");
            _guita = _content.Load<Texture2D>("HUD/Guita");
            chocar = _content.Load<Texture2D>("Controles/boton");
            rChocar = new Rectangle(1900, 500, 40, 200);
            jC = new JefeCangrejo(_guita, _guita, _guita, _guita, _guita, _guita, new Vector2(0, 0), _guita, _guita);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Mapa.Draw(gameTime, spriteBatch);
            if (felix.habitacion == 1)
            {
                spriteBatch.Draw(Hab1Tuto, new Rectangle(610, 300, 600, 600), Color.White * 0.59f);
            }
            if (felix.habitacion == 2)
            {
                spriteBatch.Draw(Hab2Tuto, new Rectangle(550, 100, 730, 600), Color.White * 0.59f);
                spriteBatch.Draw(TirarArmaTuto, new Rectangle(600, 500, 750, 620), Color.White * 0.59f);
            }

            enemigo.Draw(gameTime, spriteBatch);
            felix.Draw(gameTime, spriteBatch);
            arma.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(_guita, new Rectangle(50, 155, 35, 35), Color.White);

            spriteBatch.DrawString(_fuen, puntos.ToString() , new Vector2(100, 147), Color.White);

            spriteBatch.Draw(CirculoUlti, new Rectangle(30, 30, 100, 100), Color.White);

            if (felix.disparo.Disparos == 3)
            {
                spriteBatch.Draw(disparoMagia, new Rectangle(27, 15, 122, 132), Color.White);
            }
            else if (felix.disparo.Disparos == 2 && felix.tieneArma == false)
            {
                spriteBatch.Draw(disparoMagia2, new Rectangle(27, 15, 122, 132), Color.White);
            }
            else if (felix.disparo.Disparos == 1 && felix.tieneArma == false)
            {
                spriteBatch.Draw(disparoMagia1, new Rectangle(27, 15, 122, 132), Color.White);
            }
            else if (felix.disparo.Disparos == 0 && felix.tieneArma == false)
            {
                spriteBatch.Draw(disparoMagia0, new Rectangle(27, 15, 122, 132), Color.White);
            }
            else
            {
                spriteBatch.Draw(disparoMagia, new Rectangle(27, 15, 122, 132), Color.White);
            }

            spriteBatch.Draw(cuadro, new Rectangle(150, 50, (felix.vida * 3), 40), Color.White); //cuadro de vida
            spriteBatch.Draw(cuadroVidaVacio, new Rectangle(150, 50, 302, 40), Color.White * 0.4f);
            spriteBatch.Draw(cuadroHabilidad, new Rectangle(138, 95, 311, 70), Color.White);

            if (felix.habitacion == 11)
            {
                spriteBatch.Draw(cuadro, new Rectangle(400, 850, (int)vidaCangrejo, 40), Color.Red);
            }
            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Enemigo e in enemigo.enemigos)
            {
                if(e is JefeCangrejo)
                {
                    jC = (JefeCangrejo)e;
                    vidaCangrejo = e.HP;
                    if (jC.DañoExplosion)
                    {
                        felix.vida -= 20;
                    }
                }
                if (e is Slime)
                {
                    posicionSlie = e.Posicion.X;
                }
            }

            enemigo.felixTieneArma = felix.tieneArma;
            enemigo.felixLanzaArma = felix.lanzaArma;
            enemigo.armasPiso = felix.armasPiso;
            enemigo.numArma = felix.numArma;
            enemigo.RecibirEnemigosMuertos(felix.enemigosMuertos);
            
            enemigo.proyectilesE = felix.disparo.proyectiles;
            enemigo.felix_posicion = felix._position;
            felix.enemigoList = enemigo.enemigos;

            felix.armasPiso = arma.ArmasLista;
            arma.ArmasLista = felix.armasPiso;
            arma.ejecucion = felix.EjecucionF;
            arma.numArma = felix.numArma;
            arma.EnemigoLista = enemigo.enemigosMuertos;
            felix.armaver = arma;

            Mapa.posicionParedesH = felix.posicionParedesH;
            Mapa.posicionParedesV = felix.posicionParedesV;
            puntos = enemigo.puntos;
            pt = "puntos: " + puntos.ToString();
            pt = "Habitacion: " + felix.habitacion;
            if(felix.EjecucionF == 0)
                enemigo.Update(gameTime);
            arma.Update(gameTime);
            felix.Update(gameTime);
            Mapa.Update(gameTime);

            if (Mapa.cam)
            {
                felix.Mapa = 0;
                felix.MapaVertical = 0;
                felix.MapaHorizontal = 0;
                Mapa.cam = false;
            }
            Mapa.mostrarPuertas = (enemigo.enemigos.Count <= 0) ? false : true;
            Mapa.cambio = felix.Mapa;
            Mapa.cambioV = felix.MapaVertical;
            Mapa.cambioH = felix.MapaHorizontal;
            felix.H = Mapa.H;
            enemigo.PonerEnemigos(felix.habitacion);
        }
    }
}
