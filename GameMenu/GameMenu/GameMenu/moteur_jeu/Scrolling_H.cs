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
    class Scrolling_H : Background
    {
        int speed;

        public Scrolling_H(Texture2D n_texture, Rectangle n_rectangle,int n_speed)
        {
            texture = n_texture;

            rectangle = n_rectangle;
            rectangle2 = n_rectangle;
            rectangle2.X = rectangle2.Width;
            speed = n_speed;
            
        }

        public void Update(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Right))
            {
                rectangle.X -= speed;
                rectangle2.X -= speed;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                rectangle.X += speed;
                rectangle2.X += speed;
            }

            if (rectangle.X + rectangle.Width <= 0)
                rectangle.X = rectangle2.X + rectangle2.Width;
            if (rectangle2.X + rectangle2.Width <= 0)
                rectangle2.X = rectangle.X + rectangle.Width;

            if (rectangle.X >= 0)
                rectangle2.X = rectangle.X - rectangle.Width;
            if (rectangle2.X >= 0)
               rectangle.X = rectangle2.X - rectangle2.Width;
     

        }
    }
}
