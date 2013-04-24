using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Umea_rana
{
    public class cercle
    {
        public int x { get; set; }
        public int Y { get; set; }
        public int R { get; set; }

        public bool is_in_bound(Rectangle rec)
        {
            return (Math.Pow(rec.Right - x, 2) + Math.Pow(rec.Top - Y, 2) <= R ||
                Math.Pow(rec.Right - x, 2) + Math.Pow(rec.Bottom - Y, 2) <= R ||
                Math.Pow(rec.Left - x, 2) + Math.Pow(rec.Top - Y, 2) <= R ||
                Math.Pow(rec.Left - x, 2) + Math.Pow(rec.Bottom - Y, 2) <= R);
        }
    }

    public class ovnis
    {
        public int FrameLine { get; set; }
        public int maxframecolumn { get; set; }
        public int FrameColumn { get; set; }
        public ovnis()
        {
            circle = new cercle();
            FrameColumn = 1;
            FrameLine = 1;
            timer = 10;
        }
        public char type;
        public int damage, vie, miss, power, bomb, launch;
        public int speedX, speedY;
        public Rectangle rectangle;
        public cercle circle;
        public int timer { get; set; }
        public int timer_dead = 60;
    }

    public class Ovni
    {
        int line, colunm;
        public List<ovnis> ovni { get; set; }
        int R1, R2, R3, R4;
        int WindoW, WindowH, fc;
        int width1, width2, width3, width4;
        int height1, height2, height3, height4;
        Texture2D texture;
        public Ovni(int width, int height)
        {
            WindoW = width; WindowH = height;
            width1 = (int)(WindoW * 0.05);
            width2 = (int)(WindoW * 0.05);
            width3 = (int)(WindoW * 0.05);
            width4 = (int)(WindoW * 0.05);
            height1 = (int)(WindowH * 0.07);
            height2 = (int)(WindowH * 0.07);
            height3 = (int)(WindowH * 0.07);
            height4 = (int)(WindowH * 0.07);
            R1 = 30;
            R2 = 30;
            R3 = 30;
            R4 = 40;
            ovni = new List<ovnis>();
            line = 64;
            colunm = 64;

        }
        public void param(int fc)
        {
            this.fc = fc;
        }
        public void Load(Texture2D texture)
        {
            this.texture = texture;
        }
        public void Update(ref int time)
        {
            for (int i = 0; i < ovni.Count; ++i)
            {
                if (ovni[i].timer_dead == 60)
                {
                    if (ovni[i].launch < time)
                    {
                        this.ovni[i].rectangle.X += ovni[i].speedX;
                        this.ovni[i].rectangle.Y += 1; //ovni[i].speedY;
                        ovni[i].circle.x = ovni[i].rectangle.Center.X;
                        ovni[i].circle.Y = ovni[i].rectangle.Center.Y;
                        if (ovni[i].type == 'v' || ovni[i].type == 'm' || ovni[i].type == 'p' || ovni[i].type == 'b')
                        {
                            if (ovni[i].rectangle.Left <= 0)
                            {
                                this.ovni[i].speedX *= -1;
                                this.ovni[i].rectangle.X = 1;
                            }
                            else if (ovni[i].rectangle.Right >= WindoW)
                            {
                                this.ovni[i].speedX *= -1;
                                this.ovni[i].rectangle.X = WindoW - this.ovni[i].rectangle.Width - 5;
                            }
                            if (ovni[i].rectangle.Top < 0)
                            {
                                this.ovni[i].speedY *= -1;
                                this.ovni[i].rectangle.Y = 1;
                            }
                            else if (ovni[i].rectangle.Bottom > WindowH)
                            {
                                this.ovni[i].speedY *= -1;
                                this.ovni[i].rectangle.Y = WindowH - this.ovni[i].rectangle.Height - 5;
                            }

                        }
                    }
                }
                else
                {
                    ovni[i].FrameLine = 10;
                    ovni[i].timer_dead--;
                    if (ovni[i].timer_dead == 0)
                        ovni.RemoveAt(i);

                }
                if (ovni[i].timer == 0)
                {
                    if (ovni[i].FrameColumn == ovni[i].maxframecolumn)
                        ovni[i].FrameColumn = 1;
                        ovni[i].timer = 10;
                    ovni[i].FrameColumn++;
                }
                ovni[i].timer--;


            }

        }

        public void Update(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.Down))
                for (int i = 0; i < this.ovni.Count; ++i)
                {
                    ovni[i].rectangle.Y -= fc;
                    ovni[i].circle.x = ovni[i].rectangle.Center.X;
                    ovni[i].circle.Y = ovni[i].rectangle.Center.Y;
                }
            else if (key.IsKeyDown(Keys.Up))
                for (int i = 0; i < this.ovni.Count; ++i)
                {
                    ovni[i].rectangle.Y += fc;
                    ovni[i].circle.x = ovni[i].rectangle.Center.X;
                    ovni[i].circle.Y = ovni[i].rectangle.Center.Y;
                }

        }
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (ovnis v in ovni)
                spritebatch.Draw(texture, v.rectangle, new Rectangle((v.FrameColumn - 1) * colunm, (v.FrameLine - 1) * line, colunm, line), Color.White);
        }
        /// <summary>
        /// add pour SEU
        /// </summary>
        /// <param name="type">v:vie,m:missile,p:power,b:bomb,a:asteroide,c:comet,default:sun</param>
        /// <param name="time"></param>
        /// <param name="speed"></param>
        /// <param name="angle"></param>
        /// <param name="X"></param>
        public void Add(Bonus bonus)
        {
            Random rnd = new Random();
            ovnis ov = new ovnis();
            int x = (int)(bonus.X * WindoW);
            int speedx = rnd.Next(1, 11), speedy = rnd.Next(1, 11);
            int speedx2 = (int)(Math.Cos((bonus.angle * Math.PI) / 180) * bonus.speed),
                speedy2 = (int)(Math.Sin(bonus.angle * Math.PI / 180) * bonus.speed);
            if (speedx2 == 0)
            {
                speedx2 = fc;
            }
            switch (bonus.type)
            {
                case 'v':
                    ov.vie = 1;
                    ov.rectangle = new Rectangle(x, -height1 - 2, width1, height1);
                    ov.circle.R = R1;
                    ov.speedX = speedx;
                    ov.speedY = speedy;
                    ov.FrameLine = 1;//hharf
                    break;
                case 'm':
                    ov.miss = 1;
                    ov.rectangle = new Rectangle(x, -height1 - 2, width1, height1);
                    ov.circle.R = R1;
                    ov.speedX = speedx;
                    ov.speedY = speedy;
                    ov.FrameLine = 1;//harff
                    break;
                case 'p':
                    ov.power = 1;
                    ov.rectangle = new Rectangle(x, -height1 - 2, width1, height1);
                    ov.circle.R = R1;
                    ov.speedX = speedx;
                    ov.speedY = speedy;
                    ov.FrameLine = 1;// harf
                    break;
                case 'b':
                    ov.bomb = 1;
                    ov.rectangle = new Rectangle(x, -height1 - 2, width1, height1);
                    ov.circle.R = R1;
                    ov.speedX = speedx;
                    ov.speedY = speedy;
                    ov.FrameLine = 1; // harff
                    break;
                case 'a':
                    ov.damage = 30;
                    ov.rectangle = new Rectangle(x, -height2 - 2, width2, height2);
                    ov.circle.R = R2;
                    ov.speedX = speedx2;
                    ov.speedY = speedy2;
                    ov.FrameLine = rnd.Next(1, 6);
                    switch (ov.FrameLine)
                    {
                        case 1:
                            ov.maxframecolumn = 25;
                            break;
                        case 2:
                            ov.maxframecolumn = 19;
                            break;
                        default:
                            ov.maxframecolumn = 20;
                            break;
                    }
                    break;
                case 'c':
                    ov.damage = 60;
                    ov.rectangle = new Rectangle(x, -height3 - 2, width3, height3);
                    ov.circle.R = R3;
                    ov.speedX = speedx2;
                    ov.speedY = speedy2;
                    ov.FrameLine = rnd.Next(8, 10);
                    ov.maxframecolumn = 30;
                    break;
                default:
                    ov.damage = 100;
                    ov.circle.R = R4;
                    ov.rectangle = new Rectangle(x, -height3 - 2, width4, height4);
                    ov.speedX = speedx2;
                    ov.speedY = speedy2;
                    ov.FrameLine = rnd.Next(6, 8);
                    ov.maxframecolumn = 30;
                    break;
            }
            ov.launch = bonus.launch;
            ov.type = bonus.type;
            ovni.Add(ov);
        }
        /// <summary>
        /// add for map's editor
        /// </summary>
        /// <param name="type">v:vie,m:missile,p:power,b:bomb,a:asteroide,c:comet,default:sun</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="spawn"></param>
        public void Add(char type, float X, float Y, int spawn)
        {
            int x = (int)(X * WindoW), y = (int)(Y * WindowH);
            ovnis ov = new ovnis();

            switch (type)
            {
                case 'v':
                    ov.rectangle = new Rectangle(x, y, width1, height1);
                    ov.circle.R = R1;
                    ov.FrameLine = 1;
                    break;
                case 'm':
                    ov.rectangle = new Rectangle(x, y, width1, height1);
                    ov.circle.R = R1;
                    ov.FrameLine = 1;
                    break;
                case 'p':
                    ov.rectangle = new Rectangle(x, y, width1, height1);
                    ov.circle.R = R1;
                    ov.FrameLine = 1;
                    break;
                case 'b':
                    ov.rectangle = new Rectangle(x, y, width1, height1);
                    ov.circle.R = R1;
                    ov.FrameLine = 1;
                    break;
                case 'a':
                    ov.rectangle = new Rectangle(x, y, width2, height2);
                    ov.circle.R = R2;
                    ov.FrameLine = 1;
                    break;
                case 'c':
                    ov.rectangle = new Rectangle(x, y, width3, height3);
                    ov.circle.R = R3;
                    ov.FrameLine = 8;
                    break;
                default:
                    ov.rectangle = new Rectangle(x, y, width4, height4);
                    ov.circle.R = R4;
                    ov.FrameLine = 7;
                    break;
            }
            ov.circle.x = ov.rectangle.Center.X;
            ov.circle.Y = ov.rectangle.Center.Y;
            ov.type = type;
            ov.launch = spawn;
            ovni.Add(ov);
        }

        public void remove_all()
        {
            this.ovni.Clear();
        }
        public void Dispose()
        {
            ovni = null;
            texture.Dispose();
        }
    }
}
