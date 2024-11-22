using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace BetaFoesAndBones.Vistas
{
    internal class VistaCreditos : Vista
    {
        Dictionary<string, List<string[]>> creditos;
        private SpriteFont _fuen;
        private SpriteFont _titulos;
        private int w;
        private float movimiento;
        private string textoAgradecimientosEspeciales;
        Vector2 posAgradecimientos;
        private Texture2D logoEmpresa;
        private float posicionLogo;
        public VistaCreditos(Game1 game, GraphicsDevice graphicsDevice, ContentManager contenedor) : base(game, graphicsDevice, contenedor)
        {
            posicionLogo = 0;
            logoEmpresa = _content.Load<Texture2D>("creditos/logoSticker");
            _fuen = _content.Load<SpriteFont>("Fuentes/creditos");
            _titulos = _content.Load<SpriteFont>("Fuentes/creditosTitulos");
            losCreditos();
            w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            movimiento = 0;
            textoAgradecimientosEspeciales = "Queremos darles un agradecimiento especial a todas las personas que estuvieron \n      con nosotros en el proceso de creacion de Foes and Bones \n       y nos dieron siempre una mano y su completo apoyo";
            posAgradecimientos = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // Posición inicial del texto
            Vector2 posicionInicialT = new Vector2(w, 100);
            Vector2 posicionInicial = new Vector2(600, 100);
            int col1 = 400;
            int col2 =( w/2) + 200;
            // Espaciado entre líneas (puedes ajustarlo a tu gusto)
            float espaciado = 40f;
            float espaciadoTitulos = 90f;
            float espacioTotal = 0;

            // Dibujar cada línea con el espaciado personalizado
            foreach(var c in creditos)
            {
                var x = (w / 2) - (50 * (c.Key.Length / 2));
                espacioTotal += espaciadoTitulos;
                Vector2 posicionTitulo = new Vector2(x, posicionInicialT.Y + espacioTotal - movimiento);
                spriteBatch.DrawString(_titulos, c.Key, posicionTitulo, Color.White);
                for (int j = 0; j < c.Value.Count; j++)
                {
                    espacioTotal += (j == 0) ? espaciadoTitulos + espaciado : espaciado;

                    for (int e = 0; e < c.Value[j].Length; e++)
                    {
                        Vector2 posicionLinea = new Vector2((e == 0) ? col1 : col2, posicionInicial.Y + espacioTotal - movimiento);
                        if (c.Key == "Agradecimientos Especiales" && j == 0)
                        {
                            posAgradecimientos = new Vector2(x, posicionLinea.Y);
                            spriteBatch.DrawString(_fuen, textoAgradecimientosEspeciales, posAgradecimientos, Color.White);
                        }
                        spriteBatch.DrawString(_fuen, c.Value[j][e], posicionLinea, Color.White);


                    }
                }
            }
            posicionLogo = espacioTotal + 400;
            spriteBatch.Draw(logoEmpresa, new Rectangle(400, (int)(posicionLogo - movimiento), 1280, 720), Color.White);
            spriteBatch.DrawString(_fuen, "Gracias sumergirte en el mundo de Foes and Bones. Tu apoyo es lo que nos motiva a seguir creando \nexperiencias inolvidables. Nos vemos en la proxima aventura...", new Vector2(200, posicionLogo + 800 - movimiento), Color.White);
            spriteBatch.End();
        }

        public override void PosUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if(movimiento < 3500)
            movimiento += 85*(float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        private void losCreditos()
        {
            List<string> nom = new List<string>() { "Muriel Soiffer", "Gino Mazzaglia", "Martin Soria" };
            creditos = new Dictionary<string, List<string[]>>
            {
                {"Direccion del proyecto", new List<string[]>
              {
                    new string[]{ "Director de Produccion", nom[1] },
                    new string[]{ "Productor Ejecutivo", nom[1]},
                    new string[]{ "Director Creativo", nom[2]},
              }},
                {"Programacion", new List<string[]>
              {
                    new string[]{ "Programador Principal", nom[0]},
                    new string[]{ "Desarrollador de Interfaz de Usuario", nom[0]},
                    new string[]{ "Desarrollador de Herramientas", nom[0]},
                    new string[]{ "Desarrollador de Animaciones", nom[0]},
                    new string[]{ "Programador de Optimizacion", nom[0]},
                    new string[]{ "Desarrollador de Gameplay", nom[1]},
              }},
                { "Arte y Produccion Visual", new List<string[]>
              {
                    new string[]{ "Director de Arte", nom[2]},
                    new string[]{ "Artista de Conceptos", nom[2]},
                    new string[]{ "Encargado de Entornos", nom[0]},
                    new string[]{ "Artista de Personajes", nom[2]},
                    new string[]{ "Animador 2D", nom[2]},
                    new string[]{ "Encargado de Efectos Visuales", nom[2]},
                    new string[]{ "Iluminacion", nom[0]}
              }},
                { "Creacion de Niveles", new List<string[]>
              {
                    new string[]{ "Jefe de Creacion de Niveles", nom[1]},
                    new string[]{ "Desarrollador de Niveles", nom[0]}
              }},
                { "Pruebas de Calidad", new List<string[]>
              {
                    new string[]{ "Tester Principal", "Bautista Lobo"},
                    new string[]{ "Tester", "Maximo Melfi"},
                    new string[]{ "Tester", "Facundo Godoy"}
              }},
                { "Agradecimientos Especiales", new List<string[]>
              {
                    new string[]{""},
                    new string[]{""},
                    new string[]{""},
                    new string[]{""},
                    new string[]{ "Profesores", "Silvina Rodriguez"},
                    new string[]{ "", "Pablo Fisella"},
                    new string[]{ "", "Karim Nehmi" },
                    new string[]{ "", "Oscar Segovia" },
                    new string[]{ "", "Estela Viana" },
                    new string[]{""},
                    new string[]{ "Colegas y amigos", "Lobo Bautista"},
                    new string[]{ "", "Santino Melfi"},
                    new string[]{ "", "Agustin Marino" },
                    new string[]{ "", "Fabricio Amado" },
                    new string[]{ "", "Maximo Goitia" },
                    new string[]{ "", "Facundo Godoy" },
                    new string[]{""},
                    new string[]{ "Familiares", "Gabriela Lopez"},
                    new string[]{ "", "Theo Soiffer"},
                    new string[]{ "", "Jorge Soria" },
                    new string[]{ "", "Malena Soria" },
                    new string[]{ "", "Gloria Bareiro" },
                    new string[]{ "", "Ruben Soria" },
                    new string[]{ "", "Daniel Soria" },
                    new string[]{ "", "Fidelina Bareiro" },
                    new string[]{ "", "Macarena Anastopulos" },
                    new string[]{ "", "Lucas Anastopulos" },
                    new string[]{ "", "Liliana Canzian" },
                    new string[]{""},
                    new string[]{ "Mascotas de la empresa", "Sofia, Kira y Zeki"},
                    new string[]{""},
                    new string[]{"  Y obviamente gracias a nuestra comunidad por apoyarnos siempre"}
              }}
            };
        }
    }
}
