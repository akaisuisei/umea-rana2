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
using Umea_rana.LocalizedStrings;

namespace Umea_rana
{

    public class sripte_V : objet
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
        int perso;
        public Texture2D texture, mtexture;
        //  Texture2D test;
        public List<munition> bulletL;
        Color color_V;

        int change_T;

        int height, width;
        public Bullet_manager bullet;
        public bool automatic_controlled;
        int speed, maxspeed, minspeed;
        int sizeX, sizeY, sizeX1, sizeX2, sizeY1, sizeY2, timer1, timer2;
        public int power { get; set; }
        public int bomb { get; set; }
        Int16 type;
        public int timer;
        int FrameLine;
        int FrameColumn;
        SpriteEffects Effects;
        int Timer;
        int AnimationSpeed = 14;

        int speedbullet;
        int damage;
        pos Vturn, Vup, Vdown, Rturn, Rup, Rdown, current, last, xplosion;


        int FrameColumn2;
        int frameline2;
        float colunm, line, xline, xclunm;

        Rectangle fond;
        Keys Kup, Kdown, Kright, Kleft, Katk, Ktrans;
        public bool activate { get; private set; }
        public int scrore;

        public sripte_V(Rectangle n_rectangle, int height, int width)
        {
            rectangle = n_rectangle;

            decallageX = (int)(0.33f * (float)rectangle.Width);
            decallageY = (int)(0.55f * (float)rectangle.Width);
            hauteurY = (int)(0.33f * (float)rectangle.Width);
            largeurX = (int)(0.33f * (float)rectangle.Width);
            Update_rec_collision();
            this.height = height;
            this.width = width;
            FrameLine = 1;
            FrameColumn = 1;
            change_T = 0;
            type = 0;
            automatic_controlled = false;
            // intencie le manager de missille 
            bulletL = new List<munition>();
            power = 0;
            sizeX = rectangle.Width / 3;
            sizeY = rectangle.Height / 2;

            sizeX1 = rectangle.Width / 8;
            sizeY1 = rectangle.Height / 6;

            sizeX2 = rectangle.Width / 4;
            sizeY2 = rectangle.Height / 5;


            FrameColumn2 = 1;

            //     test = Content.Load<Texture2D>("ListBoxBG");
            Vturn = new pos(2, 2, 9, 1, 5);
            Vup = new pos(2, 1, 8, 1, 5);
            Vdown = new pos(2, 1, 2, 3, 5);
            Rturn = new pos(1, 2, 6, 1, 5);

            Rup = new pos(1, 1, 5, 1, 5);
            Rdown = new pos(1, 1, 7, 1, 5);
            // xplode invertion entre truster et image normale
            xplosion = new pos(1, 8, 1, 18, 40);
            current = Rup;
            colunm = 150f;
            line = 200f;
            xline = 45;
            xclunm = 45;

            Katk = Keys.Space;
            Kdown = Keys.Down;
            Kup = Keys.Up;
            Kright = Keys.Right;
            Kleft = Keys.Left;
            Ktrans = Keys.T;
            fond = new Rectangle(0, 0, width, height);
            perso = 1;
            activate = true;
        }
        public sripte_V(Rectangle n_rectangle, Rectangle fond, int perso)
        {
            rectangle = n_rectangle;

            this.fond = fond;
            rectangle.X = fond.Center.X;
            rectangle.Width = fond.Width / 10;
            rectangle.Height = fond.Height / 10;
            rectangle.Y = fond.Bottom - rectangle.Height;
            decallageX = (int)(0.33f * (float)rectangle.Width);
            decallageY = (int)(0.55f * (float)rectangle.Width);
            hauteurY = (int)(0.33f * (float)rectangle.Width);
            largeurX = (int)(0.33f * (float)rectangle.Width);
            Update_rec_collision();

            FrameLine = 1;
            FrameColumn = 1;
            change_T = 0;
            type = 0;
            automatic_controlled = false;
            // intencie le manager de missille 
            bulletL = new List<munition>();
            power = 0;
            sizeX = rectangle.Width / 3;
            sizeY = rectangle.Height / 2;

            sizeX1 = rectangle.Width / 8;
            sizeY1 = rectangle.Height / 6;

            sizeX2 = rectangle.Width / 4;
            sizeY2 = rectangle.Height / 5;
            FrameColumn2 = 1;

            //     test = Content.Load<Texture2D>("ListBoxBG");
            Vturn = new pos(2, 2, 9, 1, 5);
            Vup = new pos(2, 1, 8, 1, 5);
            Vdown = new pos(2, 1, 2, 3, 5);
            Rturn = new pos(1, 2, 6, 1, 5);

            Rup = new pos(1, 1, 5, 1, 5);
            Rdown = new pos(1, 1, 7, 1, 5);
            // xplode invertion entre truster et image normale
            xplosion = new pos(1, 8, 1, 18, 40);
            current = Rup;
            colunm = 150f;
            line = 200f;
            xline = 45;
            xclunm = 45;
            if (perso == 1)
            {
                Katk = Keys.Space;
                Kdown = Keys.Down;
                Kup = Keys.Up;
                Kright = Keys.Right;
                Kleft = Keys.Left;
                Ktrans = Keys.T;
                activate = true;
            }
            else
            {
                Katk = Keys.LeftControl;
                Kdown = Keys.S;
                Kup = Keys.W;
                Kright = Keys.D;
                Kleft = Keys.A;
                Ktrans = Keys.Q;
                rectangle.X = -300;
                activate = false;
            }
            this.perso = perso;
    

        }
        public void parametrage(ref levelProfile level)
        {
            this.speed = level.player_speed;
            speedbullet = level.bullet_speed;
            this.color_V = level.color;
            damage = level.damage;
            this.vie = level.playerLife;
            timer = level.firerate;
            timer1 = timer / 2;
            timer2 = timer;
            maxspeed = (int)((float)speed * 1.5f);
            minspeed = speed;
        }
        public void Load(ContentManager Content, Texture2D n_texture)
        {
            texture = n_texture;
            mtexture = Content.Load<Texture2D>("bullet//bullet");
            bullet = new Bullet_manager(new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 10, Content.Load<SoundEffect>("hero//vaisseau//tir2"), color_V, width, 30, Katk);
        }
        public void parametrage(ref sripte_V sprite)
        {
            this.speed = sprite.speed;
            speedbullet = sprite.speedbullet;
            this.color_V = sprite.color_V;
            damage = sprite.damage;
            this.vie = sprite.vie; ;
            timer = sprite.timer;
            timer1 = sprite.timer / 2;
            timer2 = sprite.timer;
            maxspeed = (int)((float)speed * 1.5f);
            minspeed = speed;
        }

