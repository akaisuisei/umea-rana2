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

namespace Umea_rana
{
    public partial class UserControl1 : Form
    {
        string life, speed, couleur, onglet1, onglet2, onglet3, onglet4, trajectoir, align, OK, firerate, end;
        string imagefond, vitessefond, vitesseV, open, cancel, load, save, filepath, filepathlabel;
        System.Drawing.Color color2, color4;
        int width, height;
        int seconde;
        public bool IHave_control;
        string imageB;
        float openX, openY;
        IA_manager_T manage_T;
        IA_manager_V manage_V;
        IA_manager_K manage_k;
        Sauveguarde sauve;
        savefile savefile;
        List<string> subdirectory;

        public UserControl1(IA_manager_T manage_T, IA_manager_V manage_V, IA_manager_K manage_k)
        {
            InitializeComponent();
            Initialize();
            this.Hide();
            color4 = System.Drawing.Color.Black;
            imagefond = string.Empty;
            this.width = Screen.PrimaryScreen.Bounds.Width;
            this.height = Screen.PrimaryScreen.Bounds.Height;
            IHave_control = false;
            //    game = this.game;

            this.manage_T = manage_T;
            this.manage_V = manage_V;
            this.manage_k = manage_k;
            seconde = 0;
            filepath = string.Empty;
            sauve = new Sauveguarde();
            savefile = new savefile();
            savefile.ia_Kamikaze = new List<couple>();
            savefile.ia_tireur = new List<quaintuplet>();
            savefile.ia_viseur = new List<quaintuplet>();
            subdirectory = new List<string>();
            button9.Enabled = false;
            string[] hello = sauve.subdirectory();
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

        public void _show(int X, int y)
        {        IHave_control = true;
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
            this.Location = new System.Drawing.Point(X, y);
            this.Show();
    
        }

        public void update(ref IA_manager_T manage_T, ref IA_manager_V manage_V, ref IA_manager_K manage_k, ref KeyboardState keybord)
        {
            manage_T = this.manage_T;
            manage_V = this.manage_V;
            manage_k = this.manage_k;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                ++seconde;
            if (keybord.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                --seconde;
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


        #region texbox chek
        private void intcheck(TextBox texbox)
        {
            int res;
            if (texbox.Text != string.Empty)
                if (int.TryParse(texbox.Text, out res))
                    if (res <= 15)
                        texbox.BackColor = System.Drawing.Color.Green;
                    else
                    {
                        texbox.BackColor = System.Drawing.Color.Red;
                    }
                else
                {
                    texbox.BackColor = System.Drawing.Color.Red;
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox3);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox6);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox4);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox5);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox7);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox8);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            intcheck(textBox9);
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
            filepath = text;
            textbox.Text = filepath;

        }


        #endregion

        #region dialogopen

        private void button2_Click(object sender, EventArgs e)// tab1 color
        {
            colorDialog1.ShowDialog();
            color2 = colorDialog1.Color;
            button2.BackColor = color2;
        }

        private void button6_Click(object sender, EventArgs e)// tab4 color
        {
            colorDialog2.ShowDialog();
            color4 = colorDialog2.Color;
            button6.BackColor = color4;
        }
        private void button5_Click(object sender, EventArgs e)// tab4 showfile
        {
            openFileDialog1.ShowDialog();
            imageB = openFileDialog1.FileName;
        }

        #endregion



        private void Initialize()
        {
            life = "point de vie";
            speed = "vitesse";
            couleur = "couleur de tir";
            onglet1 = "Tireur";
            onglet2 = "Viseur";
            onglet3 = "kamikaze";
            onglet4 = "fond";
            trajectoir = "trajectoire";
            align = "alignement enemie";
            OK = "OK";
            firerate = "cadence de tir";
            end = "terminer";
            imagefond = "image du fond";
            vitessefond = "vitesse du fond";
            vitesseV = "vitesse du vaissau";
            open = "ouvrir un dossier";
            cancel = "annulee";
            load = "charger un fichier";
            save = "sauvegarder";
            filepathlabel = "nom du niveau";
            color2 = System.Drawing.Color.Black;



            //tap page

            tabPage2.Text = onglet1;// +"/" + onglet2;
            tabPage3.Text = onglet3;
            tabPage4.Text = onglet4;
            tabPage1.Text = cancel;

            //tab2
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
            button2.BackColor = button1.BackColor;
            label1.Text = firerate;
            button1.Text = OK;
            //tab3
            label8.Text = life;
            label9.Text = speed;
            button3.Text = OK;
            // tab 4
            label2.Text = imagefond;
            button5.Text = open;
            label10.Text = vitessefond;
            label11.Text = vitesseV;
            label12.Text = firerate;
            label13.Text = couleur;
            button4.Text = end;
            button6.Text = couleur;
            button6.BackColor = button5.BackColor;
            //tab 5
            button7.Text = cancel;
            button8.Text = save;
            button9.Text = load;
            label14.Text = filepathlabel;
            if (textBox10.Text == string.Empty)
                textBox10.BackColor = System.Drawing.Color.Red;
            else
                textBox10.BackColor = System.Drawing.Color.Green;
            textBox1.BackColor = TextBox.DefaultBackColor;
            textBox2.BackColor = TextBox.DefaultBackColor;
            textBox3.BackColor = TextBox.DefaultBackColor;
            textBox4.BackColor = TextBox.DefaultBackColor;
            textBox5.BackColor = TextBox.DefaultBackColor;
            textBox6.BackColor = TextBox.DefaultBackColor;
            textBox7.BackColor = TextBox.DefaultBackColor;
            textBox8.BackColor = TextBox.DefaultBackColor;
            textBox9.BackColor = TextBox.DefaultBackColor;

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;

            openX = 0;
            openY = 0;
        }
        #region validate button

        private void button3_Click(object sender, EventArgs e)// tab2
        {
            if (textBox4.BackColor == System.Drawing.Color.Green && textBox5.BackColor == System.Drawing.Color.Green)
            {
                couple couple = new couple();
                couple.X = openX;
                couple.Y = openY;
                couple.seconde = seconde;
                savefile.ia_Kamikaze.Add(couple);

                manage_k.Add(openX, openY, seconde);
                this.hidou();

            }
        }
        private void button7_Click(object sender, EventArgs e)// cancel
        {
            this.hidou();
        }
        private void button8_Click(object sender, EventArgs e)// validate
        {
            savegame(filepath);
            this.hidou();
        }
        private void button1_Click_1(object sender, EventArgs e)// tab
        {

            if (textBox1.BackColor == System.Drawing.Color.Green && textBox2.BackColor == System.Drawing.Color.Green &&
                textBox3.BackColor == System.Drawing.Color.Green && textBox6.BackColor == System.Drawing.Color.Green &&
                color2 != System.Drawing.Color.Black)
            {
                quaintuplet quaint = new quaintuplet();
                quaint.color = new Microsoft.Xna.Framework.Color(color2.R, color2.B, color2.G, color2.A);
                quaint.X = openX;
                quaint.Y = openY-1;
                quaint.seconde = seconde;
                quaint.nombre = int.Parse(textBox3.Text);

                if (radioButton1.Checked)
                {

                    savefile.ia_tireur.Add(quaint);
                    manage_T.Add(openX, openY, seconde, quaint.nombre, quaint.color);
                    this.hidou();
                }
                else if (radioButton2.Checked)
                {
                    savefile.ia_viseur.Add(quaint);
                    manage_V.Add(openX, openY, seconde, int.Parse(textBox3.Text), new Microsoft.Xna.Framework.Color(color2.R, color2.B, color2.G, color2.A));
                    this.hidou();
                }

            }
        } // tab1
        private void button4_Click(object sender, EventArgs e) //tab4
        {
            if (textBox1.BackColor == System.Drawing.Color.Green && textBox8.BackColor == System.Drawing.Color.Green &&
                textBox9.BackColor == System.Drawing.Color.Green &&
                color4 != System.Drawing.Color.Black && imageB != string.Empty)
            {

                this.hidou();
            }

        }



        private void button9_Click(object sender, EventArgs e)// load
        { 
            
            loadgame((string)  comboBox2.SelectedItem );
            hidou();
        }

        #endregion

        private void savegame(string file_name)
        {
            sauve.save(file_name, ref savefile.ia_tireur, ref savefile.ia_viseur, ref savefile.ia_Kamikaze);
        }

        private void loadgame(string file_name)
        {
            sauve.load(file_name, ref savefile.ia_tireur, ref savefile.ia_viseur, ref savefile.ia_Kamikaze);

            for (int i = 0; i < savefile.ia_tireur.Count; ++i)
                manage_T.Add(savefile.ia_tireur[i].X, savefile.ia_tireur[i].Y+1, savefile.ia_tireur[i].seconde, savefile.ia_tireur[i].nombre, savefile.ia_tireur[i].color);
            for (int i = 0; i < savefile.ia_viseur.Count; ++i)
                manage_V.Add(savefile.ia_viseur[i].X, savefile.ia_viseur[i].Y,savefile . ia_viseur[i].seconde, savefile.ia_viseur[i].nombre, savefile.ia_viseur[i].color);
            for (int i = 0; i < savefile.ia_Kamikaze.Count; ++i)
                manage_k.Add(savefile.ia_Kamikaze[i].X, savefile.ia_Kamikaze[i].Y, savefile.ia_Kamikaze[i].seconde);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            nameCheck(ref textBox10);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button9.Enabled = true;
        }


    }
}
