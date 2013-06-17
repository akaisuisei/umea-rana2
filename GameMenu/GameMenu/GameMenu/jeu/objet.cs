using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Umea_rana
{
    public class objet
    {
        public Rectangle rectangle_C, rectangle;
        protected int decallageX, decallageY, largeurX, hauteurY;
        public int poid;
        public bool tombe;
        public float vie { get; set; }
        public int decalageX { get { return decallageX; } set { decallageX = decalageX; } }
        public int decalageY { get { return decallageY; } set { decallageY = decalageY; } }
        

        public void Update_rec_collision()
        {
            rectangle_C.X = rectangle.X + decallageX;
            rectangle_C.Y = rectangle.Y + decallageY;
            rectangle_C.Height = hauteurY;
            rectangle_C.Width = largeurX;
        }
       public Vector2 vise(objet sprt)
        {
            Vector2 _vecteur;
            _vecteur.X = -rectangle.Center.X + sprt.rectangle.Center.X;
            _vecteur.Y = rectangle.Center.Y - sprt.rectangle.Center.Y;
            _vecteur.Normalize();
            return (_vecteur);
        }
        protected Vector2 vise(Rectangle rect)
        {
            Vector2 _vecteur;
            _vecteur.X = -rectangle_C.Center.X + rect.Center.X;
            _vecteur.Y = rectangle_C.Center.Y - rect.Center.Y;
            _vecteur.Normalize();
            return (_vecteur);
        }
        protected void vise(objet j1, objet j2,ref  Rectangle cible,ref Vector2 cibleV)
        {
            int distance_j1 = j1.rectangle_C.X + j1.rectangle_C.Y - rectangle_C.Center.X - rectangle_C.Center.Y;

            if (Math.Min(distance_j1, j2.rectangle_C.X + j2.rectangle_C.Y - rectangle_C.Center.X - rectangle_C.Center.Y) == distance_j1)
            {
                cibleV = vise(j1);
                cible = j1.rectangle_C;
            }
            else
            {
                cibleV = vise(j2);
                cible = j2.rectangle_C;
            }
        }
    }  
}
