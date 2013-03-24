using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

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
        Button butts, butts2;
        int latence, latdefault;
        public Leveleditorselect(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {

            game1.IsMouseVisible = true;
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            list1 = new Listbox("SEU", 0.5f, 0.1f, 0.4f, 0.3f, width, height, 1);
            list2 = new Listbox("PLA", 0.5f, 0.5f, 0.4f, 0.3f, width, height, 2);
            tab = 0;
            MediaPlayer.Volume = vol;
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            butts = new Button(1, 3, width, height, 0.1f, 0.1f, 0);
            butts.activate(0, 0, 0.15f, 0.15f, "EditSEU", "Editeur de jeu1");
            butts.activate(0, 1, 0.15f, 0.5f, "play", "editeur de jeu2");
            butts.activate(0, 2, 0.15f, 0.85f, "", "retour");

            butts2= new Button(1, 2, width, height, 0.1f, 0.1f, 0);
            butts2.activate(0, 0, 0.85f, 0.15f, "LevelEdit", "jouer");
            butts2.activate(0, 1, 0.85f, 0.5f, "play", "jouer");
            latence = 0;
            latdefault = 10;
        }

        public override void LoadContent(ContentManager content, string level,GraphicsDevice graph)
        {
            background = content.Load<Texture2D>("Menu//background menu");
            songMenu = content.Load<Song>("Menu//songMenu");
            titre = content.Load<Texture2D>("Menu//LevelSelect//niveau");

            list2.LoadContent(content);
            list1.LoadContent(content);
            butts.LoadContent(content);
            butts2.LoadContent(content);
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            Rectangle mouse_rec = new Rectangle(mouse.X, mouse.Y, 1, 1); 
            --latence;
            if (keyboard.IsKeyDown(Keys.Tab) && latence < 0)
            {
                tab = (tab + 1) % 3;
                latence = latdefault;
            }
            if (list1.in_use)
            {
                selected_item = list1.selectedItem;
            }
            else if (list2.in_use)
            {
                selected_item = list2.selectedItem;
            }
            

            butts.Update(ref keyboard, ref mouse, ref mouse_rec, ref game, ref tab,"");
            butts2.Update(ref keyboard, ref mouse, ref mouse_rec, ref game, ref tab, selected_item);
            list1.Update(ref keyboard, ref mouse,ref mouse_rec   ,ref tab);
            list2.Update(ref keyboard, ref mouse,ref mouse_rec  ,ref tab);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, rectangle, Color.White);
            spriteBatch.Draw(titre, titre_P, Color.White);
            butts.Draw(spriteBatch);
        

            list1.Draw(spriteBatch);
            list2.Draw(spriteBatch); 
            butts2.Draw(spriteBatch);
        }
    }
}
