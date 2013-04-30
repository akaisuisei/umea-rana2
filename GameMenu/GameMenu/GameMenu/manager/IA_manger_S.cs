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
   public class IA_manager_S : IA_Manager_max
    {


        public IA_manager_S(Texture2D n_textture, Rectangle n_rectangle, int front_sc, int speed, int window_H, int window_W)
        {
            ia_manage = new List<vaisseau_IA>();
            this._texture = n_textture;
            this._rectangle = n_rectangle;
            this.speed = speed;
            this.front_sc = front_sc;
            this.window_H = window_H;
            this.window_W = window_W;
            poid = 10;
            AnimationSpeed = 10;
        }

    
        private void mov(ref int i )
        {
            if (ia_manage[i].vie >= 0)
            {
                if (ia_manage[i].tombe)
                {
                    ia_manage[i].FrameColunm = 1;
                    ia_manage[i].FrameLine = 5;
                }
                else if (this.ia_manage[i].attaque)
                {
                    ia_manage[i].FrameLine = 4;
                    this.ia_manage[i].Timer++;
                    if (this.ia_manage[i].Timer == this.AnimationSpeed)
                    {
                        this.ia_manage[i].Timer = 0;
                        ia_manage[i].FrameColunm = ia_manage[i].FrameColunm % 6 + 1;
                    }
                }
                else if (ia_manage[i].Speed  == 0)
                {
                    ia_manage[i].FrameLine = 1;
                    this.ia_manage[i].Timer++;
                    if (this.ia_manage[i].Timer == this.AnimationSpeed)
                    {
                        this.ia_manage[i].Timer = 0;
                        ia_manage[i].FrameColunm = ia_manage[i].FrameColunm % 4 + 1;
                    }
                }
                else
                {
                    ia_manage[i].FrameLine = 2;
                    this.ia_manage[i].Timer++;
                    if (this.ia_manage[i].Timer == this.AnimationSpeed)
                    {
                        this.ia_manage[i].Timer = 0;
                        ia_manage[i].FrameColunm = ia_manage[i].FrameColunm % 4 + 1;
                    }
                }
            }

            else
            {
                if (ia_manage[i].FrameLine != 3)
                {
                    ia_manage[i].FrameLine = 3;
                    ia_manage[i].FrameColunm = 1;
                }
                
              
                else if (this.ia_manage[i].Timer == this.AnimationSpeed &&    ia_manage[i].FrameColunm != 4)
                {
                    this.ia_manage[i].Timer = 0;
                    this.ia_manage[i].FrameColunm++;

                } 
                this.ia_manage[i].Timer++;
            }
        }
        public void Update(objet sprite, ref KeyboardState keyboard)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
               ia_manage[i]. Update_rec_collision();
               if (ia_manage[i].vie >= 0 && !ia_manage[i].attaque && this.ia_manage[i].rectangle_C.X < 1.2f * window_W && this.ia_manage[i].rectangle_C.X > -0.2f * window_W)
                {

                    if (ia_manage[i].tombe)
                        ia_manage[i].rectangle.Y += poid;

                   if(ia_manage[i].rectangle_C.Center.X - ia_manage[i].longueur_attaque < sprite.rectangle_C.Right  &&ia_manage[i].rectangle_C.Center.X + ia_manage[i].longueur_attaque > sprite.rectangle_C.Left )
                        ia_manage[i].Speed = 0;
                   else 
                    if (ia_manage[i].rectangle_C.Center.X - ia_manage[i].longueur_attaque > sprite.rectangle_C.Center.X)
                    {
                        ia_manage[i].dir = -1;
                        ia_manage[i].Speed = ia_manage[i].normalspeed;
                    }
                    else
                        
                        {
                            ia_manage[i].dir = 1;
                            ia_manage[i].Speed  = ia_manage[i].normalspeed;
                        }
  

                    ia_manage[i].rectangle.X += ia_manage[i].dir * ia_manage[i].Speed ;

                }
                if (keyboard.IsKeyDown(Keys.Right))
                    ia_manage[i].rectangle.X -= front_sc;
                if (keyboard.IsKeyDown(Keys.Left))
                    ia_manage[i].rectangle.X += front_sc;

                //  if (rectangle.Center.Y  < sprite.rectangle.Center.Y)
                //    rectangle.Y += 1;
                //else
                //  rectangle.Y -= 1;
                if (ia_manage[i].dir == 1)
                {
                    ia_manage[i].decalageX  = 43;
                    this.ia_manage[i].Effects = SpriteEffects.None;
                    mov(ref i);
                    
                }
                else 
                {
                    ia_manage[i].decalageX  = 25;
                   ia_manage[i] .Effects = SpriteEffects.FlipHorizontally;
                    mov (ref i );
                }
                
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                spritebatch.Draw(_texture,ia_manage[i]. rectangle, new Rectangle((this.ia_manage[i].FrameColunm - 1) * 150, (this.ia_manage[i].FrameLine - 1) * 72, 150, 72), Color.White, 0f, new Vector2(0, 0), this.ia_manage[i].Effects, 0f);   
            }
        }
        public virtual void Add(float X, float Y)
        {
            ia_manage.Add(new Stalker( new Rectangle((int)(X * window_W) + 1, (int)(Y * window_H) - 1, _rectangle.Width, _rectangle.Height), speed, window_H, window_W, 0, 1));
        }

        public void Add(IA_S hello, int i)
        {
            ia_manage.Add(new Stalker(new Rectangle((int)(hello.X * window_W) + 1, (int)(hello.Y * window_H) - 1, _rectangle.Width, _rectangle.Height), speed, window_H, window_W, 0, 3));
        }
    }

  


 
}
