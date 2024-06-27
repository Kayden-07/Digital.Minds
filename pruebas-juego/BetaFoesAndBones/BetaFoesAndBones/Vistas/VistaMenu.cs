using BetaFoesAndBones.Controles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaFoesAndBones.Vistas
{
    public class VistaMenu : Vista
    {
        
        private List<Componentes> _componentes;

        public VistaMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            var botonTexture = _content.Load<Texture2D>("Controles/boton");
            var botonFuente = _content.Load<SpriteFont>("Fuentes/fuente");
            int x = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (botonTexture.Width / 2);
            int y = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - 300;

            var botonNuevoJuego = new Boton(botonTexture, botonFuente)
            {
                Posicion = new Vector2(x,y + 200),
                Texto = "New Game",
            };
            botonNuevoJuego.Click += BotonNuevoJuego_Click;

            var botonCargarJuego = new Boton(botonTexture, botonFuente)
            {
                Posicion = new Vector2(x, y + 300),
                Texto = "Cargar Juego",
            };

            botonCargarJuego.Click += BotonCargarJuego_Click;

            var botonSalir = new Boton(botonTexture, botonFuente)
            {
                Posicion = new Vector2(x, y + 400),
                Texto = "Salir",
            };

            botonSalir.Click += BotonSalir_Click;

            _componentes = new List<Componentes>()
            {
                botonNuevoJuego,
                botonCargarJuego,
                botonSalir,
            };
        }

        private void BotonSalir_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void BotonCargarJuego_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LoadGame");
        }

        private void BotonNuevoJuego_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new VistaJuego(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var componente in _componentes)
                componente.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var componente in _componentes)
                componente.Update(gameTime);
        }
    }

    }

