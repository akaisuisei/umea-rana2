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
using Umea_rana.LocalizedStrings;
using Microsoft.Xna.Framework.Media;

namespace Umea_rana
{
    public partial class UserControl1 : Form
    {
        string life, speed, couleur, onglet1, onglet2, onglet3, onglet4, trajectoir, align, OK, firerate, end;
        string imagefond, vitessefond, vitesseV, open, cancel, load, save, filepath, filepathlabel, onglet5;
        string scrolling, file, damage, bullet_speed, supp, musique, add, boss, bonus, bomb, missile, power, ovini;
        string aster, comete, sun, angle;
        string[] playlist;
        // tag
        string t_life = "life", t_speed = "speed", t_damage = "damage", t_firerate = "fire", t_nbr = "nbr", t_angle = "angle";
        // type
        string type;
        System.Drawing.Color color2, color4, color5;
        int width, height;
        int seconde;
        public bool IHave_control;
        bool openF;
        string imageB;
        float openX, openY;
        IA_manager_T manage_T;
        IA_manager_V manage_V;
        IA_manager_K manage_k;
        Ovni ovni;
        Sauveguarde sauve;
        public savefile _savefile;
        List<string> subdirectory;
        Scrolling_ManagerV scrollingM;
        Game1 game;
        int spawn;
        string ia_type, selectedbos;
        ContentManager Content;
        List<Song> listsong;
        int song;

        public void dispose()
        {
            type = null;
            subdirectory = null;
            scrollingM = null;
            ovini = null;
            ovni = null;
            ia_type = null;
            sauve = null;
            manage_k.Dipose();
            manage_T.Dipose();
            manage_V.Dipose();
        }
        /// <summary>
        /// windows form pour le SEU
        /// </summary>
        public UserControl1()
        {
            selectedbos = "";
            InitializeComponent();

            this.Hide();
            color4 = System.Drawing.Color.Black;
            imagefond = string.Empty;
            this.width = Screen.PrimaryScreen.Bounds.Width;
            this.height = Screen.PrimaryScreen.Bounds.Height;
            IHave_control = false;

            seconde = 0;
            filepath = string.Empty;
            sauve = new Sauveguarde();
            _savefile = new savefile();
            _savefile.ia_Kamikaze = new List<couple>();
            _savefile.ia_tireur = new List<quaintuplet>();
            _savefile.ia_viseur = new List<quaintuplet>();
            _savefile.levelProfile = new levelProfile();
            _savefile.bonus = new List<Bonus>();
            subdirectory = new List<string>();
            button9.Enabled = false;
            type = "SEU";
            Initialize();
            //8,9,14,16
            textBox7.BackColor = System.Drawing.Color.Red;
            textBox8.BackColor = System.Drawing.Color.Red;
            textBox9.BackColor = System.Drawing.Color.Red;
            textBox14.BackColor = System.Drawing.Color.Red;
            textBox16.BackColor = System.Drawing.Color.Red;
            textBox10.BackColor = System.Drawing.Color.Red;
            textBox11.BackColor = System.Drawing.Color.Red;
            scrollingM = new Scrolling_ManagerV(new Microsoft.Xna.Framework.Rectangle(0, 0, width, height));
            openF = false;

            playlist = new string[4] { "", "", "", "" };
        }

