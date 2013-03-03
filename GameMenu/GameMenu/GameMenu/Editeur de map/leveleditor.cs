﻿using System;
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
    class leveleditor : GameState
    {
        Scrolling scrolling1, scrolling2;
        sripte_V vaisseau;
       
     KeyboardState oldkey;
        Texture2D bacgkround1, background2, aster_t, planet1, star;
        List<Texture2D> T_sprite;
        Collision collision;
        IA_manager_T manage_T;
        IA_manager_V manage_V;
        IA_manager_K manage_k;
        int taille_sprt, taille_sprt2;
        int timer;
        int game_time;
        _Pause _pause;
        bool _checkpause = false;
        int latence = 0;
        Sauveguarde save;
        string path;


        public leveleditor(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            game1.IsMouseVisible = false;
            oldkey = Keyboard.GetState();
            T_sprite = new List<Texture2D>();
            collision = new Collision();
            _pause = new _Pause(game1, graphics, content);
            save = new Sauveguarde();
            path = game1.path;
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            // TODO: Add your initialization logic here

            timer = -100;
            taille_sprt = (int)(Math.Min(width, height) * 0.05);
            taille_sprt2 = (int)(Math.Min(width, height) * 0.1);

            game_time = 0;
 
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
            planet1 = Content.Load<Texture2D>("IA/asteroid/planet4");
            star = Content.Load<Texture2D>("IA/asteroid/star");

            //instancie le scolling

            scrolling1 = new Scrolling(bacgkround1, new Rectangle(0, 0, width, height), 2, height,0.01f);

            scrolling2 = new Scrolling(background2, new Rectangle(0, 0, width, height), 3, height,1f);

            //intancie le vaisseau
            vaisseau = new sripte_V(T_sprite,
                new Rectangle(height / 2 + taille_sprt / 2, width / 2 + taille_sprt / 2, taille_sprt, taille_sprt), Content, height, width, Color.Gray, 9);

            //instancie l ia
            //     aster = new asteroid(aster_t, new Rectangle(100, 75, taille_sprt, taille_sprt), 0.01f, width, height);
            manage_T = new IA_manager_T(planet1, new Rectangle(0, 0, taille_sprt, taille_sprt), Content, height, width);
            manage_V = new IA_manager_V(star, new Rectangle(0, 0, taille_sprt, taille_sprt), Content, height, width);
            manage_k = new IA_manager_K(aster_t, new Rectangle(0, 0, taille_sprt, taille_sprt), height,width );

            // ajout IA
            save.load_leveleditor_SEU("level1", ref manage_k, ref manage_T, ref manage_V);
            //instancie les donnees de la pause
            _pause.LoadContent(Content);
        }

        public override void UnloadContent()
        {

            // TODO: Unload any non ContentManager content here
        }
        public override void Update(Game1 game, Audio audio)
        {
   
            KeyboardState keyboard;
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.P) && latence <= 0)
            {
                _pause.checkpause(keyboard, ref _checkpause);
                latence = 30;
            }
            if (latence > 0)
                --latence;
            if (_checkpause == false)
            {


                // scrolling verticale
                scrolling1.Update();
                scrolling2.Update();

                //vaisseau
                vaisseau.Update(keyboard, game, oldkey);

                //update ia jbdcvf
   //             aster.update();


                manage_T.Update(ref game, ref game_time);
                manage_V.Update(ref vaisseau, ref game_time);
                manage_k.Update(ref vaisseau, ref game_time);
             //   collision.Collision_hero_missile(manage_T, ref vaisseau, ref  game);
              //  collision.Collision_hero_missile(manage_V, ref  vaisseau, ref  game);
                collision.col_H_IA(manage_k, ref vaisseau, ref game);
                collision.col_H_IA(manage_V, ref vaisseau, ref game);
                collision.col_H_IA(manage_T, ref vaisseau, ref game);


                //update collision

         //       collision.collision_ai_missile(ref vaisseau, manage_k);
           //     collision.collision_ai_missile(ref vaisseau, manage_V);
             //   collision.collision_ai_missile(ref vaisseau, manage_T);
            }
            // update fin de jeu
            if ( manage_k.Ia_manage.Count == 0 && manage_T.Ia_manage.Count == 0 && manage_V.Ia_manage.Count == 0)
            {
                if (timer < -50)
                {
                    vaisseau.gagne();
                    timer = vaisseau.rectangle.Y / 2;
                    //aster.visible = false;
                    manage_k.bulletL.Clear();
                    manage_T.bulletL.Clear();
                    manage_V.bulletL.Clear();
                }
                if (timer < 0 && timer != -100)
                    game.ChangeState(Game1.gameState.Level1_state);//va au level2
                timer--;
            }
            else
            {
                game.ChangeState2(Game1.gameState.Checkpause);
                _pause.Update(game, audio, ref _checkpause);

            }



            //update interface

            oldkey = keyboard;
            game_time++;

        }



        public override void Draw(SpriteBatch spriteBatch)
        {

                scrolling1.Draw(spriteBatch);
                vaisseau.Draw(spriteBatch);
                scrolling2.Draw(spriteBatch);
                //     aster.Draw(spriteBatch);
                manage_T.Draw(spriteBatch);
                manage_V.Draw(spriteBatch);
                manage_k.Draw(spriteBatch);
                if(_checkpause)
                _pause.Draw(spriteBatch);
            
        }

    }
}
