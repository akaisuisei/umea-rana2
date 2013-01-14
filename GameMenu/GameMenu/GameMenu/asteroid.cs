using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Umea_rana;

namespace Umea_rana
{
    public class asteroid
    {
        public Rectangle rectangle;
        Texture2D texture;
        int dir;
        Random rnd;
        int widt_max;

        public asteroid(Texture2D n_texture, Rectangle n_rectanle, float rot, int witdhm)
        {
            rectangle = n_rectanle;
            texture = n_texture;
            rnd = new Random();
            dir = 1;
            widt_max = witdhm ;
        }

        public void update()
        {
            if (rectangle.Right > widt_max  )
            {
                rectangle.X = widt_max-rectangle.Width -1 ;
                dir = -dir;
            }
            if(rectangle.Left < 0)
            {
            rectangle.X = 0;
                dir = -dir;
            }
            rectangle.X += rnd.Next(0, 20) * dir;

        }


        public void Draw(SpriteBatch spritebach)
        {
            spritebach.Draw(texture, rectangle, Color.White);
        }

    }
}
