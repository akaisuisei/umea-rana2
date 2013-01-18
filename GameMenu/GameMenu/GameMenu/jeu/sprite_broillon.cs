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


    public class sprite_broillon
    {
        Texture2D texture;
        public Rectangle rectangle, rectangle_Colision;
        Collision collision;
        public int poid;
        public bool jump_ok, jump_off;
        bool in_air;
        public int impulse, pos_marche;
        Song marchell;

        Direction Direction;
        int FrameLine;
        int FrameColumn;
        SpriteEffects Effects;
        bool dir = false;
        int Timer;
        int AnimationSpeed = 10;
        public bool chute = true;


        public sprite_broillon(Texture2D n_textture, Rectangle n_rectangle, Collision n_collision, ContentManager content)
        {
            texture = n_textture;
            rectangle_Colision = new Rectangle(n_rectangle.X + 49, n_rectangle.Y + 4, 30, n_rectangle.Height);
            rectangle = n_rectangle;
            poid = 10;
            in_air = false;
            jump_off = false;
            collision = n_collision;
            impulse = 300;
            pos_marche = rectangle.Y;
            marchell = content.Load<Song>("hero//jogging");
            MediaPlayer.Play(marchell);

            this.FrameLine = 1;
            this.FrameColumn = 1;
            this.Timer = 0;

        }

        public void update(KeyboardState keyboard)
        {


            if (in_air)
            {
                rectangle.Y += poid;
                MediaPlayer.Pause();
                rectangle_Colision.Y = rectangle.Y;
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

            this.Effects = SpriteEffects.None;
            if (keyboard.IsKeyDown(Keys.Left))
            {

                dir = true;
                this.FrameLine = 2;
                this.Direction = Direction.Left;
                this.Animate();


            }
            else

                if (keyboard.IsKeyDown(Keys.Right))
                {
                    dir = false;
                    this.FrameLine = 2;
                    this.Direction = Direction.Right;
                    this.Animate();

                }
                else if (keyboard.IsKeyDown(Keys.X))
                {
                    this.FrameLine = 8;
                    this.Animate();
                }
                else if (keyboard.IsKeyUp(Keys.Space) && chute == true ^ jump_off)
                {
                    this.FrameLine = 5;

                }
                else if (keyboard.IsKeyDown(Keys.Space) && chute == false)
                {
                    this.FrameLine = 3;
                    this.FrameColumn = 1;
                }
                else if (keyboard.IsKeyDown(Keys.Space) && chute == true)
                {
                    this.FrameLine = 3;
                    this.FrameColumn = 2;
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

            if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.X) && in_air == false)
            {
                this.FrameLine = 1;
                this.Animate();
                this.FrameColumn = 1;
                this.Timer = 0;
            }

        }



        public void Animate()
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

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rectangle, new Rectangle((this.FrameColumn - 1) * 125, (this.FrameLine - 1) * 93, 125, 93), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
        }


    }
}
