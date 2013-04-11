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
using System.IO;

namespace Umea_rana
{
    public class Scrolling_ManagerV
    {
       public List<Scrolling> scroll;
        public Texture2D[] texture;

        public Rectangle[] rec1, rec2;
        public float[] couche;
        public int[] speed { get; set; }
       public int count { get; set; }

        int windows_W, window_H;
        Rectangle rectangle;
        public Scrolling_ManagerV(Rectangle rect)
        {
   
            rec1 = new Rectangle[3];
            rec2 = new Rectangle[3];
            texture = new Texture2D[3];
            couche = new float[3];
            speed = new int[3];
            rectangle = rect;
            window_H = rect.Height ;
            windows_W = rect.Width ;
            count = 0;
        }

        public void Load(ContentManager Content, levelProfile levelprofile, GraphicsDevice graph)
        {               
            speed[1]= levelprofile.fc_speed / 2;
            speed[2] = levelprofile.fc_speed / 3;
            speed[0] = levelprofile.fc_speed;
            string name= string.Empty ;
            for (int i = 0; i < levelprofile.background_name.Length && levelprofile.background_name[i] != '.'; ++i)
            {
                
                name += levelprofile.background_name[i];
                if (levelprofile.background_name[i] == '\\')
                    name = "";
            }
            if (name  == "backgroundT")// si c est un jeu normal
            {
              
                texture[count ]=(Content.Load<Texture2D>(levelprofile.levelname + "\\" + levelprofile.background_name));
                rec1[count] = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                rec2[count] = new Rectangle(rectangle.X, -rectangle.Height, rectangle.Width, rectangle.Height);
                couche[count] = 1f;
                ++count;
            }
            else if (name  == "BackgrounD")// si c est un jeu perso
            {
                Sauveguarde save = new Sauveguarde();
                FileStream file = new FileStream(save._path + "\\SEU\\" + levelprofile.levelname +"\\"+ levelprofile.background_name, FileMode.Open, FileAccess.Read);
              texture[count] = (Texture2D.FromStream(graph, file));
                rec1[count] = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                rec2[count] = new Rectangle(rectangle.X, -rectangle.Height, rectangle.Width, rectangle.Height);
                couche[count] = 0.5f;
                
         
            //    scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H, 0.5f));
                ++count;
            }
            else
            {
                texture[count] = (Content.Load<Texture2D>("level2\\fond"));
                rec1[count] = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                rec2[count] = new Rectangle(rectangle.X, -rectangle.Height, rectangle.Width, rectangle.Height);
                couche[count] = 0.5f;
           //     scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H, 0.5f));
                ++count;
            }

            if (levelprofile.second_background != null)
            {
                texture[count] = (Content.Load<Texture2D>("back\\" + levelprofile.second_background));
                rec1[count] = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                rec2[count] = new Rectangle(rectangle.X, -rectangle.Height, rectangle.Width, rectangle.Height);
                couche[count] = 0.9f;
              //  scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), speed1, window_H, 0.9f));
                ++count;
            }
            if (levelprofile.third_bacground != null)
            {
                texture[count] = (Content.Load<Texture2D>("back\\" + levelprofile.third_bacground));
                rec1[count] = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                rec2[count] = new Rectangle(rectangle.X, -rectangle.Height, rectangle.Width, rectangle.Height);
                couche[count] = 0.1f;
             //   scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), speed2, window_H, 1f));
                ++count;
            }            
        }
        
        public void Update()
        {
            for (int i = 0; i < count; ++i)
            {
                rec1[i].Y += speed[i];
                rec2[i].Y += speed[i];
                if (rec1[i].Y >= window_H)
                    rec1[i].Y = rec2[i].Y - rec2[i].Height;
                if (rec2[i].Y >= window_H)
                    rec2[i].Y = rec1[i].Y - rectangle.Height;
            }

        }
        public void Update_ophelia(KeyboardState keybord)
        {
            for (int i = 0; i < count; ++i)
            {
                if (keybord.IsKeyDown(Keys.Up))
                {
                    rec1[i].Y += speed[i];
                    rec2[i].Y += speed[i];
                }
                if (keybord.IsKeyDown(Keys.Down))
                {
                    rec1[i].Y -= speed[i];
                    rec2[i].Y -= speed[i];
                }
                if (rec1[i].Y >= window_H)
                    rec1[i].Y = rec2[i].Y - rec2[i].Height;
                if (rec2[i].Y >= window_H)
                    rec2[i].Y = rec1[i].Y - rectangle.Height;
                if (rectangle.Bottom <= 0)
                    rec1[i].Y = rec2[i].Bottom;
                if (rec2[i].Bottom <= 0)
                    rec2[i].Y = rectangle.Bottom;
            }
    
        }

        public void Draw(SpriteBatch spritebatch)
        {
       
            for (int i = 0; i < count; ++i)
            {
                spritebatch.Draw(texture[i], rec1[i], new Rectangle(0, 0, texture[i].Width, texture[i].Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, couche[i]);
                spritebatch.Draw(texture[i], rec2[i], new Rectangle(0, 0, texture[i].Width, texture[i].Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, couche[i]);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < count ; ++i)
            {
                texture[i]=null;
            }
            rec1 = null;
            rec2 = null;
            couche = null;
            speed = null;
        }
        public void dispose()
        {
            for (int i = 0; i < count; ++i)
            {
                texture[i].Dispose();

            }
            rec1 = null;
            rec2 = null;
            couche = null;
            speed = null;
        }
    }
}
