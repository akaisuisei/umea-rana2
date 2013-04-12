using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Umea_rana
{
    public class IA_manager_V : IA_Manager_max
    {

        public IA_manager_V(Texture2D texture, Rectangle rectangle, ContentManager content, Rectangle fond)
        {
            ia_manage = new List<vaisseau_IA>();
            bulletL = new List<munition>();
            this._texture = texture;
            this._rectangle = rectangle;
            this.content = content;
            rectangleF = fond;
            this.window_H = fond.Height;
            this.window_W = fond.Width;
            AnimationSpeed = 10;
            mtexture = content.Load<Texture2D>("bullet//bullet");
        }
        public IA_manager_V(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width)
        {
            ia_manage = new List<vaisseau_IA>();
            bulletL = new List<munition>();
            this._texture = texture;
            this._rectangle = rectangle;
            this.content = content;
            this.window_H = height;
            this.window_H = height;
            this.window_W = width;
            AnimationSpeed = 10;
             mtexture = content.Load<Texture2D>("bullet//bullet");
        }

        public void Update(ref sripte_V sprite, ref int gameTime)
        {
            //update che chaque missile
            for (int i = 0; i < bulletL.Count; i++)
            {
                bulletL[i].update2();
                if (bulletL[i].rectangle_C.Top > 1080)
                    bulletL.RemoveAt(i);
            }
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (ia_manage[i].timer_lunch <= gameTime)
                {
                    ia_manage[i].bullet.Bullet_Update2(this . ia_manage[i], vise(sprite, ref ia_manage[i].rectangle_C ), 1, ref bulletL);
                    ia_manage[i].Update_rec_collision();
                    moveH(ref i);
                }

                this.ia_manage[i].Timer++;
                if (this.ia_manage[i].Timer == this.AnimationSpeed)
                {
                    this.ia_manage[i].Timer = 0;
                    this.ia_manage[i].FrameColunm++;
                    if (ia_manage[i].FrameLine == 1 && ia_manage[i].FrameColunm > 5)
                    {
                        ia_manage[i].FrameColunm = 1;
                        ia_manage[i].FrameLine = 2;
                    }
                    else if (ia_manage[i].FrameLine == 2 && ia_manage[i].FrameColunm > 8)
                    {
                        ia_manage[i].FrameColunm = 1;
                        ia_manage[i].FrameLine = 1;
                    }

                    remove(ref i);
                }
            }
        }


        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                spritebatch.Draw(_texture, ia_manage[i].rectangle, new Rectangle((this.ia_manage[i].FrameColunm - 1) * 42, (this.ia_manage[i].FrameLine - 1) * 42, 42, 42), Color.White, 0f, new Vector2(0, 0), this.ia_manage[i].Effects, 0f);
             }
            for (int i = 0; i < bulletL.Count; i++)
                spritebatch.Draw(mtexture, bulletL[i].rectangle, new Rectangle(0, 0,mtexture.Width, mtexture.Height), bulletL[i].colo);
        }
        public void Add(quaintuplet quaint, int spawn)
        {
            for (int i = 0; i < quaint.nombre; ++i)
                ia_manage.Add(new Viseur_aI( new Rectangle((int)(quaint.X * window_W) + i * _rectangle.Width, (int)(quaint.Y * window_H), _rectangle.Width, _rectangle.Height),
                    content, window_H, window_W, quaint, spawn));
        }


        /// <summary>
        /// add pour jouer
        /// </summary>
        /// <param name="quaint"></param>
        public void Add(quaintuplet quaint)
        {
            for (int i = 0; i < quaint.nombre; ++i)
                ia_manage.Add(new Viseur_aI( new Rectangle((int)(quaint.X * window_W) + i * _rectangle.Width, -_rectangle.Height, _rectangle.Width, _rectangle.Height),
                    content, window_H, window_W, quaint));
        }

        public void Add(float X, float Y, int lunch_time, int number, Color colo)
        {
            int x = (int)(X * window_W);
            int y = (int)(Y * window_H);
            for (int i = 0; i < number; ++i)
                ia_manage.Add(new Viseur_aI( new Rectangle(x + i * _rectangle.Width, y, _rectangle.Width, _rectangle.Height), content, window_H, window_W, colo, lunch_time));
        }


    }
}
