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

        // collision sprite sol fini
        public bool Collision_sp_sol(ref sprite_broillon sprite, ref Platform_manager platform_m)
        {
            foreach (platform plato in platform_m.plato)
                if (sprite.rectangle.Bottom >= plato.rectangle_C.Top && sprite.rectangle_C.Right >= plato.rectangle_C.Left &&
                    sprite.rectangle_C.Left <= plato.rectangle_C.Right && sprite.rectangle.Bottom - 9 <= plato.rectangle_C.Top)
                {
                    sprite.rectangle.Y = plato.rectangle_C.Top - sprite.rectangle.Height;

                    return true;
                }
            return false;
        }

        // collision ia_sprite sol fini
        public void collision_ia_sol(IA_Manager_max ia, ref Platform_manager platform_m) //ia stalker
        {
            bool b;
            int top = 0;
            for (int i = 0; i < ia.Ia_manage.Count; ++i)
            {
                b = false;
                foreach (platform plato in platform_m.plato)
                {
                    b |= (ia.Ia_manage[i].rectangle_C.Bottom >= plato.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Left &&
                         ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato.rectangle_C.Top);

                    if ((ia.Ia_manage[i].rectangle_C.Bottom >= plato.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Left &&
                        ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato.rectangle_C.Top))
                        top = plato.rectangle_C.Top;
                }
                if (b)
                {
                    ia.Ia_manage[i].tombe = false;
                    ia.Ia_manage[i].rectangle.Y = top - ia.Ia_manage[i].rectangle_C.Height;
                }
                else
                    ia.Ia_manage[i].tombe = true;
            }
        }

        // ia AR avec sol 
        public void collision_ia_AR_sol(IA_Manager_max ia, ref Platform_manager platform_m)
        {
            bool b, b2 = false;
            int top = 0;
            for (int i = 0; i < ia.Ia_manage.Count; ++i)
            {
                b = false;
                foreach (platform plato in platform_m.plato)
                {
                    b |= (ia.Ia_manage[i].rectangle_C.Bottom >= plato.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Left &&
                         ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato.rectangle_C.Top);

                    if ((ia.Ia_manage[i].rectangle_C.Bottom >= plato.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Left &&
                        ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato.rectangle_C.Top))
                        top = plato.rectangle_C.Top;
                    b2 ^= (ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Right ^ ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Left);
                }
                if (b)
                {
                    ia.Ia_manage[i].tombe = false;
                    ia.Ia_manage[i].rectangle.Y = top - ia.Ia_manage[i].rectangle_C.Height;
                }
                else
                    ia.Ia_manage[i].tombe = true;
                if (b2) ia.Ia_manage[i].dir = -ia.Ia_manage[i].dir;
            }


        }


        // saut non fini
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


        // collision objet missible
        public bool Collision_as_mis(objet aster, sripte_V  sprite)
        {
          
            for (int i = 0; i < sprite.bullet.bullet .Count ; ++i)
                if (aster.rectangle_C.Intersects(sprite.bullet .bullet[i].rectangle_C))
                {
                    sprite.bullet .bullet.RemoveAt(i);
                    sprite.bullet .bullet[i].existe = false;
                    return true;
                }
            return false;
        }

        // collision hero avec missille ou ia
        public void Collision_hero_missile( IA_Manager_max  ia_manage, ref sripte_V sprite, ref Game1 game)
        {
            foreach (vaisseau_IA ia in ia_manage.Ia_manage)
            {
                for (int i = 0; i < ia.bullet.bullet.Count(); ++i)
                    if (ia.bullet.bullet[i].rectangle.Intersects(sprite.rectangle))
                    {
                        game.ChangeState(Game1.gameState.Pause);
                    }
            }
        }

        public void col_H_IA( IA_Manager_max  ia_manage, ref sripte_V sprite, ref Game1 game)
        {
            foreach (vaisseau_IA ia in ia_manage.Ia_manage)
               if( ia.rectangle_C.Intersects(sprite.rectangle_C ))
                   game.ChangeState(Game1.gameState.Pause );
        }

    }
}
