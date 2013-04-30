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
        string t_life = "life", t_speed = "speed", t_damage = "damage";
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
        IA_manager_AA manage_AA;
        IA_manager_AR manage_AR;
        IA_manager_S manage_S;
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
            manage_AA.Dipose();
            manage_AR.Dipose();
            manage_S.Dipose();
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
            savefile.ia_AA = new List<IA_AA>();
            savefile.ia_AR = new List<IA_AR>();
            savefile.ia_S = new List<IA_S>();
            savefile.levelProfile = new levelProfile();
            savefile.bonus = new List<Bonus>();
            subdirectory = new List<string>();
            button9.Enabled = false;
            type = "SEU";
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
                    Naruto.Checked = true;
                    vie_p.Text = "" + savefile.ia_S[spawn].Vie;
                    vitesse_p.Text = "" + savefile.ia_S[spawn].Speed;                  
                    
                    puissance_p.Text = "" + savefile.ia_S[spawn].Puissance;                   
                                        
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
            ref KeyboardState keybord, Game1 game, ref Scrolling_ManagerV scrollM)
        {
            manage_aa = this.manage_AA;
            manage_ar = this.manage_AR;
            manage_s = this.manage_S;
            scrollM = this.scrollingM;            
            this.game = game;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                ++seconde;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                --seconde;

        }

        public void LoadContent(IA_manager_S manage_S, IA_manager_AR manage_AR, IA_manager_AA manage_AA, Scrolling_ManagerV scrolling, ContentManager Content)
        {
            
            this.manage_AA = manage_AA;
            this.manage_AR = manage_AR;
            this.manage_S = manage_S;
            
            this.scrollingM = scrolling;
            this.Content = Content;
            string[] item = sauve.filename(Content);
            for (int i = 0; i < item.Length; ++i)
            {
                comboBox1.Items.Add(item[i]);
                comboBox3.Items.Add(item[i]);
            }
            
            
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

            // tab 5
            
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
                if (savefile.levelProfile.background_name != null &&
                    savefile.levelProfile.playerLife != 0)
                {
                    savegame();
                    this.hidou();
                }
            }
        }
        private void Valider_Click(object sender, EventArgs e)// tab viseur et tireur
        {

            if (vie_p.BackColor == System.Drawing.Color.Green && puissance_p.BackColor == System.Drawing.Color.Green &&
                vitesse_p.BackColor == System.Drawing.Color.Green && color2 != System.Drawing.Color.Black)//+combobox4 a veriff
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
                        savefile.ia_S.Add(ias);
                        manage_S.Add(ias, manage_S.Ia_manage.Count);
                        this.hidou();
                    }
                    else if (Eve.Checked)
                    {
                        savefile.ia_AR.Add(iar);
                        manage_AR.Add(iar, manage_AR.Ia_manage.Count);
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
            if (Sauvegarde_.BackColor == System.Drawing.Color.Green && imageB != string.Empty)
            {
                savefile.ia_AA.Clear();
                savefile.ia_AR.Clear();
                savefile.ia_S.Clear();
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
            manage_S.remove_all();
            manage_AA.remove_all();
            manage_AR.remove_all();
            ovni.remove_all();

            for (int i = 0; i < savefile.ia_AA.Count; ++i)
                manage_AA.Add(savefile.ia_AA[i], i);
            for (int i = 0; i < savefile.ia_AR.Count; ++i)
                manage_AR.Add(savefile.ia_AR[i], i);
            for (int i = 0; i < savefile.ia_S.Count; ++i)
                manage_S.Add(savefile.ia_S[i], i);
           
            Sauvegarde_.BackColor = System.Drawing.Color.Green;            
            textBox10.BackColor = System.Drawing.Color.Green;

            Sauvegarde_.Text = "" + savefile.levelProfile.fc_speed;
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
            switch (type)
            {
                case "IA_AA":
                    savefile.ia_AA.RemoveAt(spawn);
                    manage_AA.remove_all();
                    for (int i = 0; i < savefile.ia_AA.Count; ++i)
                        manage_AA.Add(savefile.ia_AA[i], i);
                    break;
                case "IA_AR":
                    savefile.ia_AR.RemoveAt(spawn);
                    manage_AR.remove_all();
                    for (int i = 0; i < savefile.ia_AR.Count; ++i)
                        manage_AR.Add(savefile.ia_AR[i], i);
                    break;
                case "IA_S":
                    savefile.ia_S.RemoveAt(spawn);
                    manage_S.remove_all();
                    for (int i = 0; i < savefile.ia_S.Count; ++i)
                        manage_S.Add(savefile.ia_S[i], i);
                    break;
                case "b":
                    savefile.bonus.RemoveAt(spawn);
                    ovni.remove_all();
                    for (int i = 0; i < savefile.bonus.Count; ++i)
                        ovni.Add(savefile.bonus[i].type, savefile.bonus[i].X, savefile.bonus[i].Y, i);
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
            }
            
            else
            {
                quaintuplet quaint = new quaintuplet();
                quaint.color = new Microsoft.Xna.Framework.Color(color2.R, color2.G, color2.B, color2.A);
                quaint.damage = int.Parse(puissance_p.Text);            
                
                quaint.seconde = seconde;
                quaint.speed = int.Parse(vitesse_p.Text);                
                quaint.vie = int.Parse(vie_p.Text);

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
                }
                else if (type == "IA_S")
                {
                    ias.X = savefile.ia_S[spawn].X;
                    ias.Y = savefile.ia_S[spawn].Y;
                    savefile.ia_S[spawn] = ias;
                    manage_S.remove_all();
                    for (int i = 0; i < savefile.ia_S.Count; ++i)
                        manage_S.Add(savefile.ia_S[i], i);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
