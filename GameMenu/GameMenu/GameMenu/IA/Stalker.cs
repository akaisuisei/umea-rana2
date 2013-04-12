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
    class Stalker : vaisseau_IA
    {



        public int window_W, window_H;

        public Stalker( Rectangle n_rectangle, couple couple, int spawn)
        {
            this.rectangle_C = n_rectangle;
            this.rectangle = n_rectangle;
        

            decallageX = 0; decallageY = 0;
            hauteurY = n_rectangle.Height - decallageX; largeurX = n_rectangle.Width - decallageY;
            longueur_attaque = 100;

            this.timer_lunch = couple.seconde;
            this.vie = couple.vie;
            _speed = couple.speed;
            this._damage = couple.damage;
            this.dir = 1;
            this.spawn = spawn;

        }
        /// <summary>
        /// pour le kamikaze pour le SEU
        /// </summary>
        /// <param name="n_textture"></param>
        /// <param name="n_rectangle"></param>
        /// <param name="front_sc"></param>
        /// <param name="speed"></param>
        /// <param name="window_H"></param>
        /// <param name="window_W"></param>
        /// <param name="launchTime"></param>
        /// <param name="vie"></param>
        public Stalker( Rectangle n_rectangle, couple couple)
        {
            this.rectangle_C = n_rectangle;
            this.rectangle = n_rectangle;
     

            decallageX = 0; decallageY = 0;
            hauteurY = n_rectangle.Height - decallageX; largeurX = n_rectangle.Width - decallageY;
            longueur_attaque = 100;

            this.timer_lunch = couple.seconde;
            this.vie = couple.vie;
            _speed = couple.speed;
            this._damage = couple.damage;
            this.dir = 1;


        }
        /// <summary>
        /// pour les ia platforme
        /// </summary>
        /// <param name="n_textture"></param>
        /// <param name="n_rectangle"></param>
        /// <param name="front_sc"></param>
        /// <param name="speed"></param>
        /// <param name="window_H"></param>
        /// <param name="window_W"></param>
        /// <param name="id"></param>
        /// <param name="vie"></param>
        public Stalker( Rectangle n_rectangle, int speed, int window_H, int window_W, int id, int vie)
        {

            this.rectangle_C = n_rectangle;
            this.rectangle = n_rectangle;

            Ia_color = Color.AliceBlue;
            this.vie = vie;
            this.window_H = window_H;
            this.window_W = window_W;
            tombe = true;
            _speed = speed;
            normalspeed = speed;
            poid = 10;
            this.dir = 1;



            switch (id)
            {

                case 1:// stalker
                    decallageX = 32; decallageY = 36;
                    hauteurY = rectangle.Height - decalageY - 13; largeurX = 37;
                    longueur_attaque = 20;
                    this.FrameLine = 1;
                    this.FrameColunm = 1;
                    this.Timer = 0;
                    break;
                case 2:// ia AR
                    decallageX = 23; decallageY = 9;
                    hauteurY = 0 /*rectangle.Height - decalageY + 4*/; largeurX = 20;
                    longueur_attaque = 18;//2;
                    this.FrameLine = 1;
                    this.FrameColunm = 1;
                    this.Timer = 0;
                    break;
                default:// iaAA
                    decallageX = 21; decallageY = 23;
                    hauteurY = rectangle.Height - decalageY - 10; largeurX = 25;
                    longueur_attaque = 14;
                    dir = -1;
                    this.FrameLine = 1;
                    this.FrameColunm = 1;
                    this.Timer = 0;
                    break;
            }
        }
    
 
        // le bon kamikaze
        public void Update_Kamikaze(objet sprite, ref int gameTime)
        {
           


        }
        // le debile ki va tout droit jusqu a se suicider
     
        public override void Draw(SpriteBatch spritback)
        {
            spritback.Draw(_texture, rectangle, Color.White);
        }
      
    }
}
