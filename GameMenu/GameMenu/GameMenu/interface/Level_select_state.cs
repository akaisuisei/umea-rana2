using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Umea_rana.LocalizedStrings;

namespace Umea_rana
{
    class Level_select_state : GameState
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;
        Rectangle rectangle;
        Button button;
        Rectangle rect;
        int tab = 0;
        KeyboardState old;
        SpriteFont font;


        public Level_select_state(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {

            game1.IsMouseVisible = true;
            songMenu = Content.Load<Song>("Menu//songMenu");
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {

            old = Keyboard.GetState();
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics, Audio audio)
        {
            unlocklevel hi = new unlocklevel();
            List<string> list = hi.unlocklevellist();

            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            titre_P = new Vector2(width * 0.8f, height * 0.1f);
            button = new Button(3, 5, width, height, 0.1f, 0.05f, 0);
            button.LoadContent(Content);
            button.activate(0, 0, 0.1f, 0.1f, "SEU", "0", "SEU0");//seu0
            button.activate(0, 1, 0.1f, 0.25f, "PLA", "1", "first",list );
            button.activate(0, 2, 0.1f, 0.40f, "Level3", "2", "level1", list);// seu1
            button.activate(0, 3, 0.1f, 0.65f, "PLA", "3", "Monde_Vue", list);
            button.activate(0, 4, 0.1f, 0.9f, "", LocalizedString.Back);

            button.disable(1, 0);//seu2
            button.activate(1, 1, 0.4f, 0.25f, "PLA", "2", "Monde_du_Gout2", list);
            button.disable(1, 2);//seu3
            button.activate(1, 3, 0.4f, 0.40f, "PLA", "2", "Monde_Toucher", list);
            button.disable(1, 4);

            button.activate(2, 0, 0.7f, 0.1f, "PLA", "3", "Monde_Odorat", list);
            button.disable(2, 1);// seu5
            button.disable(2, 2);
            button.disable(2, 3);//seu7
            button.activate(2, 4, 0.5f, 0.9f, "LevelSelect2J", LocalizedStrings.LocalizedString.Multiplayer);
            background = Content.Load<Texture2D>("Menu//background menu");
            font = Content.Load<SpriteFont>("FontList");
        }
        public override void UnloadContent()
        {
            songMenu.Dispose();
            background.Dispose();
            button.Dispose();

        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            rect = new Rectangle(mouse.X, mouse.Y, 1, 1);
            button.Update(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab, "");
            old = keyboard;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            button.Draw(spriteBatch);
            spriteBatch.DrawString(font, "level select", titre_P, Color.White);
        }
    }

    class Level_select_state2J : GameState
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;
        Rectangle rectangle;
        Button button;
        Rectangle rect;
        int tab = 0;
        KeyboardState old;
        SpriteFont font;


        public Level_select_state2J(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {

            game1.IsMouseVisible = true;
            songMenu = Content.Load<Song>("Menu//songMenu");
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {

            old = Keyboard.GetState();
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics, Audio audio)
        {
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            titre_P = new Vector2(width * 0.8f, height * 0.1f);
            button = new Button(3, 5, width, height, 0.1f, 0.05f, 0);
            button.LoadContent(Content);
            button.activate(0, 0, 0.1f, 0.1f, "SEU", "0", "SEU0");//seu0
            button.activate(0, 1, 0.1f, 0.25f, "PLA", "1", "first");
            button.activate(0, 2, 0.1f, 0.40f, "Level3", "2", "level1");// seu1
            button.activate(0, 3, 0.1f, 0.65f, "PLA", "3", "Monde_Vue");
            button.activate(0, 4, 0.1f, 0.9f, "", LocalizedString.Back);

            button.activate(0, 0, 0.1f, 0.1f, "SEU", "4", "SEU0");//seu0
            button.activate(1, 1, 0.4f, 0.25f, "PLA", "5", "Monde_du_Gout2");
            button.activate(0, 0, 0.1f, 0.1f, "SEU", "6", "SEU0");//seu0
            button.activate(1, 3, 0.4f, 0.40f, "PLA", "7", "Monde_Toucher");
            button.activate(0, 0, 0.1f, 0.1f, "SEU", "8", "SEU0");//seu0

            button.activate(2, 0, 0.7f, 0.1f, "PLA", "9", "Monde_Odorat");
            button.activate(0, 0, 0.1f, 0.1f, "SEU", "10", "SEU0");//seu0
            button.disable(2, 2);
            button.activate(0, 0, 0.1f, 0.1f, "SEU", "11", "SEU0");//seu0
            button.activate(2, 4, 0.5f, 0.9f, "LevelSelect2J", LocalizedStrings.LocalizedString.singleplayer );
            background = Content.Load<Texture2D>("Menu//background menu");
            font = Content.Load<SpriteFont>("FontList");
        }
        public override void UnloadContent()
        {
            songMenu.Dispose();
            background.Dispose();
            button.Dispose();

        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            rect = new Rectangle(mouse.X, mouse.Y, 1, 1);
            button.Update(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab, "");
            old = keyboard;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            button.Draw(spriteBatch);
            spriteBatch.DrawString(font, "level select", titre_P, Color.White);
        }
    }
}