        /// <summary>
        /// fonction si l on touche un ojet permet sa modification et sa suppression
        /// </summary>
        /// <param name="X">position X de la souris</param>
        /// <param name="y">position Y de la souris</param>
        /// <param name="touch">l objet toucher</param>
        /// <param name="spawn">quel ia</param>
        /// <param name="fond2">fond du jeu</param>
        public void _show(int X, int y, string touch, int spawn, Microsoft.Xna.Framework.Rectangle fond2)
        {
            IHave_control = true;
            openX = (float)X / (float)width;
            openY = (float)y / (float)height;


            EnableTab(tabPage2, false);
            this.Location = new System.Drawing.Point(fond2.X, fond2.Y);
            this.spawn = spawn;
            this.ia_type = touch;

            switch (touch)
            {
                case "IA_K":
                    EnableTab(tabPage3, true);
                    EnableTab(tabPage2, false);
                    EnableTab(tabPage7, false);
                    EnableTab(tabPage8, false);
                    enableall(false);

                    textBox4.Text = "" + _savefile.ia_Kamikaze[spawn].vie;
                    textBox5.Text = "" + _savefile.ia_Kamikaze[spawn].speed;
                    textBox12.Text = "" + _savefile.ia_Kamikaze[spawn].damage;
                    break;
                case "IA_V":
                    EnableTab(tabPage3, false);
                    EnableTab(tabPage2, true);
                    EnableTab(tabPage7, false);
                    EnableTab(tabPage8, false);
                    enableall(false);
                    radioButton1.Enabled = false;
                    radioButton2.Enabled = true;
                    radioButton2.Checked = true;
                    textBox1.Text = "" + _savefile.ia_viseur[spawn].vie;
                    textBox2.Text = "" + _savefile.ia_viseur[spawn].speed;
                    textBox3.Text = "" + _savefile.ia_viseur[spawn].nombre;
                    textBox6.Text = "" + _savefile.ia_viseur[spawn].firerate;
                    textBox13.Text = "" + _savefile.ia_viseur[spawn].damage;
                    textBox15.Text = "" + _savefile.ia_viseur[spawn].bullet_Speed;
                    comboBox4.SelectedText = _savefile.ia_viseur[spawn].trajectory;
                    button2.BackColor = System.Drawing.Color.FromArgb(_savefile.ia_viseur[spawn].color.A, _savefile.ia_viseur[spawn].color.R, _savefile.ia_viseur[spawn].color.G, _savefile.ia_viseur[spawn].color.B);
                    color2 = button2.BackColor;
                    break;
                case "IA_T":
                    EnableTab(tabPage3, false);
                    EnableTab(tabPage2, true);
                    EnableTab(tabPage7, false);
                    EnableTab(tabPage8, false);
                    enableall(false);
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = false;
                    radioButton1.Checked = true;
                    textBox1.Text = "" + _savefile.ia_tireur[spawn].vie;
                    textBox2.Text = "" + _savefile.ia_tireur[spawn].speed;
                    textBox3.Text = "" + _savefile.ia_tireur[spawn].nombre;
                    textBox6.Text = "" + _savefile.ia_tireur[spawn].firerate;
                    textBox13.Text = "" + _savefile.ia_tireur[spawn].damage;
                    textBox15.Text = "" + _savefile.ia_tireur[spawn].bullet_Speed;
                    comboBox4.SelectedText = _savefile.ia_tireur[spawn].trajectory;

                    button2.BackColor = System.Drawing.Color.FromArgb(_savefile.ia_tireur[spawn].color.A, _savefile.ia_tireur[spawn].color.R, _savefile.ia_tireur[spawn].color.G, _savefile.ia_tireur[spawn].color.B);
                    color2 = button2.BackColor;
                    break;
                case "b":
                    EnableTab(tabPage3, false);
                    EnableTab(tabPage2, false);
                    EnableTab(tabPage7, false);
                    EnableTab(tabPage8, true);
                    enableall(false);
                    textBox17.Text = "" + _savefile.bonus[spawn].speed;
                    textBox18.Text = "" + _savefile.bonus[spawn].angle;
                    break;
                default:
                    EnableTab(tabPage2, true);
                    EnableTab(tabPage3, true);
                    EnableTab(tabPage7, true);
                    EnableTab(tabPage8, true);
                    enableall(true);
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;

                    break;
            }
            unblockAss(false);
            this.Show();
        }
        private void enableall(bool f)
        {
            EnableTab(tabPage4, f);
            EnableTab(tabPage5, f);
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
        /// <summary>
        /// copie les manager 
        /// </summary>
        /// <param name="manage_T"></param>
        /// <param name="manage_V"></param>
        /// <param name="manage_k"></param>
        /// <param name="keybord"></param>
        /// <param name="game"></param>
        /// <param name="scrollM"></param>
        /// <param name="ovni"></param>
        public void update(ref IA_manager_T manage_T, ref IA_manager_V manage_V, ref IA_manager_K manage_k,
            ref KeyboardState keybord, Game1 game, ref Scrolling_ManagerV scrollM, ref Ovni ovni)
        {
            MediaPlayer.IsRepeating = false;
            manage_T = this.manage_T;
            manage_V = this.manage_V;
            manage_k = this.manage_k;
            scrollM = this.scrollingM;
            ovni = this.ovni;
            this.game = game;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                ++seconde;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                --seconde;
            /*         if (MediaPlayer.State == MediaState.Stopped)
                     {
                         ++song;
                         if (song < listsong.Count)
                             MediaPlayer.Play(listsong[song]);
                         else
                             song = -1;
                     }*/
        }

        public void LoadContent(IA_manager_T manage_T, IA_manager_V manage_V, IA_manager_K manage_k, Scrolling_ManagerV scrolling, ContentManager Content, Ovni ovni, Microsoft.Xna.Framework.Rectangle fond)
        {
            this.manage_T = manage_T;
            this.manage_V = manage_V;
            this.manage_k = manage_k;
            this.ovni = ovni;
            this.scrollingM = scrolling;
            this.Content = Content;
            string[] item = sauve.filename(Content, "back");
            for (int i = 0; i < item.Length; ++i)
            {
                comboBox1.Items.Add(item[i]);
                comboBox3.Items.Add(item[i]);
            }
            ovni = new Ovni(width, height);
            ovni.param(3);
            height = fond.Height;
            width = fond.Width;
            string[] item2 = sauve.filename(Content, "BossSEU");
            foreach (string st in item2)
                listBox2.Items.Add(st);

        }
        /// <summary>
        /// met a defaut les paarametres
        /// </summary>
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
            this.scrolling = LocalizedString.ScrollingVertical;
            file = LocalizedString.File;
            damage = LocalizedString.inficted_damage;
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
            //tap page

            tabPage2.Text = onglet1;// +"/" + onglet2;
            tabPage3.Text = onglet3;
            tabPage4.Text = onglet4;
            tabPage1.Text = file;
            tabPage5.Text = onglet5;
            tabPage6.Text = musique;
            tabPage7.Text = boss;
            tabPage8.Text = bonus;
            //tab1
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton1.Text = onglet1;
            radioButton2.Text = onglet2;
            label3.Text = life;
            label4.Text = speed;
            label5.Text = trajectoir;
            label6.Text = align;
            label7.Text = couleur;
            button2.Text = couleur;

            label1.Text = firerate;
            button1.Text = OK;
            label19.Text = damage;
            label22.Text = bullet_speed;
            //tab2
            label18.Text = damage;
            button3.Text = OK;

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

            // tab 5
            label11.Text = vitesseV;
            label12.Text = firerate;
            label13.Text = couleur;
            button6.Text = couleur;
            label21.Text = bullet_speed;

            label8.Text = life;
            label9.Text = speed;
            button10.Text = OK;
            label17.Text = damage;
            label20.Text = life;
            //tab6 musique
            label23.Text = musique;
            button12.Text = open;
            button13.Text = add;
            button14.Text = supp;
            button15.Text = OK;
            //tab8 OVNI
            radioButton3.Text = life;
            radioButton4.Text = bomb;
            radioButton5.Text = missile;
            radioButton6.Text = power;
            radioButton7.Text = ovini;
            radioButton8.Text = aster;
            radioButton9.Text = comete;
            radioButton10.Text = sun;
            button16.Text = OK;
            label24.Text = speed;
            label25.Text = angle;
            radioButton1.Enabled = true;
            //tab7 boss
            textBox19.Tag = t_life;
            textBox20.Tag = t_damage;
            textBox21.Tag = t_speed;
            textBox22.Tag = t_speed;
            label26.Text = life;
            label27.Text = damage;
            label28.Text = speed;
            label29.Text = couleur;
            label30.Text = firerate;
            button17.Text = couleur;

            // default 
            button6.BackColor = button1.BackColor;
            button2.BackColor = button1.BackColor;

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            //       textBox7.Text = string.Empty;
            //    textBox8.Text = string.Empty;
            //   textBox9.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox15.Text = string.Empty;
            textBox17.Text = string.Empty;
            textBox18.Text = string.Empty;
            //tag
            textBox1.Tag = t_life;
            textBox2.Tag = t_speed;
            textBox3.Tag = t_nbr;
            textBox4.Tag = t_life;
            textBox5.Tag = t_speed;
            textBox6.Tag = t_firerate;
            textBox7.Tag = t_speed;
            textBox8.Tag = t_speed;
            textBox9.Tag = t_firerate;
            //  / textBox10. Tag = t_speed;  check name
            textBox11.Tag = t_damage;
            textBox12.Tag = t_damage;
            textBox13.Tag = t_damage;
            textBox14.Tag = t_life;
            textBox15.Tag = t_speed;
            textBox16.Tag = t_speed;
            textBox17.Tag = t_speed;
            textBox18.Tag = t_angle;
            openX = 0;
            openY = 0;

            button11.Enabled = false;

            textBox1.BackColor = System.Drawing.Color.White;
            textBox2.BackColor = System.Drawing.Color.White;
            textBox3.BackColor = System.Drawing.Color.White;
            textBox4.BackColor = System.Drawing.Color.White;
            textBox5.BackColor = System.Drawing.Color.White;
            textBox6.BackColor = System.Drawing.Color.White;
            //     textBox7.BackColor = System.Drawing.Color.White;
            //   textBox8.BackColor = System.Drawing.Color.White;
            //    textBox9.BackColor = System.Drawing.Color.White;

            textBox12.BackColor = System.Drawing.Color.White;
            textBox13.BackColor = System.Drawing.Color.White;
            //   textBox14.BackColor = System.Drawing.Color.White; tab5
            textBox15.BackColor = System.Drawing.Color.White;
            //    textBox16.BackColor = System.Drawing.Color.White; tab5
            textBox17.BackColor = System.Drawing.Color.White;
            textBox18.BackColor = System.Drawing.Color.White;

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


        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox17);
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox18);
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox8);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox9);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox11);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox1);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox3);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox4);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox5);
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox6);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox7);
        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox12);
        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox13);
        }
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox14);
        }
        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox15);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox16);
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            nameCheck(ref textBox10);
        }
        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox19);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox20);
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox21);
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox22);
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
        private void button6_Click(object sender, EventArgs e)// tab 4couleur
        {
            colorDialog2.ShowDialog();
            color4 = colorDialog2.Color;
            button6.BackColor = color4;
        }
        private void button2_Click(object sender, EventArgs e)// tab1 color
        {
            colorDialog1.ShowDialog();
            color2 = colorDialog1.Color;
            button2.BackColor = color2;
        }


        private void button5_Click(object sender, EventArgs e)// tab4 showfile image
        {
            open_File_dialogue('i');
        }

        private void button12_Click(object sender, EventArgs e)// tab 6 show file musique
        {
            open_File_dialogue('m');
            // Additem();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            colorDialog3.ShowDialog();
            color5 = colorDialog3.Color;
            button17.BackColor = color5;

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
        private void button3_Click(object sender, EventArgs e)// tab2 kamikaze
        {
            if (textBox4.BackColor == System.Drawing.Color.Green &&
                textBox5.BackColor == System.Drawing.Color.Green &&
                textBox12.BackColor == System.Drawing.Color.Green)
            {
                if (spawn == -1)
                {
                    couple couple = new couple();
                    couple.X = openX;
                    couple.Y = openY;
                    couple.seconde = seconde;
                    couple.damage = int.Parse(textBox12.Text);
                    couple.speed = int.Parse(textBox5.Text);
                    couple.vie = int.Parse(textBox4.Text);
                    _savefile.ia_Kamikaze.Add(couple);
                    manage_k.Add(couple, manage_k.Ia_manage.Count);
                    this.hidou();
                }
                else
                {
                    modif(spawn, ia_type);
                    this.hidou();
                }

            }
        }
        private void button7_Click(object sender, EventArgs e)// cancel
        {
            this.hidou();
        }
        private void button8_Click(object sender, EventArgs e)// save 
        {
            if (textBox10.BackColor == System.Drawing.Color.Green)
            {
                _savefile.levelProfile.levelname = filepath;
                if (_savefile.levelProfile.background_name != null &&
                    _savefile.levelProfile.playerLife != 0)
                {
                    savegame();
                    this.hidou();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)// tab viseur et tireur
        {

            if (textBox1.BackColor == System.Drawing.Color.Green && textBox2.BackColor == System.Drawing.Color.Green &&
                textBox3.BackColor == System.Drawing.Color.Green && textBox6.BackColor == System.Drawing.Color.Green &&
                textBox13.BackColor == System.Drawing.Color.Green && textBox15.BackColor == System.Drawing.Color.Green &&
                color2 != System.Drawing.Color.Black)//+combobox4 a veriff
            {
                if (spawn == -1)
                {
                    quaintuplet quaint = new quaintuplet();
                    quaint.color = new Microsoft.Xna.Framework.Color(color2.R, color2.G, color2.B, color2.A);
                    quaint.damage = int.Parse(textBox13.Text);
                    quaint.firerate = int.Parse(textBox6.Text);
                    quaint.nombre = int.Parse(textBox3.Text);
                    quaint.seconde = seconde;
                    quaint.speed = int.Parse(textBox2.Text);
                    quaint.trajectory = (string)comboBox4.SelectedItem;
                    quaint.vie = int.Parse(textBox1.Text);
                    quaint.bullet_Speed = int.Parse(textBox15.Text);
                    quaint.X = openX;
                    quaint.Y = openY;

                    if (radioButton1.Checked)
                    {
                        _savefile.ia_tireur.Add(quaint);
                        manage_T.Add(quaint, manage_T.Ia_manage.Count);
                        this.hidou();
                    }
                    else if (radioButton2.Checked)
                    {
                        _savefile.ia_viseur.Add(quaint);
                        manage_V.Add(quaint, manage_V.Ia_manage.Count);
                        this.hidou();
                    }
                }
                else
                {
                    modif(spawn, ia_type);
                    this.hidou();
                }
            }
        } // tab1
        private void button4_Click(object sender, EventArgs e) //tab4 bacground
        {
            if (textBox7.BackColor == System.Drawing.Color.Green && imageB != string.Empty)
            {
                _savefile.ia_Kamikaze.Clear();
                _savefile.ia_tireur.Clear();
                _savefile.ia_viseur.Clear();
                _savefile.levelProfile.background_name = imageB;
                _savefile.levelProfile.fc_speed = int.Parse(textBox7.Text);
                _savefile.levelProfile.second_background = (string)comboBox1.SelectedItem;
                _savefile.levelProfile.third_bacground = (string)comboBox3.SelectedItem;
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
        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox14.BackColor == System.Drawing.Color.Green && textBox8.BackColor == System.Drawing.Color.Green &&
                textBox9.BackColor == System.Drawing.Color.Green && textBox11.BackColor == System.Drawing.Color.Green &&
                textBox16.BackColor == System.Drawing.Color.Green &&
                color4 != System.Drawing.Color.Black)
            {
                _savefile.levelProfile.color = new Microsoft.Xna.Framework.Color(color4.R, color4.G, color4.B, color4.A);
                _savefile.levelProfile.damage = int.Parse(textBox11.Text);
                _savefile.levelProfile.firerate = int.Parse(textBox9.Text);
                _savefile.levelProfile.playerLife = int.Parse(textBox14.Text);
                _savefile.levelProfile.player_speed = int.Parse(textBox8.Text);
                _savefile.levelProfile.bullet_speed = int.Parse(textBox16.Text);
                hidou();
            }

        }// player profile
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
        private void button16_Click(object sender, EventArgs e)
        {
            if (spawn == -1)
            {
                Bonus bonus = new Bonus();
                char type = '\0';
                if (radioButton3.Checked)
                    type = 'v';
                else if (radioButton4.Checked)
                    type = 'b';
                else if (radioButton5.Checked)
                    type = 'm';
                else if (radioButton6.Checked)
                    type = 'p';
                else if (radioButton7.Checked && textBox18.BackColor == System.Drawing.Color.Green &&
        textBox17.BackColor == System.Drawing.Color.Green)
                {
                    if (radioButton8.Checked)
                        type = 'a';
                    else if (radioButton9.Checked)
                        type = 'c';
                    else if (radioButton10.Checked)
                        type = 's';
                    bonus.speed = int.Parse(textBox17.Text);
                    bonus.angle = int.Parse(textBox18.Text);
                }
                if (type != '\0')
                {
                    ovni.Add(type, openX, openY, ovni.ovni.Count);
               
                    bonus.type = type;
                    bonus.launch = seconde;
                    bonus.X = openX;
                    bonus.Y = openY;
                    _savefile.bonus.Add(bonus);
                    hidou();
                }
            }
            else
                modif(spawn, ia_type);
        }// bonus
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
        /// <summary>
        /// verifie la validite des nombre entrer dans la listbox
        /// </summary>
        /// <param name="texbox">textbox a actualiser</param>
        private void intcheck(TextBox texbox)
        {
            int res, n = 0;
            if ((string)texbox.Tag == t_damage)
                n = 3;
            else if ((string)texbox.Tag == t_firerate)
                n = 200;
            else if ((string)texbox.Tag == t_life)
                n = 300;
            else if ((string)texbox.Tag == t_nbr)
                n = 17;
            else if ((string)texbox.Tag == t_speed)
                n = 10;
            else if ((string)texbox.Tag == t_angle)
            {
                n = 180;
            }

            if (texbox.Text != string.Empty && int.TryParse(texbox.Text, out res) && res <= n && res > 0)
            {
                texbox.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                texbox.BackColor = System.Drawing.Color.Red;
            }
        }
        /// <summary>
        /// charge le fond des jeu perso
        /// </summary>
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
                file = new FileStream(sauve._path + "\\SEU\\" + _savefile.levelProfile.levelname + "\\" + imageB, FileMode.Open, FileAccess.Read);
            if (scrollingM.count == 0)
            {
                scrollingM.Add(Texture2D.FromStream(game.GraphicsDevice, file), 0.5f);
            }
            else
            {
                scrollingM.texture[0] = Texture2D.FromStream(game.GraphicsDevice, file);
            }

            if (_savefile.levelProfile.second_background != null)
                if (scrollingM.count >= 1)
                    scrollingM.Add(Content.Load<Texture2D>("back\\" + _savefile.levelProfile.second_background), 0.5f);
                else
                    scrollingM.texture[1] = Content.Load<Texture2D>("back\\" + _savefile.levelProfile.second_background);
            else if (_savefile.levelProfile.third_bacground != null)
                if (scrollingM.count >= 1)
                    scrollingM.Add(Content.Load<Texture2D>("back\\" + _savefile.levelProfile.third_bacground), 0.5f);
                else
                    scrollingM.texture[1] = Content.Load<Texture2D>("back\\" + _savefile.levelProfile.third_bacground);

            if (_savefile.levelProfile.third_bacground != null)
                if (scrollingM.count >= 2)
                    scrollingM.Add(Content.Load<Texture2D>("back\\" + _savefile.levelProfile.third_bacground), 0.5f);
                else
                    scrollingM.texture[2] = Content.Load<Texture2D>("back\\" + _savefile.levelProfile.third_bacground);

        }
        /// <summary>
        /// verifie si les nom entrer en parametre est bon
        /// </summary>
        /// <param name="textbox"></param>
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
        /// <summary>
        /// sauvegade le jeu
        /// </summary>
        private void savegame()
        {
            _savefile.levelProfile.musique = playlist;
            sauve.save_SEU(ref _savefile, "SEU");
        }

        /// <summary>
        /// charge un jeu contenu dans le dossier SEU
        /// </summary>
        /// <param name="file_name">nom du fichier a charger</param>
        private void loadgame(string file_name)
        {
            sauve.load_SEU(ref file_name, ref _savefile, "PLA");
            manage_k.remove_all();
            manage_T.remove_all();
            manage_V.remove_all();
            ovni.remove_all();

            for (int i = 0; i < _savefile.ia_tireur.Count; ++i)
                manage_T.Add(_savefile.ia_tireur[i], i);
            for (int i = 0; i < _savefile.ia_viseur.Count; ++i)
                manage_V.Add(_savefile.ia_viseur[i], i);
            for (int i = 0; i < _savefile.ia_Kamikaze.Count; ++i)
                manage_k.Add(_savefile.ia_Kamikaze[i], i);
            for (int i = 0; i < _savefile.bonus.Count; ++i)
                ovni.Add(_savefile.bonus[i].type, _savefile.bonus[i].X, _savefile.bonus[i].Y, i);
            textBox7.BackColor = System.Drawing.Color.Green;
            textBox8.BackColor = System.Drawing.Color.Green;
            textBox14.BackColor = System.Drawing.Color.Green;
            textBox16.BackColor = System.Drawing.Color.Green;
            textBox10.BackColor = System.Drawing.Color.Green;

            textBox7.Text = "" + _savefile.levelProfile.fc_speed;
            comboBox1.SelectedText = _savefile.levelProfile.second_background;
            comboBox3.SelectedText = _savefile.levelProfile.third_bacground;
            imageB = _savefile.levelProfile.background_name;
            comboBox2.SelectedText = _savefile.levelProfile.levelname;
            textBox10.Text = _savefile.levelProfile.levelname;

            button6.BackColor = System.Drawing.Color.FromArgb(_savefile.levelProfile.color.A, _savefile.levelProfile.color.R, _savefile.levelProfile.color.G, _savefile.levelProfile.color.B);
            textBox8.Text = "" + _savefile.levelProfile.player_speed;
            textBox9.Text = "" + _savefile.levelProfile.firerate;
            textBox11.Text = "" + _savefile.levelProfile.damage;
            textBox14.Text = "" + _savefile.levelProfile.playerLife;
            textBox16.Text = "" + _savefile.levelProfile.bullet_speed;
            if ( _savefile.levelProfile.musique != null)
            {
                listsong = new List<Song>();

                listBox1.Items.AddRange(_savefile.levelProfile.musique);
                foreach (string st in _savefile.levelProfile.musique)
                    if (st != null && st != "")
                    {

                        listsong.Add(Song.FromUri("s", new Uri("file:" + sauve._path + "//" + type + "//" + _savefile.levelProfile.levelname + "//" + st)));
                    }
                if (listsong.Count > 0)
                {
                    MediaPlayer.Play(listsong[0]);
                    song = 0;
                }
            }
            scrollingLoad();
        }
        /// <summary>
        /// spprime un ia de la carte
        /// </summary>
        /// <param name="spawn"></param>
        /// <param name="type"></param>
        private void delete(int spawn, string type)
        {
            switch (type)
            {
                case "IA_K":
                    _savefile.ia_Kamikaze.RemoveAt(spawn);
                    manage_k.remove_all();
                    for (int i = 0; i < _savefile.ia_Kamikaze.Count; ++i)
                        manage_k.Add(_savefile.ia_Kamikaze[i], i);
                    break;
                case "IA_V":
                    _savefile.ia_viseur.RemoveAt(spawn);
                    manage_V.remove_all();
                    for (int i = 0; i < _savefile.ia_viseur.Count; ++i)
                        manage_V.Add(_savefile.ia_viseur[i], i);
                    break;
                case "IA_T":
                    _savefile.ia_tireur.RemoveAt(spawn);
                    manage_T.remove_all();
                    for (int i = 0; i < _savefile.ia_viseur.Count; ++i)
                        manage_T.Add(_savefile.ia_tireur[i], i);
                    break;
                case "b":
                    _savefile.bonus.RemoveAt(spawn);
                    ovni.remove_all();
                    for (int i = 0; i < _savefile.bonus.Count; ++i)
                        ovni.Add(_savefile.bonus[i].type, _savefile.bonus[i].X, _savefile.bonus[i].Y, i);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// modifie un ia
        /// </summary>
        /// <param name="spawn"></param>
        /// <param name="type"></param>
        private void modif(int spawn, string type)
        {
            if (type == "IA_K")
            {
                couple cople = new couple();
                cople.vie = int.Parse(textBox4.Text);
                cople.speed = int.Parse(textBox5.Text);
                cople.damage = int.Parse(textBox12.Text);
                cople.X = _savefile.ia_Kamikaze[spawn].X;
                cople.Y = _savefile.ia_Kamikaze[spawn].Y;
                _savefile.ia_Kamikaze[spawn] = cople;
                manage_k.remove_all();
                for (int i = 0; i < _savefile.ia_Kamikaze.Count; i++)
                    manage_k.Add(cople, i);
            }
            else if (type == "b")
            {
                Bonus bonu = new Bonus();
                bonu.speed = int.Parse(textBox17.Text);
                bonu.angle = int.Parse(textBox18.Text);
                bonu.launch = _savefile.bonus[spawn].launch;
                bonu.X = openX;
                bonu.Y = openY;
                char types = _savefile.bonus[spawn].type;
                if (radioButton3.Checked)
                    types = 'v';
                else if (radioButton4.Checked)
                    types = 'b';
                else if (radioButton5.Checked)
                    types = 'm';
                else if (radioButton6.Checked)
                    types = 'p';
                else if (radioButton7.Checked && textBox18.BackColor == System.Drawing.Color.Green &&
        textBox17.BackColor == System.Drawing.Color.Green)
                {
                    if (radioButton8.Checked)
                        types = 'a';
                    else if (radioButton9.Checked)
                        types = 'c';
                    else if (radioButton10.Checked)
                        types = 's';
                }
                bonu.type = types;
                _savefile.bonus[spawn] = bonu;
                ovni.remove_all();
                for (int i = 0; i < _savefile.bonus.Count; ++i)
                    ovni.Add(_savefile.bonus[i].type, _savefile.bonus[i].X, _savefile.bonus[i].Y, i);
            }
            else
            {
                quaintuplet quaint = new quaintuplet();
                quaint.color = new Microsoft.Xna.Framework.Color(color2.R, color2.G, color2.B, color2.A);
                quaint.damage = int.Parse(textBox13.Text);
                quaint.firerate = int.Parse(textBox6.Text);
                quaint.nombre = int.Parse(textBox3.Text);
                quaint.seconde = seconde;
                quaint.speed = int.Parse(textBox2.Text);
                quaint.trajectory = (string)comboBox4.SelectedItem;
                quaint.vie = int.Parse(textBox1.Text);


                quaint.bullet_Speed = int.Parse(textBox15.Text);

                if (type == "IA_V")
                {
                    quaint.X = _savefile.ia_viseur[spawn].X;
                    quaint.Y = _savefile.ia_viseur[spawn].Y;
                    _savefile.ia_viseur[spawn] = quaint;
                    manage_V.remove_all();
                    for (int i = 0; i < _savefile.ia_viseur.Count; ++i)
                        manage_V.Add(_savefile.ia_viseur[i], i);
                }
                else if (type == "IA_T")
                {
                    quaint.X = _savefile.ia_tireur[spawn].X;
                    quaint.Y = _savefile.ia_tireur[spawn].Y;
                    _savefile.ia_tireur[spawn] = quaint;
                    manage_T.remove_all();
                    for (int i = 0; i < _savefile.ia_tireur.Count; ++i)
                        manage_T.Add(_savefile.ia_tireur[i], i);
                }

            }
        }
        /// <summary>
        /// ouvr une fenetre de dilogue avec un thread
        /// </summary>
        /// <param name="type">musique ou image (m|i)</param>
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
        /// <summary>
        /// ajoute de la musique de dossier
        /// </summary>
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
        /// <summary>
        /// suprimer le dossier selectionner ds la listbox
        /// </summary>
        /// <param name="list"></param>
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
            radioButton10.Enabled = b;
            radioButton8.Enabled = b;
            radioButton9.Enabled = b;
            textBox17.Enabled = b;
            textBox18.Enabled = b;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedbos =(string) listBox2.SelectedItem;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (selectedbos != "" && textBox19.BackColor == System.Drawing.Color.Green && textBox20.BackColor == System.Drawing.Color.Green &&
                textBox21.BackColor == System.Drawing.Color.Green && textBox22.BackColor == System.Drawing.Color.Green&& color5!= System.Drawing.Color.Black )
            {
                BossSEUstruct boss2 = new BossSEUstruct();
                boss2.bulletcolor = new Microsoft.Xna.Framework.Color (color5.R,color5.G,color5.B,color5.A) ;
                boss2.damage = int.Parse (textBox20.Text );
                boss2.speed = int.Parse (textBox21 .Text );
                boss2.life= int.Parse (textBox19.Text );
                boss2.speedball = int.Parse (textBox22.Text );
                _savefile.bossSEU = boss2 ;
                hidou ();
            }
        }




    }
}
