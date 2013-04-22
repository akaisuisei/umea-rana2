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



    public class sprite_broillon : objet
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
        bool in_air;
        public int impulse, pos_marche, longattaque;
        Song marchell;

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

        int prout;

        public int upsidedown { get { return prout; } set { if (upsidedown < 0) prout = 1; } }

        pos idle, atk, walk, die, jump, fall;
        pos current, last;

        Texture2D test;

       
        public sprite_broillon(Texture2D n_textture, Rectangle n_rectangle, Collision n_collision, ContentManager content, char type)
        { 
            test = content.Load<Texture2D>("ListBoxBG");
            texture = n_textture;
            rectangle_C = new Rectangle(n_rectangle.X + 49, n_rectangle.Y + 4, 30, n_rectangle.Height);
            rectangle = n_rectangle;
            poid = 10;
            in_air = false;
            jump_off = false;
            collision = n_collision;
            impulse = 150;
            pos_marche = rectangle.Y;
            marchell = content.Load<Song>("hero//jogging");
            MediaPlayer.Play(marchell);

            this.FrameLine = 1;
            this.FrameColumn = 1;
            this.Timer = 0;
            vie = 100;


            upsidedown = 10;
            atq = false;
            longattaque = 17;
            dead = false;
            timer_dead = 200;

            if (type == '2')
            {
                colunm = 151;
                line = 110;
                largeurX = 22;
                hauteurY = 40;
                decallageX = 50;
                decallageY = 34;
                idle = new pos(1, 1, 4);
                walk = new pos(1, 5, 10);
                atk = new pos(9, 5, 10);
                die = new pos(3, 5, 9);
                jump = new pos(2, 8, 8);
                fall = new pos(2, 9, 9);

            }
            else
            {
                colunm = 125;
                line = 93;
                largeurX = 17;
                hauteurY = 50;
                decallageX = 55;
                decallageY = 31;
                idle = new pos(1, 1, 3);
                walk = new pos(2, 1, 3);
                atk = new pos(8, 1, 3);
                die = new pos(6, 1, 5);
                jump = new pos(3, 1, 2);
                fall = new pos(5, 1, 3);
            }
            current = fall;
            last = current;
        }

        public void update(KeyboardState keyboard)
        {
            if (in_air)
            {
                rectangle.Y += poid;
                MediaPlayer.Pause();
                rectangle_C.Y = rectangle.Y;
            }
            else
            {
                pos_marche = rectangle.Y;
                if (keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.Right))
                    MediaPlayer.Resume();
                else
                    MediaPlayer.Pause();
            }

            if (keyboard.IsKeyDown(Keys.Space) && jump_off)
            {
                collision.jump(this);
            }
            if (keyboard.IsKeyDown(Keys.X))
                atq = true;
            else
                atq = false;

            this.AnimeSPrite(ref keyboard);
            Update_rec_collision();
            if (vie < 0)
                --timer_dead;
            if (timer_dead < 0)
                dead = true;
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
                if (keyboard.IsKeyDown(Keys.Left) && in_air == false) //court vers la gauche
                {

                    dir = true;
                    this.FrameLine = 2;
                    this.Direction = Direction.Left;
                    this.Animate();


                }
                else if (keyboard.IsKeyDown(Keys.Left) && in_air == true) //saute vers la gauche
                {

                    dir = true;
                    if (keyboard.IsKeyUp(Keys.Space)) //phase descendante
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

                else if (keyboard.IsKeyDown(Keys.Right) && in_air == false) //court vers la droite
                {

                    dir = false;
                    this.FrameLine = 2;
                    this.Direction = Direction.Right;
                    this.Animate();


                }
                else if (keyboard.IsKeyDown(Keys.Left) && in_air == true) //saute vers la droite
                {

                    dir = false;
                    if (keyboard.IsKeyUp(Keys.Space))  //phase descendante
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

                else if (keyboard.IsKeyDown(Keys.X)) //attaque
                {
                    this.FrameLine = 8;
                    this.Animate();
                }
                else if (keyboard.IsKeyUp(Keys.Space) && chute == true ^ jump_off) //saut phase descendante
                {
                    this.FrameColumn = 1;
                    this.FrameLine = 5;

                }
                else if (keyboard.IsKeyDown(Keys.Space) && chute == false) //saut phase ascendante
                {
                    this.FrameLine = 3;
                    this.FrameColumn = 1;
                }
                else if (keyboard.IsKeyDown(Keys.Space) && chute == true) //saut phase ascendante complément ?
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

            if ((vie > 0) && (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.X) && in_air == false) ||
                (keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyDown(Keys.Right))) //cas ou aucune touche n est appuyée ou touche gauche et droite ensemble : ne fais rien
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

        public void AnimeSPrite(ref KeyboardState keyboard)// line = 151, colunm = 110
        {
            if (vie > 0)
            {
                if (keyboard.IsKeyDown(Keys.Left)) //court vers la gauche
                {
                    this.Effects = SpriteEffects.FlipHorizontally;
                    dir = true;
                    if (!in_air)
                    {
                        current = walk;
                    }
                    else
                        if (keyboard.IsKeyUp(Keys.Space)) //phase descendante
                        {
                            current = fall;
                        }
                        else                              //phase ascendante
                        {
                            current = jump;
                        }
                }
                else if (keyboard.IsKeyDown(Keys.Right)) //court vers la droite
                {
                    dir = false;
                    this.Effects = SpriteEffects.None;
                    if (!in_air)
                    {
                        current = walk;
                    }
                    else
                        if (keyboard.IsKeyUp(Keys.Space)) //phase descendante
                        {
                            current = fall;
                        }
                        else                              //phase ascendante
                        {
                            current = jump;
                        }
                }
                else if (keyboard.IsKeyDown(Keys.X)) //attaque
                {
                    current = atk;
                }
                else if (keyboard.IsKeyUp(Keys.Space) && chute ^ jump_off) //saut phase descendante
                {
                    current = fall;
                }
                else
                    if (!in_air)
                    {
                        current = walk;
                    }
                    else
                        if (keyboard.IsKeyUp(Keys.Space)) //phase descendante
                        {
                            current = fall;
                        }
                        else                              //phase ascendante
                        {
                            current = jump;
                        }


                if ((keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.X) && !in_air) ||
                (keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyDown(Keys.Right))) //cas ou aucune touche n est appuyée ou touche gauche et droite ensemble : ne fais rien
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
                    FrameLine = die.lstart ;
                }
                if (FrameColumn != die.cend  && this.Timer == this.AnimationSpeed)
                {
                    this.Timer = 0;
                    this.FrameColumn++;
                }
                this.Timer++;
            }
            last = current;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(test, rectangle_C, Color.Pink);
            spritebatch.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * colunm, (this.FrameLine - 1) * line, colunm, line), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
        }
        public void Dispose()
        {
            texture.Dispose();
            marchell.Dispose();
        }

    }
}
