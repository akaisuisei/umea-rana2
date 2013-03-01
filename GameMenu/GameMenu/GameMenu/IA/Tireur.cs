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

    public class Tireur : vaisseau_IA
    {
        int FrameColumn;
        SpriteEffects Effects;
        int Timer;
        int AnimationSpeed = 10;


        public Tireur(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width, quaintuplet quaint)
        {
            this._texture = texture;
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;
            this.width = width;
            dir = -1;

            bullet = new Bullet_manager(content.Load<Texture2D>("bullet//bullet"), new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, quaint.bullet_Speed,
                content.Load<SoundEffect>("hero//vaisseau//tir2"), quaint.color, width, quaint.firerate);

            timer_lunch = quaint.seconde;
            _speed = quaint.speed;
            vie = quaint.vie;
            trajectory = quaint.trajectory;

            this.FrameColumn = 1;
            AnimationSpeed = 10;

        }

        public Tireur(Texture2D texture, Rectangle rectangle, ContentManager content, int height, int width, Color colo, int time_lunch)
        {
            this._texture = texture;
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            this.decallageX = 0; decallageY = 0;
            largeurX = rectangle.Width - decallageX; hauteurY = rectangle.Height - decallageY;
            this.Ia_color = colo;
            this.timer_lunch = time_lunch;
            Munition_color = colo;
            bullet = new Bullet_manager(content.Load<Texture2D>("bullet//bullet"), new Rectangle(rectangle.X, rectangle.Y, 10, 50), 15, 4, content.Load<SoundEffect>("hero//vaisseau//tir2"), Munition_color, width, 50);
            this.width = width;
            dir = -1;
            _speed = 7;
            vie = 2;

            this.FrameColumn = 1;
            AnimationSpeed = 10;
            
        }

        public void Animate()  //animation de base d'une frame à 12 images EXACTEMENT
        {
            this.Timer++;
            if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn++;
                if (FrameColumn >= 1 && FrameColumn < 13)
                {
                    this.Effects = SpriteEffects.None;
                }
                if (FrameColumn > 12 && FrameColumn < 25)
                {
                    this.Effects = SpriteEffects.FlipHorizontally;
                    FrameColumn = 1;
                }
                if (FrameColumn > 24)
                {
                    FrameColumn = 1;
                    this.Effects = SpriteEffects.None;
                }
            }
        }

        public void Update(Game1 game, ref int gameTime, ref List <munition> bulletL)
        {
            if (gameTime >= timer_lunch)
            {
                bullet.Bullet_Update2(this, new Vector2(0, -1), 1,ref bulletL );
                move_H();
                Update_rec_collision();
            }

            this.Animate();


        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, rectangle, new Rectangle((this.FrameColumn - 1) * 104, 0, 104, 132), Color.White , 0f, new Vector2(0, 0), this.Effects, 0f);
        }


    }
}
