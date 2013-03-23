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
using Microsoft.Xna.Framework.Media;

namespace Umea_rana
{
    class Listbox
    {
        Sauveguarde save;
        Rectangle fond, UP, Down;
        Texture2D fondT, fleche;
        Rectangle[] rectangle;
        Color[] color;
        string[] level;
        SpriteFont font;
        int selected, oldselected, surlig, oldsur, latence = 0, lat = 10;
        public string selectedItem;
        public bool in_use;


        public Listbox(string type, int X, int Y, int width, int height)
        {
            selectedItem = "";
            save = new Sauveguarde();
            level = save.subdirectory(type);
            rectangle = new Rectangle[level.Length];
            color = new Color[level.Length];
            selected = 0;
            surlig = 0;
            oldsur = 0;
            fond = new Rectangle(X, Y, width, height);
            UP = new Rectangle(width + X - width / 10, Y + 10, width / 10, height / 10);
            Down = new Rectangle(width + X - width / 10, Y + height - height / 10, width / 10, height / 10);
            for (int i = 0; i < level.Length; ++i)
            {
                rectangle[i] = new Rectangle(X, Y * (i + 1) + height / 10, width - UP.Width, height / level.Length);
                color[i] = Color.White;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.font = Content.Load<SpriteFont>("FontList");
            this.fondT = Content.Load<Texture2D>("ListBoxBG");
            this.fleche = Content.Load<Texture2D>("fleche");
        }

        public void Update(ref KeyboardState keyboard, ref MouseState mouse)
        {
            Rectangle Mouse_rec;
            Mouse_rec = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouse.LeftButton == ButtonState.Pressed && Mouse_rec.Intersects(fond))
                in_use = true;
            else if (mouse.LeftButton == ButtonState.Pressed && !Mouse_rec.Intersects(fond))
                in_use = false;
            if (in_use)
            {
                for (int i = 0; i < rectangle.Length; ++i)
                {
                    if (Mouse_rec.Intersects(rectangle[i]))
                    {
                        surlig = i;
                        color[oldsur] = Color.White;// desurligne              
                        color[surlig] = Color.Yellow;// surligner  
                        oldsur = surlig;
                    }
                }
                if (latence > 0)
                    latence--;
                else
                {
                    if (keyboard.IsKeyDown(Keys.Down))
                    {
                        if (surlig != level.Length - 1)
                            surlig = (surlig + 1) % level.Length;
                        latence = lat;
                        color[oldsur] = Color.White;
                        if (!fond.Contains(rectangle[surlig]))
                            Downlist();

                        oldsur = surlig;

                    }
                    else if (keyboard.IsKeyDown(Keys.Up))
                    {
                        if (surlig != 0)
                            surlig = (surlig - 1) % level.Length;
                        latence = lat;
                        color[oldsur] = Color.White;
                        if (!fond.Contains(rectangle[surlig]))
                            Uplist();
                        oldsur = surlig;

                    }

                    color[surlig] = Color.Yellow;// surligner
                }
                // selection
                if ((mouse.LeftButton == ButtonState.Pressed))
                {
                    if ((Mouse_rec.Intersects(rectangle[surlig])) || keyboard.IsKeyDown(Keys.Right))
                    {
                        selected = surlig;
                        color[oldselected] = Color.White;
                        selectedItem = level[selected];
                        oldselected = selected;
                    }
                    // UP Down

                    if (Down.Intersects(Mouse_rec))
                        Downlist();
                    if (UP.Intersects(Mouse_rec))
                        Uplist();
                }
                color[selected] = Color.Azure;
            }
        }

        public void Draw(SpriteBatch spritbach)
        {
            spritbach.Draw(fondT, fond, Color.White);
            for (int i = 0; i < rectangle.Length; ++i)
            {
                if (fond.Contains(rectangle[i]))
                {
                    spritbach.Draw(fondT, rectangle[i], color[i]);
                    spritbach.DrawString(font, level[i], new Vector2(rectangle[i].X, rectangle[i].Y), Color.Black);
                }
            }
            // vecteur a modif
            spritbach.Draw(fleche, UP, new Rectangle(0, 0, fleche.Width, fleche.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
            spritbach.Draw(fleche, Down, new Rectangle(0, 0, fleche.Width, fleche.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
       //     spritbach.DrawString(font, "selected Item : " + selected, new Vector2(fond.X, fond.Y), Color.Black);
        }
        private void Uplist()
        {
            if (rectangle[0].Top < fond.Top)
                for (int i = 0; i < rectangle.Length; ++i)
                    rectangle[i].Y += 1;
        }
        private void Downlist()
        {
            if (rectangle[rectangle.Length - 1].Top > fond.Top)
                for (int i = 0; i < rectangle.Length; ++i)
                    rectangle[i].Y -= 1;
        }

    }
}
