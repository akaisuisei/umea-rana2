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

        public IA_manager_T(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width)
        {
            ia_manage = new List<vaisseau_IA>();
            bulletL = new List<munition>();
            this._texture = texture;
            this._rectangle = rectangle;
            this.content = content;
            this.window_H = height;
            this.window_H = height;
            this.window_W = width;
        }

        public void Update(ref Game1 game, ref int gametime)
        {
            foreach (Tireur vaiss in ia_manage)
                vaiss.Update(game, ref gametime, ref bulletL);
            remove();
            //update che chaque missile
            for (int i = 0; i < bulletL.Count; i++)
            {
                bulletL[i].update2();
                if (bulletL[i].rectangle_C.Top > 1080)
                    bulletL.RemoveAt(i);
            }
        }

        public void Update_ophelia(KeyboardState keybord)
        {
            foreach (Tireur vaiss in ia_manage)
                vaiss.Update_ophelia(keybord);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Tireur vaiss in ia_manage)
                vaiss.Draw(spritebatch);
            for (int i = 0; i < bulletL.Count; i++)
                bulletL[i].Draw(spritebatch);
        }

        public void Add(quaintuplet quaint,int spawn)
        {
            for (int i = 0; i < quaint.nombre; ++i)
                ia_manage.Add(new Tireur(_texture, new Rectangle((int)(quaint.X * window_W) + i * _rectangle.Width, (int)(quaint.Y * window_H), _rectangle.Width, _rectangle.Height),
                    content, window_H, window_W, quaint,spawn ));
        }

        /// <summary>
        /// add pour jouer
        /// </summary>
        /// <param name="quaint"></param>
        public void Add(quaintuplet quaint)
        {
            for (int i = 0; i < quaint.nombre; ++i)
                ia_manage.Add(new Tireur(_texture, new Rectangle((int)(quaint.X * window_W) + i * _rectangle.Width, -_rectangle.Height, _rectangle.Width, _rectangle.Height),
                    content, window_H, window_W, quaint));
        }

        public void Add(float X, float Y, int seconde, int number, Color colo)
        {
            int x = (int)(X * window_W);
            int y = (int)(Y * window_H);
            for (int i = 0; i < number; ++i)
                ia_manage.Add(new Tireur(_texture, new Rectangle(x + i * _rectangle.Width, y, _rectangle.Width, _rectangle.Height), content, window_H, window_W, colo, seconde));
        }

    }

    public class IA_manager_V : IA_Manager_max
    {


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

        }

        public void Update(ref sripte_V sprite, ref int gameTime)
        {
            foreach (Viseur_aI vaiss in ia_manage)
            {
                vaiss.Update(ref sprite, ref gameTime, ref bulletL);
            }
            //update che chaque missile
            for (int i = 0; i < bulletL.Count; i++)
            {
                bulletL[i].update2();
                if (bulletL[i].rectangle_C.Top > 1080)
                    bulletL.RemoveAt(i);
            }
            remove();
        }
        public void Update_ophelia(KeyboardState keybord)
        {
            foreach (Viseur_aI vaiss in ia_manage)
                vaiss.Update_ophelia(keybord);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Viseur_aI vaiss in ia_manage)
                vaiss.Draw(spritebatch);
            for (int i = 0; i < bulletL.Count; i++)
                bulletL[i].Draw(spritebatch);
        }
        public void Add(quaintuplet quaint,int spawn)
        {
            for (int i = 0; i < quaint.nombre; ++i)
                ia_manage.Add(new Viseur_aI(_texture, new Rectangle((int)(quaint.X * window_W) + i * _rectangle.Width,(int)( quaint.Y *window_H ), _rectangle.Width, _rectangle.Height),
                    content, window_H, window_W, quaint,spawn ));
        }
        

        /// <summary>
        /// add pour jouer
        /// </summary>
        /// <param name="quaint"></param>
        public void Add(quaintuplet quaint)
        {
            for (int i = 0; i < quaint.nombre; ++i)
                ia_manage.Add(new Viseur_aI(_texture, new Rectangle((int)(quaint.X * window_W) + i * _rectangle.Width, -_rectangle.Height, _rectangle.Width, _rectangle.Height),
                    content, window_H, window_W,quaint ));
        }

        public void Add(float X, float Y, int lunch_time, int number, Color colo)
        {
            int x = (int)(X * window_W);
            int y = (int)(Y * window_H);
            for (int i = 0; i < number; ++i)
                ia_manage.Add(new Viseur_aI(_texture, new Rectangle(x + i * _rectangle.Width, y, _rectangle.Width, _rectangle.Height), content, window_H, window_W, colo, lunch_time));
        }


    }

    public class IA_manager_K : IA_Manager_max
    {


        public IA_manager_K(Texture2D n_textture, Rectangle n_rectangle, int window_H)
        {

            ia_manage = new List<vaisseau_IA>();

            this._texture = n_textture;
            this._rectangle = n_rectangle;
            this.window_H = window_H;
        }

        public void Update(ref sripte_V sprite, ref int gametime)
        {
            foreach (Stalker vaiss in ia_manage)
                vaiss.Update_Kamikaze(sprite, ref gametime);
            remove();
        }

        public void Update_ophelia(KeyboardState keybord)
        {
            foreach (Stalker vaiss in ia_manage)
                vaiss.Update_ophelia(keybord);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Stalker vaiss in ia_manage)
                vaiss.Draw(spritebatch);
        }
        #region ADD

        public void Add(couple couple,int spawn)
        {
            ia_manage.Add(new Stalker(_texture, new Rectangle((int)(couple.X * window_W), (int)(couple.Y * window_H), _rectangle.Width, _rectangle.Height), couple,spawn ));
        }

        public void Add(couple couple)
        {
            ia_manage.Add(new Stalker(_texture, new Rectangle((int)(couple.X * window_W), -1*_rectangle.Height , _rectangle.Width, _rectangle.Height), couple));
        }
        public override void Add(float X, float Y, int launch_time)
        {
            ia_manage.Add(new Stalker(_texture, new Rectangle((int)(X * window_W), (int)(Y * window_H), _rectangle.Width, _rectangle.Height), front_sc, speed, window_H, window_W, launch_time, 0));
        }
        #endregion
    }
}
