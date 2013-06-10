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
        int backgeoundspeed, width_pla, heith_pla, window_H, window_W;
        public List<platform> plato;
        Dictionary<string, Texture2D> texture2;
        public Platform_manager(Texture2D n_texture, float width_pla, float heith_pla, int backgroundspeed, int window_H, int window_W)
        {
            this.texture = n_texture;
            this.backgeoundspeed = backgroundspeed;
            this.width_pla = (int)width_pla;
            this.heith_pla = (int)heith_pla;
            this.window_H = window_H;
            this.window_W = window_W;
            plato = new List<platform>();
        }
        public Platform_manager(Dictionary<string, Texture2D> texture, float width_pla, float heith_pla, int window_H, int window_W)
        {
            this.texture2 = texture;
            this.width_pla = (int)width_pla;
            this.heith_pla = (int)heith_pla;
            this.window_H = window_H;
            this.window_W = window_W;
            plato = new List<platform>();
        }

        public void parrametrage(savefile save)
        {
            backgeoundspeed = save.levelProfile.fc_speed;
        }


        public void Update(KeyboardState keyboard)
        {
            foreach (platform platoo in plato)
            {
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    platoo.X -= backgeoundspeed;
                }
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    platoo.X += backgeoundspeed;
                }
                platoo.X += platoo.dir * platoo.direction.X * platoo.speed;
                platoo.X += platoo.dir * platoo.direction.Y * platoo.speed;
                platoo.rectangle.X = (int)platoo.X;
                platoo.rectangle.Y = (int)platoo.Y;

             

                --platoo.parcouru;
                if (platoo.parcouru <= 0)
                {
                    platoo.dir *= -1;
                    platoo.parcouru = platoo.distance;
                } 
                platoo.rectangle_C = platoo.rectangle;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (platform patate in plato)
            {
                spritebatch.Draw(texture, patate.rectangle, Color.White);
            }
        }
        public void Draw2(SpriteBatch spritebatch)
        {
            foreach (platform patate in plato)
            {
                spritebatch.Draw(texture2[patate.name], patate.rectangle, Color.White);
            }
        }


        public void Add(float X, float Y, int number)
        {
            int x2 = (int)(X * window_W), y2 = (int)(Y * window_H);

            for (int i = 0; i < number; ++i)
            {
                plato.Add(new platform(texture, new Rectangle(x2 + i * width_pla, y2, width_pla, heith_pla), backgeoundspeed));
            }
        }

        public void Add(Plat platform)
        {
            int x2 = (int)(platform.X * window_W), y2 = (int)(platform.Y * window_H);

            for (int i = 0; i < platform.nbr; ++i)
            {
                plato.Add(new platform(new Rectangle(x2 + i * width_pla, y2, width_pla, heith_pla), (int)(platform.distance * window_W), platform));
            }
        }

    }
}
