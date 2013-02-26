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

        Scrolling_H scrolling1;//, scrolling3, scrolling4;
        sprite_broillon allen;
        Platform_manager platform_M;
        IA_manager_AA managerAA;
        IA_manager_AR managerAR;
        IA_manager_S manageS;
        Collision collision;
        KeyboardState oldkey;
        ContentManager content;
        _Pause _pause;
        int latence = 0;
        bool _checkpause = false;
        Texture2D aster, alllenT, backgroundT, platform_t, naruto_stalker, eve, truc_jaune;
        int front_sc, back_sc;

        public Level2(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            game1.IsMouseVisible = false;
            collision = new Collision();
            oldkey = Keyboard.GetState();
            this.content = content;
            _pause = new _Pause(game1, graphics, content);
        }


        public override void LoadContent(ContentManager Content)
        {

            //background
            backgroundT = Content.Load<Texture2D>("level1//fond_niv1");
            //sprite brouillon
            alllenT = Content.Load<Texture2D>("hero//fiches_sprite_allen");
            //platfom
            platform_t = Content.Load<Texture2D>("level1//platform");
            //ia
            aster = Content.Load<Texture2D>("IA//asteroid//asteroide-sprite");
            naruto_stalker = Content.Load<Texture2D>("IA//naruto");
            eve = Content.Load<Texture2D>("IA//eve");
            truc_jaune = Content.Load<Texture2D>("IA//tuc_jaune");
            //truc_jaune = Content.Load<Texture2D>("IA//truc_jaune");

            //background
            scrolling1 = new Scrolling_H(backgroundT, new Rectangle(0, 0, width, height), back_sc);
            //sprite brouillon
            allen = new sprite_broillon(alllenT, new Rectangle(width / 2, 0, 125, 93), collision, content);
            //instanciement du manager d ia
            platform_M = new Platform_manager(platform_t, width * 0.1f, height * 0.1f, front_sc, height, width);
            //intenciement des 3 ia
            managerAA = new IA_manager_AA(truc_jaune, new Rectangle(0, 0, 100, 100), front_sc, 3, height, width);
            managerAR = new IA_manager_AR(eve, new Rectangle(0, 0, 100, 100), front_sc, 4, height, width);
            manageS = new IA_manager_S(naruto_stalker, new Rectangle(0, 0, 100, 100), front_sc, 3, height, width);
            //instancie les donnees de la pause
            _pause.LoadContent(Content);
            for (int i = 0; i < 100; ++i)
            {
                // ajout ia aller retour (X,Y)
                managerAR.Add(1.1f, 0);
                managerAR.Add(0.5f, 0.5f);
                managerAR.Add(2.1f, 0.5f);
                managerAR.Add(-0.5f, 0f);
                // ajout IA qui vont tous droit(X,Y)
                managerAA.Add(1.11f, 0);
                managerAA.Add(0.54f, 0.5f);
                managerAA.Add(2.14f, 0.5f);
                managerAA.Add(-0.58f, 0f);
                // ajout des ia Stalker (X,Y)
                manageS.Add(1.15f, 0);
                manageS.Add(0.57f, 0.5f);
                manageS.Add(2.18f, 0.5f);
                manageS.Add(-0.5f, 0f);
            }


            // ajout platform (position X,position Y, nombre de plateforme juxtaposer)
            platform_M.Add(0.4f, 0.8f, 9);
            platform_M.Add(1f, 0.7f, 9);
            platform_M.Add(1.86f, 0.9f, 10);
            platform_M.Add(-0.76f, 0.6f, 10);
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            front_sc = 4;
            back_sc = 5;
        }



        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
            if (_checkpause == false)
            {
                // scrolling
                scrolling1.Update(keyboard);

                // collision Allen
                if (collision.Collision_sp_sol(ref allen, ref platform_M))
                {
                    allen.marche();
                    allen.jump_off = true;
                    allen.chute = false;
                }
                else
                {
                    allen.air();
                }
                allen.update(keyboard);

                //collision ia
                collision.collision_ia_sol(manageS, ref platform_M);
                manageS.Update(allen, ref keyboard);
                collision.collision_ia_AR_sol(managerAR, ref platform_M);
                managerAA.Update(ref keyboard);
                collision.collision_ia_sol(managerAA, ref platform_M);

                collision.coll_AL_IA(manageS, ref allen);
               collision.coll_AL_IA(managerAA , ref allen);
                collision.coll_AL_IA(managerAR, ref allen);
                //manager IA 
                managerAR.Update(ref keyboard);


                //manager platform
                platform_M.Update(keyboard);
            }
            else
            {
                ParticleAdder.adder(game, Game1.gameState.Checkpause);
                _pause.Update(game, audio, ref _checkpause);
            }

            //partie perdu
            fail(game, allen, Game1.gameState.level2);

            //audio

            if (allen.rectangle.Right >= width * 2 - 50)
                game.ChangeState(Game1.gameState.Pause, Game1.gameState.level2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            if (_checkpause == false)
            {
                scrolling1.Draw(spriteBatch);
                //scrolling3.Draw(spriteBatch);
                allen.Draw(spriteBatch);
                platform_M.Draw(spriteBatch);
                managerAA.Draw(spriteBatch);
                managerAR.Draw(spriteBatch);
                manageS.Draw(spriteBatch);
            }
            else
                _pause.Draw(spriteBatch);
        }
    }
}
