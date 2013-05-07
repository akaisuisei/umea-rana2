using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using Umea_rana.LocalizedStrings;

namespace Umea_rana
{
    public class Score
    {
        int scoreJ1, scoreJ2;
        float vieJ1, vieJ2;
        int bombej1, bombej2;
        int powerj1, powerj2;
        bool activate;
        int highscore;
        Rectangle fond, fond2, image1, image2;
        SpriteFont font;
        Texture2D texturej1, texturej2;
        public Score()
        {
            this.scoreJ1 = 0;
            this.scoreJ2 = 0;
            this.vieJ1 = 0;
            this.vieJ2 = 0;
            this.bombej1 = 0;
            this.bombej2 = 0;
            highscore = 0;
        }
        public void LoadContent(Rectangle fond, Rectangle fond2, ContentManager Content)
        {
            this.fond = fond;
            this.fond2 = fond2;
            this.font = Content.Load<SpriteFont>("FontList");
            image1 = new Rectangle(fond.X, (int)((float)fond.Height * 7f / 14f), fond.Width, fond.Height - (int)((float)fond.Height * 7f / 14f));
            image2 = new Rectangle(fond2.X, (int)((float)fond2.Height * 8f / 14f), fond2.Width, fond2.Height - (int)((float)fond2.Height * 7f / 14f));
            texturej1 = Content.Load<Texture2D>("hero\\image1");
            texturej2 = Content.Load<Texture2D>("hero\\image2");
        }
        public void Update(ref sripte_V perso1, ref sripte_V perso2)
        {
            this.scoreJ1 = perso1.scrore;
            this.vieJ1 = perso1.vie;
            this.bombej1 = perso1.bomb;
            this.powerj1 = perso1.power;
            this.scoreJ2 = perso2.scrore;
            this.vieJ2 = perso2.vie;
            this.bombej2 = perso2.bomb;
            this.powerj2 = perso2.power;
            activate = perso2.activate;
            highscore = Math.Max(Math.Max(scoreJ1, scoreJ2), highscore);
        }
        public void Draw(SpriteBatch sp)
        {
            sp.Draw(texturej1, image1, Color.White);
            sp.DrawString(font, LocalizedString.Highscore, new Vector2(fond2.Left, 0f / 14f * fond2.Height), Color.White);
            sp.DrawString(font, "" + highscore, new Vector2(fond2.Left, 1f / 14f * fond2.Height), Color.White);
            sp.DrawString(font, LocalizedString.player + " 1", new Vector2(fond.Left, 0f / 14f * fond.Height), Color.White);
            sp.DrawString(font, LocalizedString.Score, new Vector2(fond.Left, 1f / 14f * fond.Height), Color.White);
            sp.DrawString(font, "" + scoreJ1, new Vector2(fond.Left, 2f / 14f * fond.Height), Color.White);
            sp.DrawString(font, LocalizedString.Life, new Vector2(fond.Left, 3f / 14f * fond.Height), Color.White);
            sp.DrawString(font, "" + vieJ1, new Vector2(fond.Left, 4f / 14f * fond.Height), Color.White);
            sp.DrawString(font, LocalizedString.bomb, new Vector2(fond.Left, 5f / 14f * fond.Height), Color.White);
            sp.DrawString(font, "" + bombej1, new Vector2(fond.Left, 6f / 14f * fond.Height), Color.White);


            if (activate)
            {
                sp.Draw(texturej2, image2, Color.White);
                sp.DrawString(font, LocalizedString.player + " 2", new Vector2(fond2.Left, 2f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, LocalizedString.Score, new Vector2(fond2.Left, 3f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, "" + scoreJ2, new Vector2(fond2.Left, 4f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, LocalizedString.Life, new Vector2(fond2.Left, 5f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, "" + vieJ2, new Vector2(fond2.Left, 6f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, LocalizedString.bomb, new Vector2(fond2.Left, 7f / 14f * fond2.Height), Color.White);
                sp.DrawString(font, "" + bombej2, new Vector2(fond2.Left, 8f / 14f * fond2.Height), Color.White);
            }
        }
    }
}
