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
    public class platform : objet
    {
        Texture2D texture;
        int speed, width, height, distance, _parcouru;
        public float X { get; set; }
        public float Y { get; set; }
        public int parcouru { get { return _parcouru; } set { _parcouru = parcouru; } }
        public int dir { get; set; }
        public platform(Texture2D n_textture, Rectangle n_rectangle, int n_speed)
        {
            texture = n_textture;
            rectangle = n_rectangle;
            speed = n_speed;
            width = rectangle.Width;
            height = rectangle.Height;
        }
        public  platform(Texture2D n_textture, Rectangle n_rectangle, int distance, int speed, char type)
        {
            texture = n_textture;
            rectangle = n_rectangle;
            this.speed = speed;
            width = rectangle.Width;
            height = rectangle.Height;
            this.distance = distance;
            _parcouru = 0;
            dir = 1;
            switch (type)
            {
                case ('0'):
                    break;
                case ('R'):
                    break;
                case ('L'):
                    break;
                case ('U'):
                    break;
                case ('D'):
                    break;
                case ('1'):
                    break;
                case ('2'):
                    break;
                case ('3'):
                    break;
                case ('4'):
                    break;

                default:
                    break;
            }
            Update_rec_collision();
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
            rectangle_C = rectangle;
        }


        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rectangle, Color.White);
        }


    }
}
