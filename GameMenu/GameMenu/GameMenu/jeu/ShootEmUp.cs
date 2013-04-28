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
    public class Shoot_Em_Up : GameState
    {
        // Scrolling scrolling1, scrolling2;
        sripte_V vaisseau;

        KeyboardState oldkey;
        Texture2D aster_t, planet1, star, stalkert;
        Texture2D T_sprite;
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
        Scrolling_ManagerV scroll;
        Ovni ovini;
        Rectangle fond;

        public Shoot_Em_Up(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {
            game1.IsMouseVisible = false;
            _pause = new _Pause(game1, graphics, Content);

        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            // TODO: Add your initialization logic here

            timer = -100;


            game_time = 0;
            oldkey = Keyboard.GetState();

            collision = new Collision();
            save = new Sauveguarde();

            // ajout IA
        }

        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            //charge le fond
            //         background1 = Content.Load<Texture2D>("level2//fond");
            //       background2 = Content.Load<Texture2D>("back//fond2");
            //charge le sprite

            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            taille_sprt = (int)(Math.Min(width, height) * 0.05);
            taille_sprt2 = (int)(Math.Min(width, height) * 0.08);
            ovini = new Ovni(width, height);
            _pause.initbutton(ref level);
            T_sprite = Content.Load<Texture2D>("hero//spriteSheet");
            fond = new Rectangle(width/2-width/4  , 0, width / 2, height);
            scroll = new Scrolling_ManagerV(fond);
            //charge l IA
            aster_t = Content.Load<Texture2D>("IA/asteroid/asteroide-sprite");
            planet1 = Content.Load<Texture2D>("IA/asteroid/planet4");
            star = Content.Load<Texture2D>("IA/asteroid/star");
            stalkert = Content.Load<Texture2D>("IA/asteroid/asteroide-sprite2");

            //instancie le scolling

            //   scrolling1 = new Scrolling(bacgkround1, new Rectangle(0, 0, width, height), 2, height,1f);

            //    scrolling2 = new Scrolling(background2, new Rectangle(0, 0, width, height), 3, height,0.5f);


            //intancie le vaisseau
            vaisseau = new sripte_V(
                new Rectangle(0,0, taille_sprt2, taille_sprt2), fond,1);

            //instancie l ia

            manage_T = new IA_manager_T(planet1, new Rectangle(0, 0, taille_sprt2, taille_sprt2), Content, fond);
            manage_V = new IA_manager_V(star, new Rectangle(0, 0, taille_sprt, taille_sprt), Content, fond);
            manage_k = new IA_manager_K(stalkert , new Rectangle(0, 0, taille_sprt, taille_sprt), fond);
            //instancie les donnees de la pause
            _pause.LoadContent(Content);
            ovini.Load(aster_t);

            // ajout IA

            save.load_level_SEU(Content, ref level, ref next, ref manage_k, ref manage_T, ref manage_V, ref scroll, ref Graph, ref vaisseau, ref ovini);
            vaisseau.Load(Content, T_sprite);


        }

        public override void UnloadContent()
        {
            vaisseau.Dispose();
            aster_t.Dispose();
            planet1.Dispose();
            star.Dispose();
            T_sprite.Dispose();
            manage_k.Dipose();
            manage_T.Dipose();
            manage_V.Dipose();
            _pause.Dispose();
            scroll.dispose();
            ovini.Dispose();

            // TODO: Unload any non ContentManager Content here
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard;
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape) && latence <= 0)
            {
                _pause.checkpause(keyboard, ref _checkpause);
                latence = 30;
            }
            if (latence > 0)
                --latence;
            if (!_checkpause)
            {
                ovini.Update(ref game_time);
                game.ChangeState2(Game1.gameState.Null);
                // scrolling verticale
                //      scrolling1.Update();
                //     scrolling2.Update();
                scroll.Update();
                //vaisseau
                vaisseau.Update(keyboard, game, oldkey);

                //update ia jbdcvf



                manage_T.Update(ref game, ref game_time);
                manage_V.Update(ref vaisseau, ref game_time);
                manage_k.Update(ref vaisseau, ref game_time);
                collision.hero_missile(manage_T, ref vaisseau);
                collision.hero_missile(manage_V, ref  vaisseau);
                collision.col_H_IA(manage_k, ref vaisseau);
                collision.col_H_IA(manage_V, ref vaisseau);
                collision.col_H_IA(manage_T, ref vaisseau);


                //update collision

                collision.collision_ai_missile(ref vaisseau, manage_k);
                collision.collision_ai_missile(ref vaisseau, manage_V);
                collision.collision_ai_missile(ref vaisseau, manage_T);

                //   collision.h_M1P(ref manage_V, ref manage_T, ref manage_k, ref vaisseau);
                collision.Ovni_vaiss(ref ovini, ref vaisseau);
            }
            else
            {
                game.ChangeState2(Game1.gameState.Checkpause);
                _pause.Update(game, audio, ref _checkpause);
            }
            // update fin de jeu
            if (manage_k.Ia_manage.Count == 0 && manage_T.Ia_manage.Count == 0 && manage_V.Ia_manage.Count == 0)
            {
                if (timer == -100)
                {
                    vaisseau.gagne();
                    timer = vaisseau.rectangle.Y / 2;

                    manage_T.bulletL.Clear();
                    manage_V.bulletL.Clear();
                }
                if (timer < 0 && timer != -100)
                    game.ChangeState(Game1.gameState.win);//va au level2
                timer--;
            }
            //update interface

            oldkey = keyboard;
            game_time++;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {


            scroll.Draw(spriteBatch);
            //scrolling
            //    scrolling1.Draw(spriteBatch);
            vaisseau.Draw(spriteBatch);
            //      scrolling2.Draw(spriteBatch);

            manage_T.Draw(spriteBatch);
            manage_V.Draw(spriteBatch);
            manage_k.Draw(spriteBatch);


            ovini.Draw(spriteBatch);
            if (_checkpause)
                _pause.Draw(spriteBatch);

        }
    }
}

