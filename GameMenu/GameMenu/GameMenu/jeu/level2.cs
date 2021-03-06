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
        
        _Pause _pause;
        int latence = 0;
        bool _checkpause = false;
        Texture2D aster, alllenT, backgroundT, platform_t, naruto_stalker, eve, truc_jaune;
        int front_sc, back_sc;

        public Level2(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {
            game1.IsMouseVisible = false;
            collision = new Collision();
            oldkey = Keyboard.GetState();
            
            _pause = new _Pause(game1, graphics, Content);
        }

        public override void LoadContent(ContentManager Content, GraphicsDevice graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            _pause.initbutton(ref level);
            //background
            backgroundT = Content.Load<Texture2D>("level1//fond_niv1");
            //sprite brouillon
            alllenT = Content.Load<Texture2D>("hero//fiches_sprite_allen");
          //  alllenT = Content.Load<Texture2D>("yoh3");
            //platfom
            platform_t = Content.Load<Texture2D>("level1//platform");
            //ia
            aster = Content.Load<Texture2D>("IA//asteroid//asteroide-sprite");
            naruto_stalker = Content.Load<Texture2D>("IA//naruto");
            eve = Content.Load<Texture2D>("IA//eve");
            truc_jaune = Content.Load<Texture2D>("IA//tuc_jaune");
            

            //background
            scrolling1 = new Scrolling_H(backgroundT, new Rectangle(0, 0, width, height), back_sc);
            //sprite brouillon
            allen = new sprite_broillon(alllenT , new Rectangle(width / 2, 0, 125, 93), collision, Content,'1');
            //instanciement du manager d ia
            platform_M = new Platform_manager(platform_t, width * 0.1f, height * 0.1f, front_sc, height, width);
            //intenciement des 3 ia
            managerAA = new IA_manager_AA(truc_jaune, new Rectangle(0, 0, 100, 100), front_sc, 3, height, width);
            managerAR = new IA_manager_AR(eve, new Rectangle(0, 0, 100, 100), front_sc, 4, height, width);
            manageS = new IA_manager_S(naruto_stalker, new Rectangle(0, 0, 100, 100), front_sc, 3, height, width);
            //instancie les donnees de la pause
            _pause.LoadContent(Content);
           
                // ajout ia aller retour (X,Y)
                managerAR.Add(1.1f, 0);
           /*     managerAR.Add(0.5f, 0.5f);
                managerAR.Add(2.1f, 0.5f);
                managerAR.Add(-0.5f, 0f);
                managerAR.Add(2.1f, 0.45f);
                managerAR.Add(2.4f, 0.55f);
                managerAR.Add(3.2f, 0.7f);
                managerAR.Add(3.6f, 0.7f);
              */  // ajout IA qui vont tous droit(X,Y)
                managerAA.Add(1.11f, 0);
                managerAA.Add(0.54f, 0.5f);
                managerAA.Add(2.14f, 0.5f);
                managerAA.Add(-0.58f, 0f);
                managerAA.Add(2.3f, 0.45f);
                managerAA.Add(2.6f, 0.45f);
                managerAA.Add(3.2f, 0.7f);
                // ajout des ia Stalker (X,Y)
                manageS.Add(1.15f, 0);
                manageS.Add(2f, 0.7f);
                manageS.Add(2.1f, 0.45f);
                manageS.Add(3.2f, 0.7f);

            


            // ajout platform (position X,position Y, nombre de plateforme juxtaposer)
            platform_M.Add(0.4f, 0.8f, 9);
            platform_M.Add(1f, 0.7f, 9);
            platform_M.Add(1.86f, 0.9f, 10);
            platform_M.Add(-0.76f, 0.6f, 10);
            platform_M.Add(1.9f,0.6f,5);
            platform_M.Add(2.6f, 0.6f, 2);
            platform_M.Add(2.4f, 0.7f, 1);
            platform_M.Add(2.9f, 0.8f, 1);
            platform_M.Add(3f, 0.9f, 1);
            platform_M.Add(3.2f, 0.9f, 6);
            platform_M.Add(3.1f, 1f, 1);
            
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            front_sc = 4;
            back_sc = 5;
        }

        public override void UnloadContent()
        {
            scrolling1.texture.Dispose();
            allen.Dispose();
            managerAA.Dipose();
            managerAR.Dipose();
            manageS.Dipose();
            _pause.Dispose();
            aster.Dispose(); alllenT.Dispose(); backgroundT.Dispose();
            platform_t.Dispose(); naruto_stalker.Dispose(); eve.Dispose(); truc_jaune.Dispose(); 
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
                game.ChangeState2(Game1.gameState.Null);
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
                game.ChangeState2(Game1.gameState.Checkpause);
                MediaPlayer.Stop();
                ParticleAdder.adder(game, Game1.gameState.Checkpause,height,width);
                _pause.Update(game, audio, ref _checkpause);
            }

            //partie perdu
            fail(game, allen, Game1.gameState.SEU);

            //audio
            
            if (allen.rectangle.Right >= width * 2 - 50)
                game.ChangeState(Game1.gameState.Pause, Game1.gameState.win );
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            if (!_checkpause)
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
            {
                scrolling1.Draw(spriteBatch);
                //scrolling3.Draw(spriteBatch);
                allen.Draw(spriteBatch);
                platform_M.Draw(spriteBatch);
                managerAA.Draw (spriteBatch);
                managerAR.Draw(spriteBatch);
                manageS.Draw(spriteBatch);
                _pause.Draw(spriteBatch);
            }
        }
    }
}