        public void Update(KeyboardState keyboard, Game1 game, KeyboardState oldkey)
        {
            if (activate)
            {
                if (vie <= 0)
                {
                    rectangle.Inflate(2,2);
                    Xplode();
                    if (FrameLine == 45)
                        if (perso == 1)
                            game.ChangeState(Game1.gameState.Pause);
                        else
                        {
                            rectangle = new Rectangle(-300, 0, fond.Width / 10, fond.Height / 10);
                            this.activate = false;
                            this.rectangle.X = -300;
                            vie = 5;
                            colunm = 150f;
                            line = 200f;
                            xline = 45;
                            xclunm = 45;
                        }
                }
                else
                {
                    if (automatic_controlled)//movement automatic de fin de jeu
                        up();
                    else// controlle du vaisseau
                    {
                        if (keyboard.IsKeyUp(Ktrans) && oldkey.IsKeyDown(Ktrans))
                            type += 3;
                        else if (type < 3)
                        {
                            if ((keyboard.IsKeyUp(Kup) && keyboard.IsKeyUp(Kdown) && keyboard.IsKeyUp(Kright) && keyboard.IsKeyUp(Kleft)) || keyboard.IsKeyDown(Kleft) && keyboard.IsKeyDown(Kright))
                                move();
                            else
                            {
                                if (keyboard.IsKeyDown(Kup) && (rectangle_C.Y > fond.Top))
                                    up();
                                if (keyboard.IsKeyDown(Kdown) && (rectangle_C.Bottom < fond.Bottom))
                                    down();
                                if (keyboard.IsKeyDown(Kright) && (rectangle_C.Right < fond.Right))
                                    right();
                                if (keyboard.IsKeyDown(Kleft) && (rectangle_C.Left > fond.Left))
                                    left();
                            }
                        }
                        else
                        {
                            type %= 5;
                            Transform();
                        }
                    }

                    change_T += 1;// timer pour l animation
                    if (keyboard.IsKeyDown(Keys.D1) || keyboard.IsKeyDown(Keys.F1))
                        power = 1;
                    if (keyboard.IsKeyDown(Keys.D2) || keyboard.IsKeyDown(Keys.F2))
                        power = 2;
                    if (keyboard.IsKeyDown(Keys.D3) || keyboard.IsKeyDown(Keys.F3))
                        power = 3;
                    if (keyboard.IsKeyDown(Keys.D4) || keyboard.IsKeyDown(Keys.F4))
                        power = 4;

                }
                bullet.Bullet_Update(keyboard, this, oldkey, new Vector2(0, 1), power, ref bulletL, ref sizeX, ref sizeY, ref timer);
                Update_rec_collision();
                last = current;
                this.Timer++;
            }
            else
            {
                if (keyboard.IsKeyDown(Ktrans))
                {

                    activate = true;
                    this.rectangle.X = fond.Center.X;
                    this.rectangle.Y = fond.Center.Y;
                }
            }

        }

