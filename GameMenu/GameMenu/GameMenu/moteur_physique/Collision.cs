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

                    if ((ia.Ia_manage[i].rectangle_C.Bottom >= plato.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Left &&
                        ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato.rectangle_C.Top))
                    {
                        top = plato.rectangle_C.Top;
                        b |= true;
                    }
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

        // ia AR avec sol avec aller et retour sur meme plateform
        public void collision_ia_AR_sol(IA_Manager_max ia, ref Platform_manager platform_m)
        {
            bool b, b2, b3, b4;
            int top, pos;
            for (int i = 0; i < ia.Ia_manage.Count; ++i) // pour chaque ia
            {
                b = false; top = 0; pos = 0;
                b2 = false; b3 = false; b4 = false;
                foreach (platform plato in platform_m.plato)// pour chaque platform
                {
                    //voir si l ia est sur la plaeform
                    if ((ia.Ia_manage[i].rectangle_C.Bottom >= plato.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Left &&
                        ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato.rectangle_C.Top))
                    {
                        b |= true;
                        top = plato.rectangle_C.Top;
                        // voir si l ia doit aller ds l autre sens
                        if (ia.Ia_manage[i].rectangle_C.Right + ia.Ia_manage[i].Speed / 3 > plato.rectangle_C.Right)
                        {
                            // verification si on est sur deux plateform a la fois donc on va ds la meme direction
                            foreach (platform plato2 in platform_m.plato)
                            {
                                b4 |= (plato != plato2 && ((ia.Ia_manage[i].rectangle_C.Bottom >= plato2.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato2.rectangle_C.Left &&
                                    ia.Ia_manage[i].rectangle_C.Left <= plato2.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato2.rectangle_C.Top)));
                            }
                            b2 ^= true;
                            pos = plato.rectangle_C.Right - ia.Ia_manage[i].rectangle_C.Width - ia.Ia_manage[i].decalageX - ia.Ia_manage[i].Speed - 1;

                        }
                        if (ia.Ia_manage[i].rectangle_C.Left - ia.Ia_manage[i].Speed / 3 < plato.rectangle_C.Left)
                        {
                            foreach (platform plato2 in platform_m.plato)
                            {
                                b4 |= (plato != plato2 && ((ia.Ia_manage[i].rectangle_C.Bottom >= plato2.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato2.rectangle_C.Left &&
                                    ia.Ia_manage[i].rectangle_C.Left <= plato2.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato2.rectangle_C.Top)));

                            }
                            b3 ^= true;
                            pos = plato.rectangle_C.Left + ia.Ia_manage[i].decalageX + ia.Ia_manage[i].Speed + 1;
                        }
                    }
                }
                if (b)
                {
                    ia.Ia_manage[i].tombe = false;
                    ia.Ia_manage[i].rectangle.Y = top - ia.Ia_manage[i].rectangle_C.Height;
                }
                else
                    ia.Ia_manage[i].tombe = true;
                if (!b4&&(b2 || b3))
                {
                    ia.Ia_manage[i].dir = -ia.Ia_manage[i].dir;
                    ia.Ia_manage[i].rectangle.X = pos;
                }

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
        public bool Collision_as_mis(objet aster, sripte_V sprite)
        {

            for (int i = 0; i < sprite.bulletL.Count; ++i)
                if (aster.rectangle_C.Intersects(sprite.bulletL[i].rectangle_C))
                {
                    sprite.bulletL.RemoveAt(i);
                    sprite.bulletL[i].existe = false;
                    return true;
                }
            return false;
        }

        // collision hero avec missille ou ia avetion game over
        public void Collision_hero_missile(IA_Manager_max ia_manage, ref sripte_V sprite, ref Game1 game)
        {

                for (int i = 0; i < ia_manage.bulletL.Count ; ++i)
                    if (ia_manage.bulletL[i].rectangle.Intersects(sprite.rectangle))
                    {
                        game.ChangeState(Game1.gameState.Pause);
                    }
            
        }
        //collision IA hero action: game over
        public void col_H_IA(IA_Manager_max ia_manage, ref sripte_V sprite, ref Game1 game)
        {
            foreach (vaisseau_IA ia in ia_manage.Ia_manage)
                if (ia.rectangle_C.Intersects(sprite.rectangle_C))
                    game.ChangeState(Game1.gameState.Pause);
        }
        //collision IA allen action vie--
        public void coll_AL_IA(IA_Manager_max ia_manage, ref sprite_broillon  sprite)
        {
            for(int i =0; i<ia_manage.Ia_manage.Count ;++i)
                if (ia_manage.Ia_manage[i].rectangle_C.Intersects(sprite.rectangle_C))
                {
                    sprite.vie--;
                    ia_manage.removed(i);
                }
        }
        //collision IA missile action ia.vie --
        public void collision_ai_missile(ref sripte_V sprite, IA_Manager_max iamanage)
        {
            for (int i = 0; i < sprite.bulletL.Count; ++i)
                foreach (vaisseau_IA ai in iamanage.Ia_manage)
                    if (sprite.bulletL[i].rectangle_C.Intersects(ai.rectangle_C))
                    {
                        sprite.bulletL.RemoveAt(i);
                        ai.vie--;
                    }
        }
    }
}
