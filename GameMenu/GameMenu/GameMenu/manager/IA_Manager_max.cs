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
    public class IA_Manager_max
    {
        protected List<vaisseau_IA> ia_manage;
        protected Texture2D _texture, mtexture;
        protected Rectangle _rectangle, rectangleF;
        protected int front_sc, speed, window_H, window_W, poid, AnimationSpeed;
        protected ContentManager Content;
        protected Color colo;

        public Texture2D _Texture { get { return _texture; } }
        public Rectangle _Rectangle { get { return _rectangle; } }
        public List<vaisseau_IA> Ia_manage { get { return ia_manage; } }
        public List<munition> bulletL;

        public virtual void Add(float X, float Y, int id)
        {
            ia_manage.Add(new Stalker( new Rectangle((int)(X * window_W) + 1, (int)(Y * window_H) - 1, _rectangle.Width, _rectangle.Height), speed, window_H, window_W, 0, id));
        }

        public void remove()//enleve ia si pv =0 ou sort de l ecran
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (ia_manage[i].vie <= 0 || ia_manage[i].rectangle.Top > window_H)
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
                    ++ia_manage[i].rectangle.Y;
                if (keyboard.IsKeyDown(Keys.Down))
                    --ia_manage[i].rectangle.Y;
                ia_manage[i].Update_rec_collision();
            }
        }
        protected Vector2 vise(objet sprt,ref Rectangle rectangle)
        {
            Vector2 _vecteur;
            _vecteur.X = -rectangle.Center.X + sprt.rectangle.Center.X;
            _vecteur.Y = rectangle.Center.Y - sprt.rectangle.Center.Y;
            _vecteur.Normalize();
            return (_vecteur);
        }

        protected void moveH(ref int i)
        {

            if (ia_manage[i].rectangle_C.Right > rectangleF.Right )
            {
                ia_manage[i].rectangle.X = rectangleF.Right  - ia_manage[i].rectangle_C.Width -1 ;
                ia_manage[i].rectangle.Y += ia_manage[i].rectangle_C.Height;
                ia_manage[i].dir *=-1;

            }
            if (  ia_manage[i].rectangle_C.Left < rectangleF.Left )
            {
                 ia_manage[i]. rectangle.X = rectangleF.Left +1;
                 ia_manage[i].rectangle.Y += ia_manage[i].rectangle_C.Height;
                 ia_manage[i]. dir *=-1;
            }
            ia_manage[i].rectangle.X += ia_manage[i].Speed  * ia_manage[i].dir;
        }
    }
}
