using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Umea_rana.LocalizedStrings;

namespace Umea_rana
{
    class GameWin : GameState
    {
        Song songMenu;
        Texture2D background;

        Rectangle rectangle;

        Button button;
        Rectangle mouserec;
        KeyboardState old;
        int tab;
        hightscorepanel hict;

        public class hightscorepanel
        {
            Rectangle fond;
            Vector2 resultatGlobal, highscore;
            Vector2 resultatJ1, resultatj2;
            Vector2 scorej1, scorej2;
            SpriteFont font;
            Texture2D texture;
            string resultaglo, resultj1, resulj2, highscor;
            public hightscorepanel()
            {
            }
            public void loadcontent(Rectangle fondaffichage, ContentManager content)
            {
                this.fond = fondaffichage;
                this.font = content.Load<SpriteFont>("FontList");
                this.texture = content.Load<Texture2D>("ListBoxBG");
                resultj1 = "";
                resultaglo = "";
                resulj2 = "";
                resultatGlobal = new Vector2(fond.X, fond.Y);
                highscore = new Vector2(fond.Left , fond.Y + 30);
                resultatJ1 = new Vector2(fond.Left, fond.Y + 60);
                resultatj2 = new Vector2(fond.Center.X, resultatJ1.Y  );
                scorej1 = new Vector2(resultatJ1.X, fond.Y + 90);
                scorej2 = new Vector2(resultatj2.X , scorej1.Y  );
                highscor = "";
            }
            public void Update(Game1 game)
            {
                resultaglo = game.endlevel;
                resulj2 = game.score.Y.ToString();
                resultj1 = game.score.X.ToString();
                highscor = LocalizedString.Highscore + ": " + game.highscor;
            }
            public void draw(SpriteBatch sp)
            {
                sp.Draw(texture, fond, Color.Gray);
                sp.DrawString(font, resultaglo, resultatGlobal, Color.Black);
                sp.DrawString(font, resultj1, scorej1, Color.Black);
               
                sp.DrawString(font, highscor , highscore , Color.Black);
                sp.DrawString(font, LocalizedString.player + " 1", resultatJ1, Color.Black);
                if (resulj2  != "0")
                {
                    sp.DrawString(font, resulj2, scorej2, Color.Black);
                    sp.DrawString(font, LocalizedString.player + " 2", resultatj2, Color.Black);
                }
            }
        }

        public GameWin(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {

            game1.IsMouseVisible = true;
            hict = new hightscorepanel();
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {

            tab = 0;
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics, Audio audio)
        {

            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;

            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            button = new Button(1, 4, width, height, 0.07f, 0.1f, 0);
            button.LoadContent(Content);
            button.activate(0, 0, 0.1f, 0.1f, "Next", LocalizedString.Next);
            button.activate(0, 1, 0.1f, 0.2f, "Last", LocalizedString.Replay);
            button.activate(0, 2, 0.1f, 0.3f, "", LocalizedString.Menu);
            button.activate(0, 3, 0.1f, 0.4f, "Exit", LocalizedString.Exit); background = Content.Load<Texture2D>("Menu//gamewin");
            hict.loadcontent(new Rectangle(width/2, height-120, width / 2, 120), Content);

        }
        public override void UnloadContent()
        {
            background.Dispose();
            button.Dispose();
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            mouserec = new Rectangle(mouse.X, mouse.Y, 1, 1);
            button.Update(ref keyboard, ref old, ref mouse, ref mouserec, ref game, ref tab, "");
            hict.Update(game);
            old = keyboard;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            button.Draw(spriteBatch);
            hict.draw(spriteBatch );
        }
    }
}

