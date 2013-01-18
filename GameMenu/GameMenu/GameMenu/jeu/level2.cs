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
    public class Level2 : GameState
    {

        Scrolling_H scrolling1, scrolling2;//, scrolling3, scrolling4;
        sprite_broillon allen;
        platform sol, sol2;
        Collision collision;
        KeyboardState oldkey;


        public Level2(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            game1.IsMouseVisible = false;
            collision = new Collision();
            oldkey = Keyboard.GetState();
            
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            // TODO: Add your initialization logic here

        }

        public override void LoadContent(ContentManager Content)
        {

            //background
            scrolling1 = new Scrolling_H(Content.Load<Texture2D>("level1//fond_niv1"), new Rectangle(0, 0, width, height), 3);
            scrolling2 = new Scrolling_H(Content.Load<Texture2D>("level1//fond_niv1"), new Rectangle(width, 0, width, height), 3);
          //  scrolling3 = new Scrolling_H(Content.Load<Texture2D>("background2"), new Rectangle(0, 0, width, height), 4);
           // scrolling4 = new Scrolling_H(Content.Load<Texture2D>("background2"), new Rectangle(width, 0, width, height), 4);
            //sprite brouillon
            allen = new sprite_broillon(Content.Load<Texture2D>("hero//fiches_sprite_allen"), new Rectangle(width / 2, 0, 125, 93), collision, Content);

            //platfom
            sol = new platform(Content.Load<Texture2D>("level1//platform"), new Rectangle(0, height / 6 * 5, width, height / 15), 4);
            sol2 = new platform(Content.Load<Texture2D>("level1//platform"), new Rectangle(width-150, height/6*4, width, height / 15), 4);
        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard;

            keyboard = Keyboard.GetState();
            // Allows the game to exit

            // TODO: Add your update logic here
            scrolling1.Update(keyboard);
            scrolling2.Update(keyboard);
        //    scrolling3.Update(keyboard);
        //    scrolling4.Update(keyboard);

            //scrolling horrizontale fond
            if (scrolling1.rectangle.X + scrolling1.rectangle.Width <= 0)
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
            if (scrolling2.rectangle.X + scrolling2.rectangle.Width <= 0)
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;

            if (scrolling1.rectangle.X >= 0)
                scrolling2.rectangle.X = scrolling1.rectangle.X - scrolling1.rectangle.Width;
            if (scrolling2.rectangle.X >= 0)
                scrolling1.rectangle.X = scrolling2.rectangle.X - scrolling2.rectangle.Width;

         /*   // scrolling horrizontale devant
            if (scrolling3.rectangle.X + scrolling3.rectangle.Width <= 0)
                scrolling3.rectangle.X = scrolling4.rectangle.X + scrolling4.rectangle.Width;
            if (scrolling4.rectangle.X + scrolling4.rectangle.Width <= 0)
                scrolling4.rectangle.X = scrolling3.rectangle.X + scrolling3.rectangle.Width;
            if (scrolling3.rectangle.X >= 0)
                scrolling4.rectangle.X = scrolling3.rectangle.X - scrolling3.rectangle.Width;
            if (scrolling4.rectangle.X >= 0)
                scrolling3.rectangle.X = scrolling4.rectangle.X - scrolling4.rectangle.Width;
            */
            if (collision.Collision_sp_sol(allen, sol.rectangle) || collision.Collision_sp_sol(allen, sol2.rectangle))
            {
                allen.marche();
                allen.chute = false;
            }
            else
            {
                allen.air();
            }
            allen.update(keyboard);



            //sprite brouillon

            //pause
            pause(game, keyboard);

            //partie perdu
            fail(game, allen);

            //platform
            sol.update(keyboard);
            sol2.update(keyboard);
            //audio

            if (allen.rectangle.Right >= width * 2 - 50)
                game.ChangeState(Game1.gameState.level2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here

            spriteBatch.Begin();
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            //scrolling3.Draw(spriteBatch);
            //scrolling4.Draw(spriteBatch);
            allen.Draw(spriteBatch);
            sol.Draw(spriteBatch);
            sol2.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
