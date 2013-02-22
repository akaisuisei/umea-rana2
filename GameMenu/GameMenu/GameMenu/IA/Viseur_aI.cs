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
    public class Viseur_aI : vaisseau_IA
    {
        public Viseur_aI(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width, Color colo, int gametime)
        {
            this._texture = texture;
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;
            this.Ia_color = Color.Green;
            this.vie = 2;
            this.timer_lunch = gametime;

            Munition_color = colo;
            bullet = new Bullet_manager(content.Load<Texture2D>("bullet//bullet"), new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 7, content.Load<SoundEffect>("hero//vaisseau//tir2"), Munition_color, width,30);
            this.width = width;
            dir = -1;
            _speed = 6;
        }

        public void Update(ref sripte_V sprite, ref int game_time, ref List<munition > bulletL)
        {
            if (timer_lunch <= game_time)
            {
                bullet.Bullet_Update2(this, vise(sprite), 1,ref bulletL );
                Update_rec_collision();
                move_H();
            }
        }

    }
}
