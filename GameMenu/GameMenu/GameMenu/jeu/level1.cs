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
        Scrolling scrolling1, scrolling2;
        sripte_V vaisseau;
        asteroid aster;
        KeyboardState oldkey;
        Texture2D bacgkround1, background2, aster_t;
        List<Texture2D> T_sprite;
        Collision collision;
        IA_manager_T manage_T;
        IA_manager_V manage_V;
        IA_manager_K manage_k;
        int taille_sprt;
        int timer;



        public level1(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            game1.IsMouseVisible = false;
            oldkey = Keyboard.GetState();
            T_sprite = new List<Texture2D>();
            collision = new Collision();
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            // TODO: Add your initialization logic here

            timer = -100;
            taille_sprt = (int)(Math.Min(width, height) * 0.05);

            // ajout IA
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
            aster_t = Content.Load<Texture2D>("IA/asteroid/asteroide-sprite");

            //instancie le scolling

            scrolling1 = new Scrolling(bacgkround1, new Rectangle(0, 0, width, height), 2, height);

            scrolling2 = new Scrolling(background2, new Rectangle(0, 0, width, height), 3, height);


            //intancie le vaisseau
            vaisseau = new sripte_V(T_sprite,
                new Rectangle(height / 2 + taille_sprt/2, width / 2 + taille_sprt  / 2,taille_sprt , taille_sprt ), Content, height, width,Color.Gray,9 );

            //instancie l ia
            aster = new asteroid(aster_t, new Rectangle(100, 75, taille_sprt, taille_sprt), 0.01f, width);
            manage_T = new IA_manager_T(aster_t, new Rectangle(0, 0, taille_sprt, taille_sprt), Content, height, width, Color.Red);
            manage_V = new IA_manager_V(aster_t, new Rectangle(0, 0, taille_sprt, taille_sprt), Content, height, width, Color.Green );
            manage_k = new IA_manager_K(aster_t, new Rectangle(0, 0, taille_sprt, taille_sprt), 0, 4);

            // ajout IA

            manage_k.Add_Stal(0f, 0f);
            manage_T.Add(0f, 0f);
            manage_V.Add(0f, 0f);
            manage_k.Add_Stal(1f, 0f);
            manage_T.Add(0.9f, 0f);
            manage_V.Add(0.9f, 0f);

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
            scrolling1.Update();
            scrolling2.Update();

            //vaisseau
            vaisseau.Update(keybord, game, oldkey);

            //update ia
            aster.update();

            if (!vaisseau.automatic_controlled)
            {
                manage_T.Update(ref game);
                manage_V.Update(ref vaisseau);
                manage_k.Update(ref vaisseau);
                collision.Collision_hero_missile( manage_T, ref vaisseau, ref  game);
                collision.Collision_hero_missile(manage_V, ref  vaisseau, ref  game);
                collision.col_H_IA(manage_k, ref vaisseau, ref game);
                collision.col_H_IA(manage_V , ref vaisseau, ref game);
                collision.col_H_IA(manage_T , ref vaisseau, ref game);
            }

            //update collision

            if (collision.Collision_as_mis(aster, vaisseau))
            {
                aster.rectangle.Y -= 20;
                aster.toucher();
            }
            collision.collision_ai_missile(ref vaisseau, manage_k);
            collision.collision_ai_missile(ref vaisseau, manage_V);
            collision.collision_ai_missile(ref vaisseau, manage_T);
            // update fin de jeu
            if (aster.rectangle.Top + 10 < 0)
            {
                if (timer == -100)
                {
                    vaisseau.gagne();
                    timer = vaisseau.rectangle.Y / 2;
                    aster.visible = false;
                }
                if (timer < 0 && timer != -100)
                    game.ChangeState(Game1.gameState.Level1_state);//va au level2
                timer--;
            }

            //update interface
            pause(game, keybord);

            oldkey = keybord;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();

            //scrolling
            scrolling1.Draw(spriteBatch);

            vaisseau.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            aster.Draw(spriteBatch);
            manage_T.Draw(spriteBatch);
            manage_V.Draw( spriteBatch);
            manage_k.Draw( spriteBatch);

            spriteBatch.End();
        }
    }
}
