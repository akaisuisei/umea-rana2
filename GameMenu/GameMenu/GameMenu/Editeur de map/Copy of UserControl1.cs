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

namespace Umea_rana
{
    public partial class UserControl2 : Form
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
        Ovni ovni;
        Sauveguarde sauve;
        savefile savefile;
        List<string> subdirectory;
        Scrolling_ManagerV scrollingM;
        Game1 game;
        int spawn;
        string ia_type;
        ContentManager Content;

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
        public UserControl2()
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
            savefile.bonus = new List<Bonus>();
            subdirectory = new List<string>();
        
            type = "SEU";
            Initialize();
            //8,9,14,16
          
            scrollingM = new Scrolling_ManagerV(new Microsoft.Xna.Framework.Rectangle(0, 0, width, height));
            openF = false;

            playlist = new string[4] { "", "", "", "" };
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
           //
            this.Location = new System.Drawing.Point(X, y);
            this.spawn = spawn;
            this.ia_type = touch;

            switch (touch)
            {
                case "IA_K":
                    
                    enableall(false);

                    break;
                case "IA_V":
                   
                    enableall(false);
                    
                    break;
                case "IA_T":
                   
                    enableall(false);
                    

                   
                    break;
                case "b":
                   
                    enableall(false);
                     break;
                default:
                    enableall(true);
                 
                    break;
            }
            unblockAss(false);
            this.Show();
        }
        private void enableall(bool f)
        {
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
            ref KeyboardState keybord, Game1 game, ref Scrolling_ManagerV scrollM, ref Ovni ovni)
        {
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
        }

        public void LoadContent(IA_manager_T manage_T, IA_manager_V manage_V, IA_manager_K manage_k, Scrolling_ManagerV scrolling, ContentManager Content, Ovni ovni)
        {
            this.manage_T = manage_T;
            this.manage_V = manage_V;
            this.manage_k = manage_k;
            this.ovni = ovni;
            this.scrollingM = scrolling;
            this.Content = Content;
            string[] item = sauve.filename(Content);
            for (int i = 0; i < item.Length; ++i)
            {
            }
            ovni = new Ovni(width, height);
            ovni.param(3);
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
            musique = "musique";
            add = "ajouter";
            boss = "Boss";
            bonus = "Bonus";
            bomb = "bombe";
            missile = "missile";
            power = "puissance";
            ovini = "ovni";
            aster = "asteroide";
            comete = "comete";
            sun = "soleil";
            angle = "angle";
            color2 = System.Drawing.Color.Black;
            //tap page

           
            //tab1
         
            openX = 0;
            openY = 0;

               //     textBox7.BackColor = System.Drawing.Color.White;
            //   textBox8.BackColor = System.Drawing.Color.White;
            //    textBox9.BackColor = System.Drawing.Color.White;

            //   textBox14.BackColor = System.Drawing.Color.White; tab5
             //    textBox16.BackColor = System.Drawing.Color.White; tab5
           
            spawn = -1;
            ia_type = "kawabunga";
          
            string[] hello = sauve.subdirectory(type);
  

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
       // supprimer
      
        private void button7_Click(object sender, EventArgs e)// cancel
        {
            this.hidou();
        }
        private void button8_Click(object sender, EventArgs e)// save 
        {
            if (textBox10.BackColor == System.Drawing.Color.Green)
            {
                savefile.levelProfile.levelname = filepath;
                if (savefile.levelProfile.background_name != null &&
                    savefile.levelProfile.playerLife != 0)
                {
                    savegame();
                    this.hidou();
                }
            }
        }
      
        private void button4_Click(object sender, EventArgs e) //tab4 bacground
        {
            if (textBox7.BackColor == System.Drawing.Color.Green && imageB != string.Empty)
            {
                savefile.ia_Kamikaze.Clear();
                savefile.ia_tireur.Clear();
                savefile.ia_viseur.Clear();
                savefile.levelProfile.background_name = imageB;
                savefile.levelProfile.fc_speed = int.Parse(textBox7.Text);
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
        // bonus
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
                file = new FileStream(sauve._path + "\\SEU\\" + savefile.levelProfile.levelname + "\\" + imageB, FileMode.Open, FileAccess.Read);
            if (scrollingM.count == 0)
            {
                scrollingM.Add(Texture2D.FromStream(game.GraphicsDevice, file), 0.5f);
            }
            else
            {
                scrollingM.texture[0] = Texture2D.FromStream(game.GraphicsDevice, file);
            }

            if (savefile.levelProfile.second_background != null)
                if (scrollingM.count >= 1)
                    scrollingM.Add(Content.Load<Texture2D>("back\\" + savefile.levelProfile.second_background), 0.5f);
                else
                    scrollingM.texture[1] = Content.Load<Texture2D>("back\\" + savefile.levelProfile.second_background);
            else if (savefile.levelProfile.third_bacground != null)
                if (scrollingM.count >= 1)
                    scrollingM.Add(Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground), 0.5f);
                else
                    scrollingM.texture[1] = Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground);

            if (savefile.levelProfile.third_bacground != null)
                if (scrollingM.count >= 2)
                    scrollingM.Add(Content.Load<Texture2D>("back\\" + savefile.levelProfile.third_bacground), 0.5f);
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
            sauve.save_SEU(ref savefile);
        }

        private void loadgame(string file_name)
        {
            sauve.load_SEU(ref file_name, ref savefile);
            manage_k.remove_all();
            manage_T.remove_all();
            manage_V.remove_all();
            ovni.remove_all();

            for (int i = 0; i < savefile.ia_tireur.Count; ++i)
                manage_T.Add(savefile.ia_tireur[i], i);
            for (int i = 0; i < savefile.ia_viseur.Count; ++i)
                manage_V.Add(savefile.ia_viseur[i], i);
            for (int i = 0; i < savefile.ia_Kamikaze.Count; ++i)
                manage_k.Add(savefile.ia_Kamikaze[i], i);
            for (int i = 0; i < savefile.bonus.Count; ++i)
                ovni.Add(savefile.bonus[i].type, savefile.bonus[i].X, savefile.bonus[i].Y, i);
            textBox7.BackColor = System.Drawing.Color.Green;
           
            textBox10.BackColor = System.Drawing.Color.Green;

            textBox7.Text = "" + savefile.levelProfile.fc_speed;
            comboBox1.SelectedText = savefile.levelProfile.second_background;
            comboBox3.SelectedText = savefile.levelProfile.third_bacground;
            imageB = savefile.levelProfile.background_name;
            comboBox2.SelectedText = savefile.levelProfile.levelname;
            textBox10.Text = savefile.levelProfile.levelname;


            listBox1.Items.AddRange(savefile.levelProfile.musique);
            scrollingLoad();
        }

        private void delete(int spawn, string type)
        {
           
        }

        private void modif(int spawn, string type)
        {
            

            
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



    }
}
