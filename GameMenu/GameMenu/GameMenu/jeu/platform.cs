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
    class platform
    {
        Texture2D texture;
        public Rectangle rectangle;
        int speed, width, height;
        


        public platform(Texture2D n_textture, Rectangle n_rectangle, int n_speed)
        {
            texture = n_textture;
            rectangle = n_rectangle;
            speed = n_speed;
            width = rectangle.Width;
            height = rectangle.Height;
        }

        public void update(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Right))
            {
                rectangle.X -= speed;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                rectangle.X += speed;
            }
        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rectangle, Color.White);
        }

    }
}
