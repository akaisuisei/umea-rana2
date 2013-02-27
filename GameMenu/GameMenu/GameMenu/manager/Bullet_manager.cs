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
    public class Bullet_manager
    {
        Texture2D texture;
        Rectangle rectangle;
        SoundEffect soundeffect;
        public bool enableFire;
        int width;
        int time1, tem2 = 0;
        Color colo;

        int speed;

        public Bullet_manager(Texture2D n_texture, Rectangle n_rectangle, int nb, int speed, SoundEffect n_soundeffect, Color colo,int width, int timer)
        {
            texture = n_texture;
            rectangle = n_rectangle;

            this.width = width;
            this.speed = speed;
            soundeffect = n_soundeffect;
            time1 = timer ;
            enableFire = true;
            this.colo = colo;
        }

        public void Bullet_Update2(vaisseau_IA  sprite, Vector2 vise, int nb,ref List<munition > bullet)
        {
            if ( tem2 <= 0 && enableFire) //autorisation de tire
            {

                tem2 = time1;
                switch(nb) 
                {
                    case 1:
                bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X , sprite.rectangle_C.Bottom , sprite.rectangle.Width / 4, sprite.rectangle.Height/2), speed, vise, colo));
                        break ;
                    case 2:
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        break;
                    case 3:
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X + sprite.rectangle.Width / 2 - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(1f, -1f), colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(-1f, -1f), colo));
                        break;
                    default :
                       bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo)); 
                  bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(1f, -1f), colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(-1f, -1f), colo));
                         break;
            }
              //  soundeffect.Play(); //lance un son lors du tire

            }
            tem2--;


        }

        public void Bullet_Update(KeyboardState keyboard, sripte_V sprite, KeyboardState oldkey, Vector2 vise, int nb, ref List<munition> bullet)
        {
            if (keyboard.IsKeyDown(Keys.Space) && oldkey.IsKeyDown(Keys.Space) && tem2 <= 0 && enableFire) //autorisation de tire
            {

                tem2 = time1;
                switch (nb)
                {
                    case 1:
                        bullet.Add(
                            new munition(texture, new Rectangle(sprite.rectangle.Left + sprite.rectangle.Width / 2 - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        break;
                    case 2:
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Left - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Right, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        break;
                    case 3:
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Left + sprite.rectangle.Width / 2 - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Left, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(1f, 1f), colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Right, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(-1f, 1f), colo));
                        break;
                    default:
                        bullet.Add(
                     new munition(texture, new Rectangle(sprite.rectangle.Left - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Right, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                          new munition(texture, new Rectangle(sprite.rectangle.Left, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(1f, 1f), colo));
                        bullet.Add(
                    new munition(texture, new Rectangle(sprite.rectangle.Right, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(-1f, 1f), colo));
                        break;
                }
                soundeffect.Play(); //lance un son lors du tire

            }
            tem2--;
            //update che chaque missile
            for (int i = 0; i < bullet.Count; i++)
            {
                bullet[i].update2();
                if (bullet[i].rectangle.Top < 0)
                    bullet.RemoveAt(i);
            }

        }


        public void Bullet_draw(SpriteBatch spritebach,ref List<munition> bullet)
        {
            for (int i = 0; i < bullet.Count; i++)
                bullet[i].Draw(spritebach);
        }

        

    }
}
