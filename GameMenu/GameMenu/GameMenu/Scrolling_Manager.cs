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
    class Scrolling_ManagerV
    {
        List<Scrolling> scroll;
        List<Texture2D> texture;
        Sauveguarde save;
        int windows_W, window_H;
        public Scrolling_ManagerV(int width, int heiht)
        {
            scroll = new List<Scrolling>();
            texture = new List<Texture2D>();
            window_H = heiht;
            windows_W = width;
        }

        public void Load(ContentManager Content, levelProfile levelprofile, GraphicsDevice graphics)
        {
            int speed1 = levelprofile.fc_speed / 2;
            int speed2 = levelprofile.fc_speed / 3;

            if (levelprofile.background_name == "backgroundT")
            {
                texture.Add(Content.Load<Texture2D>(levelprofile.levelname + "\\" + levelprofile.background_name));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H));
            }
            else
            {
                FileStream file = new FileStream(save._path + "\\" + levelprofile.background_name, FileMode.Open, FileAccess.Read);
                texture.Add(Texture2D.FromStream(graphics, file));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H));

            }
            if (levelprofile.second_background != "")
            {
                texture.Add(Content.Load<Texture2D>("background\\" + levelprofile.second_background));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), speed1, window_H));
            }
            if (levelprofile.third_bacground != "")
            {
                texture.Add(Content.Load<Texture2D>("background\\" + levelprofile.third_bacground));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), speed2, window_H));
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
    }
}
