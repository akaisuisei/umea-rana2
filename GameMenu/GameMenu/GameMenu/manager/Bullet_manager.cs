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
        int time1, tem2 = 0, time3;
        Color colo;
        int speed;
        Keys keyatq;
        Random rdn;
        float angle;
        public Bullet_manager(Rectangle n_rectangle, int nb, int speed, SoundEffect n_soundeffect, Color colo, int width, int timer)
        {


            rectangle = n_rectangle;

            this.width = width;
            this.speed = speed;
            soundeffect = n_soundeffect;
            time1 = timer;
            enableFire = true;
            this.colo = colo;
            rdn = new Random();
            time3 = 0;
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
            rdn = new Random();
            time3 = 0;
        }


        public void Bullet_Update2(vaisseau_IA sprite, Vector2 vise, int nb, ref List<munition> bullet)
        {
            if (tem2 <= 0 && enableFire) //autorisation de tire
            {

                tem2 = time1;
                switch (nb)
                {
                    case 1:
                        bullet.Add(
                            new munition(new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Bottom, sprite.rectangle.Width / 4, sprite.rectangle.Height / 2), speed, vise, colo));
                        break;
                    case 2:
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle.Center.X - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        break;
                    case 3:
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle.Center.X + sprite.rectangle.Width / 2 - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(1f, -1f), colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(-1f, -1f), colo));
                        break;
                    default:
                        bullet.Add(
                     new munition(new Rectangle(sprite.rectangle.Center.X - sprite.rectangle.Width / 8, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, vise, colo));
                        bullet.Add(
                          new munition(new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(1f, -1f), colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle.Center.X, sprite.rectangle.Top - sprite.rectangle.Height / 2, sprite.rectangle.Width / 4, sprite.rectangle.Height), speed, new Vector2(-1f, -1f), colo));
                        break;
                }
                //  soundeffect.Play(); //lance un son lors du tire

            }
            tem2--;


        }
        public void patternpdate(Boss boss, spripte_V J1, ref List<munition> munition)
        {
            rdn = new Random();
            if (tem2 <= 0 && enableFire)
            {
                tem2 = time1;
                munition.Add(new munition(new Rectangle(boss.rectangle_C.Center.X, boss.rectangle_C.Center.Y, 15, 15),
                    5, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)), new Color(rdn.Next(0, 255), rdn.Next(0, 255), rdn.Next(0, 255))));
                munition.Add(new munition(new Rectangle(boss.rectangle_C.Center.X, boss.rectangle_C.Center.Y, 15, 15),
           5, new Vector2((float )Math.Pow ( Math.Cos(angle ),2),(float )Math .Pow( Math.Sin(angle ),2)), new Color(rdn.Next(0, 255), rdn.Next(0, 255), rdn.Next(0, 255))));
                time3++;
                if (time3 == 7)
                {
                    munition.Add(new munition(new Rectangle(boss.rectangle_C.Center.X, boss.rectangle_C.Center.Y, 15, 15),
                 3, boss.vise(J1), colo));
                    time3 = 0;
                }
              angle =(float ) (( angle + 0.3f)% (2*Math.PI ));
            }
            foreach (munition m in munition)
            {

                m.update2();
            }

            tem2--;
        }


        public void Bullet_Update(KeyboardState keyboard, spripte_V sprite, KeyboardState oldkey, Vector2 vise, int nb, ref List<munition> bullet, ref int sizeX, ref int sizeY, ref int timer)
        {
            if (timer != time1)
                time1 = timer;
            if (keyboard.IsKeyDown(keyatq) && tem2 <= 0 && enableFire) //autorisation de tire
            {

                tem2 = time1;
                switch (nb)
                {
                    case 1:
                        bullet.Add(
                            new munition(new Rectangle(sprite.rectangle_C.Center.X - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                        break;
                    case 2:
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle_C.Left - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle_C.Right - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                        break;
                    case 3:
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle_C.Center.X - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle_C.Left - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, new Vector2(1f, 1f), colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle_C.Right - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, new Vector2(-1f, 1f), colo));
                        break;
                    default:
                        bullet.Add(
                     new munition(new Rectangle(sprite.rectangle_C.Left - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle_C.Right - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, vise, colo));
                        bullet.Add(
                            new munition(new Rectangle(sprite.rectangle_C.Left - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, new Vector2(1f, 1f), colo));
                        bullet.Add(
                    new munition(new Rectangle(sprite.rectangle_C.Right - sizeX / 2, sprite.rectangle_C.Center.Y, sizeX, sizeY), speed, new Vector2(-1f, 1f), colo));


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
