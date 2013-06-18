using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Threading;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Umea_rana;
using Umea_rana.LocalizedStrings;
using Microsoft.Xna.Framework.Media;

namespace Umea_rana
{
    public partial class UserControl2 : Form
    {
        string life, speed, couleur, onglet1, onglet2, onglet3, onglet4, trajectoir, align, OK, firerate, end;
        string imagefond, vitessefond, vitesseV, open, cancel, load, save, filepath, filepathlabel, onglet5;
        string scrolling, file, damage, bullet_speed, supp, musique, add, boss, bonus, bomb, missile, power, ovini;
        string aster, comete, sun, angle;
        string ennemis, naruto, eve, Truc, stab_plat, mob_plat, distance, plateformes, choix_of_boss, home, hero, allen, yoh,bandw;
        string[] playlist;
        // tag
        string t_life = "life", t_speed = "speed", t_damage = "damage", t_plateforme_nombre = "nombre plateformes", t_boss_vie = "vie", t_boss_puiss = "puissance",
            t_plat_vit = "vitesse", t_plat_dist = "distance", t_viedep = "vie";
        // type
        string type;
        System.Drawing.Color color2, color4;
        int width, height;
        float X0;
        public bool IHave_control;
        bool openF;
        string imageB;
        float openX, openY;

        string file_bosspath;
        string file_platpath;
        IA_manager_AA manage_AA;
        IA_manager_AR manage_AR;
        IA_manager_S manage_S;
        Housse safe_home;
        Platform_manager plateform;
        Sauveguarde sauve;
        savefile savefile;
        List<string> subdirectory;
        Scrolling_ManagerV scrollingM;
        Game1 game;
        int spawn;
        string ia_type;
        ContentManager Content;
        bossPLAT bossfuck;
        List<Song> listsong;
        int song;
        public void dispose()
        {
            type = null;
            subdirectory = null;
            scrollingM = null;
            ia_type = null;
            sauve = null;
            manage_AA.Dipose();
            manage_AR.Dipose();
            manage_S.Dipose();
        }
        public UserControl2()
        {
            song = 0;
            InitializeComponent();

            this.Hide();
            color4 = System.Drawing.Color.Black;
            imagefond = string.Empty;
            this.width = Screen.PrimaryScreen.Bounds.Width;
            this.height = Screen.PrimaryScreen.Bounds.Height;
            IHave_control = false;

            file_bosspath = "";
            file_platpath = "";
            X0 = 0f;
            filepath = string.Empty;
            sauve = new Sauveguarde();
            savefile = new savefile();
            savefile.ia_AA = new List<IA_AA>();
            savefile.ia_AR = new List<IA_AR>();
            savefile.ia_S = new List<IA_S>();
            savefile.levelProfile = new levelProfile();
            savefile.bonus = new List<Bonus>();
            savefile.plat_f = new List<Plat>();
            subdirectory = new List<string>();
            button9.Enabled = false;
            type = "PLA";
            Initialize();
            //8,9,14,16
            Sauvegarde_.BackColor = System.Drawing.Color.Red;

            textBox10.BackColor = System.Drawing.Color.Red;
            scrollingM = new Scrolling_ManagerV(new Microsoft.Xna.Framework.Rectangle(0, 0, width, height));
            openF = false;

            playlist = new string[4] { "", "", "", "" };
        }

        public void _show(int X, int y, string touch, int spawn)
        {
            IHave_control = true;

            openX = (float)X / (float)width;
            openY = (float)y / (float)height;

            if (X > width / 2)
                X = 0;
            else
                X = width / 2;
            if (y > height / 2)
                y = 0;
            else
                y = height / 2;
            EnableTab(Ennemis, false);
            this.Location = new System.Drawing.Point(X, y);
            this.spawn = spawn;
            this.ia_type = touch;

            switch (touch)
            {
                case "IA_AA":
                    EnableTab(Ennemis, true);
                    EnableTab(Plateformes, false);
                    enableall(false);


                    vie_p.Text = "" + savefile.ia_Kamikaze[spawn].vie;
                    vitesse_p.Text = "" + savefile.ia_Kamikaze[spawn].speed;
                    puissance_p.Text = "" + savefile.ia_Kamikaze[spawn].damage;
                    break;
                case "IA_AR":
                    EnableTab(Plateformes, false);
                    EnableTab(Ennemis, true);
                    enableall(false);
                    Naruto.Enabled = false;
                    Eve.Enabled = true;
                    Eve.Checked = true;
                    Tuc.Enabled = false;
                    vie_p.Text = "" + savefile.ia_viseur[spawn].vie;
                    vitesse_p.Text = "" + savefile.ia_viseur[spawn].speed;

                    puissance_p.Text = "" + savefile.ia_viseur[spawn].damage;


                    break;
                case "IA_S":
                    EnableTab(Plateformes, false);
                    EnableTab(Ennemis, true);

                    enableall(false);
                    Naruto.Enabled = true;
                    Eve.Enabled = false;
                    Tuc.Checked = true;
                    Tuc.Enabled = true;
                    vie_p.Text = "" + savefile.ia_S[spawn].Vie;
                    vitesse_p.Text = "" + savefile.ia_S[spawn].Speed;

                    puissance_p.Text = "" + savefile.ia_S[spawn].Puissance;

                    break;
                case "plateformes":// a finir
                    EnableTab(Plateformes, true);
                    EnableTab(Ennemis, false);

                    enableall(false);
                    Naruto.Enabled = false;
                    Eve.Enabled = false;
                    Naruto.Checked = false;


                    break;

                case "b":
                    EnableTab(Plateformes, false);
                    EnableTab(Ennemis, false);
                    enableall(false);
                    break;
                default:
                    EnableTab(Ennemis, true);
                    EnableTab(Plateformes, true);
                    enableall(true);
                    Naruto.Enabled = true;
                    Eve.Enabled = true;

                    break;
            }
            unblockAss(false);
            this.Show();
        }
        private void enableall(bool f)
        {
            EnableTab(Fond, f);
            comboBox2.Enabled = f;
            button8.Enabled = f;
            button11.Enabled = !f;
            button7.Enabled = true;
            textBox10.Enabled = f;
        }

