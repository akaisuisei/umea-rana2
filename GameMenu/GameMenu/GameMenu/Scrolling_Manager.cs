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
  public   class Scrolling_ManagerV
    {
     public    List<Scrolling> scroll;
     public    List<Texture2D> texture;
     GraphicsDevice graphics;

        int windows_W, window_H;
        public Scrolling_ManagerV(int width, int heiht)
        {
            scroll = new List<Scrolling>();
            texture = new List<Texture2D>();
            window_H = heiht;
            windows_W = width;
            
        }
        public Scrolling_ManagerV(int width, int heiht, GraphicsDevice graph)
        {
            scroll = new List<Scrolling>();
            texture = new List<Texture2D>();
            window_H = heiht;
            windows_W = width;
            graphics = graph;
        }

        public void Load(ContentManager Content, levelProfile levelprofile)
        {        Sauveguarde save=new Sauveguarde();
            int speed1 = levelprofile.fc_speed / 2;
            int speed2 = levelprofile.fc_speed / 3;

            if (levelprofile.background_name == "backgroundT")
            {
                texture.Add(Content.Load<Texture2D>(levelprofile.levelname + "\\" + levelprofile.background_name));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H,1f));
            }
            else if (levelprofile.background_name == "background")
            {
                FileStream file = new FileStream(save._path + "\\" + levelprofile.background_name, FileMode.Open, FileAccess.Read);
                texture.Add(Texture2D.FromStream(graphics, file));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H, 1f));

            }
            else
            {
                texture.Add(Content.Load<Texture2D>("level2\\fond"));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H, 1f));
         
            }

            if (levelprofile.second_background != "")
            {
                texture.Add(Content.Load<Texture2D>("background\\" + levelprofile.second_background));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), speed1, window_H,1f));
            }
            if (levelprofile.third_bacground != "")
            {
                texture.Add(Content.Load<Texture2D>("background\\" + levelprofile.third_bacground));
                scroll.Add(new Scrolling(texture[texture.Count -1], new Rectangle(0, 0, windows_W, window_H), speed2, window_H,1f));
            }

        }

        public void Update()
        {
            foreach (Scrolling sc in scroll)
                sc.Update();
        }
        public void Update_ophelia(KeyboardState keybord)
        {
            foreach (Scrolling sc in scroll)
                sc.update_ophelia(keybord);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Scrolling sc in scroll)
                sc.Draw(spritebatch);

        }
    }
}
