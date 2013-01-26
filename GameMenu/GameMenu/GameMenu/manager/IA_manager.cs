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

        public IA_manager_T(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width, Color colo_min)
        {
            ia_manage = new List<vaisseau_IA>();
            this._texture = texture;
            this._rectangle = rectangle;
            this.content = content;
            this.height = height;
            this.height = height;
            this.width = width;
            this.colo = colo_min;
        }

        public void Update(ref Game1 game)
        {
            foreach (Tireur vaiss in ia_manage)
                vaiss.Update(game);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Tireur vaiss in ia_manage)
                vaiss.draw(spritebatch);
        }

        public void Add(float X, float Y)
        {
            ia_manage.Add(new Tireur(_texture, new Rectangle((int)X, (int)Y, _rectangle.Width, _rectangle.Height), content, height, width, colo));
        }

    }

    public class IA_manager_V : IA_Manager_max
    {


        public IA_manager_V(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width, Color colo_min)
        {
            ia_manage = new List<vaisseau_IA>();
            this._texture = texture;
            this._rectangle = rectangle;
            this.content = content;
            this.height = height;
            this.height = height;
            this.width = width;
            this.colo = colo_min;
        }

        public void Update(ref sripte_V sprite)
        {
            foreach (Viseur_aI vaiss in ia_manage)
                vaiss.Update(sprite);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Viseur_aI vaiss in ia_manage)
                vaiss.draw(spritebatch);
        }

        public void Add(float X, float Y)
        {
            ia_manage.Add(new Viseur_aI(_texture, new Rectangle((int)X, (int)Y, _rectangle.Width, _rectangle.Height), content, height, width, colo));
        }
    }

    public class IA_manager_K : IA_Manager_max
    {


        public IA_manager_K(Texture2D n_textture, Rectangle n_rectangle, int front_sc, int speed)
        {

            ia_manage = new List<vaisseau_IA>();
            this._texture = n_textture;
            this._rectangle = n_rectangle;
            this.speed = speed;
        }

        public void Update(ref sripte_V sprite)
        {
            foreach (Stalker vaiss in ia_manage)
                vaiss.Update_Kamikaze(sprite);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Stalker vaiss in ia_manage)
                vaiss.draw(spritebatch);
        }
    }
}
