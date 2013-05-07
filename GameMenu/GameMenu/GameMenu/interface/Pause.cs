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
    class Pause : GameState
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;

        Rectangle rectangle;

        Button button;
        Rectangle mouserec;
        KeyboardState old;
        int tab;

        public Pause(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {

            game1.IsMouseVisible = true;
            MediaPlayer.Volume = vol;
            songMenu = Content.Load<Song>("Menu//songMenu");

        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {

            tab = 0;
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            button = new Button(1, 3, width, height, 0.07f, 0.1f, 0);
            button.LoadContent(Content);
            button.activate(0, 0, 0.1f, 0.1f, "Last", LocalizedString.Replay);
            button.activate(0, 1, 0.1f, 0.2f, "", LocalizedString.Menu);
            button.activate(0, 2, 0.1f, 0.3f, "Exit", LocalizedString.Exit);
            background = Content.Load<Texture2D>("Menu//game_over");
        }
        public override void UnloadContent()
        {
            songMenu.Dispose();
            button.Dispose();
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            mouserec = new Rectangle(mouse.X, mouse.Y, 1, 1);
            button.Update(ref keyboard, ref old, ref mouse, ref mouserec, ref game, ref tab, "");

            old = keyboard;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            button.Draw(spriteBatch);
        }


    }
}
