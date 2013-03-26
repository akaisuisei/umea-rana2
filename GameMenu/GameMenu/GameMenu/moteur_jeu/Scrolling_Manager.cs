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
        public List<Texture2D> texture;
        GraphicsDevice graphics;

        int windows_W, window_H;
        public Scrolling_ManagerV(int width, int heiht)
        {
            scroll = new List<Scrolling>();
            texture = new List<Texture2D>();
            window_H = heiht;
            windows_W = width;

        }

        public void Load(ContentManager Content, levelProfile levelprofile, GraphicsDevice graph)
        {


          
        
            int speed1 = levelprofile.fc_speed / 2;
            int speed2 = levelprofile.fc_speed / 3;
            string name= string.Empty ;
            for (int i = 0; i < levelprofile.background_name.Length && levelprofile.background_name[i] != '.'; ++i)
                name += levelprofile.background_name[i];
            if (name  == "backgroundT")// si c est un jeu normal
            {
                texture.Add(Content.Load<Texture2D>(levelprofile.levelname + "\\" + levelprofile.background_name));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H, 1f));
            }
            else if (name  == "background")// si c est un jeu perso
            {
                Sauveguarde save = new Sauveguarde();
                FileStream file = new FileStream(save._path + "\\SEU\\" + levelprofile.levelname +"\\"+ levelprofile.background_name, FileMode.Open, FileAccess.Read);
              //  Texture2D tex = Texture2D.FromStream(graphics, file);

                texture.Add(Texture2D.FromStream(graph, file));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H, 0.5f));

            }
            else
            {
                texture.Add(Content.Load<Texture2D>("level2\\fond"));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), levelprofile.fc_speed, window_H, 0.5f));

            }

            if (levelprofile.second_background != null)
            {
                texture.Add(Content.Load<Texture2D>("back\\" + levelprofile.second_background));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), speed1, window_H, 0.9f));
            }
            if (levelprofile.third_bacground != null)
            {
                texture.Add(Content.Load<Texture2D>("back\\" + levelprofile.third_bacground));
                scroll.Add(new Scrolling(texture[texture.Count - 1], new Rectangle(0, 0, windows_W, window_H), speed2, window_H, 1f));
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

        public void Clear()
        {
            this.scroll.Clear();
        }
    }
}
