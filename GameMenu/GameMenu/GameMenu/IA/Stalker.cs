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
        int FrameLine;
        int FrameColumn;
        SpriteEffects Effects;
        int Timer;
        int AnimationSpeed = 10;

        int FrameLine2;
        int FrameColumn2;
        SpriteEffects Effects2;
        int Timer2;

        int FrameLine3;
        int FrameColumn3;
        SpriteEffects Effects3;
        int Timer3;


        int window_W, window_H;
        public Stalker(Texture2D n_textture, Rectangle n_rectangle, int front_sc, int speed, int window_H, int window_W, int launchTime, int id)
        {

            this.rectangle_C = n_rectangle;

            decallageX = 0; decallageY = 0;
            hauteurY = n_rectangle.Height - decallageX; largeurX = n_rectangle.Width - decallageY;

            this.rectangle = n_rectangle;


            this._texture = n_textture;
            Ia_color = Color.AliceBlue;
            vie = 2;

            this.window_H = window_H;
            this.window_W = window_W;
            tombe = true;
            _speed = speed;
            normalspeed = speed;
            poid = 10;
            this.dir = 1;
            this.front_sc = front_sc;
            this.timer_lunch = launchTime;

            this.FrameLine = 1;
            this.FrameColumn = 1;
            this.Timer = 0;

            this.FrameLine2 = 1;
            this.FrameColumn2 = 1;
            this.Timer2 = 0;

            this.FrameLine3 = 1;
            this.FrameColumn3 = 1;
            this.Timer3 = 0;

            switch (id)
            {
                case 0://kamikaze

                    decallageX = 0; decallageY = 0;
                    hauteurY = n_rectangle.Height - decallageX; largeurX = n_rectangle.Width - decallageY;
                    longueur_attaque = 100;
                    break;

                case 1:// stalker
                    decallageX = 72; decallageY = 36;
                    hauteurY = rectangle.Height - decalageY - 10; largeurX = 37;
                    longueur_attaque = 100;
                    break;
                case 2:// ia AR
                    decallageX = 0; decallageY = 0;
                    hauteurY = n_rectangle.Height - decallageX; largeurX = n_rectangle.Width - decallageY;
                    longueur_attaque = 2;
                    break;
                default:// iaAA
                    decallageX = 0; decallageY = 0;
                    hauteurY = n_rectangle.Height - decallageX; largeurX = n_rectangle.Width - decallageY;
                    longueur_attaque = 100;
                    break;

            }
        }
        // le stalker
        public void Update(objet sprite, ref KeyboardState keyboard)
        {
            Update_rec_collision();
            if (vie>=0 && this.rectangle_C.X < 1.2f * window_W && this.rectangle_C.X > -0.2f * window_W)
            {

                if (tombe)
                    rectangle.Y += poid;

                if (rectangle_C.Center.X  - longueur_attaque/2 > sprite.rectangle_C.Center.X )
                {
                    dir = -1;
                    _speed = normalspeed;
                }
                else
                 if (rectangle_C.Center.X  + longueur_attaque/2< sprite.rectangle_C.Center.X )
                {
                    dir = 1;
                    _speed = normalspeed;
                }
                 else
                     _speed = 0;
               
                rectangle.X += dir * _speed;

            }
            if (keyboard.IsKeyDown(Keys.Right))
                rectangle.X -= front_sc;
            if (keyboard.IsKeyDown(Keys.Left))
                rectangle.X += front_sc;

            //  if (rectangle.Center.Y  < sprite.rectangle.Center.Y)
            //    rectangle.Y += 1;
            //else
            //  rectangle.Y -= 1;
            if (dir == 1)
            {
                this.Effects = SpriteEffects.None;
                
                if (vie >= 0)
                {
                    if (tombe == true)
                    {
                        FrameColumn = 1;
                        FrameLine = 5;
                    }
                    else if (this.attaque == true)
                    {
                        FrameLine = 4;
                        this.Timer++;
                        if (this.Timer == this.AnimationSpeed)
                        {
                            this.Timer = 0;
                            this.FrameColumn++;
                            if (FrameColumn > 6)
                            {
                                FrameColumn = 1;
                            }
                        }
                    }
                    else 
                    {
                        FrameLine = 2;
                        this.Timer++;
                        if (this.Timer == this.AnimationSpeed)
                        {
                            this.Timer = 0;
                            this.FrameColumn++;
                            if (FrameColumn > 4)
                            {
                                FrameColumn = 1;
                            }
                        }
                    }
                }

                else
                {
                    FrameLine = 3;
                    this.Timer++;
                    if (FrameColumn == 4)
                    {

                    }
                    else if (FrameColumn > 4)
                    {
                        FrameColumn = 1;
                    }
                    else if (this.Timer == this.AnimationSpeed)
                    {
                        this.Timer = 0;
                        this.FrameColumn++;

                    }
                }
            }
            else if (dir == -1)
            {
                this.Effects = SpriteEffects.FlipHorizontally;
                
                if (vie >= 0)
                {
                    if (tombe == true)
                    {
                        FrameColumn = 1;
                        FrameLine = 5;
                    }
                    else if (this.attaque == true)
                    {
                        FrameLine = 4;
                        this.Timer++;
                        if (this.Timer == this.AnimationSpeed)
                        {
                            this.Timer = 0;
                            this.FrameColumn++;
                            if (FrameColumn > 6)
                            {
                                FrameColumn = 1;
                            }
                        }
                    }
                    else
                    {
                        FrameLine = 2;
                        this.Timer++;
                        if (this.Timer == this.AnimationSpeed)
                        {
                            this.Timer = 0;
                            this.FrameColumn++;
                            if (FrameColumn > 4)
                            {
                                FrameColumn = 1;
                            }
                        }
                    }
                }

                else
                {
                    FrameLine = 3;
                    this.Timer++;
                    if (FrameColumn == 4)
                    {

                    }
                    else if (FrameColumn > 4)
                    {
                        FrameColumn = 1;
                    }
                    else if (this.Timer == this.AnimationSpeed)
                    {
                        this.Timer = 0;
                        this.FrameColumn++;

                    }
                }
            }
        }
        // le debile ki avnace et recule
        public void UpdateAR(ref KeyboardState keyboard)
        {
            if (vie != 0 && this.rectangle_C.Center.X < 1.2f * window_W && this.rectangle_C.X > -0.2f * window_W)
            {
                if (tombe)
                {
                    rectangle.Y += poid;

                }
                if(!attaque )
                rectangle.X += dir * _speed;
            }
            if (keyboard.IsKeyDown(Keys.Right))
                rectangle.X -= front_sc;
            if (keyboard.IsKeyDown(Keys.Left))
                rectangle.X += front_sc;
            
            if (dir == 1)
            {
                this.Effects2 = SpriteEffects.None;
                if (vie != 0)
                {
                    if (this.attaque == true)
                    {
                        FrameLine2 = 4;
                        this.Timer2++;
                        if (this.Timer2 == this.AnimationSpeed)
                        {
                            this.Timer2 = 0;
                            this.FrameColumn2++;
                            if (FrameColumn2 > 10)
                            {
                                FrameColumn2 = 1;
                            }
                        }
                    }
                    else
                    {
                        FrameLine2 = 2;
                        this.Timer2++;
                        if (this.Timer2 == this.AnimationSpeed)
                        {
                            this.Timer2 = 0;
                            this.FrameColumn2++;
                            if (FrameColumn2 > 4)
                            {
                                FrameColumn2 = 1;
                            }

                        }
                    }
                }
                else
                {
                    FrameLine2 = 3;
                    this.Timer2++;
                    if (FrameColumn2 == 4)
                    {

                    }
                    else if (FrameColumn2 > 4)
                    {
                        FrameColumn2 = 1;
                    }
                    else if (this.Timer == this.AnimationSpeed)
                    {
                        this.Timer2 = 0;
                        this.FrameColumn2++;

                    }
                }
            }

            else if (dir == -1)
            {
                this.Effects2 = SpriteEffects.FlipHorizontally;
                if (vie != 0)
                {
                    if (this.attaque == true)
                    {
                        FrameLine2 = 4;
                        this.Timer2++;
                        if (this.Timer2 == this.AnimationSpeed)
                        {
                            this.Timer2 = 0;
                            this.FrameColumn2++;
                            if (FrameColumn2 > 10)
                            {
                                FrameColumn2 = 1;
                            }
                        }
                    }
                    else
                    {
                        FrameLine2 = 2;
                        this.Timer2++;
                        if (this.Timer2 == this.AnimationSpeed)
                        {
                            this.Timer2 = 0;
                            this.FrameColumn2++;
                            if (FrameColumn2 > 4)
                            {
                                FrameColumn2 = 1;
                            }

                        }
                    }
                }
                else
                {
                    FrameLine2 = 3;
                    this.Timer2++;
                    if (FrameColumn2 == 4)
                    {

                    }
                    else if (FrameColumn2 > 4)
                    {
                        FrameColumn2 = 1;
                    }
                    else if (this.Timer == this.AnimationSpeed)
                    {
                        this.Timer2 = 0;
                        this.FrameColumn2++;

                    }
                }
            }
            Update_rec_collision();




        }
        // le bon kamikaze
        public void Update_Kamikaze(objet sprite, ref int gameTime)
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
                if (attaque == false)
                rectangle.X -= _speed;
                if (tombe)
                    rectangle.Y += poid;
            }
            if (keyboard.IsKeyDown(Keys.Right))
                rectangle.X -= front_sc;
            if (keyboard.IsKeyDown(Keys.Left))
                rectangle.X += front_sc;

            if (dir == -1)
            {
                this.Effects3 = SpriteEffects.None;
                if (tombe)
                {
                    FrameLine3 = 5;
                    FrameColumn3 = 1;
                }
                else
                {
                    FrameLine3 = 2;
                    this.Timer3++;
                    if (this.Timer3 == this.AnimationSpeed)
                    {
                        this.Timer3 = 0;
                        this.FrameColumn3++;
                        if (FrameColumn3 > 4)
                        {

                            FrameColumn3 = 1;
                        }
                    }
                }
            }

            else if (dir == 1)
            {
                this.Effects3 = SpriteEffects.FlipHorizontally;
                if (tombe)
                {
                    FrameLine3 = 5;
                    FrameColumn3 = 1;
                }
                else
                {
                    FrameLine3 = 2;
                    this.Timer3++;
                    if (this.Timer3 == this.AnimationSpeed)
                    {
                        this.Timer3 = 0;
                        this.FrameColumn3++;
                        if (FrameColumn3 > 4)
                        {
                            FrameColumn3 = 1;
                        }
                    }
                }
            }

        }


        public override void Draw(SpriteBatch spritback)
        {
            spritback.Draw(_texture, rectangle, Ia_color);
        }

        public void Draw_S(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, rectangle, new Rectangle((this.FrameColumn - 1) * 150, (this.FrameLine - 1) * 72, 150, 72), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
        }

        public void Draw_AR(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, rectangle, new Rectangle((this.FrameColumn2 - 1) * 130, (this.FrameLine2 - 1) * 85, 130, 85), Color.White, 0f, new Vector2(0, 0), this.Effects2, 0f);
        }

        public void Draw_AA(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, rectangle, new Rectangle((this.FrameColumn3 - 1) * 90, (this.FrameLine3 - 1) * 63, 90, 63), Color.White, 0f, new Vector2(0, 0), this.Effects3, 0f);
        }
    }
}
