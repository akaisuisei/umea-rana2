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
        float rotation, mov_rot;
        int mov, dir;
        Random rnd;

        public asteroid(Texture2D n_texture,Rectangle n_rectanle,float rot)       
        {
            rectangle = n_rectanle;
            texture = n_texture;
            rnd = new Random();
            mov=rnd.Next(50);
            rotation = 1f;
            mov_rot = rot;
            dir = 1;
        }

        public void update()
        {
            mov = rnd.Next(50);
            if(rectangle.Right>2000||rectangle.Left<0)
                dir=-dir;

            
            rotation += mov_rot/100 ;

            rectangle.X += mov * dir;

        }


        public void Draw(SpriteBatch spritebach)
        {
            spritebach.Draw(texture, rectangle,Color.White );
        }

    }
}
