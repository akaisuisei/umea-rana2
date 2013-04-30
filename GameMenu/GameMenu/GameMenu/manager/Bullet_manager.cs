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
  
        Rectangle rectangle;
        SoundEffect soundeffect;
        public bool enableFire;
        int width;
        int time1, tem2 = 0;
        Color colo;
        int speed;
        Keys keyatq;
        public Bullet_manager(Rectangle n_rectangle, int nb, int speed, SoundEffect n_soundeffect, Color colo,int width, int timer)
        {
         
    
            rectangle = n_rectangle;

            this.width = width;
            this.speed = speed;
            soundeffect = n_soundeffect;
            time1 = timer ;
            enableFire = true;
            this.colo = colo;
        }
        public Bullet_manager(Rectangle n_rectangle, int nb, int speed, SoundEffect n_soundeffect, Color colo, int width, int timer, Keys atq)
        {


            rectangle = n_rectangle;

            this.width = width;
            this.speed = speed;
            soundeffect = n_soundeffect;
            time1 = timer;
            enableFire = true;
            this.colo = colo;
            keyatq = atq;
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
                    new munition( new Rectangle(sprite.rectangle.Center.X , sprite.rectangle.Bottom , sprite.rectangle.Width / 4, sprite.rectangle.Height/2), speed, vise, colo));
                        break ;
                    case 2:
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        break;
                    case 3:
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X + sprite.rectangle.Width / 2 - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(1f, -1f), colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(-1f, -1f), colo));
                        break;
                    default :
                       bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo)); 
                  bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(1f, -1f), colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(-1f, -1f), colo));
                         break;
            }
              //  soundeffect.Play(); //lance un son lors du tire

            }
            tem2--;


        }

        public void Bullet_Update(KeyboardState keyboard, sripte_V sprite, KeyboardState oldkey, Vector2 vise, int nb, ref List<munition> bullet,ref int sizeX,ref int sizeY,ref int timer)
        {
            if (timer != time1)
                time1 = timer;
            if (keyboard.IsKeyDown(keyatq )  && tem2 <= 0 && enableFire) //autorisation de tire
            {

                tem2 = time1;
                switch (nb)
                {
                    case 1:
                        bullet.Add(
                            new munition( new Rectangle(sprite.rectangle_C.Center.X  - sizeX/2, sprite.rectangle_C.Center.Y , sizeX, sizeY), speed, vise, colo));
                        break;
                    case 2:
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle_C.Left - sizeX/2 , sprite.rectangle_C.Center.Y , sizeX, sizeY), speed, vise, colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle_C.Right -sizeX/2 , sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                        break;
                    case 3:
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle_C.Center.X -sizeX/2 , sprite.rectangle_C.Center.Y ,  sizeX, sizeY), speed, vise, colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle_C.Left - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, new Vector2(1f, 1f), colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle_C.Right - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, new Vector2(-1f, 1f), colo));
                        break;
                    default: 
                        bullet.Add(
                     new munition( new Rectangle(sprite.rectangle_C.Left - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle_C.Right - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                     bullet.Add(
                         new munition( new Rectangle(sprite.rectangle_C.Left - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, new Vector2(1f, 1f), colo));
                        bullet.Add(
                    new munition( new Rectangle(sprite.rectangle_C.Right - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, new Vector2(-1f, 1f), colo));
                    

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
      
    }
}
