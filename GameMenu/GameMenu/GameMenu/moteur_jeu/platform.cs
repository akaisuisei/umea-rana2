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
        int  width, height, _distance, _parcouru;
        public int speed { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 direction { get; set; }
        public int parcouru { get; set; }
        public int distance { get { return _distance; } }
        public int dir { get; set; }
        public string name { get; set; }
        public platform(Texture2D n_textture, Rectangle n_rectangle, int n_speed)
        {
            texture = n_textture;
            rectangle = n_rectangle;
            speed = n_speed;
            width = rectangle.Width;
            height = rectangle.Height;
            this.X = rectangle.X;
            this.Y = rectangle.Y;
        }
        public  platform( Rectangle n_rectangle,int distance, Plat plat)
        {
            name = plat.name;
            rectangle = n_rectangle;
            this.speed = plat.speed;
            width = rectangle.Width;
            height = rectangle.Height;
            this._distance =distance  ;
            _parcouru = distance ;
            dir = 1;
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            switch (plat.type)
            {
                case ('0')://bouge pas
                    direction = Vector2.Zero;
                    break;
                case ('R'):// va vers la droite par rapport a son point de depart
                    direction = new Vector2(1, 0);
                    break;
                case ('L')://  vers la gauche par rapport a son point de depart
                    direction = new Vector2(-1, 0);
                    break;                    
                case ('U'):// vers le haut par rapport a son pt de depart
                    direction = new Vector2(0, 1);
                    break;
                case ('D'):// vers le bas par rapport a son pt de depart
                    direction = new Vector2(0, -1);
                    break;
                case ('1'):// vers 3pi/4 par rapport a son pt de depart
                    direction = new Vector2(-0.7071067812f, 0.7071067812f);
                    break;
                case ('2'):// vers pi/4le bas par rapport a son pt de depart
                    direction =new Vector2 (0.7071067812f,0.7071067812f);
                    break;
                case ('3'):// vers -pi/4le bas par rapport a son pt de depart
                    direction = new Vector2(0.7071067812f, -0.7071067812f);
                    break;
                case ('4'):// vers -3pi/4le bas par rapport a son pt de depart
                    direction = new Vector2(-0.7071067812f, -0.7071067812f);
                    break;

                default:
                    break;
            }
            Update_rec_collision();
        }
        public platform(Rectangle n_rectangle, int distance, Plat plat, int spawn)
        {
            vie = spawn;
            name = plat.name;
            rectangle = n_rectangle;
            this.speed = plat.speed;
            width = rectangle.Width;
            height = rectangle.Height;
            this._distance = distance;
            _parcouru = distance ;
            dir = 1;
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            switch (plat.type)
            {
                case ('0')://bouge pas
                    direction = Vector2.Zero;
                    break;
                case ('R'):// va vers la droite par rapport a son point de depart
                    direction = new Vector2(1, 0);
                    break;
                case ('L')://  vers la gauche par rapport a son point de depart
                    direction = new Vector2(-1, 0);
                    break;
                case ('U'):// vers le haut par rapport a sont pt de depart
                    direction = new Vector2(0, 1);
                    break;
                case ('D'):// vers le bas par rapport a sont pt de depart
                    direction = new Vector2(0, -1);
                    break;
                case ('1'):// vers 3pi/4 par rapport a sont pt de depart
                    direction = new Vector2(-0.7071067812f, 0.7071067812f);
                    break;
                case ('2'):// vers pi/4le bas par rapport a sont pt de depart
                    direction = new Vector2(0.7071067812f, 0.7071067812f);
                    break;
                case ('3'):// vers -pi/4le bas par rapport a sont pt de depart
                    direction = new Vector2(0.7071067812f, -0.7071067812f);
                    break;
                case ('4'):// vers -3pi/4le bas par rapport a sont pt de depart
                    direction = new Vector2(-0.7071067812f, -0.7071067812f);
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
