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
        public class Pointaction
        {
            public int dir { get; set; }
            public Vector2 direction { get; set; }
            public Rectangle hitbox { get; set; }
            public Rectangle affichage { get; set; }
            public Pointaction(int dir, Vector2 vector, Rectangle hitbox, Rectangle affichage)
            {
                this.dir = dir;
                this.direction = vector;
                this.hitbox = hitbox;
                this.affichage = affichage;


            }
            public void Update_C()
            {
                hitbox = new Rectangle(affichage.X, affichage.Y, hitbox.Width, hitbox.Height);
            }
            public Vector2 vise(objet sprt)
            {
                Vector2 _vecteur;
                _vecteur.X = -hitbox.Center.X + sprt.rectangle_C.Center.X;
                _vecteur.Y = hitbox.Center.Y - sprt.rectangle_C.Center.Y;
                _vecteur.Normalize();
                return (_vecteur);
            }
        }
        public int FrameLine;
        public int FrameColumn;
        public SpriteEffects effects;
        public int Timer;
        public int AnimationSpeed = 10;
        int colunm, line, dir;
        int timerdead = -1;
        public int degat { get; private set; }
        float lastvie;
        string type = "";
        Texture2D texture, ptforttexture, ptfaible_texture;
        public List<Pointaction> ptfaible_ { get; private set; }
        public List<Pointaction> ptfort_ { get; private set; }
        private bool run;
        private int timerrun, speed, front_sc, timeatk;

        Color color;
        private Rectangle rect, atk;
        Rectangle fond;
        bool attaque = false;
        bool deplace = false;


        public bossPLAT(Rectangle fond)
        {
            degat = 1;
            vie = 1;
            ptfaible_ = new List<Pointaction>();
            ptfort_ = new List<Pointaction>();
            poid = 10;


            this.FrameLine = 1;
            this.FrameColumn = 1;
            this.Timer = 0;
            dir = 1;
            this.fond = fond;
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
                    colunm = 180;
                    break;
                case "Boubou":
                    line = 93;
                    colunm = 125;
                    break;
                case "Taizo":
                    line = 93;
                    colunm = 125;
                    break;
                case "Kinukuman":
                    line = 93;
                    colunm = 125;
                    break;
                default:
                    line = 100;
                    colunm = 100;
                    break;
            }
            dir = 1;
        }
        public void loadContent(ContentManager content, Rectangle fond)
        {
            this.fond = fond;

            this.atk = new Rectangle(1000, 200, 100, 100);
            this.rectangle_C = rectangle;
            this.degat = 1;
            this.vie = 15;
            this.lastvie = vie;
            this.speed = 7;

            color = Color.Yellow;
            this.effects = SpriteEffects.FlipHorizontally;
            switch (type)
            {
                case "Light":

                    ptfort_.Add(new Pointaction(dir, new Vector2(0, 0), new Rectangle(800, (int)(fond.Height * 0.8f), 50, 50), new Rectangle(800, (int)(fond.Height * 0.8f), 50, 50)));
                    ptfort_.Add(new Pointaction(dir, new Vector2(0, 0), new Rectangle(0, 200, 50, 50), new Rectangle(0, 200, 50, 50)));
                    ptfort_.Add(new Pointaction(dir, new Vector2(0, 0), new Rectangle(1000, (int)(fond.Height * 0.6f), 50, 50), new Rectangle(1000, (int)(fond.Height * 0.6f), 50, 50)));
                    ptfort_.Add(new Pointaction(dir, new Vector2(0, 0), new Rectangle(1100, (int)(fond.Height * 0.7f), 50, 50), new Rectangle(1100, (int)(fond.Height * 0.7f), 50, 50)));

                    ptfort_.Add(new Pointaction(dir, new Vector2(0, 0), new Rectangle(1300, (int)(fond.Height * 0.8f), 50, 50), new Rectangle(1300, (int)(fond.Height * 0.8f), 50, 50)));
                    ptfort_.Add(new Pointaction(dir, new Vector2(0, 0), new Rectangle(900, (int)(fond.Height * 0.8f), 50, 50), new Rectangle(900, (int)(fond.Height * 0.8f), 50, 50)));
                    line = 100;
                    colunm = 100;
                    hauteurY = (int)((53f / (float)line) * rectangle.Height);
                    largeurX = (int)((16f / (float)colunm) * rectangle.Width);
                    decallageX = (int)((41f / (float)colunm) * rectangle.Width);
                    decallageY = (int)((28f / (float)line) * rectangle.Height);
                               Update_rec_collision();
                    ptfaible_.Add(new Pointaction(dir, Vector2.Zero, rectangle_C, rectangle));
                    break;
                case "Cascade":

                    line = 150;
                    colunm = 180;
                    hauteurY = (int)((68f / (float)line) * rectangle.Height);
                    largeurX = (int)((70f / (float)colunm) * rectangle.Width);
                    decallageX = (int)((46f / (float)colunm) * rectangle.Width);
                    decallageY = (int)((44f / (float)line) * rectangle.Height);
                               Update_rec_collision();
                    ptfaible_.Add(new Pointaction(dir, Vector2.Zero, rectangle_C, rectangle));
                    break;
                case "":
                    type = "null";
                    break;
                case null:
                    type = "null";
                    break;


                case "Kinukuman":
                  
                    line = 93;
                    colunm = 125;
                    hauteurY = (int)((55f / (float)line) * rectangle.Height);
                    largeurX = (int)((36f / (float)colunm) * rectangle.Width);
                    decallageX = (int)((35f / (float)colunm) * rectangle.Width);
                    decallageY = (int)((25f / (float)line) * rectangle.Height);
                    Update_rec_collision();
                    ptfaible_.Add(new Pointaction(dir, Vector2.Zero, rectangle_C, rectangle));
                    break;
                case "Boubou":

       
                    line = 93;
                    colunm = 125;
                    hauteurY = (int)((45f / (float)line) * rectangle.Height);
                    largeurX = (int)((33f / (float)colunm) * rectangle.Width);
                    decallageX = (int)((32f / (float)colunm) * rectangle.Width);
                    decallageY = (int)((35f / (float)line) * rectangle.Height);
                               Update_rec_collision();
                    ptfaible_.Add(new Pointaction(dir, Vector2.Zero, rectangle_C, rectangle));
                    break;
                case "Taizo":

                
                    line = 93;
                    colunm = 125;
                    hauteurY = (int)((45f / (float)line) * rectangle.Height);
                    largeurX = (int)((29f / (float)colunm) * rectangle.Width);
                    decallageX = (int)((47f / (float)colunm) * rectangle.Width);
                    decallageY = (int)((36f / (float)line) * rectangle.Height);
                               Update_rec_collision();
                    ptfaible_.Add(new Pointaction(dir, Vector2.Zero, rectangle_C, rectangle));
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
            this.rectangle_C = rectangle;
            this.degat = 1;
            this.vie = 15;
            this.lastvie = vie;
            this.type = type;
            this.speed = 7;

            color = Color.Yellow;
            this.effects = SpriteEffects.FlipHorizontally;





            switch (type)
            {
                case "Light":

                    line = 100;
                    colunm = 100;
                    break;
                case "Cascade":

                    line = 150;
                    colunm = 180;
                    break;

                case "Boubou":
                    line = 93;
                    FrameColumn = 125;
                    break;
                case "Taizo":
                    line = 93;
                    FrameColumn = 125;
                    break;
                case "Kinukuman":
                    line = 93;
                    FrameColumn = 125;
                    break;
                default:
                    break;
            }
            this.texture = content.Load<Texture2D>("Boss/" + type);
            ptforttexture = content.Load<Texture2D>("pointfort/" + type);
            ptfaible_texture = content.Load<Texture2D>("pointfaible/" + type);
        }
        public void UpdateEDIT(ref KeyboardState keyboard)
        {


            if (keyboard.IsKeyDown(Keys.Right))
            {
                this.rectangle.X -= front_sc;

            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                this.rectangle.X += front_sc;
            }
            Update_rec_collision();
        }
        public void Update(ref KeyboardState keyboard)
        {
            int bouge = 0;
            this.Animated();
            if (rectangle.Center.X > fond.Width / 2)
                dir = -1;
            else
                dir = 1;
            // if ( this.rectangle_C.Center.X < 1.2f * fond.Width  && this.rectangle_C.Center.X > -0.2f * fond.Width )
            if (vie >= 0)
            {
                if (this.rectangle_C.Center.X < 1.2f * fond.Width && this.rectangle_C.Center.X > -0.2f * fond.Width)
                    switch (type)
                    {
                        case "Light":
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle;
                                pt.Update_C();
                            }
                            break;
                        case "Cascade":
                            if (lastvie != vie)
                            {
                                timerrun = 100;
                            }
                            if (timerrun >= 0)// se deplace
                                rectangle.X += speed;
                                

                            if (timeatk <= 0)// lance son attaque
                            {
                                timeatk = 60;
                                ptfort_.Add(new Pointaction(dir, new Vector2(1, 0), new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60), new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60)));
                                
                            }
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle;
                                pt.Update_C();
                            }
                            break;
                        case "Boubou":
                            if (timeatk < 0)// lance son attaque
                            {
                                ptfort_.Add(new Pointaction(1, new Vector2(0, 1), new Rectangle(fond.Width / 2, 0, 100, 50), new Rectangle(fond.Width / 2, 0, 100, 50)));
                                timeatk = 60;
                                
                            }

                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle;
                                pt.Update_C();
                            }
                            break;
                        case "Taizo":
                            if (timeatk < 0)
                            {
                                ptfort_.Add(new Pointaction(dir, new Vector2(1, 0), new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60), new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60)));
                                timeatk = 60;
                                attaque = true;
                                deplace = false;
                            }
                            if (timeatk == 50)// pour lancer le deplacement avec un decallage par rapport a l attaque
                                timerrun = 10;
                            if (timerrun >= 0)// se deplace
                            {
                                rectangle.X += speed;
                                attaque = false;
                                deplace = true;
                            }
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle_C;
                                pt.Update_C();
                            }
                            break;
                        case "Kinukuman":
                            if (timeatk < 0)
                            {
                                ptfort_.Clear();
                                ptfort_.Add(new Pointaction(dir, new Vector2(1, 0), rectangle_C, rectangle_C));// cree un pt fort pour garder la diretion
                                timeatk = 120;
                                timerrun = 50;
                                attaque = true;
                            }
                            if (timerrun >= 0)// se deplace
                            {
                                // direction du dernier pt fort
                                rectangle.X += ptfort_[ptfort_.Count - 1].dir * speed;
                                dir = ptfort_[ptfort_.Count - 1].dir;
                                attaque = false;
                            }
                            else
                                ptfort_.Clear();
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage  = rectangle_C;
                                pt.Update_C();
                            }
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage  = rectangle_C;
                                pt.Update_C();
                            }
                            break;
                        default:
                            break;
                    }
            }
            else// mort
            {
                switch (type)
                {
                    case "Light":
                        if (timerdead == -1)
                            timerdead = 60;
                        foreach (Pointaction pt in ptfort_)
                        {
                            pt.direction = pt.vise(this);
                            pt.dir = 1;
                        }
                        break;
                    default:
                        timerdead = 1;
                        break;
                }
                if (timerdead == 0)
                {
                    ptfort_.Clear();
                    ptfaible_.Clear();
                }
                timerdead--;
            }

            if (keyboard.IsKeyDown(Keys.Right) != keyboard.IsKeyDown(Keys.Left))
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    this.rectangle.X -= front_sc;
                    bouge = -front_sc;
                }
                else
                {
                    this.rectangle.X += front_sc;
                    bouge = front_sc;
                }
            foreach (Pointaction pt in ptfaible_)
            {
                pt.affichage = new Rectangle(
                   (int)(pt.affichage.X + (pt.dir * pt.direction.X * speed) + bouge),
                    (int)(pt.affichage.Y + pt.dir * pt.direction.Y * speed),
                    pt.affichage.Width, pt.affichage.Height);
                pt.Update_C();
            }
            foreach (Pointaction pt in ptfort_)
            {
                pt.affichage = new Rectangle(
                   (int)(pt.affichage.X + (pt.dir * pt.direction.X * speed + bouge)),
                    (int)(pt.affichage.Y + pt.dir * pt.direction.Y * speed),
                    pt.affichage.Width, pt.affichage.Height);
                pt.Update_C();
            }
            if (tombe)
                rectangle.Y += poid;
            Update_rec_collision();
            timeatk--;
            lastvie = vie;
            timerrun--;
        }
        public void Update(ref KeyboardState keyboard, Sprite_PLA p1, Sprite_PLA p2)
        {
            Rectangle cible = Rectangle.Empty;
            Vector2 cibleV = Vector2.Zero;
            this.vise(p1, p2, ref cible, ref cibleV);
            int bouge = 0;
            this.Animated();
            if (cibleV.X > 0)
                dir = 1;
            else
                dir = -1;
            // if ( this.rectangle_C.Center.X < 1.2f * fond.Width  && this.rectangle_C.Center.X > -0.2f * fond.Width )
            if (vie >= 0)
            {
                if (this.rectangle_C.Center.X < 1.2f * fond.Width && this.rectangle_C.Center.X > -0.2f * fond.Width)
                    switch (type)
                    {
                        case "Light":
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle;
                                pt.Update_C();
                            }
                            break;
                        case "Cascade":
                            if (lastvie != vie)
                            {
                                timerrun = 100;
                            }
                            if (timerrun >= 0)// se deplace
                                rectangle.X += speed;

                            if (timeatk <= 0)// lance son attaque
                            {
                                timeatk = 60;
                                ptfort_.Add(new Pointaction(dir, cibleV, new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60), new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60)));

                            }
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle;
                                pt.Update_C();
                            }
                            break;
                        case "Boubou":
                            if (timeatk < 0)// lance son attaque
                            {
                                ptfort_.Add(new Pointaction(1, new Vector2(0, 1), new Rectangle(cible.X, 0, 100, 50), new Rectangle(cible.X, 0, 100, 50)));
                                timeatk = 60;
                            }

                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle_C;
                                pt.Update_C();
                            }
                            break;
                        case "Taizo":
                            if (timeatk < 0)
                            {
                                ptfort_.Add(new Pointaction(dir, cibleV, new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60), new Rectangle(rectangle_C.X, rectangle_C.Center.Y, 60, 60)));
                                timeatk = 60;
                                deplace = false;
                                attaque = true;
                            }
                            if (timeatk == 50)// pour lancer le deplacement avec un decallage par rapport a l attaque
                                timerrun = 5;
                            if (timerrun >= 0)// se deplace
                            {
                                rectangle.X += speed;
                                deplace = true;
                                deplace = false;
                            }
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle;
                                pt.Update_C();
                            }
                            break;
                        case "Kinukuman":
                            if (timeatk < 0)
                            {
                                ptfort_.Clear();

                                ptfort_.Add(new Pointaction(dir, new Vector2(1, 0), rectangle_C, rectangle_C));// cree un pt fort pour garder la diretion

                                timeatk = 120;
                                timerrun = 50;
                                attaque = true;
                            }
                            if (timerrun >= 0)// se deplace
                            {
                                // direction du dernier pt fort
                                rectangle.X += ptfort_[ptfort_.Count - 1].dir * speed;
                                attaque = false;
                            }
                            else
                                ptfort_.Clear();
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle_C;
                                pt.Update_C();
                            }
                            foreach (Pointaction pt in ptfaible_)
                            {
                                pt.affichage = rectangle_C;
                                pt.Update_C();
                            }
                            break;
                        default:
                            break;
                    }
            }
            else// mort
            {
                switch (type)
                {
                    case "Light":
                        if (timerdead == -1)
                            timerdead = 60;
                        foreach (Pointaction pt in ptfort_)
                        {
                            pt.direction = pt.vise(this);
                            pt.dir = 1;
                        }
                        break;
                    default:
                        timerdead = 1;
                        break;
                }
                if (timerdead == 0)
                {
                    ptfort_.Clear();
                    ptfaible_.Clear();
                }
                timerdead--;
            }

            if (keyboard.IsKeyDown(Keys.Right) != keyboard.IsKeyDown(Keys.Left))
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    this.rectangle.X -= front_sc;
                    bouge = -front_sc;
                }
                else
                {
                    this.rectangle.X += front_sc;
                    bouge = front_sc;
                }
            foreach (Pointaction pt in ptfaible_)
            {
                pt.affichage = new Rectangle(
                   (int)(pt.affichage.X + (pt.dir * pt.direction.X * speed) + bouge),
                    (int)(pt.affichage.Y + pt.dir * pt.direction.Y * speed),
                    pt.affichage.Width, pt.affichage.Height);
                pt.Update_C();
            }
            foreach (Pointaction pt in ptfort_)
            {
                pt.affichage = new Rectangle(
                   (int)(pt.affichage.X + (pt.dir * pt.direction.X * speed + bouge)),
                    (int)(pt.affichage.Y + pt.dir * pt.direction.Y * speed),
                    pt.affichage.Width, pt.affichage.Height);
                pt.Update_C();
            }
            if (tombe)
                rectangle.Y += poid;
            Update_rec_collision();
            timeatk--;
            lastvie = vie;
            timerrun--;
        }

        public void Animation(int n)
        {
            this.Timer++;
            if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn++;
                if (FrameColumn > n)
                    FrameColumn = 1;
            }
        }



        public void Animated()
        {
            if (type == "Light")
            {
                if (vie > 0)
                {
                    FrameLine = 1;
                    Animation(13);
                }
                else
                {
                    FrameLine = 2;

                    this.Timer++;
                    if (FrameColumn == 7)
                    {

                    }
                    else if (FrameColumn > 7)
                    {
                        FrameColumn = 1;
                    }
                    else if (this.Timer == this.AnimationSpeed)
                    {
                        this.Timer = 0;
                        this.FrameColumn++;

                    }
                    
                }

                if (dir >= 0)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;
            }

            else if (type == "Cascade")
            {
                if (vie > 0)
                {
                    if (timerrun >= 0)
                    {
                        FrameLine = 3;
                        Animation(7);
                    }
                    else
                    {
                        FrameLine = 1;
                        Animation(8);
                    }
                }
                else
                {
                    FrameLine = 4;

                    this.Timer++;
                    if (FrameColumn == 10)
                    {

                    }
                    else if (FrameColumn > 10)
                    {
                        FrameColumn = 1;
                    }
                    else if (this.Timer == this.AnimationSpeed)
                    {
                        this.Timer = 0;
                        this.FrameColumn++;

                    }

                }
                if (dir >= 0 || timerrun >= 0)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;
            }

            else if (type == "Boubou")
            {
                if (vie > 0)
                {
                    FrameLine = 2;
                    Animation(14);
                }
                else
                {
                    FrameLine = 3;

                    this.Timer++;
                    if (FrameColumn == 5)
                    {

                    }
                    else if (FrameColumn > 5)
                    {
                        FrameColumn = 1;
                    }
                    else if (this.Timer == this.AnimationSpeed)
                    {
                        this.Timer = 0;
                        this.FrameColumn++;

                    }

                }
                if (dir >= 0)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;
            }

            else if (type == "Kinukuman")
            {
                if (vie > 0)
                {
                    if (timeatk < 0)
                    {
                        FrameLine = 4;
                        Animation(12);
                    }
                    else if (timerrun >= 0)
                    {
                        FrameLine = 2;
                        Animation(8);
                    }
                    else
                    {
                        FrameLine = 1;
                        Animation(4);
                    }
                }
                else
                {
                    FrameLine = 3;

                    this.Timer++;
                    if (FrameColumn == 6)
                    {

                    }
                    else if (FrameColumn > 6)
                    {
                        FrameColumn = 1;
                    }
                    else if (this.Timer == this.AnimationSpeed)
                    {
                        this.Timer = 0;
                        this.FrameColumn++;

                    }

                }

                if (dir >= 0)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;

            }

            else if (type == "Taizo")
            {

                if (vie > 0)
                {
                    if (deplace)
                    {
                        FrameLine = 2;
                        Animation(4);
                    }
                    else if (attaque)
                    {
                        FrameLine = 3;
                        Animation(10);
                    }

                    else
                    {
                        FrameLine = 1;
                        Animation(3);
                    }
                }
                else
                {
                    if (FrameLine != 5 && FrameLine != 6)
                    {
                        FrameLine = 5;
                        FrameColumn = 1;
                    }
                    else if (FrameLine == 5)
                    {
                        this.Timer++;
                        if (FrameColumn == 16)
                        {
                            FrameLine = 6;
                            FrameColumn = 1;
                        }
                        else if (FrameColumn > 5)
                        {
                            FrameLine = 6;
                            FrameColumn = 1;
                        }
                        else if (this.Timer == this.AnimationSpeed)
                        {
                            this.Timer = 0;
                            this.FrameColumn++;

                        }

                    }

                    else if (FrameLine == 6)
                    {
                        this.Timer++;
                        if (FrameColumn == 13)
                        {

                        }
                        else if (FrameColumn > 13)
                        {

                        }
                        else if (this.Timer == this.AnimationSpeed)
                        {
                            this.Timer = 0;
                            this.FrameColumn++;

                        }
                    }


                }
                if (dir >= 0)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;
            }
        }

        public void Draw(SpriteBatch sp)
        {
            foreach (Pointaction pt in ptfaible_)
                sp.Draw(ptforttexture  , pt.affichage, Color.White);
            foreach (Pointaction pt in ptfort_)
                sp.Draw(ptforttexture , pt.affichage, Color.White);
            sp.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * colunm, (this.FrameLine - 1) * line, colunm, line), Color.White, 0f, new Vector2(0, 0), this.effects, 0f);
        }
        public void DrawEDIT(SpriteBatch sp)
        {
            if (texture != null)
                sp.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * colunm, (this.FrameLine - 1) * line, colunm, line), Color.White, 0f, new Vector2(0, 0), this.effects, 0f);
        }

    }
}
