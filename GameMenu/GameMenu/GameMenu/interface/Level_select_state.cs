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



        public Level_select_state(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            game1.IsMouseVisible = true;
            MediaPlayer.Volume = vol;
            songMenu = content.Load<Song>("Menu//songMenu");
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            titre_P = new Vector2(width * 0.8f, height * 0.1f);
            button = new Button(1, 3, width, height, 0.1f, 0.05f, 0);
            old = Keyboard.GetState();
        }
        public override void LoadContent(ContentManager content, GraphicsDevice graph, ref string level, ref string next)
        {
            button.LoadContent(content);
            button.activate(0, 0, 0.1f, 0.1f,"SEU", "1","SEU1");
            button.activate(0, 1, 0.1f, 0.2f,"Level2" , "2","level1");
            button.activate(0, 2, 0.1f, 0.3f, "", "retour");
            background = content.Load<Texture2D>("Menu//background menu");
            titre = content.Load<Texture2D>("Menu//pause//Menu");
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
            spriteBatch.Draw(titre, titre_P, Color.White);
        }
    }
}
