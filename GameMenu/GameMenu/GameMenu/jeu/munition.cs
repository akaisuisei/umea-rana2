﻿using System;
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
    public class munition
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public bool existe;
        int speed;

        public munition(Texture2D n_texture, Rectangle n_rectangle, int n_speed)
        {
            texture = n_texture;
            rectangle = n_rectangle;
            existe = true;
            speed = n_speed;
        }


        public void update2()
        {
            rectangle.Y -= speed;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if(existe)
            spritebatch.Draw(texture, rectangle, Color.White);
        }
    }
}
