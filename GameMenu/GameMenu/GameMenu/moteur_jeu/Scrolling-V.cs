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
    class Scrolling : Background
    {
        private int speed, height;

        public Scrolling(Texture2D n_texture, Rectangle n_rectangle, int n_speed, int height)
        {
            texture = n_texture;
            rectangle = n_rectangle;
            rectangle2 = n_rectangle;
            rectangle2.Y = -height;
            speed = n_speed;
            this.height = height;
        }

        public void Update()
        {
            rectangle.Y += speed;
            rectangle2.Y += speed;
            if (rectangle.Y >= height)
                rectangle.Y = rectangle2.Y - rectangle2.Height;
            if (rectangle2.Y >= height)
                rectangle2.Y = rectangle.Y - rectangle.Height;
        }

        public void update_ophelia(KeyboardState keybord)
        {
            if (keybord.IsKeyDown(Keys.Up))
            {
                rectangle.Y += speed;
                rectangle2.Y += speed;
            }
            if (keybord.IsKeyDown(Keys.Down))
            {
                rectangle.Y -= speed;
                rectangle2.Y -= speed;
            }
            if (rectangle.Y >= height)
                rectangle.Y = rectangle2.Y - rectangle2.Height;
            if (rectangle2.Y >= height)
                rectangle2.Y = rectangle.Y - rectangle.Height;
            if (rectangle.Bottom <= 0)
                rectangle.Y = rectangle2.Bottom ;
            if (rectangle2.Bottom  <= 0)
                rectangle2.Y = rectangle.Bottom ;
        }
    }
}
