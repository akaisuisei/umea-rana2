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
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            titre_P = new Vector2(width * 0.8f, height * 0.1f);
            button = new Button(3, 5, width, height, 0.1f, 0.05f, 0);
            button.LoadContent(Content);
            button.activate(0, 0, 0.1f, 0.1f, "SEU", "1", "SEU1");
            button.activate(0, 1, 0.1f, 0.25f, "Level2", "2", "level1");
            button.activate(0, 2, 0.1f, 0.40f, "Level3", "3", "level1");
            button.disable(0, 3);
            button.activate(0, 4, 0.1f, 0.9f, "", LocalizedString.Back);

            button.disable(1, 0);
            button.disable(1, 1);
            button.disable(1, 2);
            button.disable(1, 3);
            button.disable(1, 4);

            button.disable(2, 0);
            button.disable(2, 1);
            button.disable(2, 2);
            button.disable(2, 3);
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
            spriteBatch.DrawString(font, "level select", titre_P,Color.White );
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
            button = new Button(1, 4, width, height, 0.1f, 0.05f, 0);
            button.LoadContent(Content);
            button.activate(0, 0, 0.1f, 0.1f, "SEU", "1", "SEU1");
            button.activate(0, 1, 0.1f, 0.25f, "Level2", "2", "level1");
            button.activate(0, 2, 0.1f, 0.40f, "Level3", "3", "level1");
            button.activate(0, 3, 0.1f, 0.6f, "", LocalizedString.Back);
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
