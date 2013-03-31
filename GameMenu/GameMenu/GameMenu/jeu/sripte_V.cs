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
    public class sripte_V : objet
    {

        public Texture2D texture;
        //  Texture2D test;
        public List<munition> bulletL;
        Color color_V;

        int change_T;

        int height, width;
        public Bullet_manager bullet;
        public bool automatic_controlled;
        int speed, maxspeed, minspeed;
        int sizeX, sizeY, timer, sizeX1, sizeX2, sizeY1, sizeY2, timer1, timer2;
        public int power { get; set; }
        public int bomb { get; set; }
        Int16 type;

        int FrameLine;
        int FrameColumn;
        SpriteEffects Effects;
        int Timer;
        int AnimationSpeed = 14;

        int speedbullet;
        int damage;


        int FrameColumn2;


        Rectangle vect;

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

            vect = new Rectangle(n_rectangle.X, n_rectangle.Y + (int)(rectangle.Height * 0.29f), rectangle.Height, rectangle.Width);
            FrameColumn2 = 1;

            //     test = content.Load<Texture2D>("ListBoxBG");
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
        public void Load(ContentManager content, Texture2D n_texture)
        {
            texture = n_texture;
            bullet = new Bullet_manager(content.Load<Texture2D>("bullet//bullet"), new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 10, content.Load<SoundEffect>("hero//vaisseau//tir2"), color_V, width, 30);
        }

        public void Update(KeyboardState keyboard, Game1 game, KeyboardState oldkey)
        {

            if (automatic_controlled)//movement automatic de fin de jeu
                up();
            else// controlle du vaisseau
            {
                if (keyboard.IsKeyUp(Keys.T) && oldkey.IsKeyDown(Keys.T))
                    type += 3;
                else if (type < 3)
                {
                    if ((keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Left)) || keyboard.IsKeyDown(Keys.Left) && keyboard.IsKeyDown(Keys.Right))
                        move();
                    else
                    {
                        if (keyboard.IsKeyDown(Keys.Up) && (rectangle_C.Y > 0))
                            up();
                        if (keyboard.IsKeyDown(Keys.Down) && (rectangle_C.Bottom < height))
                            down();
                        if (keyboard.IsKeyDown(Keys.Right) && (rectangle_C.X + rectangle_C.Width < width))
                            right();
                        if (keyboard.IsKeyDown(Keys.Left) && (rectangle_C.X > 0))
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
            if (vie <= 0)
                game.ChangeState(Game1.gameState.Pause);

            bullet.Bullet_Update(keyboard, this, oldkey, new Vector2(0, 1), power, ref bulletL, ref sizeX, ref sizeY, ref timer);
            Update_rec_collision();
        }

        // movement et animation
        private void up()
        {
            rectangle.Y -= speed;
            move();
            vect.Y -= speed;

        }
        private void down()
        {
            if (type == 0)
            {
                FrameColumn2 = 3;
                FrameLine = 1;
                FrameColumn = 1;
                FrameLine = 1;
            }
            else
            {
                FrameColumn2 = 3;

                FrameColumn = 1;
                FrameLine = 2;
            }
            rectangle.Y += speed;
            vect.Y += speed;
        }
        private void right()
        {
            Effects = SpriteEffects.FlipHorizontally;
            if (type == 0)
            {
                FrameColumn2 = 2;
                FrameColumn = 2;
                FrameLine = 1;
            }
            else
            {
                FrameColumn2 = 2;
                FrameColumn = 2;
                FrameLine = 2;
                decallageX = (int)(0.467f * (float)rectangle.Width);
            }
            rectangle.X += speed;
            vect.X += speed;

        }
        private void left()
        {
            Effects = SpriteEffects.None;
            if (type == 0)
            {
                FrameColumn2 = 2;
                FrameColumn = 2;
                FrameLine = 1;
            }
            else
            {
                FrameColumn2 = 2;
                FrameColumn = 2;
                FrameLine = 2;
                decallageX = (int)(0.44f * (float)rectangle.Width);
            }
            rectangle.X -= speed;
            vect.X -= speed;
        }
        // animation
        private void move()
        {
            Effects = SpriteEffects.None;
            if (type == 0)
            {
                FrameColumn2 = 1;
                FrameColumn = 1;
                FrameLine = 1;
            }
            else
            {
                FrameColumn2 = 1;
                FrameColumn = 1;
                FrameLine = 2;
                decallageX = (int)(0.44f * (float)rectangle.Width);
            }
        }

        private void Transform()
        {
            Effects = SpriteEffects.None;
            decallageX = (int)(0.33f * (float)rectangle.Width);
            decallageY = (int)(0.55f * (float)rectangle.Width);
            hauteurY = (int)(0.20f * (float)rectangle.Width); ;
            largeurX = (int)(0.33f * (float)rectangle.Width);
            this.Timer++;
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
                    vect.Y -= (int)(vect.Height * 0.263f);
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
                    vect.Y += (int)(vect.Height * 0.267f);
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
        // procedure pour indiquer au vaisseau la fin de niveau
        public void gagne()
        {
            automatic_controlled = true;
            this.bullet.enableFire = false;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            bullet.Bullet_draw(spritebatch, ref bulletL);
            spritebatch.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * 300, (this.FrameLine - 1) * 400, 300, 400), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
            //       spritebatch.Draw(test ,rectangle_C,Color.Turquoise );  
            spritebatch.Draw(texture, vect, new Rectangle(600 + (this.FrameColumn2 - 1) * 300, (this.FrameLine - 1) * 200, 300, 200), color_V, 0f, Vector2.Zero, this.Effects, 0.001f);

        }
        public void Dispose()
        {
            texture.Dispose();
            bulletL = null;
            bullet.Dipose();
        }
    }
}
