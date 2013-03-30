using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Umea_rana
{
    class GameWin:GameState 
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;

        Rectangle rectangle;

        Button button;
        Rectangle mouserec;
        KeyboardState old;
        int tab;

        public GameWin(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            game1.IsMouseVisible = true;
            MediaPlayer.Volume = vol;
            songMenu = content.Load<Song>("Menu//songMenu");
   
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            button = new Button(1, 4, width, height, 0.07f, 0.1f, 0);
            tab = 0;
        }
        public override void LoadContent(ContentManager content,GraphicsDevice graph,ref string level,ref string next)
        {
            
            button.LoadContent(content);
            button.activate(0, 0, 0.1f, 0.1f, "Next", "suivant");
              button.activate(0, 1, 0.1f, 0.2f,"Last", "rejouer");
            button.activate(0, 2, 0.1f, 0.3f, "", "Menu");
            button.activate(0, 3, 0.1f, 0.4f, "Exit", "quitter");
            background = content.Load<Texture2D>("Menu//game_over");
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            mouserec=new Rectangle (mouse.X,mouse.Y,1,1);
            button.Update(ref keyboard, ref old, ref mouse, ref mouserec, ref game, ref tab, "");
          
            old=keyboard;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            button.Draw(spriteBatch);
        }


    }
    }

