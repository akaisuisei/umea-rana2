﻿using System;
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
        #region collision avec le sol
        // collision sprite sol fini
        public bool Collision_sp_sol(ref sprite_broillon sprite, ref Platform_manager platform_m)
        {
            foreach (platform plato in platform_m.plato)
                if (sprite.rectangle_C.Bottom >= plato.rectangle_C.Top && sprite.rectangle_C.Right >= plato.rectangle_C.Left &&
                    sprite.rectangle_C.Left <= plato.rectangle_C.Right && sprite.rectangle_C.Bottom - 9 <= plato.rectangle_C.Top)
                {

                    sprite.rectangle.Y = plato.rectangle_C.Top - sprite.decalageY - sprite.rectangle_C.Height;

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

                    if (!b && (ia.Ia_manage[i].rectangle_C.Bottom >= plato.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Left &&
                        ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - 9 <= plato.rectangle_C.Top))
                    {
                        top = plato.rectangle_C.Top;
                        b |= true;
                    }
                }
                if (b)
                {
                    ia.Ia_manage[i].tombe = false;
                    ia.Ia_manage[i].rectangle.Y = top - ia.Ia_manage[i].rectangle_C.Height - ia.Ia_manage[i].decalageY;
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
                        break;
                    }
                }
                if (b)
                {
                    ia.Ia_manage[i].tombe = false;
                    ia.Ia_manage[i].rectangle.Y = top - ia.Ia_manage[i].rectangle_C.Height-15;
                    
                }
                else
                    ia.Ia_manage[i].tombe = true;
                if (!b4 && (b2 || b3))
                {
                    ia.Ia_manage[i].dir = -ia.Ia_manage[i].dir;
                    ia.Ia_manage[i].rectangle.X = pos;
                }

            }
        }

        //collision IA allen action vie--
        public void coll_AL_IA(IA_Manager_max ia_manage, ref sprite_broillon sprite)
        {

            for (int i = 0; i < ia_manage.Ia_manage.Count; ++i)
            {
                if (ia_manage.Ia_manage[i].rectangle_C.Bottom > sprite.rectangle_C.Top && sprite.rectangle_C.Bottom > ia_manage.Ia_manage[i].rectangle_C.Top)
                {// attaque vers droite
                    if (!sprite.atq)
                    {
                        if ((ia_manage.Ia_manage[i].dir == 1 &&
                            ia_manage.Ia_manage[i].rectangle_C.Right + ia_manage.Ia_manage[i].longueur_Attaque >= sprite.rectangle_C.Left &&
                            ia_manage.Ia_manage[i].rectangle_C.Right < sprite.rectangle_C.Left) ||
                            (ia_manage.Ia_manage[i].dir == -1 &&
                            ia_manage.Ia_manage[i].rectangle_C.Left - ia_manage.Ia_manage[i].longueur_Attaque <= sprite.rectangle_C.Right &&
                            ia_manage.Ia_manage[i].rectangle_C.Left > sprite.rectangle_C.Right))
                        {
                            sprite.vie--;
                            //bool pr dire qd on attaque
                            ia_manage.Ia_manage[i].attaque = true;
                        }
                        // attaque vers la gauche-
                        else
                            ia_manage.Ia_manage[i].attaque = false;
                    }
                    else
                    {
                        if (!sprite._dir && ia_manage.Ia_manage[i].rectangle_C.Left - sprite.longattaque < sprite.rectangle_C.Right &&
                             ia_manage.Ia_manage[i].rectangle_C.Left > sprite.rectangle_C.Right)
                        {
                            --ia_manage.Ia_manage[i].vie;
                        }
                        else if (sprite._dir &&
                          ia_manage.Ia_manage[i].rectangle_C.Right + sprite.longattaque >= sprite.rectangle_C.Left &&
                                        ia_manage.Ia_manage[i].rectangle_C.Right < sprite.rectangle_C.Left)
                        {
                            --ia_manage.Ia_manage[i].vie;
                        }


                    }
                }
                else
                    ia_manage.Ia_manage[i].attaque = false;
            }
        }
        #endregion


        // saut non fini
        public void jump(sprite_broillon sprite)
        {
            int i = 10;
            if (sprite.rectangle.Y >= sprite.pos_marche - sprite.impulse)
            {
                sprite.rectangle.Y -= (i + sprite.poid);
                sprite.jump_off = true;

            }
            if (sprite.rectangle.Y == sprite.pos_marche - sprite.impulse)
            {
                sprite.jump_off = false;

            }
        }
        #region collisionshout em up
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
        // a finir
        public void h_M2P(ref IA_manager_AA AA, ref IA_manager_AR AR, ref IA_manager_K K, ref sripte_V pl1, ref sripte_V pl2)
        {
            for (int i = 0; i < AA.bulletL.Count; ++i)
            {
                if (AA.bulletL[i].rectangle_C.Intersects(pl1.rectangle_C))
                {
                    pl1.vie -= AA.bulletL[i].degat;
                    AA.bulletL.RemoveAt(i);
                }
                else if (AA.bulletL[i].rectangle_C.Intersects(pl2.rectangle_C))
                {
                    pl2.vie -= AA.bulletL[i].degat;
                    AA.bulletL.RemoveAt(i);
                }
            }
            for (int i = 0; i < AR.bulletL.Count; ++i)
            {
                if (AR.bulletL[i].rectangle_C.Intersects(pl1.rectangle_C))
                {
                    pl1.vie -= AR.bulletL[i].degat;
                    AR.bulletL.RemoveAt(i);
                }
                else if (AR.bulletL[i].rectangle_C.Intersects(pl2.rectangle_C))
                {
                    pl2.vie -= AR.bulletL[i].degat;
                    AR.bulletL.RemoveAt(i);
                }
            }
            for (int i = 0; i < AA.Ia_manage.Count; ++i)
            {
                for (int j = 0; j < pl1.bulletL.Count; ++j)
                {
                    if (pl1.bulletL[j].rectangle_C.Intersects(AA.Ia_manage[i].rectangle_C))
                        AA.Ia_manage[i].vie -= pl1.bulletL[i].degat;
                }
                for (int j = 0; j < pl2.bulletL.Count; ++j)
                {
                    if (pl2.bulletL[j].rectangle_C.Intersects(AA.Ia_manage[i].rectangle_C))
                        AA.Ia_manage[i].vie -= pl2.bulletL[i].degat;
                }
                if (AA.Ia_manage[i].rectangle_C.Intersects(pl1.rectangle_C))
                {
                    pl1.vie -= 30;
                    AA.Ia_manage.RemoveAt(i);
                }
                else if (AA.Ia_manage[i].rectangle_C.Intersects(pl2.rectangle_C))
                {
                    pl2.vie -= 30;
                    AA.Ia_manage.RemoveAt(i);
                }
            }
            for (int i = 0; i < AR.Ia_manage.Count; ++i)
            {
                for (int j = 0; j < pl1.bulletL.Count; ++j)
                {
                    if (pl1.bulletL[j].rectangle_C.Intersects(AR.Ia_manage[i].rectangle_C))
                        AR.Ia_manage[i].vie -= pl1.bulletL[i].degat;
                }
                for (int j = 0; j < pl2.bulletL.Count; ++j)
                {
                    if (pl2.bulletL[j].rectangle_C.Intersects(AR.Ia_manage[i].rectangle_C))
                        AR.Ia_manage[i].vie -= pl2.bulletL[i].degat;
                }
                if (AR.Ia_manage[i].rectangle_C.Intersects(pl1.rectangle_C))
                {
                    pl1.vie -= 30;
                    AR.Ia_manage.RemoveAt(i);
                }
                else if (AR.Ia_manage[i].rectangle_C.Intersects(pl2.rectangle_C))
                {
                    pl2.vie -= 30;
                    AR.Ia_manage.RemoveAt(i);
                }
            }
            for (int i = 0; i < K.Ia_manage.Count; ++i)
            {
                for (int j = 0; j < pl1.bulletL.Count; ++j)
                {
                    if (pl1.bulletL[j].rectangle_C.Intersects(K.Ia_manage[i].rectangle_C))
                        K.Ia_manage[i].vie -= pl1.bulletL[i].degat;
                }
                for (int j = 0; j < pl2.bulletL.Count; ++j)
                {
                    if (pl2.bulletL[j].rectangle_C.Intersects(K.Ia_manage[i].rectangle_C))
                        K.Ia_manage[i].vie -= pl2.bulletL[i].degat;
                }
                if (K.Ia_manage[i].rectangle_C.Intersects(pl1.rectangle_C))
                {
                    pl1.vie -= K.Ia_manage[i].damage;
                    AA.Ia_manage.RemoveAt(i);
                }
                else if (K.Ia_manage[i].rectangle_C.Intersects(pl2.rectangle_C))
                {
                    pl2.vie -= K.Ia_manage[i].damage;
                    K.Ia_manage.RemoveAt(i);
                }
            }

        }

        public void h_M1P(ref IA_manager_V  AA, ref IA_manager_T  AR, ref IA_manager_K K, ref sripte_V pl)
    {
        for (int i = 0; i < AA.bulletL.Count; ++i)
        {
            if (AA.bulletL[i].rectangle_C.Intersects(pl.rectangle_C))
            {
                pl.vie -= AA.bulletL[i].degat;
                AA.bulletL.RemoveAt(i);
            }
      
            
        }
        for (int i = 0; i < AR.bulletL.Count; ++i)
        {
            if (AR.bulletL[i].rectangle_C.Intersects(pl.rectangle_C))
            {
                pl.vie -= AR.bulletL[i].degat;
                AR.bulletL.RemoveAt(i);
            }
        
        }
        for (int i = 0; i < AA.Ia_manage.Count; ++i)
        {
            for (int j = 0; j < pl.bulletL.Count; ++j)
            {
                if (pl.bulletL[j].rectangle_C.Intersects(AA.Ia_manage[i].rectangle_C))
                    AA.Ia_manage[i].vie -= pl.bulletL[i].degat;
            }
            if (AA.Ia_manage[i].rectangle_C.Intersects(pl.rectangle_C))
            {
                pl.vie -= 30;
                AA.Ia_manage.RemoveAt(i);
            }

        }
        for (int i = 0; i < AR.Ia_manage.Count; ++i)
        {
            for (int j = 0; j < pl.bulletL.Count; ++j)
            {
                if (pl.bulletL[j].rectangle_C.Intersects(AR.Ia_manage[i].rectangle_C))
                    AR.Ia_manage[i].vie -= pl.bulletL[i].degat;
            }

            if (AR.Ia_manage[i].rectangle_C.Intersects(pl.rectangle_C))
            {
                pl.vie -= 30;
                AR.Ia_manage.RemoveAt(i);
            }

        }
        for (int i = 0; i < K.Ia_manage.Count; ++i)
        {
            for (int j = 0; j < pl.bulletL.Count; ++j)
            {
                if (pl.bulletL[j].rectangle_C.Intersects(K.Ia_manage[i].rectangle_C))
                    K.Ia_manage[i].vie -= pl.bulletL[i].degat;
            }

            if (K.Ia_manage[i].rectangle_C.Intersects(pl.rectangle_C))
            {
                pl.vie -= K.Ia_manage[i].damage;
                AA.Ia_manage.RemoveAt(i);
            }

        }
    }
        // collision hero avec missille ou ia avetion game over
        public void hero_missile(IA_Manager_max ia_manage, ref sripte_V sprite)
        {

            for (int i = 0; i < ia_manage.bulletL.Count; ++i)
                if (ia_manage.bulletL[i].rectangle_C.Intersects(sprite.rectangle_C))
                {
                    sprite.vie -= ia_manage.bulletL[i].degat;
                    ia_manage.bulletL.RemoveAt(i);
                }

        }
        //collision IA hero action: vie--
        public void col_H_IA(IA_Manager_max ia_manage, ref sripte_V sprite)
        {
            foreach (vaisseau_IA ia in ia_manage.Ia_manage)
                if (ia.rectangle_C.Intersects(sprite.rectangle_C))
                {
                    ia.vie = -30;
                    sprite.vie -= 30;
                }
        }
       
        //collision IA missile action ia.vie --
        public void collision_ai_missile(ref sripte_V sprite, IA_Manager_max iamanage)
        {
            for (int i = 0; i < sprite.bulletL.Count; ++i)
                foreach (vaisseau_IA ai in iamanage.Ia_manage)
                    if (sprite.bulletL[i].rectangle_C.Intersects(ai.rectangle_C))
                    {  
                        ai.vie -= sprite.bulletL[i].degat;
                        sprite.bulletL.RemoveAt(i);                     
                    }
        }
        #endregion

        public void Ovni_vaiss(ref Ovni ovnis, ref sripte_V sprite)
        {
            for (int i = 0; i < ovnis.ovni.Count; ++i)
            {
                if (ovnis.ovni[i].circle.is_in_bound(sprite.rectangle_C))
                {
                    sprite.vie += ovnis.ovni[i].vie;
                    sprite.vie -= ovnis.ovni[i].damage;
                    sprite.power += ovnis.ovni[i].power;
                    sprite.bomb += ovnis.ovni[i].bomb;
                    ovnis.ovni.RemoveAt(i);
                }
            }
        }

    }
}
