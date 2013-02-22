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
    public class munition :objet 
    {
        public Texture2D texture;
        Vector2 vect;
        public bool existe, bool_dexistence;
        int speed;
        public int temps_dexitence = 0;
        Color colo;

        public munition(Texture2D n_texture, Rectangle n_rectangle, int n_speed, Vector2 vect ,Color  colo)
        {
            texture = n_texture;
            rectangle = n_rectangle;
            existe = true;
            speed = n_speed;
            bool_dexistence = true;
            this.vect = vect;
            this.colo = colo;
        }


        public void update2()
        {
            rectangle.Y -=(int) (speed*vect.Y) ;
            rectangle.X += (int)(speed * vect.X);
            rectangle_C = rectangle;
        }

        public void Draw(SpriteBatch spritebatch)
        {

                spritebatch.Draw(texture, rectangle, colo );
        }
    }
}
