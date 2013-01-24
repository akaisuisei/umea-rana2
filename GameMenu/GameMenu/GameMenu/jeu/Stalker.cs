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
    class Stalker :vaisseau_IA 
    {
        int witi;
       
        public Stalker(Texture2D n_textture, Rectangle n_rectangle, int wiwit, int front_sc,int speed)
        {
            decallageX = 0; decallageY = 0;
            hauteurY = 100; largeurX = 100;
            this.rectangle = n_rectangle;
            this.rectangle_Colision = rectangle;
            rectangle_Colision.Width = largeurX  ;
            rectangle_Colision.Height = hauteurY  ;

            this._texture = n_textture;
            Ia_color = Color.AliceBlue;


            witi = wiwit;
            tombe = true;
            _speed = speed ;
            poid = 10;
            this.dir = 1;
            this.front_sc = front_sc;
        }

        public void Update(objet   sprite, ref KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Right))
                rectangle.X -= front_sc  ;
            if (keyboard.IsKeyDown(Keys.Left))
                rectangle.X +=front_sc  ;
             
            if ( rectangle_Colision .Center.X - _speed  > sprite.rectangle_Colision.Center.X )
               rectangle.X -= _speed ;
            else if (rectangle_Colision.Center.X  +_speed < sprite.rectangle_Colision.Center.X)
                rectangle.X  += _speed ;

            
            if (tombe)
                rectangle.Y += poid;
          //  if (rectangle.Center.Y  < sprite.rectangle.Center.Y)
            //    rectangle.Y += 1;
            //else
              //  rectangle.Y -= 1;
            Update_rec_collision();          
        }

        public void UpdateAR(ref KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Right))
                rectangle.X -= front_sc ;
            if (keyboard.IsKeyDown(Keys.Left))
                rectangle.X += front_sc ;
            rectangle.X += dir * _speed ;

            if (tombe)
                rectangle.Y += poid;
            Update_rec_collision();
        }

        public void Update_Kamikaze(objet sprite)
        {
            if (rectangle_Colision.Center.X - _speed > sprite.rectangle_Colision.Center.X)
                rectangle.X -= _speed;
            else if (rectangle_Colision.Center.X + _speed < sprite.rectangle_Colision.Center.X)
                rectangle.X += _speed;
            if (rectangle_Colision.Center.Y - _speed > sprite.rectangle_Colision.Center.Y)
                rectangle.Y -= _speed;
            else if (rectangle_Colision.Center.Y + _speed < sprite.rectangle_Colision.Center.Y)
                rectangle.Y += _speed;
        }


        public override  void draw(SpriteBatch spritback)
        {
            spritback.Draw(_texture, rectangle, Ia_color);
        }
    }
}
