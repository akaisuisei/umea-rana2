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
    public class vaisseau_IA : objet
    {
        Texture2D _texture;
        public Rectangle rectangle;
        int _speed;
        int decallageX, decallageY, largeurX, hauteurY;
        Bullet_manager bullet;


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

        public void Update()
        {
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


        protected Vector2 vise( objet sprt)
        {
            Vector2 _vecteur;
            _vecteur.X = sprt.rectangle_Colision.Center.X - this.rectangle_Colision.Center.X;
            _vecteur.Y = sprt.rectangle_Colision.Center.Y - this.rectangle_Colision.Center.Y;
            _vecteur.Normalize();
            return _vecteur;
        }


    }
}
