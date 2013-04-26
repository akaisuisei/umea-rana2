using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Umea_rana
{
    struct boss
    {
        public char type { get; set; }
    }
    class Boss : vaisseau_IA 
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
        char type;
        int touch, touches, last_vie;


        public Boss( Rectangle rectangle, Collision collision)
        {
            this.collision = collision;
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
        }
        public void Load(ContentManager Content)
        {
            string st="";
            switch (type )
            {
                default :
                    break ;
            }
            texture = Content.Load<Texture2D>(st);
        }
        public void param(boss b)
                {
            this.type = b.type;
            switch (type)
            {
                default :
                    break;
            }
        }
        public void Update(sprite_broillon  sprite,KeyboardState keyboard)
        {
            // condition de proximiter
            switch (type)
            {
                case '1':
                    if (touched())
                        current = walk;
                    else
                    {

                    }
                    break;
                default:
                    break;
            }
            Update_rec_collision();
            AnimeSPrite();
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
        private bool touched()
        {
            if (last_vie != vie||touch >0)
            {
                if (touch <= 0)
                    touch = touches;
                return true;
            }
            else
                return false;
        }
        public void AnimeSPrite()
        {
            if (vie > 0)
            {
                if (touched()) //court vers la gauche
                {
                    this.Effects = SpriteEffects.FlipHorizontally;
                    dir = true;
                    if (!in_air)
                    {
                        current = walk;
                    }
                    else
                        if (true) //phase descendante
                        {
                            current = fall;
                        }
                        else                              //phase ascendante
                        {
                            current = jump;
                        }
                }
                else if (true) //court vers la droite
                {
                    dir = false;
                    this.Effects = SpriteEffects.None;
                    if (!in_air)
                    {
                        current = walk;
                    }
                    else
                        if (true) //phase descendante
                        {
                            current = fall;
                        }
                        else                              //phase ascendante
                        {
                            current = jump;
                        }
                }
                else if (true) //attaque
                {
                    current = atk;
                }
                else if (true && chute ^ jump_off) //saut phase descendante
                {
                    current = fall;
                }
                else
                    if (!in_air)
                    {
                        current = walk;
                    }
                    else
                        if (true) //phase descendante
                        {
                            current = fall;
                        }
                        else                              //phase ascendante
                        {
                            current = jump;
                        }


                if (true) //cas ou aucune touche n est appuyée ou touche gauche et droite ensemble : ne fais rien
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
            last = current;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * colunm, (this.FrameLine - 1) * line, colunm, line), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
        }

        public void dispose()
        {
            texture.Dispose();
        }
    }
}
