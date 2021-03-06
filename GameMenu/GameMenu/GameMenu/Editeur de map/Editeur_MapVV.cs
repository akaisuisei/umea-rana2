﻿
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
using System.Windows.Forms;
using System.Threading;

namespace Umea_rana
{
    class Editeur_MapVV : GameState
    {
        //  Scrolling scrolling1;
        sripte_V vaisseau;

        KeyboardState oldkey;
        Texture2D/* bacgkround1,*/ aster_t, planet1, star;
        Texture2D T_sprite;
        Collision collision;
        IA_manager_T manage_T;
        IA_manager_V manage_V;
        IA_manager_K manage_k;
        Ovni ovni;
        int taille_sprt;
        Scrolling_ManagerV Scroll_manager;
        _Pause _pause;
        bool _checkpause = false;
        int latence = 0;

        UserControl1 user;
        //string backGround;
        int spawn;
        string iaType;

        public Editeur_MapVV(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {
            game1.IsMouseVisible = false;
            oldkey = Keyboard.GetState();

            collision = new Collision();
            _pause = new _Pause(game1, graphics, Content);

        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            // TODO: Add your initialization logic here
            taille_sprt = (int)(Math.Min(width, height) * 0.05);
            //    backGround = "level2//fond";
            // ajout IA 
            user = new UserControl1();
            Application.Run(user);
            spawn = -1;
            iaType = "kawabunga";
        }

              public override void LoadContent(ContentManager Content, GraphicsDevice graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            _pause.initbutton(ref level);
            Scroll_manager = new Scrolling_ManagerV(new Rectangle(0,0,width,height ));
            //charge le fond
            //  bacgkround1 = Content.Load<Texture2D>(backGround);
            //charge le sprite
            T_sprite = Content.Load<Texture2D>("hero//spriteSheet");


            //charge l IA

            aster_t = Content.Load<Texture2D>("IA/asteroid/asteroide-sprite");
            planet1 = Content.Load<Texture2D>("IA/asteroid/planet4");
            star = Content.Load<Texture2D>("IA/asteroid/star");
            //instancie le scolling

            manage_T = new IA_manager_T(planet1, new Rectangle(0, 0, taille_sprt, taille_sprt), Content, height, width);
            manage_V = new IA_manager_V(star, new Rectangle(0, 0, taille_sprt, taille_sprt), Content, height, width);
            manage_k = new IA_manager_K(aster_t, new Rectangle(0, 0, taille_sprt, taille_sprt), height, width);

            //intancie le vaisseau
            vaisseau = new sripte_V(
                new Rectangle(height / 2 + taille_sprt / 2, width / 2 + taille_sprt / 2, taille_sprt, taille_sprt), height, width);
            vaisseau.Load(Content, T_sprite);
            ovni = new Ovni(width, height);
            ovni.param(3);
            ovni.Load(aster_t);

            //instancie les donnees de la pause
            _pause.LoadContent(Content);
            user.LoadContent(manage_T, manage_V, manage_k, Scroll_manager, Content, ovni);
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
            ovni.Dispose();
            Scroll_manager.dispose();
            _pause.Dispose();
            // TODO: Unload any non ContentManager Content here
            user.destroy();
            user.dispose();
            user.Dispose();
            
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard;
            MouseState mouse;
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape) && latence <= 0)
            {

                _pause.checkpause(keyboard, ref _checkpause);
                latence = 30;
                user.Hide();
            }
            if (latence > 0)
                --latence;
            if (!_checkpause)
            {
                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !user.IHave_control)
                {
                    spawn = existcheck(ref iaType, mouse);

                    user._show(mouse.X, mouse.Y, iaType, spawn);
                }
                if (user.IHave_control)
                    user.TopMost = true;
                else
                {
                    user.update(ref manage_T, ref manage_V, ref manage_k, ref keyboard, game, ref Scroll_manager, ref ovni);
                    //        scrolling1.update_ophelia(keyboard);
                    Scroll_manager.Update_ophelia(keyboard);
                }
                manage_k.Update_ophelia(keyboard);
                manage_T.Update_ophelia(keyboard);
                manage_V.Update_ophelia(keyboard);
                ovni.Update(keyboard);
            }
            else
            {
                game.ChangeState2(Game1.gameState.Checkpause);
                MediaPlayer.Stop();
                ParticleAdder.adder(game, Game1.gameState.Checkpause, height, width);
                _pause.Update(game, audio, ref _checkpause);
            }

            //update interface


            oldkey = keyboard;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {



            //scrolling
            //scrolling1.Draw(spriteBatch);
            Scroll_manager.Draw(spriteBatch);


            manage_T.Draw(spriteBatch);
            manage_V.Draw(spriteBatch);
            manage_k.Draw(spriteBatch);
            ovni.Draw(spriteBatch);
            vaisseau.Draw(spriteBatch);

            if (_checkpause)
                _pause.Draw(spriteBatch);

        }

        private int existcheck(ref string hello, MouseState mouse)
        {
            Rectangle recM = new Rectangle(mouse.X, mouse.Y, 1, 1);

            for (int i = 0; i < manage_k.Ia_manage.Count; ++i)
                if (manage_k.Ia_manage[i].rectangle.Intersects(recM))
                {
                    hello = "IA_K";
                    return manage_k.Ia_manage[i].spawn;
                }
            for (int i = 0; i < manage_T.Ia_manage.Count; ++i)
                if (manage_T.Ia_manage[i].rectangle.Intersects(recM))
                {
                    hello = "IA_T";
                    return manage_T.Ia_manage[i].spawn;
                }
            for (int i = 0; i < manage_V.Ia_manage.Count; ++i)
                if (manage_V.Ia_manage[i].rectangle.Intersects(recM))
                {
                    hello = "IA_V";
                    return manage_V.Ia_manage[i].spawn;
                }
            for (int i = 0; i < ovni.ovni.Count; ++i)
                if (ovni.ovni[i].circle.is_in_bound(recM))
                {
                    hello = "b";
                    return ovni.ovni[i].launch;
                }
            hello = "";
            return -1;

        }
    }
}
