using MenuInicio.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuInicio.States
{
    public class MenuState : State
    {

        private List<Component> _components;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/boton");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            int x = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (buttonTexture.Width / 2);
            int y = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - 300;
            

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(x,y + 200),
                Text = "New Game",
            };
            newGameButton.Click += NewGameButton_Click;
            
            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(x, y + 250),
                Text = "Load Game",
            };

            loadGameButton.Click += LoadGameButton_Click; 

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(x, y + 300),
                Text = "Quit",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                loadGameButton,
                quitGameButton,
            };
        }

        
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LoadGame");
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
