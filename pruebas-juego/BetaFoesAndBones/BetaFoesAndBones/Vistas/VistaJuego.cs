﻿using BetaFoesAndBones.Controles;
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
        public int puntos;
        public string pt;


        // --------HUD----------

        private SpriteFont _fuente;
        private SpriteFont _fuen;
        private Texture2D _corazon;
        private Texture2D cuadro;
        private Texture2D _guita;
        public VistaJuego(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            Mapa = new Mapa(contenedor);
            felix = new Felix(game, graphicsDevice, contenedor, Mapa.coli);
            enemigo = new Enemigos(game, contenedor);
            _fuente = _content.Load<SpriteFont>("Fuentes/fuente");
            _fuen = _content.Load<SpriteFont>("Fuentes/arial");
            _corazon = _content.Load<Texture2D>("HUD/corazon");
            _guita = _content.Load<Texture2D>("HUD/Guita");
            cuadro = _content.Load<Texture2D>("Controles/boton");
        }

        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Mapa.Draw(gameTime, spriteBatch);
            enemigo.Draw(gameTime, spriteBatch);
            felix.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(_guita, new Rectangle(50, 100, 40, 40), Color.White);
            spriteBatch.DrawString(_fuen, puntos.ToString(), new Vector2(100, 100), Color.White);
            spriteBatch.Draw(_corazon,new Rectangle(50,150, 40, 40), Color.White);
            spriteBatch.Draw(cuadro,new Rectangle(100,150,( felix.vida * 2), 40), Color.Red);
            //spriteBatch.DrawString(_fuen, felix.vida.ToString(), new Vector2(100, 150), Color.White);
            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            enemigo.proyectilesE = felix.disparo.proyectiles;
            enemigo.felix_posicion = felix._position;
            felix.enemigoList = enemigo.enemigos;
            puntos = enemigo.puntos;
            pt = "puntos: " + puntos.ToString();
            enemigo.Update(gameTime);
            felix.Update(gameTime);
            Mapa.Update(gameTime);
        }
    }
}
