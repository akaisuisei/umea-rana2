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
    public class IA_Manager_max
    {
        protected List<vaisseau_IA> ia_manage;
        protected Texture2D _texture, mtexture;
        protected Rectangle _rectangle, rectangleF, screen;
        protected int front_sc, speed, window_H, window_W, poid, AnimationSpeed;
        protected ContentManager Content;
        protected Color colo;

        public Texture2D _Texture { get { return _texture; } }
        public Rectangle _Rectangle { get { return _rectangle; } }
        public List<vaisseau_IA> Ia_manage { get { return ia_manage; } }
        public List<munition> bulletL;
        public Rectangle fond;

        public void parrametrage(savefile savefile)
        {
            front_sc = savefile.levelProfile.fc_speed;
        }

        public virtual void Add(float X, float Y, int id)
        {
            ia_manage.Add(new Stalker(new Rectangle((int)(X * window_W) + 1, (int)(Y * window_H) - 1, _rectangle.Width, _rectangle.Height), speed, window_H, window_W, 0, id));
        }

        public void remove()//enleve ia si pv =0 ou sort de l ecran
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (ia_manage[i].vie <= 0 || !ia_manage[i].rectangle.Intersects(screen))
                    ia_manage.RemoveAt(i);
            }
        }
        protected void remove(ref int i)
        {
            if (ia_manage[i].vie <= 0 || ia_manage[i].rectangle.Top > window_H)
                ia_manage.RemoveAt(i);
        }
        public void removed(int i_de_list)// enleve un ia
        {
            this.ia_manage.RemoveAt(i_de_list);
        }
        public void remove_all()
        {
            this.ia_manage = new List<vaisseau_IA>();
        }
        public void Dipose()
        {
            for (int i = 0; i < ia_manage.Count; ++i)
                ia_manage[i].Dipose();
            _texture.Dispose();

        }

        public void Update_ophelia(KeyboardState keyboard)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (keyboard.IsKeyDown(Keys.Up))
                    ia_manage[i].rectangle.Y += front_sc;
                if (keyboard.IsKeyDown(Keys.Down))
                    ia_manage[i].rectangle.Y -= front_sc;
                ia_manage[i].Update_rec_collision();
            }
        }
        public void UpdateEDIT(KeyboardState keyboard)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (keyboard.IsKeyDown(Keys.Right ))
                    ia_manage[i].rectangle.X -= front_sc;
                if (keyboard.IsKeyDown(Keys.Left ))
                    ia_manage[i].rectangle.X += front_sc;
                ia_manage[i].Update_rec_collision();
            }
        }
        protected Vector2 vise(objet sprt, ref Rectangle rectangle)
        {
            Vector2 _vecteur;
            _vecteur.X = -rectangle.Center.X + sprt.rectangle.Center.X;
            _vecteur.Y = rectangle.Center.Y - sprt.rectangle.Center.Y;
            _vecteur.Normalize();
            return (_vecteur);
        }
        protected Vector2 vise(objet sprt, objet perso2, ref Rectangle rectangle)
        {
            Vector2 _vecteur;
            int distance1 = sprt.rectangle_C.Center.Y - rectangle.Center.Y + sprt.rectangle_C.Center.X - rectangle.Center.X;
            int dis2 = perso2.rectangle_C.Center.Y - rectangle.Center.Y + perso2.rectangle_C.Center.X - rectangle.Center.X;
            if (Math.Min(distance1, dis2) == distance1)
            {
                _vecteur.X = -rectangle.Center.X + sprt.rectangle.Center.X;
                _vecteur.Y = rectangle.Center.Y - sprt.rectangle.Center.Y;
            }
            else
            {
                _vecteur.X = -rectangle.Center.X + perso2.rectangle.Center.X;
                _vecteur.Y = rectangle.Center.Y - perso2.rectangle.Center.Y;
            }
            _vecteur.Normalize();
            return (_vecteur);
        }

        protected void moveH(ref int i)
        {
            switch (ia_manage[i].trajectory)
            {
                case "sinus":
                    if (ia_manage[i].shift <= 0f)
                    {
                        ia_manage[i].shift = 30;
                        ia_manage[i].dir = -ia_manage[i].dir;
                    }
                    ia_manage[i].shift--;
                    ia_manage[i].rectangle.X += ia_manage[i].Speed * ia_manage[i].dir;
                    ia_manage[i].rectangle.Y++;
                    break;
                case "log":

                    ia_manage[i].shift += 0.1f;
                    ia_manage[i].rectangle.X += (int)(ia_manage[i].Speed * ia_manage[i].dir + ia_manage[i].shift);
                    ia_manage[i].rectangle.Y += ia_manage[i].Speed;
                    break;
                case "-log":
                    ia_manage[i].shift -= 0.1f;
                    ia_manage[i].rectangle.X += (int)(ia_manage[i].Speed * ia_manage[i].dir + ia_manage[i].shift);
                    ia_manage[i].rectangle.Y += ia_manage[i].Speed;
                    break;
                case "expo":
                    ia_manage[i].shift += 0.009f;
                    ia_manage[i].rectangle.X += (int)(ia_manage[i].Speed * ia_manage[i].dir + ia_manage[i].shift);
                    ia_manage[i].rectangle.Y += ia_manage[i].Speed;
                    break;
                case "-expo":
                    ia_manage[i].shift -= 0.009f;
                    ia_manage[i].rectangle.X += (int)(ia_manage[i].Speed * ia_manage[i].dir + ia_manage[i].shift);
                    ia_manage[i].rectangle.Y += ia_manage[i].Speed;
                    break;
                default:
                    if (ia_manage[i].rectangle_C.Right > rectangleF.Right)
                    {
                        ia_manage[i].rectangle.X = rectangleF.Right - ia_manage[i].rectangle_C.Width - 1;
                        ia_manage[i].rectangle.Y += ia_manage[i].rectangle_C.Height;
                        ia_manage[i].dir *= -1;

                    }
                    if (ia_manage[i].rectangle_C.Left < rectangleF.Left)
                    {
                        ia_manage[i].rectangle.X = rectangleF.Left + 1;
                        ia_manage[i].rectangle.Y += ia_manage[i].rectangle_C.Height;
                        ia_manage[i].dir *= -1;
                    }
                    ia_manage[i].rectangle.X += ia_manage[i].Speed * ia_manage[i].dir;
                    break;
            }
        }
    }
}
