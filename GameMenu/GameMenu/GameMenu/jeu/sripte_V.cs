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
    public class sripte_V :objet 
    {
        public Texture2D texture;
        List<Texture2D> L_texture;
        public List<munition> bulletL;
        Color color_V;

        int change_T;
        
        int height, width;
        public Bullet_manager bullet;
        public bool automatic_controlled;
        int speed;
        int n;
        public void Draw(SpriteBatch spritebatch)
        {
            bullet.Bullet_draw(spritebatch,ref bulletL );
            spritebatch.Draw(texture, rectangle, Color.White);
        }

        public sripte_V(List<Texture2D> n_texture, Rectangle n_rectangle, ContentManager content, int height, int width, Color colo, int speed)
        {
            decallageX = 10;
            decallageY = 10;
            largeurX = n_rectangle.Width - decallageX;
            hauteurY = n_rectangle.Height - decallageY;

            texture = n_texture[0];
            L_texture = n_texture;
            rectangle = n_rectangle;
            rectangle_C = rectangle;
            this.height = height; this.width = width;this.speed = speed;
            change_T = 0;
            
            automatic_controlled = false;
            color_V = colo;
            // intencie le manager de missille 
            bulletL = new List<munition>();
            bullet = new Bullet_manager(content.Load<Texture2D>("bullet//bullet"), new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 10,content.Load<SoundEffect>("hero//vaisseau//tir2"),color_V,width ,30 );
            n = 0;
        }

        public void Update(KeyboardState keyboard, Game1 game, KeyboardState oldkey)
        {
      
            if (automatic_controlled)//movement automatic de fin de jeu
                up();
            else// controlle du vaisseau
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
            }

            change_T += 1;// timer pour l animation
            if(keyboard.IsKeyDown(Keys.D1)||keyboard.IsKeyDown(Keys.F1 ))
                n=1;
            if (keyboard.IsKeyDown(Keys.D2) || keyboard.IsKeyDown(Keys.F2))
                n = 2;
            if (keyboard.IsKeyDown(Keys.D3) || keyboard.IsKeyDown(Keys.F3))
                n = 3;
            if (keyboard.IsKeyDown(Keys.D4) || keyboard.IsKeyDown(Keys.F4))
                n = 4;

            bullet.Bullet_Update(keyboard, this, oldkey,new Vector2 (0,1),n,ref bulletL );
            Update_rec_collision();
        }

        // movement et animation
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

       // animation
        private void move()
        {
            if (change_T % 5 == 0)
                texture = L_texture[0];
            else
                texture = L_texture[3];
        }

        // procedure pour indiquer au vaisseau la fin de niveau
        public void gagne()
        {
            automatic_controlled = true;
            this.bullet.enableFire = false;
        }
    }
}