        private static void EnableTab(TabPage page, bool enable)
        {
            EnableControls(page.Controls, enable);
        }
        private static void EnableControls(Control.ControlCollection ctls, bool enable)
        {
            foreach (Control ctl in ctls)
            {
                ctl.Enabled = enable;
                EnableControls(ctl.Controls, enable);
            }
        }

        public void update(ref IA_manager_AA manage_aa, ref IA_manager_AR manage_ar, ref IA_manager_S manage_s,
            ref KeyboardState keybord, Game1 game, ref Scrolling_ManagerV scrollM, ref Platform_manager platef, ref bossPLAT bossyeah, ref Housse finally_home)
        {
            manage_aa = this.manage_AA;
            manage_ar = this.manage_AR;
            manage_s = this.manage_S;
            platef = this.plateform;
            bossyeah = this.bossfuck;
            finally_home = safe_home;
            scrollM = this.scrollingM;
            this.game = game;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                X0 += 3;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                X0 -= 3;

        }

        public void LoadContent(IA_manager_S manage_S, IA_manager_AR manage_AR, IA_manager_AA manage_AA, Scrolling_ManagerV scrolling, ContentManager Content, Platform_manager platefom, Microsoft.Xna.Framework.Rectangle fond, bossPLAT boss, Housse safe_house)
        {

            this.manage_AA = manage_AA;
            this.manage_AR = manage_AR;
            this.manage_S = manage_S;
            this.plateform = platefom;
            this.safe_home = safe_house;

            this.scrollingM = scrolling;
            this.Content = Content;
            string[] item = sauve.filename(Content, "back");
            string[] item2 = sauve.filename(Content, "Boss");
            string[] item3 = sauve.filename(Content, "platform");
            for (int i = 0; i < item.Length; ++i)
            {
                comboBox1.Items.Add(item[i]);
                comboBox3.Items.Add(item[i]);

            }

            for (int i = 0; i < item2.Length; i++)
            {
                choix_boss.Items.Add(item2[i]);
            }

            for (int i = 0; i < item3.Length; i++)
            {
                listBox2.Items.Add(item3[i]);
            }

            textBox1_v.Tag = t_boss_vie;
            textBox2_p.Tag = t_boss_puiss;
            textBox1.Tag = t_plat_vit;
            textBox2.Tag = t_plat_dist;
            vie_dep.Tag = t_viedep;
            vie_p.Tag = t_life;
            puissance_p.Tag = t_damage;
            vitesse_p.Tag = t_speed;
            allasuite.Tag = t_plateforme_nombre;
            height = fond.Height;
            width = fond.Width;
            this.bossfuck = boss;

        }
        private void Initialize()
        {

            life = LocalizedString.Life;
            speed = LocalizedString.Speed;
            couleur = LocalizedString.Bullet_Color;
            onglet1 = LocalizedString.Shooter;
            onglet2 = LocalizedString.Sniper;
            onglet3 = LocalizedString.Kamikaze;
            onglet4 = LocalizedString.Background;
            onglet5 = LocalizedString.player;
            trajectoir = LocalizedString.Trajectory;
            align = LocalizedString.AI_align;
            OK = LocalizedString.OK;
            firerate = LocalizedString.Firerate;
            end = LocalizedString.terminated;
            imagefond = LocalizedString.Background_image;
            vitessefond = LocalizedString.Background_speed;
            vitesseV = LocalizedString.Speed;
            open = LocalizedString.Open;
            cancel = LocalizedString.Cancel;
            load = LocalizedString.Load;
            save = LocalizedString.Save;
            filepathlabel = LocalizedString.level_name;
            scrolling = LocalizedString.ScrollingVertical;
            file = LocalizedString.File;
            damage = LocalizedString.Damage;
            bullet_speed = LocalizedString.Bullet_speed;
            supp = LocalizedString.delete;
            musique = LocalizedString.music;
            add = LocalizedString.Add;
            boss = LocalizedString.Boss;
            bonus = LocalizedString.bonus;
            bomb = LocalizedString.bomb;
            missile = LocalizedString.missile;
            power = LocalizedString.power;
            ovini = LocalizedString.UFO;
            aster = LocalizedString.Asteroid;
            comete = LocalizedString.Comet;
            sun = LocalizedString.Sun;
            angle = LocalizedString.angle;
            color2 = System.Drawing.Color.Black;
            ennemis = LocalizedString.ennemis;
            naruto = LocalizedString.naruto;
            eve = LocalizedString.eve;
            Truc = LocalizedString.Tuc;
            stab_plat = LocalizedString.stab_plat;
            mob_plat = LocalizedString.mob_plat;
            distance = LocalizedString.distance;
            plateformes = LocalizedString.plateformes;
            choix_of_boss = LocalizedString.choix_boss;
            home = LocalizedString.home;
            allen = LocalizedString.allen;
            yoh = LocalizedString.yoh;
            bandw = LocalizedString.n_et_b;
            //tap page

            //tab ennemis
            Ennemis.Text = ennemis;
            Naruto.Text = naruto;
            Eve.Text = eve;
            Tuc.Text = Truc;
            Vie.Text = life;
            Puissance.Text = power;
            Vitesse.Text = speed;
            Valider.Text = OK;

            //tab Plateformes
            Plateformes.Text = plateformes;
            Plateforme_Stable.Text = stab_plat;
            Plateforme_Mobile.Text = mob_plat;
            vitesse_plat.Text = speed;
            distance_plat.Text = distance;
            Valider_p.Text = OK;

            //tab Boss
            choix_boss.Text = choix_of_boss;
            Vie_b.Text = life;
            puissance_b.Text = power;
            button1_ok.Text = OK;

            //tab Maison
            Home_point.Text = home;
            valid_maison.Text = OK;

            //tab Hero
            choix_hero.Text = hero;
            Allen.Text = allen;
            Yoh.Text = yoh;
            vie_depart.Text = life;
            valid_hero.Text = OK;

            //tab couleur
            couleur2.Text = couleur;
            incolore.Text = bandw;
            color.Text = couleur;
            valid_couleur.Text = OK;

            // +"/" + onglet2;            
            Fond.Text = onglet4;
            Sauvegarde_Chargement.Text = file;
            Music.Text = musique;

            //tab1
            Naruto.Checked = true;
            Eve.Checked = false;
            Naruto.Text = "Naruto";
            Eve.Text = "Eve";



            Vie.Text = "Vie";

            //tab2            

            // tab 3
            label2.Text = imagefond;
            button5.Text = open;
            label10.Text = vitessefond;
            button4.Text = end;
            label15.Text = scrolling + " : 1";
            label16.Text = scrolling + " : 2";
            //tab 4
            button7.Text = cancel;
            button11.Text = supp;
            button8.Text = save;
            button9.Text = load;
            label14.Text = filepathlabel;
            if (textBox10.Text == string.Empty)
                textBox10.BackColor = System.Drawing.Color.Red;
            else
                textBox10.BackColor = System.Drawing.Color.Green;

            // tab Ennemis
            if (vie_p.Text == string.Empty)
                vie_p.BackColor = System.Drawing.Color.Red;
            else
                vie_p.BackColor = System.Drawing.Color.Green;

            if (puissance_p.Text == string.Empty)
                puissance_p.BackColor = System.Drawing.Color.Red;
            else
                puissance_p.BackColor = System.Drawing.Color.Green;

            if (vitesse_p.Text == string.Empty)
                vitesse_p.BackColor = System.Drawing.Color.Red;
            else
                vitesse_p.BackColor = System.Drawing.Color.Green;

            //tab plateformes
            if (allasuite.Text == string.Empty)
                allasuite.BackColor = System.Drawing.Color.Red;
            else
                allasuite.BackColor = System.Drawing.Color.Green;

            //tab6 musique
            label23.Text = musique;
            button12.Text = open;
            button13.Text = add;
            button14.Text = supp;
            button15.Text = OK;
            //tab7 boss

            // default                             


            vie_p.Text = string.Empty; //garder
            vitesse_p.Text = string.Empty; //garder            
            //       textBox7.Text = string.Empty;
            //    textBox8.Text = string.Empty;
            //   textBox9.Text = string.Empty;
            puissance_p.Text = string.Empty; //garder         

            //tag           

            textBox2_p.Tag = t_boss_puiss;
            textBox1_v.Tag = t_boss_vie;
            textBox1.Tag = t_plat_vit;
            textBox2.Tag = t_plat_dist;
            vie_dep.Tag = t_viedep;
            vie_p.Tag = t_life; //garder
            vitesse_p.Tag = t_speed;  //garder          
            Sauvegarde_.Tag = t_speed;
            //  / textBox10. Tag = t_speed;  check name           
            puissance_p.Tag = t_damage; //garder        


            openX = 0;
            openY = 0;

            button11.Enabled = false;


            vie_p.BackColor = System.Drawing.Color.White;//garder
            vitesse_p.BackColor = System.Drawing.Color.White; //garder          
            //     textBox7.BackColor = System.Drawing.Color.White;
            //   textBox8.BackColor = System.Drawing.Color.White;
            //    textBox9.BackColor = System.Drawing.Color.White;

            puissance_p.BackColor = System.Drawing.Color.White; //garder            
            //   textBox14.BackColor = System.Drawing.Color.White; tab5            
            //    textBox16.BackColor = System.Drawing.Color.White; tab5          


            spawn = -1;
            ia_type = "kawabunga";
            comboBox2.Items.Clear();
            string[] hello = sauve.subdirectory(type);
            if (hello.Length == 0)
            {
                comboBox2.Enabled = false;
                comboBox2.Text = "vide";
            }
            else
            {
                foreach (string h in hello)
                    comboBox2.Items.Add(h);
                comboBox2.Enabled = true;
                comboBox2.Text = "dossier existant";
            }

        }

