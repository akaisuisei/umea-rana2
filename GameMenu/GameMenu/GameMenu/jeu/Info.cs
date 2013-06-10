using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Umea_rana.LocalizedStrings;
using Microsoft.Xna.Framework;


namespace Umea_rana.jeu
{

    class info
    {
        Rectangle fond;
        int nb_iaT, nbIA_V, nb_objet, nb_iaK, pVie, bvie;
        SpriteFont font;
        public info()
        {
            nb_iaK = 0;
            nb_iaT = 0;
            nb_objet = 0;
            nbIA_V = 0;
            pVie = 0;
            bvie = 0;
        }
        public void Update(int iak, int iat, int iav, int ob, int vie, int vieboss)
        {
            this.nb_iaK = iak;
            this.nb_iaT = iat;
            this.nb_objet = ob;
            this.nbIA_V = iav;
            this.pVie = vie;
            this.bvie = vieboss;
        }
        public void LoadContent(Rectangle fond, SpriteFont font)
        {
            this.fond = fond;
            this.font = font;
        }
        public void draw(SpriteBatch sp)
        {
            sp.DrawString(font, LocalizedString.player_life + ":", new Vector2(fond.X, 0 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + pVie, new Vector2(fond.X, 1 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.nb_IAK + ":", new Vector2(fond.X, 2 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + nb_iaK, new Vector2(fond.X, 3 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.nb_iaV + ":", new Vector2(fond.X, 4 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + nbIA_V, new Vector2(fond.X, 5 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.nb_iaT + ":", new Vector2(fond.X, 6 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + nb_iaT, new Vector2(fond.X, 7 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.nb_objet + ":", new Vector2(fond.X, 8 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + nb_objet, new Vector2(fond.X, 9 * fond.Height / 12), Color.White);
            sp.DrawString(font, LocalizedString.BossLife + ":", new Vector2(fond.X, 12 * fond.Height / 12), Color.White);
            sp.DrawString(font, "" + bvie, new Vector2(fond.X, 13 * fond.Height / 12), Color.White);
        }
    }
}
