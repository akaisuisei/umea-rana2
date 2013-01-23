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

        public bool Collision_sp_sol(ref sprite_broillon sprite,ref Rectangle sprite2)
        {
            if (sprite.rectangle.Bottom >= sprite2.Top && sprite.rectangle_Colision .Right >= sprite2.Left && sprite.rectangle_Colision .Left <= sprite2.Right && sprite.rectangle.Bottom -9<= sprite2.Top )
            {
                sprite.rectangle.Y = sprite2.Top - sprite.rectangle.Height;
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

        public bool Collision_as_mis(asteroid aster, munition muni)
        {
            if (aster.rectangle.Center.Y >= muni.rectangle.Top&& aster.rectangle.Right>=muni.rectangle.Right &&aster.rectangle.Left<muni.rectangle.Left)
            {
                muni.existe = false;
                return true;
            }
            return false;
        }

        public void  Collision_hero_missile( vaisseau_IA  ia, sripte_V  sprite, Game1 game)
        {
            for(int i=0;i<ia.bullet.bullet.Count ();++i)
                if (ia.bullet.bullet[i].rectangle.Intersects(sprite.rectangle))
                {
                    game.ChangeState(Game1.gameState.Pause);

                }
            if (ia.rectangle.Intersects(sprite.rectangle ))
                game.ChangeState(Game1.gameState.Pause);
        }
    }
}
