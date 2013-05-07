
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Umea_rana
{
    public class scoreplat
    {
        Color color;
        Texture2D texture;
        Rectangle fond1, fond2;
        int viemax, maxwidth, width;
        float vie;
        public scoreplat()
        {
        }
        public void LoadContent(Rectangle fond, ContentManager content)
        {
            fond1 = new Rectangle(fond.X + (int)(fond.Width * 0.05), fond.Y + (int)(fond.Height * 0.05), (int)(fond.Width * 0.4), (int)(fond.Height * 0.07));
            fond2 = new Rectangle(fond1.X + 3, fond1.Y + 3, fond1.Width - 6, fond1.Height - 6);
            maxwidth = fond2.Width;
            texture = content.Load<Texture2D>("ListBoxBG");
            viemax = 300;
        }
        public void Update(ref sprite_broillon sprite)
        {
            vie = sprite.vie;
            fond2.Width = (int)((float)vie / (float)viemax * (float)maxwidth);
            if ((float)vie / (float)viemax < 0.2f)
                color = Color.Red;
            else
                color = Color.Green;
        }
        public void Draw(SpriteBatch sp)
        {
            sp.Draw(texture, fond1, Color.Gray);
            sp.Draw(texture, fond2, color);
        }
    }
}
