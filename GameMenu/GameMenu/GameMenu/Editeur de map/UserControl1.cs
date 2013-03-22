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

namespace Umea_rana
{
    public partial class UserControl1 : Form
    {
        string life, speed, couleur, onglet1, onglet2, onglet3, onglet4, trajectoir, align, OK, firerate, end;
        string imagefond, vitessefond, vitesseV, open, cancel, load, save, filepath, filepathlabel, onglet5;
        string scrolling, file, damage, bullet_speed, supp;
        // tag
        string t_life = "life", t_speed = "speed", t_damage = "damage", t_firerate = "fire", t_nbr = "nbr";
        // type
        string type;
        System.Drawing.Color color2, color4;
        int width, height;
        int seconde;
        public bool IHave_control;
        bool openF;
        string imageB;
        float openX, openY;
        IA_manager_T manage_T;
        IA_manager_V manage_V;
        IA_manager_K manage_k;
        Sauveguarde sauve;
        savefile savefile;
        List<string> subdirectory;
        Scrolling_ManagerV scrollingM;
        Game1 game;
        int spawn;
        string ia_type;
        ContentManager Content;

        public UserControl1()
        {
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
            savefile = new savefile();
            savefile.ia_Kamikaze = new List<couple>();
            savefile.ia_tireur = new List<quaintuplet>();
            savefile.ia_viseur = new List<quaintuplet>();
            savefile.levelProfile = new levelProfile();
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
            scrollingM = new Scrolling_ManagerV(width, height);
                      openF = false;
        }

        public void _show(int X, int y, string touch, int spawn)
        {
            IHave_control = true;
            int decal = 100;
            openX = (float)X / (float)width;
            openY = (float)y / (float)height;

            if (X > width / 2)
                X -= (decal + this.Width);
            else
                X += decal;
            if (y > height / 2)
                y -= (decal + this.Height);
            else
                y += decal;
            EnableTab(tabPage2, false);
            this.Location = new System.Drawing.Point(X, y);
            this.spawn = spawn;
            this.ia_type = touch;

            switch (touch)
            {
                case "IA_K":
                    EnableTab(tabPage3, true);
                    EnableTab(tabPage2, false);
                    enableall(false);
                    textBox4.Text = "" + savefile.ia_Kamikaze[spawn].vie;
                    textBox5.Text = "" + savefile.ia_Kamikaze[spawn].speed;
                    textBox12.Text = "" + savefile.ia_Kamikaze[spawn].damage;
                    break;
                case "IA_V":
                    EnableTab(tabPage3, false);
                    EnableTab(tabPage2, true);
                    enableall(false);
                    radioButton1.Enabled = false;
                    radioButton2.Enabled = false;
                    radioButton2.Checked = true;
                    textBox1.Text = "" + savefile.ia_viseur[spawn].vie;
                    textBox2.Text = "" + savefile.ia_viseur[spawn].speed;
                    textBox3.Text = "" + savefile.ia_viseur[spawn].nombre;
                    textBox6.Text = "" + savefile.ia_viseur[spawn].firerate;
                    textBox13.Text = "" + savefile.ia_viseur[spawn].damage;
                    textBox15.Text = "" + savefile.ia_viseur[spawn].bullet_Speed;
                    comboBox4.SelectedText = savefile.ia_viseur[spawn].trajectory;
                    button2.BackColor = System.Drawing.Color.FromArgb(savefile.ia_viseur[spawn].color.A, savefile.ia_viseur[spawn].color.R, savefile.ia_viseur[spawn].color.G, savefile.ia_viseur[spawn].color.B);
                    break;
                case "IA_T":
                    EnableTab(tabPage3, false);
                    EnableTab(tabPage2, true);
                    enableall(false);
                    radioButton1.Enabled = false;
                    radioButton2.Enabled = false;
                    radioButton1.Checked = true;
                    textBox1.Text = "" + savefile.ia_tireur[spawn].vie;
                    textBox2.Text = "" + savefile.ia_tireur[spawn].speed;
                    textBox3.Text = "" + savefile.ia_tireur[spawn].nombre;
                    textBox6.Text = "" + savefile.ia_tireur[spawn].firerate;
                    textBox13.Text = "" + savefile.ia_tireur[spawn].damage;
                    textBox15.Text = "" + savefile.ia_tireur[spawn].bullet_Speed;
                    comboBox4.SelectedText = savefile.ia_tireur[spawn].trajectory;
                    button2.BackColor = System.Drawing.Color.FromArgb(savefile.ia_tireur[spawn].color.A, savefile.ia_tireur[spawn].color.R, savefile.ia_tireur[spawn].color.G, savefile.ia_tireur[spawn].color.B);

                    break;
                default:
                    EnableTab(tabPage2, true);
                    EnableTab(tabPage3, true);
                    enableall(true);
                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;
                    break;
            }
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


        public void update(ref IA_manager_T manage_T, ref IA_manager_V manage_V, ref IA_manager_K manage_k,
            ref KeyboardState keybord, Game1 game, ref Scrolling_ManagerV scrollM)
        {
            manage_T = this.manage_T;
            manage_V = this.manage_V;
            manage_k = this.manage_k;
            scrollM = this.scrollingM;
            this.game = game;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                ++seconde;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                --seconde;
        }

        public void LoadContent(IA_manager_T manage_T, IA_manager_V manage_V, IA_manager_K manage_k, Scrolling_ManagerV scrolling, ContentManager Content)
        {
            this.manage_T = manage_T;
            this.manage_V = manage_V;
            this.manage_k = manage_k;
            this.scrollingM = scrolling;
            this.Content = Content;
            string[] item = sauve.filename(Content);
            for (int i = 0; i < item.Length; ++i)
            {
                comboBox1.Items.Add(item[i]);
                comboBox3.Items.Add(item[i]); 
            }
        }

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
            else if ((string)texbox.Tag == t_firerate)
                n = 200;
            else if ((string)texbox.Tag == t_life)
                n = 300;
            else if ((string)texbox.Tag == t_nbr)
                n = 17;
            else if ((string)texbox.Tag == t_speed)
                n = 10;

            if (texbox.Text != string.Empty)
            {
                if (int.TryParse(texbox.Text, out res))
                    if (res <= n)
                        texbox.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                texbox.BackColor = System.Drawing.Color.Red;
            }
        }



