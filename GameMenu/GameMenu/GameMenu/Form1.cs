using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Umea_rana
{
    public partial class Form1 : Form
    {

        string charger, filename;
        Sauveguarde sauve;
        savefile savefile;
        List<string> subdirectory;
        public bool loading;
        string type;
        public string file { get { return filename; } }

        public Form1()
        {
            InitializeComponent();
            charger = "charger";
            sauve = new Sauveguarde();
            savefile = new savefile();
            savefile.ia_Kamikaze = new List<couple>();
            savefile.ia_tireur = new List<quaintuplet>();
            savefile.ia_viseur = new List<quaintuplet>();
            subdirectory = new List<string>();
            this.Show();
            subdirectory = new List<string>();
            button1.Enabled = false;
            button1.Text = charger;
            type = "S_";
            string[] hello = sauve.subdirectory(type );
            if (hello.Length == 0)
            {
                listBox1. Enabled = false;
                listBox1.Text = "vide";
            }
            else
            {
                foreach (string h in hello)
                    listBox1.Items.Add(h);
                 listBox1.Enabled = true;
                 listBox1.Text = "dossier existant";
            }
            loading = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sauve.load(ref filename,ref savefile );
          
            this.Hide();
            loading = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            filename = (string)listBox1.SelectedItem;
        }


    }
}
