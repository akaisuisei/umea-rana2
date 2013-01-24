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
    public class vaisseau_IA : objet
    {

        protected Texture2D _texture;
       protected  int _speed, width, dir;
        protected int decallageX, decallageY, largeurX, hauteurY;
        public Bullet_manager bullet;
       protected  Color Munition_color, Ia_color;
       
        /*
     
        public vaisseau_IA(Texture2D _texture, Rectangle n_rectangle, ContentManager content, int height, int width, int _speed)
        {
            this.rectangle = n_rectangle;
            this._texture = _texture;
            this._speed = _speed;
            //def rectangle collision
            rectangle_Colision = rectangle;
            rectangle_Colision.Width = largeurX;
            rectangle_Colision.Height = hauteurY;
            decallageX = 0;
            decallageY = 0;
        }
        

        public void moveUp()
        {
            rectangle.Y -= _speed;
            rectangle_Colision.Y = rectangle.Y + decallageY;
        }
        public void moveDown()
        {
            rectangle.Y += _speed;
            rectangle_Colision.Y = rectangle.Y + decallageY;
        }
        public void moveRight()
        {
            rectangle.X += _speed;
            rectangle_Colision.X = rectangle.X + decallageX;
        }
        public void moveleft()
        {
            rectangle.Y -= _speed;
            rectangle_Colision.X = rectangle.X + decallageX;
        }

        */
        protected Vector2 vise( objet sprt)
        {
            Vector2 _vecteur;
            _vecteur.X =-rectangle.Center.X + sprt.rectangle.Center.X ;
            _vecteur.Y = rectangle.Center.Y- sprt.rectangle.Center.Y ;
            _vecteur.Normalize();
            return (_vecteur);
        }

        
       public virtual void draw(SpriteBatch spritback)
       {
           bullet.Bullet_draw(spritback);
           spritback.Draw(_texture, rectangle, Ia_color );
       }

       public void move_H()
       {
           if (rectangle.Right > width + rectangle.Width)
           {
               rectangle.X = width - 1;
               dir = -dir;
           }
           if (rectangle.Left < 0)
           {
               rectangle.X = 0;
               dir = -dir;

           }
           rectangle.X += _speed  * dir;
       }
    }
}