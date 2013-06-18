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
        SoundEffect sbire, normal, boss, hit;
        int time;
        bool last;
        public Collision(ContentManager content)
        {
            sbire = content.Load<SoundEffect>("explosionsbire");
            time = 0;
        }
        #region collision avec le sol
        // collision sprite sol fini
        public bool Collision_sp_sol(ref Sprite_PLA sprite, ref Platform_manager platform_m)
        {
            if ( time == 0)
            {
                foreach (platform plato in platform_m.plato)
                    if (sprite.rectangle_C.Bottom >= plato.rectangle_C.Top && sprite.rectangle_C.Right >= plato.rectangle_C.Left &&
                        sprite.rectangle_C.Left <= plato.rectangle_C.Right && sprite.rectangle_C.Bottom - 18 -plato.direction.Y *plato.speed<= plato.rectangle_C.Top)
                    {

                        sprite.rectangle.Y = (int)(plato.rectangle_C.Top - sprite.decalageY - sprite.rectangle_C.Height);
                        last = true;
                        time = (time + 1) % 2;
                        return true;
                    }
                time = (time + 1) % 2;
                last = false;
                return false;

            }
            
            time = (time + 1) % 2;
            return last;
        }

        // collision ia_sprite sol fini
        public void collision_ia_sol(IA_Manager_max ia, ref Platform_manager platform_m) //ia stalker
        {
            for (int i = 0; i < ia.Ia_manage.Count; ++i)
            {
               ia.Ia_manage[i].tombe = true;
                foreach (platform plato in platform_m.plato)
                {

                    if ( (ia.Ia_manage[i].rectangle_C.Bottom >= plato.rectangle_C.Top && ia.Ia_manage[i].rectangle_C.Right >= plato.rectangle_C.Left &&
                        ia.Ia_manage[i].rectangle_C.Left <= plato.rectangle_C.Right && ia.Ia_manage[i].rectangle_C.Bottom - ia.Ia_manage[i].poid  -4<= plato.rectangle_C.Top))
                    {
                        ia.Ia_manage[i].rectangle.Y = plato.rectangle_C.Top - ia.Ia_manage[i].rectangle_C.Height - ia.Ia_manage[i].decalageY;
                        ia.Ia_manage[i].tombe = false;
                        break;
                    }
                }                
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
                            pos = plato.rectangle_C.Left + ia.Ia_manage[i].decalageX  + 1;
                        }
                        break;
                    }
                }
                if (b)
                {
                    ia.Ia_manage[i].tombe = false;
                    ia.Ia_manage[i].rectangle.Y = top - ia.Ia_manage[i].rectangle_C.Height - ia.Ia_manage[i].decalageY ;

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
        public void coll_AL_IA(IA_Manager_max ia_manage, ref Sprite_PLA sprite)
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
                            if (sprite.block)
                                sprite.vie -= 0.5f;
                            else
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
        public void coll_AL_IA(IA_Manager_max ia_manage, ref Sprite_PLA sprite,ref Sprite_PLA p2)
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
                            if (sprite.block)
                                sprite.vie -= 0.5f;
                            else
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
                //p2
                if (ia_manage.Ia_manage[i].rectangle_C.Bottom > p2.rectangle_C.Top && p2.rectangle_C.Bottom > ia_manage.Ia_manage[i].rectangle_C.Top)
                {// attaque vers droite
                    if (!p2.atq)
                    {
                        if ((ia_manage.Ia_manage[i].dir == 1 &&
                            ia_manage.Ia_manage[i].rectangle_C.Right + ia_manage.Ia_manage[i].longueur_Attaque >= p2.rectangle_C.Left &&
                            ia_manage.Ia_manage[i].rectangle_C.Right < p2.rectangle_C.Left) ||
                            (ia_manage.Ia_manage[i].dir == -1 &&
                            ia_manage.Ia_manage[i].rectangle_C.Left - ia_manage.Ia_manage[i].longueur_Attaque <= p2.rectangle_C.Right &&
                            ia_manage.Ia_manage[i].rectangle_C.Left > p2.rectangle_C.Right))
                        {
                            if (p2.block)
                                p2.vie -= 0.5f;
                            else
                                p2.vie--;
                            //bool pr dire qd on attaque

                            ia_manage.Ia_manage[i].attaque = true;
                        }
                        // attaque vers la gauche-
                        else
                            ia_manage.Ia_manage[i].attaque = false;
                    }
                    else
                    {
                        if (!p2._dir && ia_manage.Ia_manage[i].rectangle_C.Left - p2.longattaque < p2.rectangle_C.Right &&
                             ia_manage.Ia_manage[i].rectangle_C.Left > p2.rectangle_C.Right)
                        {
                            --ia_manage.Ia_manage[i].vie;
                        }
                        else if (p2._dir &&
                          ia_manage.Ia_manage[i].rectangle_C.Right + p2.longattaque >= p2.rectangle_C.Left &&
                                        ia_manage.Ia_manage[i].rectangle_C.Right < p2.rectangle_C.Left)
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
        public void jump(Sprite_PLA sprite)
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
        public bool Collision_as_mis(objet aster, spripte_V sprite)
        {
            for (int i = 0; i < sprite.bulletL.Count; ++i)
                if (aster.rectangle_C.Intersects(sprite.bulletL[i].rectangle_C))
                {
                    sprite.bulletL.RemoveAt(i);
                    sprite.bulletL[i].existe = false;
                    sprite.scrore += 10;
                    return true;
                }
            return false;
        }
        // a finir
        public void h_M2P(ref IA_manager_AA AA, ref IA_manager_AR AR, ref IA_manager_K K, ref spripte_V pl1, ref spripte_V pl2)
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

        public void h_M1P(ref IA_manager_V AA, ref IA_manager_T AR, ref IA_manager_K K, ref spripte_V pl)
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
        public void hero_missile(IA_Manager_max ia_manage, ref spripte_V sprite)
        {

            for (int i = 0; i < ia_manage.bulletL.Count; ++i)
                if (ia_manage.bulletL[i].rectangle_C.Intersects(sprite.rectangle_C))
                {
                    sprite.vie -= ia_manage.bulletL[i].degat;
                    ia_manage.bulletL.RemoveAt(i);
                }

        }
        //collision IA hero action: vie--
        public void col_H_IA(IA_Manager_max ia_manage, ref spripte_V sprite)
        {
            foreach (vaisseau_IA ia in ia_manage.Ia_manage)
                if (ia.rectangle_C.Intersects(sprite.rectangle_C))
                {
                    ia.vie = -30;
                    sprite.vie -= 30;
                    sprite.scrore += 30;
                }
        }


        //collision IA missile action ia.vie --
        public void collision_ai_missile(ref spripte_V sprite, IA_Manager_max iamanage)
        {
            for (int i = 0; i < sprite.bulletL.Count; ++i)
                foreach (vaisseau_IA ai in iamanage.Ia_manage)
                    if (sprite.bulletL[i].rectangle_C.Intersects(ai.rectangle_C))
                    {
                        ai.vie -= sprite.bulletL[i].degat;
                        sprite.scrore += 100;
                        sprite.bulletL.RemoveAt(i);
                        sbire.Play();
                    }
        }

        public void Boss_vaiss(ref Boss boss, ref spripte_V J1,ref spripte_V J2)
        {
            if (boss.rectangle_C.Intersects(J1.rectangle_C))
                J1.vie -= 30;
            if (boss.rectangle_C.Intersects(J2.rectangle_C))
                J2.vie -= 30;
   
            for (int i = 0; i < J1.bulletL.Count; ++i)
            
                if (J1.bulletL[i].rectangle_C.Intersects(boss.rectangle_C))
                {
                    boss.vie -= J1.damage;
                    J1.scrore += 130;
                    J1.bulletL .RemoveAt(i);
                    sbire.Play();
                }
            for (int i = 0; i < J2.bulletL.Count; ++i)

                if (J2.bulletL[i].rectangle_C.Intersects(boss.rectangle_C))
                {
                    boss.vie -= J2.damage;
                    J2.scrore += 130;
                    J2.bulletL.RemoveAt(i);
                    sbire.Play();
                }
  
  
            for (int i = 0; i < boss.munition.Count; ++i)
            {
                if (boss.munition[i].rectangle_C.Intersects(J1.rectangle_C))
                {
                    J1.vie -= boss.damage;
                    J1.scrore += 1;
                    boss.munition.RemoveAt(i);

                }
                if (boss.munition[i].rectangle_C.Intersects(J2.rectangle_C))
                {
                    J2.vie -= boss.damage;
                    J2.scrore += 1;
                    boss.munition.RemoveAt(i);

                }
            }

        }

        #endregion

        public void Ovni_vaiss(ref Ovni ovnis, ref spripte_V sprite)
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
                for (int j = 0; j < sprite.bulletL.Count; ++j)
                {
                    if (ovnis.ovni[i].circle.is_in_bound(sprite.bulletL[i].rectangle_C) && ovnis.ovni[i].damage > 0)
                    {
                        ovnis.ovni.RemoveAt(i);
                    }

                }
            }
        }

        public void Bossplat_hero(ref bossPLAT boss, ref Sprite_PLA sprite, ref Platform_manager platform)
        {

            foreach (bossPLAT.Pointaction  pt in boss.ptfort_ )
                if (pt.hitbox.Intersects(sprite.rectangle_C))
                    if (sprite.block)
                        sprite.vie -= (float)boss.degat / 2;
                    else
                        sprite.vie -= boss.degat;
            foreach (bossPLAT.Pointaction pt in boss.ptfaible_ )
                if (sprite.atq)
                    if((pt.hitbox .Intersects(sprite.hitboxatq )))
                    boss.vie -= sprite.damage ;
            foreach (platform plato in platform.plato)
                if (boss.rectangle_C.Bottom >= plato.rectangle_C.Top && boss.rectangle_C.Right >= boss.rectangle_C.Left &&
                    boss.rectangle_C.Left <= plato.rectangle_C.Right && boss.rectangle_C.Bottom - 9 <= plato.rectangle_C.Top)
                {
                    boss.rectangle.Y = plato.rectangle_C.Top - boss.decalageY - boss.rectangle_C.Height;
                    boss.tombe = false;
                    break;
                }
                else
                    boss.tombe = true;

        }
        public void Bossplat_hero(ref bossPLAT boss, ref Sprite_PLA sprite,ref Sprite_PLA p2, ref Platform_manager platform)
        {

            foreach (bossPLAT.Pointaction pt in boss.ptfort_ )
            {
                if (pt.hitbox .Intersects(sprite.rectangle_C))
                    if (sprite.block)
                        sprite.vie -= (float)boss.degat / 2;
                    else
                        sprite.vie -= boss.degat;
                if (pt.hitbox .Intersects(p2.rectangle_C))
                    if (p2.block)
                        p2.vie -= (float)boss.degat / 2;
                    else
                        p2.vie -= boss.degat;
            }
            foreach (bossPLAT.Pointaction pt in boss.ptfaible_)
                if ((sprite.atq && pt.hitbox.Intersects(sprite.hitboxatq)) || (p2.atq && pt.hitbox.Intersects(p2.hitboxatq )))
                    boss.vie -= sprite.damage ;
            foreach (platform plato in platform.plato)
                if (boss.rectangle_C.Bottom >= plato.rectangle_C.Top && boss.rectangle_C.Right >= boss.rectangle_C.Left &&
                    boss.rectangle_C.Left <= plato.rectangle_C.Right && boss.rectangle_C.Bottom - 9 <= plato.rectangle_C.Top)
                {
                    boss.rectangle.Y = plato.rectangle_C.Top - boss.decalageY - boss.rectangle_C.Height;
                    boss.tombe = false;
                    break;
                }
                else
                    boss.tombe = true;

        }
    }
}
