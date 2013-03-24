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
   public class Background
    {
        public Texture2D texture;
        public Rectangle rectangle, rectangle2;
        public float couche;
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rectangle, new Rectangle(0,0,texture.Width ,texture.Height ), Color.White, 0, Vector2.Zero, SpriteEffects.None, couche);
            spritebatch.Draw(texture, rectangle2, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, couche);
        }
    }
}