        // movement et animation
        private void up()
        {
            rectangle.Y -= speed;
            move();


        }
        private void down()
        {
            if (type == 0)
            {
                current = Rdown;
            }
            else
            {
                current = Vdown;
            }
            if (current != last)
            {
                FrameLine = current.lstart;
                frameline2 = current.Tlstart;
                FrameColumn = current.cstart;
                FrameColumn2 = current.Tcstart;
            }
            else if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn2 = FrameColumn2 % current.Tcend + 1;
            }
            rectangle.Y += speed;

        }
        private void right()
        {
            Effects = SpriteEffects.FlipHorizontally;
            if (type == 0)
            {
                current = Rturn;
            }
            else
            {
                current = Vturn;
                decallageX = (int)(0.467f * (float)rectangle.Width);
            }
            if (current != last)
            {
                FrameLine = current.lstart;
                frameline2 = current.Tlstart;
                FrameColumn = current.cstart;
                FrameColumn2 = current.Tcstart;
            }
            else if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn2 = FrameColumn2 % current.Tcend + 1;
            }
            rectangle.X += speed;

        }
        private void left()
        {
            Effects = SpriteEffects.None;

            if (type == 0)
            {
                current = Rturn;
            }
            else
            {
                current = Vturn;
                decallageX = (int)(0.44f * (float)rectangle.Width);
            }
            if (current != last)
            {
                FrameLine = current.lstart;
                frameline2 = current.Tlstart;
                FrameColumn = current.cstart;
                FrameColumn2 = current.Tcstart;
            }
            else if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn2 = FrameColumn2 % current.Tcend + 1;
            }
            rectangle.X -= speed;

        }
        // animation
        private void move()
        {
            Effects = SpriteEffects.None;
            if (type == 0)
            {
                current = Rup;
            }
            else
            {
                current = Vup;
                decallageX = (int)(0.44f * (float)rectangle.Width);
            }
            if (current != last)
            {
                FrameLine = current.lstart;
                frameline2 = current.Tlstart;
                FrameColumn = current.cstart;
                FrameColumn2 = current.Tcstart;
            }
            else if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn2 = FrameColumn2 % current.Tcend + 1;
            }
        }

        private void Transform()
        {
            Effects = SpriteEffects.None;
            decallageX = (int)(0.33f * (float)rectangle.Width);
            decallageY = (int)(0.55f * (float)rectangle.Width);
            hauteurY = (int)(0.20f * (float)rectangle.Width); ;
            largeurX = (int)(0.33f * (float)rectangle.Width);
            frameline2 = 1;
            FrameColumn2 = 4;
            if (type % 2 == 1)
            {

                if (FrameLine == 1 && FrameColumn == 1)
                {
                    FrameColumn = 4;
                    FrameLine = 4;
                    Timer = 0;
                }
                else if (FrameColumn == 1 && FrameLine == 3)
                {
                    FrameColumn = 1;
                    FrameLine = 2;
                    type = 1;
                    speed = maxspeed;
                    decallageX = (int)(0.44f * (float)rectangle.Width);
                    decallageY = (int)(0.17f * (float)rectangle.Width);
                    hauteurY = (int)(0.555f * (float)rectangle.Width);
                    largeurX = (int)(0.12f * (float)rectangle.Width);
                    sizeX = sizeX1;
                    sizeY = sizeY1;
                    timer = timer1;

                }
                else if (FrameColumn == 1)
                {
                    FrameColumn = 4;
                    FrameLine = 3;
                }
                else if (this.Timer == this.AnimationSpeed)
                {
                    this.Timer = 0;
                    this.FrameColumn--;

                }
            }
            else
            {
                if (FrameLine == 2 && FrameColumn == 1)
                {
                    FrameColumn = 1;
                    FrameLine = 3;
                    Timer = 0;
                }
                else if (FrameColumn == 4 && FrameLine == 4)
                {
                    FrameColumn = 2;
                    FrameLine = 1;
                    type = 0;
                    speed = minspeed;
                    decallageX = (int)(0.33f * (float)rectangle.Width);
                    decallageY = (int)(0.55f * (float)rectangle.Width);
                    hauteurY = (int)(0.33f * (float)rectangle.Width); ;
                    largeurX = (int)(0.33f * (float)rectangle.Width);
                    sizeX = sizeX2;
                    sizeY = sizeY2;
                    timer = timer2;

                }
                else if (FrameColumn == 4)
                {
                    FrameColumn = 1;
                    FrameLine = 4;
                }
                else if (this.Timer == this.AnimationSpeed)
                {
                    this.Timer = 0;
                    this.FrameColumn++;
                }
            }
        }
        /// <summary>
        /// animattion de l'explosion
        /// </summary>
        private void Xplode()
        {
            current = xplosion;

            if (current != last)
            {
                FrameLine = current.Tlstart;

                FrameColumn = current.Tcstart;
                frameline2 = current.lstart;
                FrameColumn2 = current.cstart;
                Timer = 0;
                AnimationSpeed = 7;
                line = xline;
                colunm = xclunm;
            }
            else if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameLine++;
            }
        }

        // procedure pour indiquer au vaisseau la fin de niveau
        public void gagne()
        {
            automatic_controlled = true;
            this.bullet.enableFire = false;
        }
        public void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(texture, rectangle, new Rectangle((int)((this.FrameColumn - 1) * colunm), (int)((this.FrameLine - 1) * line), (int)colunm, (int)line), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
            //       spritebatch.Draw(test ,rectangle_C,Color.Turquoise );  
            spritebatch.Draw(texture, rectangle, new Rectangle((int)((this.FrameColumn2 - 1) * colunm), (int)((this.frameline2 - 1) * line), (int)colunm, (int)line), color_V, 0f, Vector2.Zero, this.Effects, 0.001f);
            for (int i = 0; i < bulletL.Count; i++)
            {
                spritebatch.Draw(mtexture, bulletL[i].rectangle, new Rectangle(0, 0, mtexture.Width, mtexture.Height), bulletL[i].colo);
            }
        }
        public void Dispose()
        {
            texture.Dispose();
            mtexture.Dispose();
            bulletL = null;

        }
    }

    class info
    {
        Rectangle fond;
        int nb_iaT, nbIA_V, nb_objet, nb_iaK, pVie, bvie;
        SpriteFont font;
        public info()
        {
            nb_iaK = 0;
            nb_iaT = 0;
            nb_objet = 0;
            nbIA_V = 0;
            pVie = 0;
            bvie = 0;
        }
        public void Update(int iak, int iat, int iav, int ob, int vie, int vieboss)
        {
            this.nb_iaK = iak;
            this.nb_iaT = iat;
            this.nb_objet = ob;
            this.nbIA_V = iav;
            this.pVie = vie;
            this.bvie = vieboss;
        }
        public void LoadContent(Rectangle fond, SpriteFont font)
        {
            this.fond = fond;
            this.font = font;
        }
        public void draw(SpriteBatch sp)
        {
            sp.DrawString(font, LocalizedString.player_life + ":", new Vector2(fond.X, 0 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + pVie, new Vector2(fond.X, 1 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.nb_IAK + ":", new Vector2(fond.X, 2 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + nb_iaK, new Vector2(fond.X, 3 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.nb_iaV + ":", new Vector2(fond.X, 4 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + nbIA_V, new Vector2(fond.X, 5 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.nb_iaT + ":", new Vector2(fond.X, 6 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + nb_iaT, new Vector2(fond.X, 7 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.nb_objet + ":", new Vector2(fond.X, 8 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + nb_objet, new Vector2(fond.X, 9 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.BossLife + ":", new Vector2(fond.X, 12 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + bvie, new Vector2(fond.X, 13 * fond.Height / 12), Color.White);
        }

    }
    public class Score
    {
        int scoreJ1, scoreJ2;
        int vieJ1, vieJ2;
        int bombej1, bombej2;
        int powerj1, powerj2;
        bool activate;
        int highscore;
        Rectangle fond, fond2, image1, image2;
        SpriteFont font;
        Texture2D texturej1, texturej2;
        public Score()
        {
            this.scoreJ1 = 0;
            this.scoreJ2 = 0;
            this.vieJ1 = 0;
            this.vieJ2 = 0;
            this.bombej1 = 0;
            this.bombej2 = 0;
            highscore = 0;
        }
        public void LoadContent(Rectangle fond, Rectangle fond2, ContentManager Content)
        {
            this.fond = fond;
            this.fond2 = fond2;
            this.font = Content.Load<SpriteFont>("FontList");
            image1 = new Rectangle(fond.X, (int)((float)fond.Height * 7f / 14f), fond.Width, fond.Height - (int)((float)fond.Height * 7f / 14f));
            image2 = new Rectangle(fond2.X, (int)((float)fond2.Height * 8f / 14f), fond2.Width, fond2.Height - (int)((float)fond2.Height * 7f / 14f));
            texturej1 = Content.Load<Texture2D>("hero\\image1");
            texturej2 = Content.Load<Texture2D>("hero\\image2");
        }
        public void Update(ref sripte_V perso1,ref sripte_V perso2)
        {
            this.scoreJ1 = perso1.scrore;
            this.vieJ1 = perso1.vie;
            this.bombej1 = perso1.bomb;
            this.powerj1 = perso1.power;
            this.scoreJ2 = perso2.scrore;
            this.vieJ2 = perso2.vie;
            this.bombej2 = perso2.bomb;
            this.powerj2 = perso2.power;
            activate = perso2.activate;
            highscore = Math.Max(Math.Max(scoreJ1, scoreJ2), highscore);
        }
        public void Draw(SpriteBatch sp)
        {
  sp.Draw(texturej1 , image1, Color.White);
            sp.DrawString(font, LocalizedString.Hightscore , new Vector2(fond2.Left,0f/14f* fond2.Height), Color.White);
            sp.DrawString(font, "" + highscore, new Vector2(fond2.Left, 1f / 14f * fond2.Height), Color.White);
            sp.DrawString(font, LocalizedString.player + " 1", new Vector2(fond.Left, 0f / 14f * fond.Height), Color.White);
            sp.DrawString(font, LocalizedString.Score, new Vector2(fond.Left, 1f / 14f * fond.Height), Color.White);
            sp.DrawString(font, "" + scoreJ1, new Vector2(fond.Left, 2f / 14f * fond.Height), Color.White);
            sp.DrawString(font, LocalizedString.Life, new Vector2(fond.Left, 3f / 14f * fond.Height), Color.White);
            sp.DrawString(font, "" + vieJ1, new Vector2(fond.Left, 4f / 14f * fond.Height), Color.White);
            sp.DrawString(font, LocalizedString.bomb, new Vector2(fond.Left, 5f / 14f * fond.Height), Color.White);
            sp.DrawString(font, "" + bombej1, new Vector2(fond.Left, 6f / 14f * fond.Height), Color.White);
          

            if (activate)
            { 
                sp.Draw(texturej2, image2, Color.White);
                sp.DrawString(font, LocalizedString.player + " 2", new Vector2(fond2.Left,2f/14f* fond2.Height), Color.White);
                sp.DrawString(font, LocalizedString.Score, new Vector2(fond2.Left, 3f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, "" + scoreJ2, new Vector2(fond2.Left, 4f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, LocalizedString.Life, new Vector2(fond2.Left, 5f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, "" + vieJ2, new Vector2(fond2.Left, 6f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, LocalizedString.bomb, new Vector2(fond2.Left, 7f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, "" + bombej2, new Vector2(fond2.Left, 8f / 14f * fond2.Height), Color.White);
              
            }

        }
    }

}
