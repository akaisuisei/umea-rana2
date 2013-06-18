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
    public enum Direction
    {
        Left, Right
    };
    public class Sprite_PLA : objet
    {
        private class pos
        {
            public int lstart = 1, cstart = 1, cend = 1;
            public pos(int ls, int cs, int ce)
            {
                lstart = ls;
                cstart = cs;
                cend = ce;
            }

        }


        Texture2D texture;
        Collision collision;
        public bool jump_ok, jump_off, atq, dead;
        public bool in_air;
        public int impulse, pos_marche, longattaque;
        SoundEffect footstep;
        SoundEffectInstance loop_footstep;
        int _elapsed;
        Direction Direction;
        int FrameLine;
        int FrameColumn;
        SpriteEffects Effects;
        bool dir = false;
        int Timer;
        int AnimationSpeed = 10;
        public bool chute = true;
        int timer_dead;
        public bool _dir { get { return dir; } }
        int colunm, line;
        int keep;


        int prout;
        public bool block { get; private set; }
        public int upsidedown { get { return prout; } set { if (upsidedown < 0) prout = 1; } }

        pos idle, atk, walk, die, jump, fall, blockstart, blockresume, blockstop;
        pos current, last;
        List<pos> listatq;
        int intatq;

        Texture2D test;
        public int damage { get; set; }
        Keys K_atq, K_right, K_left, K_jump, K_block, K_atknext, K_atkdown;
        private int Speed { get; set; }
        public Rectangle hitboxatq;
        public Sprite_PLA(Texture2D n_textture, Rectangle n_rectangle, Collision n_collision, ContentManager Content, char type)
        {
            test = Content.Load<Texture2D>("ListBoxBG");
            texture = n_textture;
            rectangle_C = new Rectangle(n_rectangle.X + 49, n_rectangle.Y + 4, 30, n_rectangle.Height);
            rectangle = n_rectangle;
            poid = 10;
            in_air = false;
            jump_off = false;
            collision = n_collision;
            impulse = (int)(this.rectangle.Height *1.5f);
            pos_marche = rectangle.Y;
            footstep = Content.Load<SoundEffect>("hero//jogging");
            loop_footstep = footstep.CreateInstance();
            loop_footstep.IsLooped = true;
            _elapsed = 601;
            this.FrameLine = 1;
            this.FrameColumn = 1;
            this.Timer = 0;
            vie = 100;

            upsidedown = 10;
            atq = false;
            longattaque = 17;
            dead = false;
            timer_dead = 200;
            intatq = 0;
            if (type == '2')
            {
                colunm = 168;
                line = 118;
                largeurX = (int)((33f / (float)colunm) * rectangle.Width);
                hauteurY = (int)((39f / (float)line) * rectangle.Height);
                decallageX = (int)((64f / (float)colunm) * rectangle.Width);
                decallageY = (int)((61f / (float)line) * rectangle.Height);
                idle = new pos(1, 1, 4);
                walk = new pos(1, 5, 12);
                die = new pos(3, 5, 9);
                jump = new pos(2, 8, 8);
                fall = new pos(2, 9, 9);
                blockstart = new pos(2, 1, 2);
                blockresume = new pos(2, 2, 2);
                blockstop = new pos(2, 2, 3);
                listatq.Add(new pos(9, 5, 10));

            }
            else
            {
                colunm = 128;
                line = 99;
                largeurX = (int)((26f / (float)colunm) * rectangle.Width);
                hauteurY = (int)((51f / (float)line) * rectangle.Height);
                decallageX = (int)((51f / (float)colunm) * rectangle.Width);
                decallageY = (int)((30f / (float)line) * rectangle.Height);
                idle = new pos(1, 1, 4);
                walk = new pos(1, 5, 12);
                die = new pos(3, 3, 7);
                jump = new pos(2, 4, 5);
                fall = new pos(2, 6, 7);
                listatq.Add(new pos(8, 5, 10));
            }
            current = fall;
            last = current;
            test = Content.Load<Texture2D>("ListBoxBG");
            atk = listatq[intatq];
        }
        public Sprite_PLA(Rectangle n_rectangle, Collision n_collision, ContentManager Content, string player)
        {
            test = Content.Load<Texture2D>("ListBoxBG");
            _elapsed = 0;
            rectangle_C = new Rectangle(n_rectangle.X + 49, n_rectangle.Y + 4, 30, n_rectangle.Height);
            rectangle = n_rectangle;
            poid = 10;
            in_air = false;
            jump_off = false;
            collision = n_collision;
            impulse = (int)(this.rectangle.Height * 1.8f);
            pos_marche = rectangle.Y;
            footstep = Content.Load<SoundEffect>("hero//footstep");
            loop_footstep = footstep.CreateInstance();


            this.FrameLine = 1;
            this.FrameColumn = 1;
            this.Timer = 0;
            vie = 100;

            upsidedown = 10;
            atq = false;
            longattaque = 17;
            dead = false;
            timer_dead = 200;


            test = Content.Load<Texture2D>("ListBoxBG");
            switch (player)
            {
                case "P1":
                    K_atq = Keys.X;
                    K_right = Keys.Right;
                    K_left = Keys.Left;
                    K_jump = Keys.Space;
                    K_block = Keys.B;
                    K_atkdown = Keys.L;
                    K_atknext = Keys.K;
                    break;
                case "P2":
                    K_atq = Keys.E;
                    K_right = Keys.D;
                    K_left = Keys.Q ;
                    K_jump = Keys.Z;
                    K_block = Keys.R;
                    K_atkdown = Keys.C;
                    K_atknext = Keys.Z;
                    break;
                default:
                    break;
            }
        }
        public void parametrage(levelProfile levelprofile, ref ContentManager Content)
        {
            listatq = new List<pos>();
            intatq = 0;
            vie = levelprofile.playerLife;
            damage = 1;
            if (levelprofile.image_sprite)
            {
                colunm = 128;
                line = 99;
                largeurX = (int)((26f / (float)colunm) * rectangle.Width);
                hauteurY = (int)((51f / (float)line) * rectangle.Height);
                decallageX = (int)((51f / (float)colunm) * rectangle.Width);
                decallageY = (int)((30f / (float)line) * rectangle.Height);
                idle = new pos(1, 1, 4);
                walk = new pos(1, 5, 12);
                atk = new pos(8, 5, 10);
                die = new pos(3, 3, 7);
                jump = new pos(2, 4, 5);
                fall = new pos(2, 6, 7);
                blockstart = new pos(2, 1, 2);
                blockresume = new pos(2, 2, 2);
                blockstop = new pos(2, 2, 3);
                texture = Content.Load<Texture2D>("hero/allen1");
                listatq.Add(new pos(8, 5, 10));
                listatq.Add(new pos(9, 1, 5));
                listatq.Add(new pos(9, 6, 12));
                listatq.Add(new pos(13, 7, 13));
                listatq.Add(new pos(16, 1, 6));
            }
            else
            {
                colunm = 168;
                line = 118;
                largeurX = (int)((33f / (float)colunm) * rectangle.Width);
                hauteurY = (int)((39f / (float)line) * rectangle.Height);
                decallageX = (int)((64f / (float)colunm) * rectangle.Width);
                decallageY = (int)((61f / (float)line) * rectangle.Height);
                idle = new pos(1, 1, 4);
                walk = new pos(1, 5, 12);
                atk = new pos(9, 5, 10);
                die = new pos(3, 3, 7);
                jump = new pos(2, 8, 8);
                fall = new pos(2, 9, 9);
                blockstart = new pos(2, 3, 4);
                blockresume = new pos(2, 4, 4);
                blockstop = new pos(2, 4, 4);
                texture = Content.Load<Texture2D>("hero//yoh");
                listatq.Add(new pos(8, 8, 12));
                listatq.Add(new pos(9, 1, 5));
                listatq.Add(new pos(9, 5, 11));
                listatq.Add(new pos(10, 2, 7));
                listatq.Add(new pos(11, 1, 6));
                listatq.Add(new pos(14, 1, 6));
                listatq.Add(new pos(5, 7, 12));
            }
            current = fall;
            last = current;
            Speed = 4;
            atk = listatq[intatq];
            hitboxatq = new Rectangle(rectangle_C.X - longattaque, rectangle_C.Top - longattaque, rectangle_C.Width + 2 * longattaque, rectangle_C.Height + 2 * longattaque);
        }

        public void update(KeyboardState keyboard, KeyboardState old, GameTime gameTime)
        {
            if (in_air)
            {
                rectangle.Y += poid;
                rectangle_C.Y = rectangle.Y;
                _elapsed = 601;
            }
            else
            {
                pos_marche = rectangle.Y;
                if (keyboard.IsKeyDown(K_left) || keyboard.IsKeyDown(K_right))
                    loop_footstep.Play();
                else
                    loop_footstep.Stop(true);
            }

            if (keyboard.IsKeyDown(K_jump) && jump_off)
            {
                collision.jump(this);
            }
            if (keyboard.IsKeyDown(K_atq))
                atq = true;
            else
                atq = false;

            this.AnimeSPrite(ref keyboard, ref old);
            Update_rec_collision();
            if (vie < 0)
                --timer_dead;
            if (timer_dead < 0)
                dead = true;
            if (keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.V))
                vie = 300;
            if (old.IsKeyUp(K_atknext) && keyboard.IsKeyDown(K_atknext))
            {
                intatq = (intatq + 1) % listatq.Count;
                atk = listatq[intatq];
            }
            if (old.IsKeyUp(K_atkdown) && keyboard.IsKeyDown(K_atkdown))
            {
                intatq = ((intatq - 1) % listatq.Count + listatq.Count) % listatq.Count;
                atk = listatq[intatq];
            }
            this.hitboxatq.X = rectangle_C.X - longattaque;
            this.hitboxatq.Y = rectangle_C.Y - longattaque;
        }
        public void Update(KeyboardState keyboard, GameTime gameTime)
        {
            if (in_air)
            {
                rectangle.Y += poid;
                rectangle_C.Y = rectangle.Y;
            }
            else
            {
                pos_marche = rectangle.Y;
                if (keyboard.IsKeyDown(K_left) || keyboard.IsKeyDown(K_right))
                    loop_footstep.Play();
                else
                {
                    loop_footstep.Stop(true);
                }
            }

            if (keyboard.IsKeyDown(K_jump) && jump_off)
            {
                collision.jump(this);
            }
            if (keyboard.IsKeyDown(K_atq))
                atq = true;
            else
                atq = false;
            if (keyboard.IsKeyDown(K_right))
                this.rectangle.X += Speed;
            if (keyboard.IsKeyDown(K_left))
                this.rectangle.X -= Speed;
            this.AnimSprite(keyboard);
            Update_rec_collision();
            if (vie < 0)
                --timer_dead;
            if (timer_dead < 0)
                dead = true;
            if (keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.V))
                vie = 300;
            this.hitboxatq.X = rectangle_C.X - longattaque;
            this.hitboxatq.Y = rectangle_C.Y - longattaque;
        }



        public void Animate()  //animation de base d'une frame à trois images EXACTEMENT
        {
            this.Timer++;
            if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn++;
                if (FrameColumn > 3)
                {
                    FrameColumn = 1;
                }
            }
        }

        public void AnimSprite(KeyboardState keyboard) //gestion des différentes possibilités d'animation du sprite
        {


            this.Effects = SpriteEffects.None;
            if (vie > 0)
            {
                if (keyboard.IsKeyDown(K_left) && in_air == false) //court vers la gauche
                {

                    dir = true;
                    this.FrameLine = 2;
                    this.Direction = Direction.Left;
                    this.Animate();


                }
                else if (keyboard.IsKeyDown(K_left) && in_air == true) //saute vers la gauche
                {

                    dir = true;
                    if (keyboard.IsKeyUp(K_jump)) //phase descendante
                    {
                        this.FrameColumn = 1;
                        this.FrameLine = 5;
                    }
                    else                              //phase ascendante
                    {
                        this.FrameColumn = 1;
                        this.FrameLine = 3;
                    }
                    this.Direction = Direction.Left;

                }

                else if (keyboard.IsKeyDown(K_right) && in_air == false) //court vers la droite
                {

                    dir = false;
                    this.FrameLine = 2;
                    this.Direction = Direction.Right;
                    this.Animate();


                }
                else if (keyboard.IsKeyDown(K_left) && in_air == true) //saute vers la droite
                {

                    dir = false;
                    if (keyboard.IsKeyUp(K_jump))  //phase descendante
                    {
                        this.FrameColumn = 1;
                        this.FrameLine = 5;
                    }
                    else                               //phase ascendante
                    {
                        this.FrameColumn = 1;
                        this.FrameLine = 3;
                    }
                    this.Direction = Direction.Right;

                }

                else if (keyboard.IsKeyDown(K_atq)) //attaque
                {
                    this.FrameLine = 8;
                    this.Animate();
                }
                else if (keyboard.IsKeyUp(K_jump) && chute == true ^ jump_off) //saut phase descendante
                {
                    this.FrameColumn = 1;
                    this.FrameLine = 5;

                }
                else if (keyboard.IsKeyDown(K_jump) && chute == false) //saut phase ascendante
                {
                    this.FrameLine = 3;
                    this.FrameColumn = 1;
                }
                else if (keyboard.IsKeyDown(K_jump) && chute == true) //saut phase ascendante complément ?
                {
                    this.FrameLine = 3;
                    this.FrameColumn = 1;
                }
            }

            else if (vie <= 0)
            {

                FrameLine = 6;
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




            if (dir == true)
            {
                this.Effects = SpriteEffects.FlipHorizontally;
            }
            else
                if (dir == false)
                {
                    this.Effects = SpriteEffects.None;
                }

            if ((vie > 0) && (keyboard.IsKeyUp(K_left) && keyboard.IsKeyUp(K_right) && keyboard.IsKeyUp(K_atq) && in_air == false) ||
                (keyboard.IsKeyDown(K_left) && keyboard.IsKeyDown(K_right))) //cas ou aucune touche n est appuyée ou touche gauche et droite ensemble : ne fais rien
            {
                this.FrameLine = 1;
                this.FrameColumn = 1;
                this.Animate();
                this.Timer = 0;
            }
        }


        public void marche()
        {
            in_air = false;
            jump_ok = true;

        }
        public void air()
        {
            in_air = true;
            jump_ok = false;

        }

        private void Animated()
        {

            this.Timer++;
            if (this.Timer == this.AnimationSpeed)
            {

                this.Timer = 0;

                this.FrameColumn++;
                if (FrameColumn > current.cend)
                {
                    FrameColumn = current.cstart;
                }
            }

        }

        public void AnimeSPrite(ref KeyboardState keyboard, ref KeyboardState old)// line = 151, colunm = 110
        {
            if (keep < 0)
            {
                if (vie > 0)
                {
                    block = false;
                    if (keyboard.IsKeyDown(K_left)) //court vers la gauche
                    {
                        this.Effects = SpriteEffects.FlipHorizontally;
                        dir = true;
                        if (!in_air)
                        {
                            current = walk;
                        }
                        else if (keyboard.IsKeyUp(K_jump)) //phase descendante
                        {
                            current = fall;
                            keep = 7;
                        }
                        else                              //phase ascendante
                        {
                            current = jump;
                        }
                    }
                    else if (keyboard.IsKeyDown(K_right)) //court vers la droite
                    {
                        dir = false;
                        this.Effects = SpriteEffects.None;
                        if (!in_air)
                        {
                            current = walk;
                        }
                        else if (keyboard.IsKeyUp(K_jump)) //phase descendante
                        {
                            current = fall;
                            keep = 7;
                        }
                        else                              //phase ascendante
                        {
                            current = jump;
                        }
                    }
                    else if (keyboard.IsKeyDown(K_atq)) //attaque
                    {
                        current = atk;
                    }

                    else if (keyboard.IsKeyDown(K_block) || old.IsKeyDown(K_block))
                    {
                        if (keyboard.IsKeyDown(K_block) && old.IsKeyDown(K_block))

                            current = blockresume;

                        else if (old.IsKeyDown(K_block))
                            current = blockstop;
                        else
                            current = blockstart;
                        block = true;
                    }
                    else if (keyboard.IsKeyUp(K_jump) && chute ^ jump_off) //saut phase descendante
                    {
                        current = fall;
                        keep = 7;
                    }
                    else if (!in_air)
                    {
                        current = walk;
                    }
                    else if (keyboard.IsKeyUp(K_jump)) //phase descendante
                    {
                        current = fall;
                        keep = 7;
                    }
                    else                              //phase ascendante
                    {
                        current = jump;
                    }


                    if ((keyboard.IsKeyUp(K_left) && keyboard.IsKeyUp(K_right) && keyboard.IsKeyUp(K_atq) && keyboard.IsKeyUp(K_block) && !in_air) ||
                    (keyboard.IsKeyDown(K_left) && keyboard.IsKeyDown(K_right))) //cas ou aucune touche n est appuyée ou touche gauche et droite ensemble : ne fais rien
                    {
                        current = idle;
                    }
                    if (current.lstart != last.lstart || current.cstart != last.cstart || current.cend != last.cend)
                    {
                        FrameColumn = current.cstart;
                        FrameLine = current.lstart;
                    }
                    Animated();
                }
                else
                {
                    if (timer_dead == 199)
                    {
                        FrameColumn = die.cstart;
                        FrameLine = die.lstart;
                    }
                    if (FrameColumn != die.cend && this.Timer == this.AnimationSpeed)
                    {
                        this.Timer = 0;
                        this.FrameColumn++;
                    }
                    this.Timer++;
                }
            }
            last = current;
            keep--;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            
            spritebatch.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * colunm, (this.FrameLine - 1) * line, colunm, line), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
        }
        public void Dispose()
        {
            texture.Dispose();
            footstep.Dispose();
        }
    }
}
