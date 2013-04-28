using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Umea_rana
{

    // bullet speed oublier

    public class Viseur_aI : vaisseau_IA
    {


        public Viseur_aI( Rectangle rectangle, ContentManager Content, int height, int width, quaintuplet quaint,int spawn)
        {
       
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;
            this.width = width;
            dir = -1;

            bullet = new Bullet_manager( new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, quaint.bullet_Speed,
                Content.Load<SoundEffect>("hero//vaisseau//tir2"), quaint.color, width, quaint.firerate);

            timer_lunch = quaint.seconde;
            _speed = quaint.speed;
            vie = quaint.vie;
            trajectory = quaint.trajectory;
            this.spawn = spawn;

            this.Effects = SpriteEffects.None;
            this.FrameLine = 1;
            this. FrameColunm = 1;
            this.Timer = 0;
        }
        /// <summary>
        /// pour le add de jeu
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="rectangle"></param>
        /// <param name="Content"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="quaint"></param>
        public Viseur_aI( Rectangle rectangle, ContentManager Content, int height, int width, quaintuplet quaint)
        {
    
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;
            this.width = width;
            dir = -1;

            bullet = new Bullet_manager( new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, quaint.bullet_Speed,
                Content.Load<SoundEffect>("hero//vaisseau//tir2"), quaint.color, width, quaint.firerate);

            timer_lunch = quaint.seconde;
            _speed = quaint.speed;
            vie = quaint.vie;
            trajectory = quaint.trajectory;

            this.Effects = SpriteEffects.None;
            this.FrameLine = 1;
            this. FrameColunm = 1;
            this.Timer = 0;
        }

        public Viseur_aI( Rectangle rectangle, ContentManager Content, int height, int width, Color colo, int gametime)
        {

            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;

            Munition_color = colo;
            bullet = new Bullet_manager( new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 7, Content.Load<SoundEffect>("hero//vaisseau//tir2"), Munition_color, width, 60);
            this.width = width;
            dir = -1;
            _speed = 6;

            this.Ia_color = Color.Green;
            this.vie = 2;
            this.timer_lunch = gametime;
            this.Effects = SpriteEffects.None;
            this.FrameLine = 1;
            this.FrameColunm = 1;
            this.Timer = 0;


        }

       

    }
}
