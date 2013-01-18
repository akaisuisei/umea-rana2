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
    public class level1 : GameState
    {
        Scrolling scrolling1, scrolling2, scrolling3, scrolling4;
        sripte_V vaisseau;
        asteroid aster;
        KeyboardState oldkey;
        Texture2D bacgkround1, background2,aster_t;
        List<Texture2D> T_sprite;
        Collision collision;
        int timer;

        int V_height, V_width;
       

        public level1(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            game1.IsMouseVisible = false;
            oldkey = Keyboard.GetState();
            T_sprite = new List<Texture2D>();
            V_height = 100; V_width = 100;
            collision = new Collision();
            timer = -100;

        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            // TODO: Add your initialization logic here
        }

        public override void LoadContent(ContentManager Content)
        {
            //charge le fond
            bacgkround1 = Content.Load<Texture2D>("level2//fond");
            background2 = Content.Load<Texture2D>("level2//fond2");
            //charge le sprite
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman1"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman1d"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman1g"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman2"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman2d"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman2g"));

            //charge l IA
            aster_t=Content.Load<Texture2D>("IA//asteroid//asteroide-sprite");

            //instancie le scolling

            scrolling1 = new Scrolling(bacgkround1, new Rectangle(0, 0, width, height), 2);
            scrolling2 = new Scrolling(bacgkround1, new Rectangle(0, -height, width, height), 2);
            scrolling3 = new Scrolling(background2, new Rectangle(0, 0, width, height), 3);
            scrolling4 = new Scrolling(background2, new Rectangle(0, -height, width, height), 3);

            //intancie le vaisseau
            vaisseau = new sripte_V(T_sprite,
                new Rectangle(height / 2 + V_height / 2, width / 2 + V_width / 2, V_height, V_width), Content, height, width);

            //instancie l ia
            aster=new asteroid (aster_t,new Rectangle (100,75,100,100),0.01f,width);
        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keybord;
            keybord = Keyboard.GetState();

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

            //update ia
            aster.update();

            //update collision
            for (int i = 0; i<vaisseau.bullet.bullet.Count; i++)
            {
                if(collision.Collision_as_mis(aster,vaisseau.bullet.bullet[i]))
                {
                    vaisseau.bullet.bullet.RemoveAt(i);
                    aster.rectangle.Y -= 20;
                    aster.toucher();
                }
            }

            // update fin de jeu
            if (aster.rectangle.Top + 10 < 0)
            {

                if (timer == -100)
                {
                    vaisseau.gagne();
                    timer = vaisseau.rectangle.Y/2;
                    aster.visible = false;
                }
                if(  timer<0 && timer!=-100)           
                    game.ChangeState(Game1.gameState.Level1_state);//va au level2
                timer--;
            }

            //update interface
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

            vaisseau.Draw(spriteBatch);

            scrolling3.Draw(spriteBatch);
            scrolling4.Draw(spriteBatch);

            aster.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
