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
using Umea_rana.localization;

namespace Umea_rana
{
    public class _Pause
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;

        Rectangle rectangle;

        int select = 0;
        int latence = 0;

        Button button;
        Rectangle mouserec;
        KeyboardState old;
        int tab;

        public _Pause(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            game1.IsMouseVisible = true;
            MediaPlayer.Volume = vol;
            songMenu = content.Load<Song>("Menu//songMenu");
            button = new Button(1, 4, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 0.07f, 0.1f, 0);
            button.activate(0, 0, 0.1f, 0.1f, "Resume", LocalizedString.Resume );

            tab = 0;
        }

        public void initbutton(ref string level)
        {
            if (level == "edit")
            {
                button.activate(0, 1, 0.1f, 0.3f, "",LocalizedString.Menu );
                button.activate(0, 2, 0.1f, 0.4f, "Exit", LocalizedString.Exit );
                button.disable(0, 3);
            }
            else
            {
                button.activate(0, 1, 0.1f, 0.2f, "Replay", LocalizedString.Replay );
                button.activate(0, 2, 0.1f, 0.3f, "", LocalizedString.Menu);
                button.activate(0, 3, 0.1f, 0.4f, "Exit", LocalizedString.Exit);
            }
        }

        public void LoadContent(ContentManager content)
        {
            button.LoadContent(content);

            background = content.Load<Texture2D>("Menu//pause//fond_pause");

        }

        public void Update(Game1 game, Audio audio, ref bool checkpause)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            mouserec = new Rectangle(mouse.X, mouse.Y, 1, 1);
            button.Update(ref keyboard, ref old, ref mouse, ref mouserec, ref game, ref tab, "", ref checkpause);

            old = keyboard;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            button.Draw(spriteBatch);
        }
        public void checkpause(KeyboardState keyboard, ref bool _checkpause)
        {
            if (_checkpause)
                _checkpause = false;
            else
                _checkpause = true;
        }

        public void Dispose()
        {
            songMenu.Dispose();
            background.Dispose();
            button.Dispose();
        }
    }
}





