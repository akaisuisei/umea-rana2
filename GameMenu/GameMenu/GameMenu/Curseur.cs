using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Umea_rana
{
    public class Curseur
    {
        Rectangle r_curseur_rectangle;
        Texture2D curseur_rectangle;
        Rectangle r_ligne;
        Vector2 _position_ligne { get; set; }
        Vector2 _position_curseur { get; set; }
        Texture2D ligne;
        float _volume { get; set; }
        public Curseur(Vector2 position_ligne, Vector2 position_curseur,float volume)
        {
            _position_curseur = position_curseur;
            _position_ligne = position_ligne;
            _volume = volume;
        }
        public void Initialize()
        {
        }
        public void LoadContent(ContentManager content)
        {
            curseur_rectangle = content.Load<Texture2D>("");
            ligne = content.Load<Texture2D>("");
        }
        public static void update(KeyboardState keyboard,MouseState mouse)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ligne, _position_ligne, r_ligne, Color.White);
            spriteBatch.Draw(curseur_rectangle, new Vector2(_position_ligne.X * _volume, _position_ligne.Y - r_curseur_rectangle.Height / 2), r_curseur_rectangle, Color.White);
        }
    }
}
