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
    public class asteroid :objet 
    {
        Texture2D texture;
        int dirX, dirY;
        Random rnd;
        int widt_max, windowH;
        float rot, movrot;
       public bool visible;

        public asteroid(Texture2D n_texture, Rectangle n_rectanle, float rot, int witdhm, int windows_H)
        {
            rectangle = n_rectanle;
            texture = n_texture;
            rnd = new Random();
            dirX = 1;
            dirY = 1;
            widt_max = witdhm ;

            this.rot = rot ;
            movrot = rot;
            visible = true;
            this.windowH = windows_H;
        }

        public void update()
        {
            
            if (rectangle.Right > widt_max +rectangle.Width  )
            {
                rectangle.X = widt_max-1;
                dirX = -dirX;
            }
            if (rectangle.Left < 0)
            {
                rectangle.X = 0;
                dirX = -dirX;
            }
            if (rectangle.Top < 0)
            {
                rectangle.Y = 0;
                dirY = -dirY;
            }
            if (rectangle.Bottom > windowH+rectangle.Height   )
            {
                rectangle.Y = windowH - 1;
                dirY = -dirY;
            }
            
            rectangle.X += rnd.Next(0, 20) * dirX;
            rectangle.Y += rnd.Next(0, 10) * dirY;
            rectangle_C = rectangle;
            rot += movrot;
        }


        public void Draw(SpriteBatch spritebach)
        {
            if (visible) 
            spritebach.Draw(texture, rectangle, null, Color.White,rot ,new Vector2(rectangle.Width/2,rectangle.Height/2 )  ,SpriteEffects.None,0f);
        }

        public void toucher()
        {
            this.dirX = -dirX;
        }
    }
}
