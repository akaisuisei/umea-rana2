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


        public Tireur(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width, Color colo)
        {
            this._texture = texture;
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.Ia_color = Color.Red;

            Munition_color = colo;
            bullet = new Bullet_manager(content.Load<Texture2D>("bullet//bullet"), new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 7, content.Load<SoundEffect>("hero//vaisseau//tir2"), Munition_color, width);
            this.width = width;
            dir = 1;
            _speed = 7;
            vie = 5;
        }

        public void Update(Game1 game)
        {
            bullet.Bullet_Update2(this, new Vector2(0, -1), 1);

            move_H();


        }


    }
}
