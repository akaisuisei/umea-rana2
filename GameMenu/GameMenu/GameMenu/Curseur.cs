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
        Rectangle taille_du_petit_curseur;
        Texture2D curseur_rectangle;
        Rectangle taille_de_la_ligne;
        Vector2 _position_ligne { get; set; }

        Texture2D ligne;
        int latence = 5;
        float _volume { get; set; }

        public Curseur(Vector2 position_ligne, Rectangle _r_curseur_rectangle, Rectangle _r_ligne, float volume)
        {
            _position_ligne = position_ligne;
            _volume = volume;
            taille_du_petit_curseur = _r_curseur_rectangle;
            taille_de_la_ligne = _r_ligne;
            taille_de_la_ligne.Height = 10;
        }
        public void Initialize()
        {
        }
        public void LoadContent(ContentManager Content)
        {
            curseur_rectangle = Content.Load<Texture2D>("fleche");
            ligne = Content.Load<Texture2D>("ListBoxBG");
        }
        public void update(KeyboardState keyboard, MouseState mouse)
        {
            if (latence < 0)
            {
                if (_volume > 0f)

                    if (keyboard.IsKeyDown(Keys.Left))
                    {
                        _volume -= 0.05f;
                        latence = 5;
                    }

                if (_volume < 1.0f)
                    if (keyboard.IsKeyDown(Keys.Right))
                    {
                        _volume += 0.05f;
                        latence = 5;
                    }
                if (taille_du_petit_curseur.Contains(mouse.X, mouse.Y))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        MouseState current_mouse = new MouseState();
                        if (mouse.X.CompareTo(current_mouse.X) > 0)
                            _volume = (float)((taille_du_petit_curseur.Center.X - _position_ligne.X) - (mouse.X - current_mouse.X)) / (float)(taille_de_la_ligne.Width);
                        if (mouse.X.CompareTo(current_mouse.X) < 0)
                            _volume = (float)((taille_du_petit_curseur.Center.X - _position_ligne.X) + (current_mouse.X - mouse.X)) / (float)(taille_de_la_ligne.Width);

                        latence = 2;
                        if (_volume > 1.0f)
                            _volume = 1.0f;
                        if (_volume < 0f)
                            _volume = 0f;
                    }
                }
            }

            latence--;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ligne, taille_de_la_ligne, Color.White);
            spriteBatch.Draw(curseur_rectangle, new Vector2(_position_ligne.X + _volume * taille_de_la_ligne.Width - taille_du_petit_curseur.Center.X, _position_ligne.Y - taille_du_petit_curseur.Height / 2), taille_du_petit_curseur, Color.White);
        }
    }
}
