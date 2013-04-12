using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Umea_rana
{
    public class IA_manager_K : IA_Manager_max
    {

        public IA_manager_K(Texture2D n_textture, Rectangle n_rectangle, Rectangle fond)
        {

            ia_manage = new List<vaisseau_IA>();

            this._texture = n_textture;
            this._rectangle = n_rectangle;
            rectangleF = fond;
            this.window_H = fond.Height;
            this.window_W = fond.Width;
        }
        public IA_manager_K(Texture2D n_textture, Rectangle n_rectangle, int window_H, int window_W)
        {

            ia_manage = new List<vaisseau_IA>();

            this._texture = n_textture;
            this._rectangle = n_rectangle;
            this.window_H = window_H;
            this.window_W = window_W;
        }

        public void Update(ref sripte_V sprite, ref int gametime)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (ia_manage[i]. timer_lunch <= gametime)
                {
                    ia_manage[i].Update_rec_collision();
                    if (ia_manage[i].rectangle_C.Center.X > sprite.rectangle_C.Center.X + 9)
                        ia_manage[i].rectangle.X -= ia_manage[i].Speed ;
                    else
                        ia_manage[i].rectangle.X += ia_manage[i].Speed ;
                    if (ia_manage[i].rectangle_C.Center.Y > sprite.rectangle_C.Center.Y + 9)
                        ia_manage[i].rectangle.Y -= ia_manage[i].Speed;
                    else
                        ia_manage[i].rectangle.Y += ia_manage[i].Speed;
                }
                remove(ref i);
            }
        }

     

        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
               spritebatch .Draw(_texture,ia_manage[i]. rectangle, Color.White);
            }
        }
        #region ADD

        public void Add(couple couple, int spawn)
        {
            ia_manage.Add(new Stalker(new Rectangle((int)(couple.X * window_W), (int)(couple.Y * window_H), _rectangle.Width, _rectangle.Height), couple, spawn));
        }

        public void Add(couple couple)
        {
            ia_manage.Add(new Stalker(new Rectangle((int)(couple.X * window_W), -1 * _rectangle.Height, _rectangle.Width, _rectangle.Height), couple));
        }
        public override void Add(float X, float Y, int launch_time)
        {
            ia_manage.Add(new Stalker(new Rectangle((int)(X * window_W), (int)(Y * window_H), _rectangle.Width, _rectangle.Height), speed, window_H, window_W, launch_time, 0));
        }
        #endregion
    }
}