        #region radiobutoncheck
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            unblockAss(false);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            unblockAss(false);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            unblockAss(false);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            unblockAss(false);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            unblockAss(true);
        }
        #endregion
        #region texbox chek





        private void Vie_TextChanged(object sender, EventArgs e)
        {
            intcheck(vie_p);
        }

        private void damages(object sender, EventArgs e)
        {
            intcheck(puissance_p);
        }


        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            intcheck(Sauvegarde_);
        }
        private void Vitesse_TextChanged(object sender, EventArgs e)
        {
            intcheck(vitesse_p);
        }


        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            nameCheck(ref textBox10);
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button9.Enabled = true;
            button11.Enabled = true;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            Additem();
        }
        #endregion
        #region dialogopen

        private void button5_Click(object sender, EventArgs e)// tab4 showfile image
        {
            open_File_dialogue('i');
        }

        private void button12_Click(object sender, EventArgs e)// tab 6 show file musique
        {
            open_File_dialogue('m');
            // Additem();
        }
        #endregion
        #region validate button
        private void button15_Click(object sender, EventArgs e)
        {
            this.hidou();
        }// ok musique
        private void button14_Click(object sender, EventArgs e)
        {
            suppitem(listBox1);
        }// supprimer

        private void button7_Click(object sender, EventArgs e)// cancel
        {
            this.hidou();
        }
        private void button8_Click(object sender, EventArgs e)// save 
        {
            if (textBox10.BackColor == System.Drawing.Color.Green)
            {
                savefile.levelProfile.levelname = filepath;
                if (savefile.levelProfile.background_name != null)/*&&
                    savefile.levelProfile.playerLife != 0)*/
                {
                    savegame();
                    this.hidou();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) //tab4 bacground
        {
            if (Sauvegarde_.BackColor == System.Drawing.Color.Green && imageB != string.Empty)
            {
                savefile.levelProfile.background_name = imageB;
                savefile.levelProfile.fc_speed = int.Parse(Sauvegarde_.Text);
                savefile.levelProfile.second_background = (string)comboBox1.SelectedItem;
                savefile.levelProfile.third_bacground = (string)comboBox3.SelectedItem;
                scrollingLoad();
                this.hidou();
            }
        }
        private void button9_Click(object sender, EventArgs e)// load
        {

            loadgame((string)comboBox2.SelectedItem);
            textBox10.Text = (string)comboBox2.SelectedItem;
            hidou();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (spawn != -1)
            {
                delete(spawn, ia_type);
            }
            else
            {
                sauve.supp_dir((string)comboBox2.SelectedItem);
            }

            hidou();

        }// delete

        #endregion

        public void destroy()
        {
            this.Dispose();
        }
        public void hidou()
        {
            this.Hide();
            Initialize();
            IHave_control = false;
        }
        private void intcheck(TextBox texbox)
        {
            int res, n = 0;
            if ((string)texbox.Tag == t_damage)
                n = 3;
            else if ((string)texbox.Tag == t_life)
                n = 300;
            else if ((string)texbox.Tag == t_speed)
                n = 10;
            else if ((string)texbox.Tag == t_plateforme_nombre)
                n = 30;
            else if ((string)texbox.Tag == t_boss_vie)
                n = 1000;
            else if ((string)texbox.Tag == t_boss_puiss)
                n = 50;
            else if ((string)texbox.Tag == t_plat_vit)
                n = 10;
            else if ((string)texbox.Tag == t_plat_dist)
                n = 100;
            else if ((string)texbox.Tag == t_viedep)
                n = 300;


            if (texbox.Text != string.Empty && int.TryParse(texbox.Text, out res) && res <= n && res > 0)
            {
                texbox.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                texbox.BackColor = System.Drawing.Color.Red;
            }
        }
        private void intcheck(RichTextBox texbox)
        {
            int res, n = 0;
            if ((string)texbox.Tag == t_damage)
                n = 3;
            else if ((string)texbox.Tag == t_life)
                n = 300;
            else if ((string)texbox.Tag == t_speed)
                n = 10;


            if (texbox.Text != string.Empty && int.TryParse(texbox.Text, out res) && res <= n && res > 0)
            {
                texbox.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                texbox.BackColor = System.Drawing.Color.Red;
            }
        }
        private void scrollingLoad()
        {
            FileStream file;
            string name = string.Empty;
            scrollingM.Clear();
            for (int i = 0; i < imageB.Length && imageB[i] != '.'; ++i)
                name += imageB[i];
            if (name != "BackgrounD")
                file = new FileStream(imageB, FileMode.Open, FileAccess.Read);
            else
                file = new FileStream(sauve._path + "\\PLA\\" + savefile.levelProfile.levelname + "\\" + imageB, FileMode.Open, FileAccess.Read);
            if (scrollingM.count == 0)
            {
                scrollingM._Add(Texture2D.FromStream(game.GraphicsDevice, file), 0.5f);
            }
            else
            {
                scrollingM.texture[0] = Texture2D.FromStream(game.GraphicsDevice, file);
            }

            if (savefile.levelProfile.second_background != null)
                if (scrollingM.count >= 1)
                    scrollingM._Add(Content.Load<Texture2D>("back\\" + savefile.levelProfile.second_background), 0.5f);
                else
                    scrollingM.texture[1] = Content.Load<Texture2D>("back\\" + savefile.levelProfile.second_background);
            else if (savefile.levelProfile.third_bacground != null)
                if (scrollingM.count >= 1)
                    scrollingM._Add(Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground), 0.5f);
                else
                    scrollingM.texture[1] = Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground);

            if (savefile.levelProfile.third_bacground != null)
                if (scrollingM.count >= 2)
                    scrollingM._Add(Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground), 0.5f);
                else
                    scrollingM.texture[2] = Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground);

        }
        private void nameCheck(ref TextBox textbox)
        {
            string text = string.Empty;
            if (textbox.Text != string.Empty)
            {
                for (int i = 0; i < textbox.Text.Length && textbox.Text[i] != '.'; ++i)
                    text += textbox.Text[i];

                textbox.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                textbox.BackColor = System.Drawing.Color.Red;
            }
            filepath = text;
            textbox.Text = filepath;

        }

        private void savegame()
        {
            savefile.levelProfile.musique = playlist;
            sauve.save_SEU(ref savefile, "PLA");
        }

        private void loadgame(string file_name)
        {
            sauve.load_SEU(ref file_name, ref savefile, type);
            manage_S.remove_all();
            manage_AA.remove_all();
            manage_AR.remove_all();
            plateform.remove_all();


            for (int i = 0; i < savefile.ia_AA.Count; ++i)
                manage_AA.Add(savefile.ia_AA[i], i);
            for (int i = 0; i < savefile.ia_AR.Count; ++i)
                manage_AR.Add(savefile.ia_AR[i], i);
            for (int i = 0; i < savefile.ia_S.Count; ++i)
                manage_S.Add(savefile.ia_S[i], i);
            foreach (Plat p in savefile.plat_f)
                plateform.Add(p);
            Sauvegarde_.BackColor = System.Drawing.Color.Green;
            textBox10.BackColor = System.Drawing.Color.Green;

            Sauvegarde_.Text = "" + savefile.levelProfile.fc_speed;
            comboBox1.SelectedText = savefile.levelProfile.second_background;
            comboBox3.SelectedText = savefile.levelProfile.third_bacground;
            imageB = savefile.levelProfile.background_name;
            comboBox2.SelectedText = savefile.levelProfile.levelname;
            textBox10.Text = savefile.levelProfile.levelname;

            if (savefile.levelProfile.musique != null)
            {
                listsong = new List<Song>();

                listBox1.Items.AddRange(savefile.levelProfile.musique);
                foreach (string st in savefile.levelProfile.musique)
                    if (st != null && st != "")
                    {

                        listsong.Add(Song.FromUri("s", new Uri("file:" +sauve._path +"//"+type +"//"+savefile.levelProfile.levelname +"//"+ st)));
                    }
                if (listsong.Count > 0)
                {
                    MediaPlayer.Play(listsong[0]);
                    song = 0;
                }
            }
            scrollingLoad();
        }

        private void delete(int spawn, string type)
        {
           
            switch (type)
            {
                case "IA_AA":
                    savefile.ia_AA.RemoveAt(spawn);
                    manage_AA.remove_all();
                    for (int i = 0; i < savefile.ia_AA.Count; ++i)
                        manage_AA.Add(savefile.ia_AA[i], i);
                    foreach (Stalker st in manage_AA.Ia_manage)
                        st.rectangle.X -=(int) X0 ;
                    break;
                case "IA_AR":
                    savefile.ia_AR.RemoveAt(spawn);
                    manage_AR.remove_all();
                    for (int i = 0; i < savefile.ia_AR.Count; ++i)
                        manage_AR.Add(savefile.ia_AR[i], i);
                    foreach (Stalker st in manage_AR.Ia_manage)
                        st.rectangle.X -= (int)X0;
                    break;
                case "IA_S":
                    savefile.ia_S.RemoveAt(spawn);
                    manage_S.remove_all();
                    for (int i = 0; i < savefile.ia_S.Count; ++i)
                        manage_S.Add(savefile.ia_S[i], i);
                    foreach (Stalker st in manage_S.Ia_manage)
                        st.rectangle.X -= (int)X0;
                    break;
                case "plateformes":
                    savefile.plat_f.RemoveAt(spawn);
                    plateform.remove_all();
                    for (int i = 0; i < savefile.plat_f.Count; ++i)
                        plateform.Add(savefile.plat_f[i], i);
                    foreach (platform p in plateform.plato)
                        p.X -= X0;
                    break;
                default:
                    break;
            }
        }

        private void modif(int spawn, string type)
        {
            if (type == "IA_AA")
            {
                IA_AA iaa = new IA_AA();
                iaa.Vie = int.Parse(vie_p.Text);
                iaa.Speed = int.Parse(vitesse_p.Text);
                iaa.Puissance = int.Parse(puissance_p.Text);
                iaa.X = savefile.ia_AA[spawn].X;
                iaa.Y = savefile.ia_AA[spawn].Y;
                savefile.ia_AA[spawn] = iaa;
                manage_AA.remove_all();
                for (int i = 0; i < savefile.ia_AA.Count; i++)
                    manage_AA.Add(iaa, i);
                foreach (Stalker st in manage_AA.Ia_manage)
                    st.rectangle.X -= (int)X0;
            }

            else
            {
                if (type == "plateformes")
                {
                    Plat modifier = new Plat();
                    if (Plateforme_Stable.Checked)
                    {
                        modifier.type = '0';
                    }
                    else if (Plateforme_Mobile.Checked)
                    {
                        if (haut.Checked)
                            modifier.type = 'U';
                        else if (bas.Checked)
                            modifier.type = 'D';
                        else if (droite.Checked)
                            modifier.type = 'R';
                        else if (gauche.Checked)
                            modifier.type = 'L';
                        else if (GaucheHaut.Checked)
                            modifier.type = '1';
                        else if (DroitHaut.Checked)
                            modifier.type = '2';
                        else if (DroitBas.Checked)
                            modifier.type = '3';
                        else if (GaucheBas.Checked)
                            modifier.type = '4';
                    }
                    // modifier.type 
                    //  modifier.speed 
                    // il faut ajouter la distance et tout c es truc
                    modifier.nbr = int.Parse(allasuite.Text);
                    modifier.X = savefile.plat_f[spawn].X;
                    modifier.Y = savefile.plat_f[spawn].X;
                    savefile.plat_f[spawn] = modifier;
                    plateform.remove_all();
                    for (int i = 0; i < savefile.plat_f.Count; ++i)
                        plateform.Add(savefile.plat_f[i], i);
                    foreach (platform p in plateform.plato)
                        p.X -= X0;
                }
                else
                {
                    IA_AR iar = new IA_AR();
                    iar.Puissance = int.Parse(puissance_p.Text);
                    iar.Speed = int.Parse(vitesse_p.Text);
                    iar.Vie = int.Parse(vie_p.Text);

                    IA_S ias = new IA_S();
                    ias.Puissance = int.Parse(puissance_p.Text);
                    ias.Speed = int.Parse(vitesse_p.Text);
                    ias.Vie = int.Parse(vie_p.Text);

                    if (type == "IA_AR")
                    {
                        iar.X = savefile.ia_AR[spawn].X;
                        iar.Y = savefile.ia_AR[spawn].Y;
                        savefile.ia_AR[spawn] = iar;
                        manage_AR.remove_all();
                        for (int i = 0; i < savefile.ia_AR.Count; ++i)
                            manage_AR.Add(savefile.ia_AR[i], i);
                        foreach (Stalker st in manage_AR.Ia_manage)
                            st.rectangle.X -= (int)X0;
                    }
                    else if (type == "IA_S")
                    {
                        ias.X = savefile.ia_S[spawn].X;
                        ias.Y = savefile.ia_S[spawn].Y;
                        savefile.ia_S[spawn] = ias;
                        manage_S.remove_all();
                        for (int i = 0; i < savefile.ia_S.Count; ++i)
                            manage_S.Add(savefile.ia_S[i], i);
                        foreach (Stalker st in manage_S.Ia_manage)
                            st.rectangle.X -= (int)X0;
                    }
                }

            }
        }
        private void open_File_dialogue(char type)
        {
            if (!openF)
            {
                Thread thread = new Thread(() =>
                {
                    openF = true;
                    var yourForm = new OpenFileDialog();
                    if (type == 'i')
                    {
                        //yourForm.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                        yourForm.Filter = "Image Files (*.jpeg;*.png;*.jpg;*.gif)|*.jpeg;*.png;*.jpg;*.gif";
                    }
                    else if (type == 'm')
                    {
                        yourForm.Filter = "Musique Files (*.mp3;*.wave;.*wav)|*.mp3;*.wave;*.wav";
                    }
                    if (yourForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (type == 'i')
                            imageB = yourForm.FileName;
                        if (type == 'm')
                        {
                            for (int i = 0; i < playlist.Length; ++i)
                                if (playlist[i] == "")
                                {
                                    playlist[i] = yourForm.FileName;
                                    break;
                                }

                        }

                    } openF = false;

                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

            }
        }
        private void Additem()
        {
            listsong = new List<Song>();
            string res = "";
            listBox1.Items.Clear();
            foreach (string s in playlist)
            {
                res = "";
                for (int i = 0; i < s.Length; ++i)
                {
                    res += s[i];
                    if (s[i] == '\\')
                        res = "";
                }
                if (res != "")
                    listBox1.Items.Add(res);
            }
            if (listBox1.Items.Count == 4)
                button12.Enabled = false;
            else
                button12.Enabled = true;
            MediaPlayer.Stop();
            foreach (string s in playlist)
                if (s != "")
                    listsong.Add(Song.FromUri("s", new Uri("file:" + s)));
            if (listsong.Count > 0)
            {
                MediaPlayer.Play(listsong[0]);
                song = 0;
            }

        }
        private void suppitem(ListBox list)
        {
            string[] res = new string[4] { "", "", "", "" };
            for (int i = 0; i < res.Length; i++)
            {

                for (int j = 0; j < playlist[i].Length; ++j)
                {
                    res[i] += playlist[i][j];
                    if (playlist[i][j] == '\\')
                        res[i] = "";
                }
                if (res[i] == (string)list.SelectedItem)
                {
                    playlist[i] = "";
                    list.Items.Remove(list.SelectedItem);
                    if (listBox1.Items.Count == 4)
                        button12.Enabled = false;
                    else
                        button12.Enabled = true;
                    return;
                }
            }

        }
        private void unblockAss(bool b)
        {



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void vie_p_TextChanged(object sender, EventArgs e)
        {
            intcheck(vie_p);
        }

        private void puissance_p_TextChanged(object sender, EventArgs e)
        {
            intcheck(puissance_p);
        }

        private void vitesse_p_TextChanged(object sender, EventArgs e)
        {
            intcheck(vitesse_p);
        }

        private void Valider_Click_1(object sender, EventArgs e)
        {
            if (vie_p.BackColor == System.Drawing.Color.Green && puissance_p.BackColor == System.Drawing.Color.Green &&
                vitesse_p.BackColor == System.Drawing.Color.Green)//+combobox4 a veriff
            {
                if (spawn == -1)
                {



                    IA_S ias = new IA_S();
                    ias.Puissance = int.Parse(puissance_p.Text);
                    ias.Speed = int.Parse(vitesse_p.Text);
                    ias.Vie = int.Parse(vie_p.Text);
                    ias.X = openX;
                    ias.Y = openY;

                    IA_AA iaa = new IA_AA();
                    iaa.Puissance = int.Parse(puissance_p.Text);
                    iaa.Speed = int.Parse(vitesse_p.Text);
                    iaa.Vie = int.Parse(vie_p.Text);
                    iaa.X = openX;
                    iaa.Y = openY;

                    IA_AR iar = new IA_AR();
                    iar.Puissance = int.Parse(puissance_p.Text);
                    iar.Speed = int.Parse(vitesse_p.Text);
                    iar.Vie = int.Parse(vie_p.Text);
                    iar.X = openX;
                    iar.Y = openY;

                    if (Naruto.Checked)
                    {

                        manage_S.Add(ias, manage_S.Ia_manage.Count);
                        iar.X = openX + X0/ width ;
                        savefile.ia_S.Add(ias);
                        this.hidou();
                    }
                    else if (Eve.Checked)
                    {

                        manage_AR.Add(iar, manage_AR.Ia_manage.Count);
                        iar.X = openX + X0/ width ;
                        savefile.ia_AR.Add(iar);
                        this.hidou();
                    }
                    else if (Tuc.Checked)
                    {

                        manage_AA.Add(iaa, manage_AA.Ia_manage.Count);
                        iar.X = openX + X0/width ;
                        savefile.ia_AA.Add(iaa);
                        this.hidou();
                    }

                }
                else
                {
                    modif(spawn, ia_type);
                    this.hidou();
                }
            }
        }

        private void allasuite_TextChanged(object sender, EventArgs e)
        {
            intcheck(allasuite);
        }

        private void Valider_p_Click(object sender, EventArgs e)
        {
           
            if (allasuite.BackColor == System.Drawing.Color.Green &&  listBox2.SelectedItem != null  )//+combobox4 a veriff
            {
                 if(Plateforme_Stable.Checked || (Plateforme_Mobile.Checked && (textBox1.BackColor == System.Drawing.Color.Green) && (textBox2.BackColor == System.Drawing.Color.Green)))
                 {
                if (spawn == -1)
                {

                    Plat platef = new Plat();
                    platef.X = openX;
                    platef.Y = openY;
                    platef.name = (string)listBox2.SelectedItem;// se qui est selectionner par la combobox des plateforme
                    platef.nbr = int.Parse(allasuite.Text);
                    if (DroitBas.Checked)
                        platef.type = '3';
                    else if (droite.Checked)
                        platef.type = 'R';
                    else if (DroitHaut.Checked)
                        platef.type = '2';
                    else if (haut.Checked)
                        platef.type = 'U';
                    else if (bas.Checked)
                        platef.type = 'D';
                    else if (gauche.Checked)
                        platef.type = 'L';
                    else if (GaucheBas.Checked)
                        platef.type = '4';
                    else if (GaucheHaut.Checked)
                        platef.type = '1';


                    if (Plateforme_Stable.Checked)
                    {

                        plateform.Add(platef, savefile.plat_f.Count - 1);
                        platef.X = openX + X0/width ;
                        savefile.plat_f.Add(platef);
                    }

                    if (Plateforme_Mobile.Checked)
                    {
                        platef.speed = int.Parse(textBox1.Text);
                        platef.distance = float.Parse(textBox2.Text);
                        plateform.Add(platef, savefile.plat_f.Count - 1);
                        platef.X = openX + X0/ width ;
                        savefile.plat_f.Add(platef);
                    }
                }
                else
                {
                    modif(spawn, ia_type);

                }
                this.hidou();
            }
            }
        }

        private void textBox1_v_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox1_v);
        }

        private void textBox2_p_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox2_p);
        }

        private void choix_boss_SelectedIndexChanged(object sender, EventArgs e)
        {
            file_bosspath = (string)choix_boss.SelectedItem;
        }

        private void button1_ok_Click(object sender, EventArgs e)
        {
            if ((file_bosspath != "") && (textBox1_v.BackColor == System.Drawing.Color.Green && textBox2_p.BackColor == System.Drawing.Color.Green))
            {
                bossP busyguy = new bossP();
                busyguy.degat = int.Parse(textBox2_p.Text);
                busyguy.speed = 6;
                busyguy.type = file_bosspath;
                busyguy.vie = int.Parse(textBox1_v.Text);
                busyguy.X = openX;
                busyguy.Y = openY;
                bossfuck.parrame(busyguy, Content, 3);
                busyguy.X = X0/ width  + openX;
                savefile.levelProfile.bossPlatforme = busyguy;
                hidou();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox2);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            file_platpath = (string)listBox2.SelectedItem;
        }

        private void valid_maison_Click(object sender, EventArgs e)
        {

            housesafe house = new housesafe();
            house.X = openX;
            house.Y = openY;
            safe_home.parrametrage(house);
            house.X = X0/width  + openX;
            savefile.levelProfile.house = house;


            hidou();
        }

        private void vie_dep_TextChanged(object sender, EventArgs e)
        {
            savefile.levelProfile.playerLife = int.Parse(vie_dep.Text);
            intcheck(vie_dep);
        }

        private void Allen_CheckedChanged(object sender, EventArgs e)
        {
            savefile.levelProfile.image_sprite = true;
        }

        private void Yoh_CheckedChanged(object sender, EventArgs e)
        {
            savefile.levelProfile.image_sprite = false;
        }

        private void valid_hero_Click(object sender, EventArgs e)
        {
            if (vie_dep.BackColor == System.Drawing.Color.Green && (Allen.Checked || Yoh.Checked ))
            {
                hidou();
            }
        }

        private void incolore_CheckedChanged(object sender, EventArgs e)
        {
            savefile.levelProfile.IAcolor = "BandW";
        }

        private void color_CheckedChanged(object sender, EventArgs e)
        {
            savefile.levelProfile.IAcolor = "color";
        }

        private void valid_couleur_Click(object sender, EventArgs e)
        {
            if (incolore.Checked || color.Checked)
            {
                hidou();
            }
        }

       








    }
}
