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
    class sripte_V
    {
        public Texture2D texture;
        List<Texture2D> L_texture;

        int change_T;
        public Rectangle rectangle;
        int height, width;
        Bullet_manager bullet;

        int speed, nb;

        public void Draw(SpriteBatch spritebatch)
        {
            bullet.Bullet_draw(spritebatch);
            spritebatch.Draw(texture, rectangle, Color.White);
        }

        public sripte_V(List<Texture2D> n_texture, Rectangle n_rectangle, ContentManager content, int height, int width)
        {
            texture = n_texture[0];
            L_texture = new List<Texture2D>();
            L_texture = n_texture;
            rectangle = n_rectangle;
            this.height = height; this.width = width;
            nb = 60;
            speed = 5;
            change_T = 0;


            bullet = new Bullet_manager(content.Load<Texture2D>("bullet"), new Rectangle(rectangle.X, rectangle.Y, 10, 50), nb, 7,content.Load<SoundEffect>("tir2"));



        }

        public void Update(KeyboardState keyboard, Game1 game, KeyboardState oldkey)
        {
            if ((keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Left)) || keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyDown(Keys.Right))
                move();
            else
            {
                if (keyboard.IsKeyDown(Keys.Up) && (rectangle.Y > 0))
                    up();
                if (keyboard.IsKeyDown(Keys.Down) && (rectangle.Bottom < height))
                    down();
                if (keyboard.IsKeyDown(Keys.Right) && (rectangle.X + rectangle.Width < width))
                    right();
                if (keyboard.IsKeyDown(Keys.Left) && (rectangle.X > 0))
                    left();
            }

            change_T += 1;

            bullet.Bullet_Update(keyboard, this, oldkey);
        }

        private void up()
        {
            rectangle.Y -= speed;
            move();
        }
        private void down()
        {
            rectangle.Y += speed;
            texture = L_texture[0];
        }
        private void right()
        {
            rectangle.X += speed;
            if (change_T % 5 == 0)
                texture = L_texture[1];
            else
                texture = L_texture[4];
        }
        private void left()
        {
            rectangle.X -= speed;
            if (change_T % 5 == 0)
                texture = L_texture[2];
            else
                texture = L_texture[5];
        }

        private void move()
        {
            if (change_T % 5 == 0)
                texture = L_texture[0];
            else
                texture = L_texture[3];
        }
    }
}
