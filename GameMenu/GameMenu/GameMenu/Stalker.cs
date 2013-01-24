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
    class Stalker :vaisseau_IA 
    {
        int witi;

       public int  poid;
        public Stalker(Texture2D n_textture, Rectangle n_rectangle, int wiwit)
        {
            this.rectangle = n_rectangle;
            this.rectangle_Colision = rectangle;
            this._texture = n_textture;
            Ia_color = Color.AliceBlue;
            witi = wiwit;

            
        }

        public void Update(sprite_broillon  sprite)
        {
            if (rectangle.Center.X > witi / 2)
               rectangle.X -= 1;
            else if (rectangle.X + rectangle.Width/2 < witi / 2)
                rectangle.X  += 1;

           
            if (rectangle.Center.Y  < sprite.rectangle.Center.Y)
                rectangle.Y += 1;
            else
                rectangle.Y -= 1;

            
        }
        
        public override  void draw(SpriteBatch spritback)
        {
            spritback.Draw(_texture, rectangle, Ia_color);
        }
    }
}
