using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Umea_rana
{
    public partial class UserControl1 : UserControl
    {
        string life, speed,couleur;
        public UserControl1()
        {
            life = "point de vie";
                speed="vitesse";
                couleur = "couleur de tir";
            InitializeComponent();
            label1.Text = life;
            label2.Text = speed;
            label4.Text = couleur;
        }



        
    }
}
