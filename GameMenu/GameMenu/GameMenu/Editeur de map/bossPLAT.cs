using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Umea_rana.Editeur_de_map
{
    struct bossP
    {
        public int degat;
        public int vie;
        public char type;
    }
    class bossPLAT : objet
    {
        int degat;
        int vie;
        char type;
        Texture2D texture;
        public List<Rectangle> ptfaible { get; private set; }
        public List<Rectangle> ptfort { get; private set; }
        public bossPLAT()
        {
            degat = 1;
            vie = 1;
            ptfaible = new List<Rectangle>();
            ptfort = new List<Rectangle>();
        }
        public void parrametrage(bossP bp)
        {
            this.degat = bp.degat;
            this.vie = bp.vie;
            this.type = bp.type;
        }
        public void loadContent(ContentManager content, Texture2D texture)
        {
            this.texture = texture;
        }
        public void Update()
        {
            if(vie<=0)
            {
            }
            else
            {
            }
        }
    }
}
