using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Umea_rana.jeu
{
    public class PLA1jperso : GameState
    {

        Scrolling_ManagerV srollingM;
        Sauveguarde sauvegarde;
        Dictionary<string, Texture2D> T_platform;
        Sprite_PLA allen;
        Platform_manager platform_M;
        IA_manager_AA managerAA;
        IA_manager_AR managerAR;
        IA_manager_S manageS;
        Collision collision;
        KeyboardState oldkey;

        _Pause _pause;
        bool _checkpause = false;
        Texture2D alllenT, naruto_stalker, eve, truc_jaune;
        int front_sc, back_sc;
        scoreplat score;
        bossPLAT boss;
        Housse housse;
        string sprite_color = "";

        public PLA1jperso(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {
            game1.IsMouseVisible = false;
            collision = new Collision(Content);
            oldkey = Keyboard.GetState();

            _pause = new _Pause(game1, graphics, Content);
            score = new scoreplat();
            boss = new bossPLAT();

            housse = new Housse();
            sauvegarde = new Sauveguarde();
        }

        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            T_platform = new Dictionary<string, Texture2D>();
            string[] platstring = sauvegarde.filename(Content, "platform");
            foreach (string p in platstring)
                T_platform.Add(p, Content.Load<Texture2D>("platform//" + p));
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            _pause.initbutton(ref level);
            managerAA = new IA_manager_AA(new Rectangle(0, 0, 100, 100), height, width);
            managerAR = new IA_manager_AR(new Rectangle(0, 0, 100, 100), height, width);
            manageS = new IA_manager_S(new Rectangle(0, 0, 100, 100), height, width);
            srollingM = new Scrolling_ManagerV(new Rectangle(0, 0, width, height));
            allen = new Sprite_PLA(new Rectangle(650, 0, 100, 100), collision, Content,"P1");
            //background
            //sprite brouillon    

            platform_M = new Platform_manager(T_platform, width * 0.1f, height * 0.1f, height, width);
            //platfom
            sauvegarde.Load_Level_PLAperso(Content, ref level, ref next, ref sprite_color, ref managerAA, ref managerAR,
                ref manageS, ref platform_M, ref housse, ref boss, ref srollingM, ref Graph, ref allen);

            //ia
            sprite_color = "color";
            naruto_stalker = Content.Load<Texture2D>("IA//" + sprite_color + "//" + "naruto");
            eve = Content.Load<Texture2D>("IA//" + sprite_color + "//" + "eve");
            truc_jaune = Content.Load<Texture2D>("IA//" + sprite_color + "//" + "tuc_jaune");

            _pause.LoadContent(Content);


            managerAA.LoadContent(truc_jaune);
            managerAR.LoadContent(eve);
            manageS.LoadContent(naruto_stalker);
            score.LoadContent(new Rectangle(0, 0, width, height), Content);

            boss.loadContent(Content, new Rectangle(0, 0, width, height));

            housse.loadContent(Content, "IA/color/house", new Rectangle(0, 0, 100, 100), height, width);
            allen.vie = 10;
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
        }

        public override void UnloadContent()
        {
            srollingM.dispose();
            allen.Dispose();
            managerAA.Dipose();
            managerAR.Dipose();
            manageS.Dipose();
            _pause.Dispose();
            naruto_stalker.Dispose(); eve.Dispose(); truc_jaune.Dispose();
            // TODO: Unload any non ContentManager Content here
        }

        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard;
            keyboard = Keyboard.GetState();
            if ((keyboard.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape) && oldkey.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape)) ^
               (keyboard.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.P) && oldkey.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P)))
            {
                _pause.checkpause(keyboard, ref _checkpause);

            }

            if (!_checkpause)
            {
                game.ChangeState2(Game1.gameState.Null);
                // scrolling
                srollingM.Update(keyboard);

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
                collision.coll_AL_IA(managerAA, ref allen);
                collision.coll_AL_IA(managerAR, ref allen);
                //manager IA 
                managerAR.Update(ref keyboard);


                //manager platform
                platform_M.Update(keyboard);
                score.Update(ref allen);
                collision.Bossplat_hero(ref boss, ref allen, ref platform_M);
                boss.Update(ref keyboard);

            }
            else
            {
                game.ChangeState2(Game1.gameState.Checkpause);
                MediaPlayer.Stop();
                ParticleAdder.adder(game, Game1.gameState.Checkpause, height, width);
                _pause.Update(game, audio, ref _checkpause, ref keyboard, ref oldkey);
            }

            //partie perdu
            fail(game, allen, Game1.gameState.SEU);

            //audio

            if (allen.rectangle.Right >= width * 2 - 50)
                game.ChangeState(Game1.gameState.Pause, Game1.gameState.win);
            housse.Update(keyboard, game, allen);
            oldkey = keyboard;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here


            srollingM.Draw(spriteBatch);
            //scrolling3.Draw(spriteBatch);
            allen.Draw(spriteBatch);
            platform_M.Draw2(spriteBatch);
            managerAA.Draw(spriteBatch);
            managerAR.Draw(spriteBatch);
            manageS.Draw(spriteBatch);
            boss.Draw(spriteBatch);
            housse.draw(spriteBatch);
            score.Draw(spriteBatch);


            if (_checkpause)
                _pause.Draw(spriteBatch);


        }
    }
}
