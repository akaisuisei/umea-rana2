using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

using Umea_rana.localization;

namespace Umea_rana
{
    class Leveleditorselect : GameState
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;

        Rectangle rectangle;
        string selected_item;
        Listbox list1, list2;
        int tab;
        Button butts;
       
        KeyboardState oldkey;
        public Leveleditorselect(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {

            game1.IsMouseVisible = true;
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            list1 = new Listbox("SEU", 0.4f, 0.1f, 0.4f, 0.3f, width, height, 1);
            list2 = new Listbox("PLA", 0.4f, 0.5f, 0.4f, 0.3f, width, height, 2);
            tab = 0;
            MediaPlayer.Volume = vol;
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            butts = new Button(2, 3, width, height, 0.1f, 0.1f, 0);
            
         
            oldkey = Keyboard.GetState();
        }

              public override void LoadContent(ContentManager Content, GraphicsDevice graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            background = Content.Load<Texture2D>("Menu//background menu");
            songMenu = Content.Load<Song>("Menu//songMenu");
            titre = Content.Load<Texture2D>("Menu//LevelSelect//niveau");

            list2.LoadContent(Content);
            list1.LoadContent(Content);
            butts.LoadContent(Content);
            butts.activate(0, 0, 0.15f, 0.15f, "EditSEU",LocalizedString.Map_Editor +"\n"+LocalizedString.SEU  );
            butts.activate(0, 1, 0.15f, 0.5f, "Play", LocalizedString.Map_Editor + "\n" + LocalizedString.PLA );
            butts.activate(0, 2, 0.15f, 0.85f, "", LocalizedString.Back );


            butts.activate(1, 0, 0.85f, 0.15f, "LevelEdit", LocalizedString.Play );
            butts.activate(1, 1, 0.85f, 0.5f, "play", LocalizedString.Play );
            butts.disable(1, 2);
        }
        public override void UnloadContent()
        {
            background.Dispose();
            list1.Dispose();
            list2.Dispose();
            butts.Dispose();
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            Rectangle mouse_rec = new Rectangle(mouse.X, mouse.Y, 1, 1); 
        
            if (keyboard.IsKeyUp (Keys.Tab) && oldkey.IsKeyDown(Keys.Tab))
            {
                tab = (tab + 1) % 3; 
            }
            if (list1.in_use)
            {
                selected_item = list1.selectedItem;
            }
            else if (list2.in_use)
            {
                selected_item = list2.selectedItem;
            }
            

            butts.Update(ref keyboard,ref oldkey , ref mouse, ref mouse_rec, ref game, ref tab,selected_item );
            list1.Update(ref keyboard,ref oldkey , ref mouse,ref mouse_rec   ,ref tab);
            list2.Update(ref keyboard,ref oldkey, ref mouse,ref mouse_rec  ,ref tab);
            oldkey = keyboard;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, rectangle, Color.White);
            spriteBatch.Draw(titre, titre_P, Color.White);
            butts.Draw(spriteBatch);
        

            list1.Draw(spriteBatch);
            list2.Draw(spriteBatch); 
        }
    }
}
