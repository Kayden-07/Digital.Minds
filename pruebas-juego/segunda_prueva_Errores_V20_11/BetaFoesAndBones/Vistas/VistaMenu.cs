using BetaFoesAndBones.Controles;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace BetaFoesAndBones.Vistas
{
    public class VistaMenu : Vista
    {
        
        private List<Boton> _componentes;
        private Texture2D _portada;
        private int w;
        private int h;
        private int valorActual;
        private bool siguienteBtn;
        private bool anteriorBtn;
        public VistaMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            siguienteBtn = false;
            anteriorBtn = false;
            valorActual = 1;
            _portada = _content.Load<Texture2D>("portada_inicio");
            var botonTexture = _content.Load<Texture2D>("Controles/Boton_A");
            var botonFuente = _content.Load<SpriteFont>("Fuentes/fuente");
            w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int x = (w / 2) - (botonTexture.Width / 2);
            int y = (h / 2) - 300;

            var botonNuevoJuego = new Boton(botonTexture, botonFuente, 1)
            {
                Posicion = new Vector2(x, y + 600),
                Texto = "Nuevo Juego",
            };
            botonNuevoJuego.Click += BotonNuevoJuego_Click;

            var botonSalir = new Boton(botonTexture, botonFuente, 2)
            {
                Posicion = new Vector2(x + 400, y + 600),
                Texto = "Salir",
            };

            botonSalir.Click += BotonSalir_Click;

            _componentes = new List<Boton>()
            {
                botonNuevoJuego,
                botonSalir,
            };
        }

        private void BotonSalir_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void BotonNuevoJuego_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new VistaJuego(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_portada,new Rectangle(0,0,w,h),Color.White);
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
            {
                if (valorActual == componente.Valor) componente.SobreBtn = true;
                else componente.SobreBtn = false;
                componente.Update(gameTime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) siguienteBtn = true;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) anteriorBtn = true;
            if (Keyboard.GetState().IsKeyUp(Keys.D) && siguienteBtn)
            {
                siguienteBtn = false;
                valorActual = (valorActual < _componentes.Count) ? valorActual + 1 : 1;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.A) && anteriorBtn)
            {
                anteriorBtn = false;
                valorActual = (valorActual > 1) ? valorActual - 1 : _componentes.Count;
            }
        }
    }

    }

