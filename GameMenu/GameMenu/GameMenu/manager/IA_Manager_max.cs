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
        protected Texture2D _texture;
        protected Rectangle _rectangle;
        protected int front_sc, speed, window_H, window_W;
        protected ContentManager content;
        protected Color colo;

        public Texture2D _Texture { get { return _texture; } }
        public Rectangle _Rectangle { get { return _rectangle; } }

        public List<vaisseau_IA> Ia_manage { get { return ia_manage; } }
        public List<munition> bulletL;
        public void Add_Stal(float X, float Y)
        {
            ia_manage.Add(new Stalker(_texture, new Rectangle((int)(X * window_W) + 1, (int)(Y * window_H) - 1, _rectangle.Width, _rectangle.Height), front_sc, speed, window_H, window_W,0));
        }

        public void remove()//enleve ia si pv =0 ou sort de l ecran
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (ia_manage[i].vie <= 0||ia_manage [i].rectangle .Top >window_H )
                    ia_manage.RemoveAt(i);
            }

        }
        public void removed(int i_de_list)// enleve un ia
        {
            this.ia_manage.RemoveAt(i_de_list);
        }
    }
}
