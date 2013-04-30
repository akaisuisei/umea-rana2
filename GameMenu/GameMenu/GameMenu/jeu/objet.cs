using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Umea_rana
{
    public class objet
    {
        public Rectangle rectangle_C, rectangle;
        protected int decallageX, decallageY, largeurX, hauteurY;
        public int poid;
        public bool tombe;
        public float vie { get; set; }
        public int decalageX { get { return decallageX; } set { decallageX = decalageX; } }
        public int decalageY { get { return decallageY; } set { decallageY = decalageY; } }


        public void Update_rec_collision()
        {
            rectangle_C.X = rectangle.X + decallageX;
            rectangle_C.Y = rectangle.Y + decallageY;
            rectangle_C.Height = hauteurY;
            rectangle_C.Width = largeurX;
        }
    }
    public class Housse
    {
        Rectangle rect;
        Texture2D texture;
        int fcspeed;
        public Housse()
        {

        }
        public void loadContent(ContentManager content, int fcspeed)
        {
            this.fcspeed = fcspeed;
            texture = content.Load<Texture2D>("ListBoxBG");
            rect = new Rectangle(2000, 200, 100, 100);
        }
        public void Update(KeyboardState keybord, Game1 game, sprite_broillon sprite)
        {
            if(keybord.IsKeyDown (Keys.Right ))
            {
                rect.X -= fcspeed;
            }
            if (keybord.IsKeyDown(Keys.Left))
            {
                rect.X += fcspeed;
            }
            if (rect.Intersects(sprite.rectangle_C))
                game.ChangeState(Game1.gameState.win);
        }
        public void draw(SpriteBatch sp)
        {
            sp.Draw(texture, rect, Color.Red );
        }
    }
}
