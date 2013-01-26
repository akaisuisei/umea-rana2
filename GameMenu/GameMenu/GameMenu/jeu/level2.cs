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


        Texture2D aster, alllenT, backgroundT, platform_t;
        int front_sc, back_sc;

        public Level2(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            game1.IsMouseVisible = false;
            collision = new Collision();
            oldkey = Keyboard.GetState();
            this.content = content;

        }


        public override void LoadContent(ContentManager Content)
        {

            //background
            backgroundT = Content.Load<Texture2D>("level1//fond_niv1");
            //  scrolling3 = new Scrolling_H(Content.Load<Texture2D>("background2"), new Rectangle(0, 0, width, height), 4);

            //sprite brouillon
            alllenT = Content.Load<Texture2D>("hero//fiches_sprite_allen");

            //platfom
            platform_t = Content.Load<Texture2D>("level1//platform");
            //ia
            aster = Content.Load<Texture2D>("IA//asteroid//asteroide-sprite");

            //background
            scrolling1 = new Scrolling_H(backgroundT, new Rectangle(0, 0, width, height), back_sc);

            //  scrolling3 = new Scrolling_H(Content.Load<Texture2D>("background2"), new Rectangle(0, 0, width, height), 4);

            //sprite brouillon
            allen = new sprite_broillon(alllenT, new Rectangle(width / 2, 0, 125, 93), collision, content);

            //platfom
            platform_M = new Platform_manager(platform_t, width, height*0.1f, front_sc);
            //ia
            managerAA = new IA_manager_AA(aster, new Rectangle(0, 0, 100, 100), front_sc, 3);
            managerAR = new IA_manager_AR(aster, new Rectangle(0, 0, 100, 100), front_sc, 3);
            manageS = new IA_manager_S(aster, new Rectangle(0, 0, 100, 100), front_sc, 3);


            managerAA.Add_Stal(1300f, 0f);
            managerAR.Add_Stal(1400f, 0);
            manageS.Add_Stal(8f, 0);

            platform_M.Add(0f, 600);
            platform_M.Add(1300, 800);


        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {

            // TODO: Add your initialization logic here
            front_sc = 4;
            back_sc = 5;




            //ajout IA


            // ajout platform

        }



        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard;

            keyboard = Keyboard.GetState();
           
            // scrolling
            scrolling1.Update(keyboard);
            // scrolling3.Update(keyboard);

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
            collision.collision_ia_AR_sol(managerAR, ref platform_M);
            collision.collision_ia_sol(managerAA, ref platform_M);

            //manager IA 
            managerAR.Update(ref keyboard);
            managerAA.Update( ref keyboard);
           
            manageS.Update(allen, ref keyboard);

            //pause
            pause(game, keyboard);

            //partie perdu
            fail(game, allen);

            //platform
            platform_M.Update(keyboard);
            //audio

            if (allen.rectangle.Right >= width * 2 - 50)
                game.ChangeState(Game1.gameState.level2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here

            spriteBatch.Begin();
            scrolling1.Draw(spriteBatch);

            //scrolling3.Draw(spriteBatch);

            allen.Draw(spriteBatch);
            platform_M.Draw(spriteBatch);
            managerAA.Draw(spriteBatch);
            managerAR.Draw(spriteBatch);
            manageS.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
