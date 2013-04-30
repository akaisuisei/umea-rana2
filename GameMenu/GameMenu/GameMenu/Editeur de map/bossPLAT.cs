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
    public struct bossP
    {

        public int degat;
        public int vie;
        public char type;
        public int speed;
    }
    public class bossPLAT : objet
    {
        private class pos
        {
            public int lstart = 1, cstart = 1;
            public int Tlstart = 1, Tcstart = 1, Tcend = 1;
            public pos(int ls, int cs, int tl, int tcs, int tce)
            {
                lstart = ls;
                cstart = cs;
                Tlstart = tl;
                Tcstart = tcs;
                Tcend = tce;
            }
        }

        public int degat { get; private set; }
        float  lastvie;
        char type;
        Texture2D texture;
        public List<Rectangle> ptfaible { get; private set; }
        public List<Rectangle> ptfort { get; private set; }
        private bool run;
        private int timerrun, speed, front_sc, timeatk;

        Color color;
        private Rectangle rect, atk;
        Rectangle fond;
        public bossPLAT()
        {
            degat = 1;
            vie = 1;
            ptfaible = new List<Rectangle>();
            ptfort = new List<Rectangle>();
            poid = 10;
        }
        public void parrametrage(bossP bp)
        {
            this.degat = bp.degat;
            this.vie = bp.vie;
            this.type = bp.type;
            this.speed = bp.speed;
        }
        public void loadContent(ContentManager content, Texture2D texture, int fc, Rectangle fond)
        {
            this.fond = fond;
            this.texture = texture;
            this.front_sc = fc;
            this.rectangle = new Rectangle(1000, 0, 100, 100);
            this.atk = new Rectangle(1000, 200, 100, 100);
            this.rectangle_C = rect;
            this.degat = 1;
            this.vie = 15;
            this.lastvie = vie;
            this.type = '2';
            this.speed = 7;
            ptfaible.Add(rect);
            ptfort.Add(rect);
            color = Color.Yellow;
            switch (type)
            {
                case '1':
                    ptfort.Add(new Rectangle(0, 200, 50, 50));
                    ptfort.Add(new Rectangle(800, 400, 50, 50));
                    ptfort.Add(new Rectangle(500, 400, 50, 50));
                    ptfort.Add(new Rectangle(400, 200, 50, 50));
                    ptfaible.Add(rect);
                    break;
                case '2':
                    ptfaible.Add(rect);

                    break;
                default:
                    break;
            }
        }
        public void Update(ref KeyboardState keyboard)
        {
           // if ( this.rectangle_C.Center.X < 1.2f * fond.Width  && this.rectangle_C.Center.X > -0.2f * fond.Width )
                if (vie >= 0)
                {

                    switch (type)
                    {
                        case '1':
                            if (vie <= 10)
                            {
                                for (int i = 0; i < ptfort.Count; ++i)
                                {
                                    rect = ptfort[i];
                                    
                                    //update poit fort
                                    if (rect.X > this.rectangle_C.X)
                                        rect.X -= speed;
                                    else
                                        rect.X += speed;
                                    if (rect.Y > this.rectangle_C.Y)
                                        rect.Y -= speed;
                                    else
                                        rect.Y += speed;
                                    ptfort[i] = rect;
                                }
                                for (int i = 0; i < ptfaible.Count; ++i)
                                {
                                    rect = ptfaible[i];
                                    // update pt faible
                                    if (rect.X > this.rectangle_C.X)
                                        rect.X -= speed;
                                    else
                                        rect.X += speed;
                                    if (rect.Y  > this.rectangle_C.Y)
                                        rect.Y -= speed;
                                    else
                                        rect.Y += speed;
                                    ptfaible[i] = rect;
                                }
                               
                            }
                            else
                                for (int i = 0; i < ptfaible.Count; ++i)
                                    ptfaible[i] = rectangle;
                            
                            break;
                        case '2':
                            if (lastvie != vie)
                            {
                                timerrun = 100;
                            }
                            if (timerrun >= 0)
                                rectangle.X += speed;
                            for (int i = 0; i < ptfaible.Count; ++i)
                                ptfaible[i] = rectangle;
                            if (timeatk <= 0)
                            {
                                timeatk = 60;
                                ptfort.Add(new Rectangle (rectangle_C.X,rectangle_C.Y ,15,15));
                            }
                            timeatk--;
                            for (int i = 0; i < ptfort.Count; ++i)
                            {
                                rect = ptfort[i];
                                rect.X -= speed ;
                                ptfort[i] = rect;
                                if (!ptfort[i].Intersects(fond))
                                    ptfort.RemoveAt(i);
                            }
                            break;
                        default:
                            break;
                    }




                    lastvie = vie;
                    timerrun--;
                }
                else
                {
                    color = Color.Pink;
                    ptfaible.Clear();
                    ptfort.Clear();
                }


            if (keyboard.IsKeyDown(Keys.Right))
            {
                this.rectangle.X -= front_sc;
                for (int i = 0; i < ptfort.Count; ++i)
                {
                    rect = ptfort[i];
                    rect.X -= front_sc;
                    ptfort[i] = rect;
                }
                for (int i = 0; i < ptfaible.Count; ++i)
                {
                    rect = ptfaible[i];
                    rect.X -= front_sc;
                    ptfaible[i] = rect;
                }
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                this.rectangle.X += front_sc;
                for (int i = 0; i < ptfort.Count; ++i)
                {
                    rect = ptfort[i];
                    rect.X += front_sc;
                    ptfort[i] = rect;
                }
                for (int i = 0; i < ptfaible.Count; ++i)
                {
                    rect = ptfaible[i];
                    rect.X += front_sc;
                    ptfaible[i] = rect;
                }
            }
            if (tombe)
                rectangle.Y += poid;
            Update_rec_collision();
        }
        public void Animated()
        {
        }
        public void Draw(SpriteBatch sp)
        {
            sp.Draw(texture, rectangle, color);
            foreach (Rectangle r in ptfaible)
                sp.Draw(texture, r, Color.Gray);
            foreach (Rectangle r in ptfort)
                sp.Draw(texture, r, Color.Indigo);


        }

    }
}
