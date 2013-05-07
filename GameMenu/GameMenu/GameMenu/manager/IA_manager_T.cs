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
    public class IA_manager_T : IA_Manager_max
    {
        public IA_manager_T(Texture2D texture, Rectangle rectangle, ContentManager Content, Rectangle fond)
        {
            ia_manage = new List<vaisseau_IA>();
            bulletL = new List<munition>();
            this._texture = texture;
            this._rectangle = rectangle;
            this.Content = Content;
            rectangleF = fond;
            this.window_H = fond.Height;
            this.window_W = fond.Width;
            AnimationSpeed = 10;
            mtexture = Content.Load<Texture2D>("bullet//bullet");
            this.fond = fond;
            this.screen = new Rectangle(0, 0, fond.Width * 2, fond.Height);
        }
        public IA_manager_T(Texture2D texture, Rectangle rectangle, ContentManager Content, int height, int width)
        {
            ia_manage = new List<vaisseau_IA>();
            bulletL = new List<munition>();
            this._texture = texture;
            this._rectangle = rectangle;
            this.Content = Content;
            this.window_H = height;
            this.window_H = height;
            this.window_W = width;
            AnimationSpeed = 10;
            mtexture = Content.Load<Texture2D>("bullet//bullet");
        }

        public void Update(ref Game1 game, ref int gametime)
        {

            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (gametime >=ia_manage[i]. timer_lunch)
                {
                    ia_manage[i].bullet.Bullet_Update2(ia_manage[i], new Vector2(0, -1), 1, ref bulletL);
                    moveH(ref i);
                    ia_manage[i].Update_rec_collision();
                }

                this.ia_manage[i].Timer++;
                if (this.ia_manage[i].Timer == this.AnimationSpeed)
                {
                    this.ia_manage[i].Timer = 0;
                    this.ia_manage[i].FrameColunm ++;
                    if (ia_manage[i].FrameColunm >= 1 && ia_manage[i].FrameColunm < 13)
                    {
                        ia_manage[i]. Effects = SpriteEffects.None;
                    }
                    if (ia_manage[i]. FrameColunm > 12 &&ia_manage[i].  FrameColunm < 25)
                    {
                       ia_manage[i].  Effects = SpriteEffects.FlipHorizontally;
                       ia_manage[i].  FrameColunm = 1;
                    }
                    if (ia_manage[i]. FrameColunm > 24)
                    {
                       ia_manage[i].  FrameColunm = 1;
                       ia_manage[i]. Effects = SpriteEffects.None;
                    }
                    remove(ref i);
                }
            }
            //update che chaque missile
            for (int i = 0; i < bulletL.Count; i++)
            {
                bulletL[i].update2();
                
                if (!bulletL[i].rectangle_C.Intersects (fond) )
                    bulletL.RemoveAt(i);
            }
        }



        public void Draw(SpriteBatch spritebatch)
        {

            for (int i = 0; i < bulletL.Count; i++)
                spritebatch.Draw(mtexture, bulletL[i].rectangle, new Rectangle(0, 0, mtexture.Width, mtexture.Height), bulletL[i].colo);
            
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                spritebatch.Draw(_texture, ia_manage[i].rectangle, new Rectangle((this.ia_manage[i].FrameColunm - 1) * 104, 0, 104, 132), Color.White, 0f, new Vector2(0, 0), this.ia_manage[i].Effects, 0f);
            }
        }

        public void Add(quaintuplet quaint, int spawn)
        {
            for (int i = 0; i < quaint.nombre; ++i)
                ia_manage.Add(new Tireur( new Rectangle((int)(quaint.X * window_W) + i * _rectangle.Width, (int)(quaint.Y * window_H), _rectangle.Width, _rectangle.Height),
                    Content, window_H, window_W, quaint, spawn));
        }

        /// <summary>
        /// add pour jouer
        /// </summary>
        /// <param name="quaint"></param>
        public void Add(quaintuplet quaint)
        {
            for (int i = 0; i < quaint.nombre; ++i)
                ia_manage.Add(new Tireur( new Rectangle(fond.X +(int)(quaint.X * fond.Width ) + i * _rectangle.Width, -_rectangle.Height, _rectangle.Width, _rectangle.Height),
                    Content, window_H, window_W, quaint));
        }

        public void Add(float X, float Y, int seconde, int number, Color colo)
        {
            int x = (int)(X * window_W);
            int y = (int)(Y * window_H);
            for (int i = 0; i < number; ++i)
                ia_manage.Add(new Tireur( new Rectangle(x + i * _rectangle.Width, y, _rectangle.Width, _rectangle.Height), Content, window_H, window_W, colo, seconde));
        }
    }
}
