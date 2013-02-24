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
    class Stalker : vaisseau_IA
    {

        int window_W, window_H;
        public Stalker(Texture2D n_textture, Rectangle n_rectangle, int front_sc, int speed, int window_H, int window_W, int launchTime)
        {
            decallageX = 0; decallageY = 0;
            hauteurY = n_rectangle.Height - decallageX; largeurX = n_rectangle.Width - decallageY;
            this.rectangle = n_rectangle;
            this.rectangle_C = rectangle;

            this._texture = n_textture;
            Ia_color = Color.AliceBlue;
            vie = 2;

            this.window_H = window_H;
            this.window_W = window_W;
            tombe = true;
            _speed = speed;
            poid = 10;
            this.dir = 1;
            this.front_sc = front_sc;
            this.timer_lunch = launchTime;
        }
        // le stalker
        public void Update(objet sprite, ref KeyboardState keyboard)
        {
            Update_rec_collision();
            if (this.rectangle_C.X < 1.2f * window_W && this.rectangle_C.X > -0.2f * window_W)
            {
                if (tombe)
                    rectangle.Y += poid;


                if (rectangle_C.Center.X - _speed > sprite.rectangle_C.Center.X)
                    rectangle.X -= _speed;
                else if (rectangle_C.Center.X + _speed < sprite.rectangle_C.Center.X)
                    rectangle.X += _speed;

            } if (keyboard.IsKeyDown(Keys.Right))
                rectangle.X -= front_sc;
            if (keyboard.IsKeyDown(Keys.Left))
                rectangle.X += front_sc;

            //  if (rectangle.Center.Y  < sprite.rectangle.Center.Y)
            //    rectangle.Y += 1;
            //else
            //  rectangle.Y -= 1;

        }
        // le debile ki avnace et recule
        public void UpdateAR(ref KeyboardState keyboard)
        {
            Update_rec_collision();
            if (this.rectangle_C.Center.X < 1.2f * window_W && this.rectangle_C.X > -0.2f * window_W)
            {
                if (tombe)
                    rectangle.Y += poid;

                rectangle.X += dir * _speed;
            }
            if (keyboard.IsKeyDown(Keys.Right))
                rectangle.X -= front_sc;
            if (keyboard.IsKeyDown(Keys.Left))
                rectangle.X += front_sc;


        }
        // le bon kamikaze
        public void Update_Kamikaze(objet sprite,ref int gameTime)
        {
            if (timer_lunch <= gameTime)
            {
                Update_rec_collision();
                if (rectangle_C.Center.X > sprite.rectangle_C.Center.X + 9)
                    rectangle.X -= _speed;
                else
                    rectangle.X += _speed;
                if (rectangle_C.Center.Y > sprite.rectangle_C.Center.Y + 9)
                    rectangle.Y -= _speed;
                else
                    rectangle.Y += _speed;
            }


        }
        // le debile ki va tout droit jusqu a se suicider
        public void Update_A(KeyboardState keyboard)
        {
            Update_rec_collision();

            if (this.rectangle_C.Center.X < 1.2f * window_W && this.rectangle_C.X > -0.2f * window_W)
            {
                rectangle.X -= _speed;
                if (tombe)
                    rectangle.Y += poid;
            }
            if (keyboard.IsKeyDown(Keys.Right))
                rectangle.X -= front_sc;
            if (keyboard.IsKeyDown(Keys.Left))
                rectangle.X += front_sc;

        }


        public override void Draw(SpriteBatch spritback)
        {
            spritback.Draw(_texture, rectangle, Ia_color);
        }
    }
}
