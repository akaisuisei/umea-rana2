using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Umea_rana
{
    public class IA_manager_AR : IA_Manager_max
    {
        public IA_manager_AR(Texture2D n_textture, Rectangle n_rectangle, int front_sc, int speed, int window_H, int window_W)
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
        public IA_manager_AR(Rectangle n_rectangle,  int window_H, int window_W)
        {
            ia_manage = new List<vaisseau_IA>();
            this._rectangle = n_rectangle;
            this.speed = speed;
            this.window_H = window_H;
            this.window_W = window_W;
            poid = 10;
            AnimationSpeed = 10;
        }
        public void LoadContent(Texture2D texture)
        {
            this._texture = texture;
        }
        private void mov(ref int i)
        {
            if (ia_manage[i].vie >= 0)
            {

                if (this.ia_manage[i].attaque == true)
                {
                    ia_manage[i].FrameLine = 4;
                    this.ia_manage[i].Timer++;
                    if (this.ia_manage[i].Timer == this.AnimationSpeed)
                    {
                        this.ia_manage[i].Timer = 0;
                        this.ia_manage[i].FrameColunm++;
                        if (ia_manage[i].FrameColunm > 10)
                        {
                            ia_manage[i].FrameColunm = 1;
                        }
                    }
                }
                else
                {
                    ia_manage[i].FrameLine = 2;
                    this.ia_manage[i].Timer++;
                    if (this.ia_manage[i].Timer == this.AnimationSpeed)
                    {
                        this.ia_manage[i].Timer = 0;
                        this.ia_manage[i].FrameColunm++;
                        if (ia_manage[i].FrameColunm > 4)
                        {
                            ia_manage[i].FrameColunm = 1;
                        }
                    }
                }
            }
            else
            {
                ia_manage[i].FrameLine = 3;
                this.ia_manage[i].Timer++;
                if (ia_manage[i].FrameColunm == 4)
                {

                }
                else if (ia_manage[i].FrameColunm > 4)
                {
                    ia_manage[i].FrameColunm = 1;
                }
                else if (this.ia_manage[i].Timer == this.AnimationSpeed)
                {
                    this.ia_manage[i].Timer = 0;
                    this.ia_manage[i].FrameColunm++;
                }
            }
        }
        public void Update(ref KeyboardState keyboard)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                if (ia_manage[i].vie >= 0 && this.ia_manage[i].rectangle_C.Center.X < 1.2f * window_W && this.ia_manage[i].rectangle_C.Center.X > -0.2f * window_W)
                {
                    if (ia_manage[i].tombe)
                    {
                        ia_manage[i].rectangle.Y += poid;

                    }
                    if (!ia_manage[i].attaque)
                        ia_manage[i].rectangle.X += ia_manage[i].dir * ia_manage[i].Speed;
                }
                if (keyboard.IsKeyDown(Keys.Right))
                    ia_manage[i].rectangle.X -= front_sc;
                if (keyboard.IsKeyDown(Keys.Left))
                    ia_manage[i].rectangle.X += front_sc;

                if (ia_manage[i].dir == 1)
                {
                    ia_manage[i].decalageX = 54;
                    this.ia_manage[i].Effects = SpriteEffects.None;
                    mov(ref i);
                }

                else
                {
                    ia_manage[i].decalageX = 23;
                    this.ia_manage[i].Effects = SpriteEffects.FlipHorizontally;
                    mov(ref i);
                }
                ia_manage[i].Update_rec_collision();

            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < ia_manage.Count; ++i)
            {
                spritebatch.Draw(_texture, ia_manage[i].rectangle, new Rectangle((this.ia_manage[i].FrameColunm - 1) * 130, (this.ia_manage[i].FrameLine - 1) * 85, 130, 85), Color.White, 0f, new Vector2(0, 0), this.ia_manage[i].Effects, 0f);
            }
        }
        #region ADD
        public void Add(float X, float Y)
        {
            ia_manage.Add(new Stalker( new Rectangle((int)(X * window_W) + 1, (int)(Y * window_H) - 1, _rectangle.Width, _rectangle.Height),  speed, window_H, window_W, 0, 2));
        }

            public void Add(IA_AR hello, int i)
            {
                ia_manage.Add(new Stalker(new Rectangle((int)(hello.X * window_W) + 1, (int)(hello.Y * window_H) - 1, _rectangle.Width, _rectangle.Height),hello ,i));
            }
            public void Add(IA_AR hello)
            {
                ia_manage.Add(new Stalker(new Rectangle((int)(hello.X * window_W) + 1, (int)(hello.Y * window_H) - 1, _rectangle.Width, _rectangle.Height), hello ));
            }
        #endregion
    }
}
