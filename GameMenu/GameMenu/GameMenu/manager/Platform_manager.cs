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
   public class Platform_manager
    {
        Texture2D texture;
        int speed,width_pla,heith_pla, window_H, window_W;
        public List<platform> plato;

       public Platform_manager(Texture2D n_texture,float  width_pla, float  heith_pla,int speed, int window_H, int window_W)
        {
            this.texture = n_texture;
            this.speed = speed;
            this.width_pla =(int) width_pla;
            this.heith_pla = (int)heith_pla;
            this.window_H = window_H;
            this.window_W = window_W;
            plato = new List<platform>();
        }

       public void Update(KeyboardState keyboard)
       {
           foreach(platform platoo in plato)
           {
               platoo.update(keyboard);
           }
       }

       public void Draw(SpriteBatch spritebatch)
       {
           foreach (platform  patate in plato )
           {
               patate.Draw(spritebatch);
           }
       }

       public void Add(float  X, float  Y, int number)
       {
           int x2 = (int)(X * window_W), y2 = (int)(Y * window_H);

           for (int i = 0; i < number; ++i)
           {
               plato.Add(new platform(texture, new Rectangle(x2+i*width_pla , y2 , width_pla, heith_pla), speed));
           }
       }

       public void Add(Plat platform)
       {
           int x2 = (int)(platform.X * window_W), y2 = (int)(platform.Y * window_H);

           for (int i = 0; i < platform.nbr; ++i)
           {
               plato.Add(new platform(texture, new Rectangle(x2+i*width_pla , y2 , width_pla, heith_pla), speed));
           }
       }

    }
}
