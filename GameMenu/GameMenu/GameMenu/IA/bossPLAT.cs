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
        public string type;
        public int speed;
        public float X { get; set; }
        public float Y { get; set; }

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
        public int FrameLine;
        public int FrameColumn;
        public SpriteEffects effects;
        public int Timer;
        public int AnimationSpeed = 10;
        int colunm, line, dir;
  
        public int degat { get; private set; }
        float lastvie;
        string type="";
        Texture2D texture, ptforttexture, ptfaible_texture;
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

            this.FrameLine = 1;
            this.FrameColumn = 1;
            this.Timer = 0;
            dir = 1;

        }
        public void parrametrage(levelProfile levelprofile)
        {
            this.degat = levelprofile.bossPlatforme.degat;
            this.vie = levelprofile.bossPlatforme.vie;
            this.type = levelprofile.bossPlatforme.type;
            this.speed = levelprofile.bossPlatforme.speed;
            front_sc = levelprofile.fc_speed;
            // truc
            rectangle.X = (int)(levelprofile.bossPlatforme.X * fond.Width);
            rectangle.Y = (int)(levelprofile.bossPlatforme.Y * fond.Height);
            dir = 1;
        }
        public void parrame(bossP bossPlatforme, ContentManager content, int fc_speed)
        {
            this.degat = bossPlatforme.degat;
            this.vie = bossPlatforme.vie;
            this.type = bossPlatforme.type;
            this.speed = bossPlatforme.speed;
            front_sc = fc_speed;
            rectangle.X = (int)(bossPlatforme.X * fond.Width);
            rectangle.Y = (int)(bossPlatforme.Y * fond.Height);
            // truc
            this.texture = content.Load<Texture2D>("Boss/" + bossPlatforme.type);
            switch (type)
            {
                case "Light":
                        line = 100;
                    colunm = 100;
                    break;
                case "Cascade":
                       line = 150;
                    colunm = 150;
                    break;
                default :
                        line = 100;
                    colunm = 100;
                    break;
            }
            dir=1;
        }
        public void loadContent(ContentManager content, Rectangle fond)
        {
            this.fond = fond;
            this.rectangle = new Rectangle(1000, 0, 100, 100);
            this.atk = new Rectangle(1000, 200, 100, 100);
            this.rectangle_C = rect;
            this.degat = 1;
            this.vie = 15;
            this.lastvie = vie;
            this.speed = 7;
            ptfaible.Add(rect);
            ptfort.Add(rect);
            color = Color.Yellow;
            this.effects = SpriteEffects.FlipHorizontally;
            switch (type)
            {
                case "Light":
                    ptfort.Add(new Rectangle(0, 200, 50, 50));
                    ptfort.Add(new Rectangle(800, (int)(fond.Height * 0.8f), 50, 50));
                    ptfort.Add(new Rectangle(1000, (int)(fond.Height * 0.6f), 50, 50));
                    ptfort.Add(new Rectangle(1100, (int)(fond.Height * 0.7f), 50, 50));
                    ptfort.Add(new Rectangle(1200, (int)(fond.Height * 0.6f), 50, 50));
                    ptfort.Add(new Rectangle(1300, (int)(fond.Height * 0.8f), 50, 50));
                    ptfort.Add(new Rectangle(800, (int)(fond.Height * 0.6f), 50, 50));
                    ptfort.Add(new Rectangle(1000, (int)(fond.Height * 0.7f), 50, 50));
                    ptfort.Add(new Rectangle(900, (int)(fond.Height * 0.8f), 50, 50));
                    ptfaible.Add(rect);
         
           
                    line = 100;
                    colunm = 100;
                    break;
                case "Cascade":
                    ptfaible.Add(rect);

                    line = 150;
                    colunm = 150;
                    break;
                case "":
                    type = "null";
                    this.rectangle = new Rectangle(0, 0, 0, 0);
                    break;
                case "Kinukuman":
                    ptfort.Add(rectangle_C);
                    ptfaible.Add(this.rectangle_C);
                    break;
                case "Bou":
                    ptfaible.Add(this.rectangle_C );
                    break;
                case "KickAss":
                    ptfaible.Add(this.rectangle_C);
                    break;
                default:
                    break;
            }
            if (type != "")
            {
                this.texture = content.Load<Texture2D>("Boss/" + type);
                ptforttexture = content.Load<Texture2D>("pointfort/" + type);
                ptfaible_texture = content.Load<Texture2D>("pointfaible/" + type);
            }
            dir = 1;
        }
        public void loadContent(ContentManager content, Texture2D texture, int fc, Rectangle fond, string type)
        {
            this.fond = fond;
            this.front_sc = fc;
            this.rectangle = new Rectangle(1000, 0, 100, 100);
            this.atk = new Rectangle(1000, 200, 100, 100);
            this.rectangle_C = rect;
            this.degat = 1;
            this.vie = 15;
            this.lastvie = vie;
            this.type = type;
            this.speed = 7;
            ptfaible.Add(rect);
            ptfort.Add(rect);
            color = Color.Yellow;
            this.effects = SpriteEffects.FlipHorizontally;





            switch (type)
            {
                case "Light":
                    ptfort.Add(new Rectangle(0, 200, 50, 50));
                    ptfort.Add(new Rectangle(800, (int)(fond.Height * 0.8f), 50, 50));
                    ptfort.Add(new Rectangle(1000, (int)(fond.Height * 0.6f), 50, 50));
                    ptfort.Add(new Rectangle(1100, (int)(fond.Height * 0.7f), 50, 50));
                    ptfort.Add(new Rectangle(1200, (int)(fond.Height * 0.6f), 50, 50));
                    ptfort.Add(new Rectangle(1300, (int)(fond.Height * 0.8f), 50, 50));
                    ptfort.Add(new Rectangle(800, (int)(fond.Height * 0.6f), 50, 50));
                    ptfort.Add(new Rectangle(1000, (int)(fond.Height * 0.7f), 50, 50));
                    ptfort.Add(new Rectangle(900, (int)(fond.Height * 0.8f), 50, 50));
                    ptfaible.Add(rect);
                    line = 100;
                    colunm = 100;
                    break;
                case "Cascade":
                    ptfaible.Add(rect);
                    line = 150;
                    colunm = 150;
                    break;
                default:
                    break;
            }
            this.texture = content.Load<Texture2D>("Boss/"+type);
            ptforttexture = content.Load<Texture2D>("pointfort/"+type );
            ptfaible_texture = content.Load<Texture2D>("pointfaible/"+type );
        }
        public void UpdateEDIT(ref KeyboardState keyboard)
        {


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
            Update_rec_collision();
        }
        public void Update(ref KeyboardState keyboard)
        {
            this.Animated();
            // if ( this.rectangle_C.Center.X < 1.2f * fond.Width  && this.rectangle_C.Center.X > -0.2f * fond.Width )
            if (vie >= 0)
            {

                switch (type)
                {
                    case "Light":
                        if (rectangle.Center.X > fond.Width / 2)
                        {
                            dir = -1;
                        }
                        else
                            dir = 1;
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
                                if (rect.Y > this.rectangle_C.Y)
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
                    case "Cascade":
                        if (rectangle.Center.X > fond.Width / 2)
                        {
                            dir = -1;
                        }
                        else
                            dir = 1;
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
                            ptfort.Add(new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60));
                        }

                        for (int i = 0; i < ptfort.Count; ++i)
                        {
                            rect = ptfort[i];
                            rect.X -= speed;
                            ptfort[i] = rect;
                            if (!ptfort[i].Intersects(fond))
                                ptfort.RemoveAt(i);
                        }
                        break;
                    case "Bou":
                        if (rectangle.Center.X > fond.Width / 2)
                        {
                            dir = -1;
                        }
                        else
                            dir = 1;
                        if (timeatk < 0)
                        {
                            ptfort.Add(new Rectangle(fond.Width / 2, 0, 100, 50));
                            timeatk = 60;
                        }
                    
                        for (int i = 0; i < ptfort.Count; ++i)
                        {
                            rect = ptfort[i];
                            rect.Y  -= speed;
                            ptfort[i] = rect;
                            if (!ptfort[i].Intersects(fond))
                                ptfort.RemoveAt(i);
                        }
                        ptfaible[0] = this.rectangle_C;
                        break;
                    case "KickAss":
                        if (rectangle.Center.X > fond.Width / 2)
                        {
                            dir = -1;
                        }
                        else
                            dir = 1;
                        if (timeatk < 0)
                        {
                            ptfort.Add(new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60));
                            timeatk = 60;
                        }
                        for (int i = 0; i < ptfort.Count; ++i)
                        {
                            rect = ptfort[i];
                            rect.X -= speed;
                            ptfort[i] = rect;
                            if (!ptfort[i].Intersects(fond))
                                ptfort.RemoveAt(i);
                        }
                        ptfaible[0] = this.rectangle_C;

                        break; 
                    case "Kinukuman":
                        if (timeatk < 0)
                        {
                            timeatk = 60;
                            timerrun = 50;
                        }
                        if (timerrun > 0)
                        {
                               rectangle.X +=dir * speed;
                        }
                        else
                            if (rectangle.Center.X > fond.Width / 2)
                            {
                                dir = -1;
                            }
                            else
                                dir = 1;
                     
                        ptfort[0] = rectangle_C;
                        ptfaible[0] = rectangle_C;
                        break;
                    default:
                        break;
                }



                        timeatk--;
                lastvie = vie;
                timerrun--;
            }
            else
            {
                color = Color.Pink;
                ptfaible.Clear();
                ptfort.Clear();
            }
            if (rectangle.Center.X > fond.Width / 2)
            {
                dir = -1;
            }
            else
                dir = 1;

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
            if (type == "Light")
            {
                this.Timer++;
                if (this.Timer == this.AnimationSpeed)
                {
                    this.Timer = 0;
                    this.FrameColumn++;
                    if (FrameColumn > 13)
                    {
                        FrameColumn = 1;
                    }
                }
            }

            else if (type == "Cascade")
            {
                this.Timer++;
                if (this.Timer == this.AnimationSpeed)
                {
                    this.Timer = 0;
                    this.FrameColumn++;
                    if (FrameColumn > 8)
                    {
                        FrameColumn = 1;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sp)
        {

            foreach (Rectangle r in ptfaible)
                sp.Draw(ptfaible_texture, r, Color.Transparent);
            foreach (Rectangle r in ptfort)
                sp.Draw(ptforttexture, r, Color.White);

            sp.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * colunm, (this.FrameLine - 1) * line, colunm, line), Color.White, 0f, new Vector2(0, 0), this.effects, 0f);
        }
        public void DrawEDIT(SpriteBatch sp)
        {
            if (texture != null)
                sp.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * colunm, (this.FrameLine - 1) * line, colunm, line), Color.White, 0f, new Vector2(0, 0), this.effects, 0f);
        }

    }
}
