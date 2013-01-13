using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Umea_rana
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class level2 : GameState
    {
        Scrolling scrolling1, scrolling2, scrolling3, scrolling4;
        sripte_V vaisseau;
        KeyboardState oldkey;
        Texture2D bacground1, background2;
        List<Texture2D> T_sprite;
        int V_height, V_width;


        public level2(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            game1.IsMouseVisible = false;
            oldkey = Keyboard.GetState();
            T_sprite = new List<Texture2D>();
            V_height = 100; V_width = 100;
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            // TODO: Add your initialization logic here
        }

        public override void LoadContent(ContentManager Content)
        {
            bacground1 = Content.Load<Texture2D>("background");
            background2 = Content.Load<Texture2D>("background2");

            T_sprite.Add(Content.Load<Texture2D>("level2//sazabiHaman1"));
            T_sprite.Add(Content.Load<Texture2D>("level2//sazabiHaman1d"));
            T_sprite.Add(Content.Load<Texture2D>("level2//sazabiHaman1g"));
            T_sprite.Add(Content.Load<Texture2D>("level2//sazabiHaman2"));
            T_sprite.Add(Content.Load<Texture2D>("level2//sazabiHaman2d"));
            T_sprite.Add(Content.Load<Texture2D>("level2//sazabiHaman2g"));
            // Create a new SpriteBatch, which can be used to draw textures.

            // TODO: use this.Content to load your game content here

            scrolling1 = new Scrolling(bacground1, new Rectangle(0, 0, width, height), 2);
            scrolling2 = new Scrolling(bacground1, new Rectangle(0, -height, width, height), 2);
            scrolling3 = new Scrolling(background2, new Rectangle(0, 0, width, height), 3);
            scrolling4 = new Scrolling(background2, new Rectangle(0, -height, width, height), 3);

            vaisseau = new sripte_V(T_sprite,
                new Rectangle(height / 2 + V_height / 2, width / 2 + V_width / 2, V_height, V_width), Content, height, width);

        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keybord;
            keybord = Keyboard.GetState();
            // Allows the game to exit

            // scrolling verticale

            if (scrolling1.rectangle.Y >= height)
                scrolling1.rectangle.Y = scrolling2.rectangle.Y - scrolling2.rectangle.Height;
            if (scrolling2.rectangle.Y >= height)
                scrolling2.rectangle.Y = scrolling1.rectangle.Y - scrolling1.rectangle.Height;

            if (scrolling3.rectangle.Y >= height)
                scrolling3.rectangle.Y = scrolling4.rectangle.Y - scrolling4.rectangle.Height;
            if (scrolling4.rectangle.Y >= height)
                scrolling4.rectangle.Y = scrolling3.rectangle.Y - scrolling3.rectangle.Height;

            scrolling1.Update();
            scrolling2.Update();
            scrolling3.Update();
            scrolling4.Update();

            //vaisseau
            vaisseau.Update(keybord, game, oldkey);

            pause(game, keybord);
            oldkey = keybord;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //scrolling
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            scrolling3.Draw(spriteBatch);
            scrolling4.Draw(spriteBatch);

            vaisseau.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
