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
using Umea_rana;

namespace Umea_rana
{
    public class Collision
    {

        public Collision()
        {
        }

        public bool Collision_sp_sol(sprite_broillon sprite, Rectangle sprite2)
        {
            if (sprite.rectangle.Bottom >= sprite2.Top && sprite.rectangle.Right > sprite2.Left && sprite.rectangle.Left < sprite2.Right && sprite.rectangle.Bottom <= sprite2.Bottom)
            {
                sprite.jump_off = true;
                return true;
            }
            return false;
        }

        public void jump(sprite_broillon sprite)
        {
            int i = 10;
            if (sprite.rectangle.Y >= sprite.pos_marche - sprite.impulse)
            {
                sprite.rectangle.Y -= (i + sprite.poid);
            }
            if (sprite.rectangle.Y == sprite.pos_marche - sprite.impulse)
                sprite.jump_off = false;
        }

    }
}
