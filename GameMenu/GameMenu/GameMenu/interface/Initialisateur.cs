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


namespace Umea_rana
{
    class Initialisateur : GameState
    {

        Texture2D background;
        Rectangle rectangle;
        //     Listbox listbox;
        int timer, taille_logo;

        public Initialisateur(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {

            game1.IsMouseVisible = false;


            //    listbox = new Listbox("SEU", 100, 100, 500, 500);
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            timer = 10;//550;
            background.Dispose();
        }
                public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics, Audio audio)
        {

            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            taille_logo = Math.Min(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            rectangle = new Rectangle(graphics.PreferredBackBufferWidth / 2 - taille_logo / 2, graphics.PreferredBackBufferHeight / 2 - taille_logo / 2,
                taille_logo, taille_logo);
            background = Content.Load<Texture2D>("Menu//logofin");
            //    listbox.LoadContent(Content);
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            timer--;
            if (timer <= 0)
                game.ChangeState(Game1.gameState.MainMenuState);

            /*                   KeyboardState keyboard = Keyboard.GetState();
                   MouseState mouse = Mouse.GetState();
                   listbox.Update(ref keyboard, ref mouse);*/

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            //  listbox.Draw(spriteBatch);
        }

    }
    class Credit : GameState
    {

        Texture2D background;
        Rectangle rectangle;
        //     Listbox listbox;
        int timer, taille_logo;
        Vector2 vect, vect2;
        SpriteFont font;
        string credit, nom;
        public Credit(Game1 game1)
        {

            game1.IsMouseVisible = false;


            //    listbox = new Listbox("SEU", 100, 100, 500, 500);
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            timer = 10;//550;
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics, Audio audio)
        {

            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            taille_logo = Math.Min(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            rectangle = new Rectangle(graphics.PreferredBackBufferWidth / 2 - taille_logo / 2, graphics.PreferredBackBufferHeight / 2 - taille_logo / 2,
                taille_logo, taille_logo);
            background = Content.Load<Texture2D>("ListBoxBG");
            font = Content.Load<SpriteFont>("FontList");
            vect = new Vector2(width *0.2f, 0);
            vect2= new Vector2 (width*0.55f,0);
            credit = LocalizedStrings.LocalizedString.Credit ;
            nom = LocalizedStrings.LocalizedString.Nom;
  
            //    listbox.LoadContent(Content);
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            vect.Y += 0.5f;
            vect2.Y+= 0.5f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.Black );
            spriteBatch.DrawString(font,credit , vect , Color.White );
            spriteBatch.DrawString(font, nom, vect2, Color.White);
            //  listbox.Draw(spriteBatch);
        }

    }
}
