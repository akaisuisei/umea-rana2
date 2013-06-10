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
        public string name { get; set; }
        public platform(Texture2D n_textture, Rectangle n_rectangle, int n_speed)
        {
            texture = n_textture;
            rectangle = n_rectangle;
            speed = n_speed;
            width = rectangle.Width;
            height = rectangle.Height;
        }
        public  platform( Rectangle n_rectangle,int distance, Plat plat)
        {
            rectangle = n_rectangle;
            this.speed = plat.speed;
            width = rectangle.Width;
            height = rectangle.Height;
            this.distance =distance  ;
            _parcouru = 0;
            dir = 1;
            switch (plat.type)
            {
                case ('0')://bouge pas
                    break;
                case ('R'):// va vers la droite par rapport a son point de depart
                    break;
                case ('L')://  vers la gauche par rapport a son point de depart
                    break;
                case ('U'):// vers le haut par rapport a sont pt de depart
                    break;
                case ('D'):// vers le bas par rapport a sont pt de depart
                    break;
                case ('1'):// vers 3pi/4 par rapport a sont pt de depart
                    break;
                case ('2'):// vers pi/4le bas par rapport a sont pt de depart
                    break;
                case ('3'):// vers -pi/4le bas par rapport a sont pt de depart
                    break;
                case ('4'):// vers -3pi/4le bas par rapport a sont pt de depart
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
