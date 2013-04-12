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

    public class Tireur : vaisseau_IA
    {

        public Tireur(Rectangle rectangle, ContentManager content, int height, int width, quaintuplet quaint, int spawn)
        {
      
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;
            this.width = width;
            dir = -1;

            bullet = new Bullet_manager( new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, quaint.bullet_Speed,
                content.Load<SoundEffect>("hero//vaisseau//tir2"), quaint.color, width, quaint.firerate);

            timer_lunch = quaint.seconde;
            _speed = quaint.speed;
            vie = quaint.vie;
            trajectory = quaint.trajectory;
            FrameColunm = 1;

            this.spawn = spawn;
        }


        public Tireur(Rectangle rectangle, ContentManager content, int height, int width, quaintuplet quaint)
        {
          
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;
            this.width = width;
            dir = -1;

            bullet = new Bullet_manager( new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, quaint.bullet_Speed,
                content.Load<SoundEffect>("hero//vaisseau//tir2"), quaint.color, width, quaint.firerate);

            timer_lunch = quaint.seconde;
            _speed = quaint.speed;
            vie = quaint.vie;
            trajectory = quaint.trajectory;
            FrameColunm  = 1;
          

        }

        public Tireur( Rectangle rectangle, ContentManager content, int height, int width, Color colo, int time_lunch)
        {
            
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;
            this.Ia_color = colo;
            this.timer_lunch = time_lunch;
            Munition_color = colo;
            bullet = new Bullet_manager(new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 4, content.Load<SoundEffect>("hero//vaisseau//tir2"), Munition_color, width, 50);
            this.width = width;
            dir = -1;
            _speed = 7;
            vie = 2;

            this.FrameColunm  = 1;


        }

       



    }
}
