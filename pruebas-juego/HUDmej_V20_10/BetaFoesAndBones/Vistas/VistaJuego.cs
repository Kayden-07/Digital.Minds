﻿using BetaFoesAndBones.ArmasUniversal;
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
        
        private Felix felix;
        private Mapa Mapa;
        private Slime Slime;
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

        private Texture2D _guita;
        private Texture2D llaves;

        private Texture2D chocar;
        private Rectangle rChocar;
        public VistaJuego(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            Mapa = new Mapa(contenedor);
            felix = new Felix(game, graphicsDevice, contenedor, Mapa.coli, Mapa.habitaciones);
            enemigo = new Enemigos(game, contenedor);
            arma = new Armas(game, contenedor);
            _fuente = _content.Load<SpriteFont>("Fuentes/fuente");
            _fuen = _content.Load<SpriteFont>("Fuentes/arial");
            CirculoUlti = _content.Load<Texture2D>("HUD/UltInactivo");
            _guita = _content.Load<Texture2D>("HUD/Guita");
            llaves = _content.Load<Texture2D>("HUD/Llave");

            disparoMagia = _content.Load<Texture2D>("HUD/MagiaCompleta");
            disparoMagia2 = _content.Load<Texture2D>("HUD/CargaEs1");
            disparoMagia1 = _content.Load<Texture2D>("HUD/CargaEs2");
            disparoMagia0 = _content.Load<Texture2D>("HUD/CargaEs3");

            cuadro = _content.Load<Texture2D>("HUD/VidaFelix");
            cuadroVidaVacio = _content.Load<Texture2D>("HUD/VidaIncompleta");
            cuadroHabilidad = _content.Load<Texture2D>("HUD/Habilidad");
            chocar = _content.Load<Texture2D>("Controles/boton");
            rChocar = new Rectangle(1900, 500, 40, 200);
        }

        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Mapa.Draw(gameTime, spriteBatch);
            enemigo.Draw(gameTime, spriteBatch);
            felix.Draw(gameTime, spriteBatch);
            arma.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(_guita, new Rectangle(50, 155, 35, 35), Color.White);
            //spriteBatch.Draw(llaves, new Rectangle(52, 200, 28, 45), Color.White);
            //
            //spriteBatch.Draw(cuadro, new Rectangle(450, 300, felix.disparo.TiempoDisparos, 40), Color.White);
            //spriteBatch.DrawString(_fuen, felix.disparo.Disparos.ToString(), new Vector2(400, 300), Color.White);
            //spriteBatch.DrawString(_fuen, puntos.ToString() + " "+ felix.cambioH + " " + (Mapa.numCambioH / 93) + " " + Mapa.H, new Vector2(100, 100), Color.White);

           // if (felix.armasPiso.Count > 0)
            //{
                spriteBatch.DrawString(_fuen, puntos.ToString()/* + "  " + Mapa.cambio + "  " + Mapa.cambioV + "  " + felix.armasPiso[0].PosicionArma.X.ToString()*/, new Vector2(100, 147), Color.White);
            //}
            //else spriteBatch.DrawString(_fuen, puntos.ToString() + "  " + Mapa.cambio + "  " + Mapa.cambioV + "  " + Mapa.cambioH, new Vector2(50, 155), Color.White);

            spriteBatch.Draw(CirculoUlti, new Rectangle(30,30, 100, 100), Color.White);

            if (felix.disparo.Disparos == 3)
            {
                spriteBatch.Draw(disparoMagia, new Rectangle(27, 15, 122, 132), Color.White);
            }
            else if (felix.disparo.Disparos == 2)
            {
                spriteBatch.Draw(disparoMagia2, new Rectangle(27, 15, 122, 132), Color.White);
            }
            else if (felix.disparo.Disparos == 1)
            {
                spriteBatch.Draw(disparoMagia1, new Rectangle(27, 15, 122, 132), Color.White);
            }
            else if (felix.disparo.Disparos == 0)
            {
                spriteBatch.Draw(disparoMagia0, new Rectangle(27, 15, 122, 132), Color.White);
            }

            spriteBatch.Draw(cuadro,new Rectangle(150,50,( felix.vida * 3), 40), Color.White); //cuadro de vida
            spriteBatch.Draw(cuadroVidaVacio, new Rectangle(150, 50, 302, 40), Color.White * 0.4f);
            spriteBatch.Draw(cuadroHabilidad, new Rectangle(138, 95, 311, 70),Color.White);
            //spriteBatch.DrawString(_fuen, felix.vida.ToString(), new Vector2(100, 150), Color.White);
            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            enemigo.felixTieneArma = felix.tieneArma;
            enemigo.felixLanzaArma = felix.lanzaArma;
            enemigo.armasPiso = felix.armasPiso;
            enemigo.numArma = felix.numArma;
            
            enemigo.proyectilesE = felix.disparo.proyectiles;
            enemigo.felix_posicion = felix._position;
            felix.enemigoList = enemigo.enemigos;

            felix.armasPiso = arma.ArmasLista;
            arma.ArmasLista = felix.armasPiso;

            Mapa.posicionParedesH = felix.posicionParedesH;
            Mapa.posicionParedesV = felix.posicionParedesV;
            puntos = enemigo.puntos;
            pt = "puntos: " + puntos.ToString();
            pt = "Habitacion: " + felix.habitacion;
            enemigo.Update(gameTime);
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