        #region texbox chek


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
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button9.Enabled = true;
            button11.Enabled = true;
        }
        #endregion

        #region dialogopen

        private void button2_Click(object sender, EventArgs e)// tab1 color
        {
            colorDialog1.ShowDialog();
            color2 = colorDialog1.Color;
            button2.BackColor = color2;
        }


        private void button5_Click(object sender, EventArgs e)// tab4 showfile
        {
            open_File_dialogue();
        }

        #endregion

        #region validate button

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
                    savefile.ia_Kamikaze.Add(couple);
                    manage_k.Add(couple, manage_k.Ia_manage.Count);
                    this.hidou();
                }
                else
                    modif(spawn, ia_type);

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
                savefile.levelProfile.levelname = filepath;
                if (savefile.levelProfile.background_name != string.Empty &&
                    savefile.levelProfile.playerLife != 0)
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
                    quaint.X = openX;
                    quaint.Y = openY;

                    if (radioButton1.Checked)
                    {

                        savefile.ia_tireur.Add(quaint);
                        manage_T.Add(quaint, manage_T.Ia_manage.Count);
                        this.hidou();
                    }
                    else if (radioButton2.Checked)
                    {
                        savefile.ia_viseur.Add(quaint);
                        manage_V.Add(quaint, manage_V.Ia_manage.Count);
                        this.hidou();
                    }
                }
                else
                    modif(spawn, ia_type);

            }
        } // tab1
        private void button4_Click(object sender, EventArgs e) //tab4 bacground
        {
            if (textBox7.BackColor == System.Drawing.Color.Green && imageB != string.Empty)
            {
                savefile.levelProfile.background_name = imageB;
                savefile.levelProfile.fc_speed = int.Parse(textBox7.Text);
                savefile.levelProfile.second_background = (string)comboBox1.SelectedItem;
                savefile.levelProfile.third_bacground = (string)comboBox3.SelectedItem;
                scrollingLoad();
                this.hidou(); 
            }

       

        }   
        private void scrollingLoad()
            {
                FileStream file;
            string name=string.Empty ;
            for (int i = 0; i < imageB.Length && imageB[i] != '.'; ++i)
                name += imageB[i];
                if (name  != "background") 
                file = new FileStream(imageB, FileMode.Open, FileAccess.Read);
                else
                      file = new FileStream(sauve._path+"\\SEU\\"+savefile.levelProfile.levelname +"\\"+  imageB, FileMode.Open, FileAccess.Read);
                if (scrollingM.scroll.Count == 0)
                    scrollingM.scroll.Add(new Scrolling(Texture2D.FromStream(game.GraphicsDevice, file), new Microsoft.Xna.Framework.Rectangle(0, 0, width, height), 3, height, 0.01f));
                else
                {
                    scrollingM.scroll[0].texture = Texture2D.FromStream(game.GraphicsDevice, file);
                }
                if (savefile.levelProfile.second_background != null)
                    if (scrollingM.scroll.Count >= 1)
                        scrollingM.scroll.Add(new Scrolling(Content.Load<Texture2D>("back\\" + savefile.levelProfile.second_background), new Microsoft.Xna.Framework.Rectangle(0, 0, width, height), 3, height, 0.5f));
                    else
                        scrollingM.scroll[1].texture = Content.Load<Texture2D>("back\\" + savefile.levelProfile.second_background);
                else if (savefile.levelProfile.third_bacground != null)
                    if (scrollingM.scroll.Count >= 1)
                        scrollingM.scroll.Add(new Scrolling(Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground), new Microsoft.Xna.Framework.Rectangle(0, 0, width, height), 2, height, 0.5f));
                    else
                        scrollingM.scroll[1].texture = Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground);

                if (savefile.levelProfile.third_bacground != null)
                    if (scrollingM.scroll.Count >= 2)
                        scrollingM.scroll.Add(new Scrolling(Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground), new Microsoft.Xna.Framework.Rectangle(0, 0, width, height), 1, height, 0.9f));
                    else
                        scrollingM.scroll[2].texture = Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground);

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
                savefile.levelProfile.color = new Microsoft.Xna.Framework.Color(color4.R, color4.G, color4.B, color4.A);
                savefile.levelProfile.damage = int.Parse(textBox11.Text);
                savefile.levelProfile.firerate = int.Parse(textBox9.Text);
                savefile.levelProfile.playerLife = int.Parse(textBox14.Text);
                savefile.levelProfile.player_speed = int.Parse(textBox8.Text);
                savefile.levelProfile.bullet_speed = int.Parse(textBox16.Text);
                hidou();
            }

        }// player profile



        #endregion

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

        private void Initialize()
        {
            life = "point de vie";
            speed = "vitesse";
            couleur = "couleur de tir";
            onglet1 = "Tireur";
            onglet2 = "Viseur";
            onglet3 = "kamikaze";
            onglet4 = "fond";
            onglet5 = "personnage";
            trajectoir = "trajectoire";
            align = "alignement enemie";
            OK = "OK";
            firerate = "cadence de tir";
            end = "terminer";
            imagefond = "image du fond";
            vitessefond = "vitesse du fond";
            vitesseV = "vitesse";
            open = "ouvrir";
            cancel = "annuler";
            load = "charger";
            save = "sauvegarder";
            filepathlabel = "nom du niveau";
            scrolling = "defilement vertical";
            file = "fichier";
            damage = "degat infligee";
            bullet_speed = "vitesse de la balle";
            supp = "supprimer";
            color2 = System.Drawing.Color.Black;
            //tap page

            tabPage2.Text = onglet1;// +"/" + onglet2;
            tabPage3.Text = onglet3;
            tabPage4.Text = onglet4;
            tabPage1.Text = file;
            tabPage5.Text = onglet5;
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

        private void savegame()
        {
            sauve.save_SEU(ref savefile);
        }

        private void loadgame(string file_name)
        {
            sauve.load_SEU(ref file_name, ref savefile);

            for (int i = 0; i < savefile.ia_tireur.Count; ++i)
                manage_T.Add(savefile.ia_tireur[i], i);
            for (int i = 0; i < savefile.ia_viseur.Count; ++i)
                manage_V.Add(savefile.ia_viseur[i], i);
            for (int i = 0; i < savefile.ia_Kamikaze.Count; ++i)
                manage_k.Add(savefile.ia_Kamikaze[i], i);

            textBox7.BackColor = System.Drawing.Color.Green;
            textBox8.BackColor = System.Drawing.Color.Green;
            textBox9.BackColor = System.Drawing.Color.Green;
            textBox14.BackColor = System.Drawing.Color.Green;
            textBox16.BackColor = System.Drawing.Color.Green;
            textBox10.BackColor = System.Drawing.Color.Green;

            textBox7.Text = "" + savefile.levelProfile.fc_speed;
            comboBox1.SelectedText = savefile.levelProfile.second_background;
            comboBox3.SelectedText = savefile.levelProfile.third_bacground;
            imageB = savefile.levelProfile.background_name;
            comboBox2.SelectedText = savefile.levelProfile.levelname;
            textBox10.Text = savefile.levelProfile.levelname;

            button6.BackColor = System.Drawing.Color.FromArgb(savefile.levelProfile.color.A, savefile.levelProfile.color.R, savefile.levelProfile.color.G, savefile.levelProfile.color.B);
            textBox8.Text = "" + savefile.levelProfile.player_speed;
            textBox9.Text = "" + savefile.levelProfile.firerate;
            textBox11.Text = "" + savefile.levelProfile.damage;
            textBox14.Text = "" + savefile.levelProfile.playerLife;
            textBox16.Text = "" + savefile.levelProfile.bullet_speed;

            scrollingLoad();
        }

        private void open_File_dialogue()
        {
            if (!openF)
            {
                Thread thread = new Thread(() =>
                {
                    openF = true;
                    var yourForm = new OpenFileDialog();
                    //yourForm.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    yourForm.Filter = "Image Files (*.jpeg;*.png;*.jpg;*.gif)|*.jpeg;*.png;*.jpg;*.gif";
            
                
                    if (yourForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        imageB = yourForm.FileName;
                        openF = false;
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (spawn != -1)
            {
                delete(spawn, ia_type);
            }
            else
            {
                sauve.supp_dir(comboBox2.SelectedText);
            }

            hidou();

        }// delete

        private void delete(int spawn, string type)
        {
            switch (type)
            {
                case "IA_K":
                    savefile.ia_Kamikaze.RemoveAt(spawn);
                    manage_k.remove_all();
                    for (int i = 0; i < savefile.ia_Kamikaze.Count; ++i)
                        manage_k.Add(savefile.ia_Kamikaze[i], i);
                    break;
                case "IA_V":
                    savefile.ia_viseur.RemoveAt(spawn);
                    manage_V.remove_all();
                    for (int i = 0; i < savefile.ia_viseur.Count; ++i)
                        manage_V.Add(savefile.ia_viseur[i], i);
                    break;
                case "IA_T":
                    savefile.ia_tireur.RemoveAt(spawn);
                    manage_T.remove_all();
                    for (int i = 0; i < savefile.ia_viseur.Count; ++i)
                        manage_T.Add(savefile.ia_tireur[i], i);
                    break;
                default:
                    break;
            }
        }

        private void modif(int spawn, string type)
        {
            if (type == "IA_K")
            {
                couple cople = new couple();
                cople.vie = int.Parse(textBox4.Text);
                cople.speed = int.Parse(textBox5.Text);
                cople.damage = int.Parse(textBox12.Text);
                savefile.ia_Kamikaze[spawn] = cople;
                manage_k.remove_all();
                for (int i = 0; i < savefile.ia_Kamikaze.Count; i++)
                    manage_k.Add(cople, i);
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
                quaint.X = openX;
                quaint.Y = openY;

                if (type == "IA_V")
                {
                    savefile.ia_viseur[spawn] = quaint;
                    manage_V.remove_all();
                    for (int i = 0; i < savefile.ia_viseur.Count; ++i)
                        manage_V.Add(savefile.ia_viseur[i], i);
                }
                if (type == "IA_T")
                {
                    savefile.ia_tireur[spawn] = quaint;
                    manage_T.remove_all();
                    for (int i = 0; i < savefile.ia_tireur.Count; ++i)
                        manage_T.Add(savefile.ia_tireur[i], i);
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)// tab 4couleur
        {
            colorDialog2.ShowDialog();
            color4 = colorDialog2.Color;
            button6.BackColor = color4;
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



    }
}
