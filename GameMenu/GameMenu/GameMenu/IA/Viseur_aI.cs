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
        public Viseur_aI(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width, Color colo)
        {
            this._texture = texture;
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.Ia_color = Color.Green;
            this.vie = 5;

            Munition_color = colo;
            bullet = new Bullet_manager(content.Load<Texture2D>("bullet//bullet"), new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 7, content.Load<SoundEffect>("hero//vaisseau//tir2"), Munition_color, width);
            this.width = width;
            dir = 1;
            _speed = 6;
        }

        public void Update(sripte_V sprite)
        {
            bullet.Bullet_Update2(this, vise(sprite), 2);
            move_H();
        }

    }
}
