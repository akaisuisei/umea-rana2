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
        public Rectangle rectangle;
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
        bool dir=false;
        
        

        public sprite_broillon(Texture2D n_textture, Rectangle n_rectangle, Collision n_collision, ContentManager content)
        {
            texture = n_textture;
            rectangle = n_rectangle;
            poid = 10;
            in_air = false;
            jump_off = false;
            collision = n_collision;
            impulse =300;
            pos_marche = rectangle.Y;
             marchell = content.Load<Song>("jogging");
             MediaPlayer.Play(marchell);
             this.FrameLine = 1;
             this.FrameColumn = 1;
             

        }

        public void update(KeyboardState keyboard)
        {
            if (in_air)
            {
                rectangle.Y += poid;
                MediaPlayer.Pause();

            }
            else
            {
                pos_marche = rectangle.Y;
                if (keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.Right))
                    MediaPlayer.Resume();
                else
                    MediaPlayer.Pause();
            }

            if (keyboard.IsKeyDown(Keys.Space)&&jump_off )
            {
                collision.jump(this);
            }

            this.Effects = SpriteEffects.None;
            if (keyboard.IsKeyDown(Keys.Left))
            {

                dir = true;
                this.Direction = Direction.Left;
                this.Animate();              
                
                
            }
            else
            {
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    dir = false;
                    this.Direction = Direction.Right;
                    this.Animate();
                    
                    
                }
            }

            if (dir == true)
            {
                this.Effects = SpriteEffects.FlipHorizontally;
            }
            else
                if(dir == false)
            {
                this.Effects = SpriteEffects.None;
            }

            if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right))
            {
                this.FrameColumn = 1;
            }

        }

        public void Animate()
        {
            this.FrameColumn++;
            if (FrameColumn > 3)
            {
                FrameColumn = 1;
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
            spritebatch.Draw(texture, rectangle, new Rectangle((this.FrameColumn-1)*125,(this.FrameLine-1)*93, 125, 93), Color.White, 0f, new Vector2(0,0), this.Effects, 0f);
        }


    }
}
