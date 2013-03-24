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
        Rectangle fond, UP, Down, Top, right, left,Botom;
        Texture2D fondT, fleche;
        Rectangle[] rectangle;
        Color[] color;
        string[] level;
        SpriteFont font;
        int selected, oldselected, surlig, oldsur, latence = 0, lat = 10,tab;
        public string selectedItem;
        public bool in_use;
        Color BGcolor;


        public Listbox(string type, float x, float y, float  _width, float  _height, int WindoW,int windowH, int tab)
        {
            int X = (int)(x * (float)WindoW );
            int Y = (int)(y * (float)windowH );
            int width = (int)(_width * (float)WindoW);
            int height = (int)(_height * (float)windowH);

            selectedItem = "";
            save = new Sauveguarde();
            level = save.subdirectory(type);
            rectangle = new Rectangle[level.Length];
            color = new Color[level.Length];
            selected = 0;
            surlig = 0;
            oldsur = 0;
            Top = new Rectangle(X, Y, width, height / 10);
            Botom = new Rectangle(X, Y + height - height / 10, width, height / 10);
            right = new Rectangle(width + X - width / 10, Y, width / 10, height);
            left = new Rectangle(X, Y, right.Width, right.Height);

            fond = new Rectangle(X+right.Width , Y + Top.Height, width-right.Width -left.Width , height -2*height/10);
            UP = new Rectangle(right.X, Y + 10, width / 10, height / 10);
            Down = new Rectangle(width + X - width / 10, Y + height - height / 10, width / 10, height / 10);
            for (int i = 0; i < level.Length; ++i)
            {
                rectangle[i] = new Rectangle(X+right.Width , Y +height/10  * (i) + height / 10, width -2*right .Width, height /10);
                color[i] = Color.White;
            }
            BGcolor = Color.DarkSlateGray;
            this.tab = tab;
        }

        public void LoadContent(ContentManager Content)
        {
            this.font = Content.Load<SpriteFont>("FontList");
            this.fondT = Content.Load<Texture2D>("ListBoxBG");
            this.fleche = Content.Load<Texture2D>("fleche");

        }

        public void Update(ref KeyboardState keyboard, ref MouseState mouse,ref int tab)
        {
            Rectangle Mouse_rec;
            Mouse_rec = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (Mouse_rec.Intersects(fond) || Mouse_rec.Intersects(Top) || Mouse_rec.Intersects(Botom)
                    || Mouse_rec.Intersects(right) || Mouse_rec.Intersects(left))
                {
                    in_use = true;
                    tab = this.tab;
                }
                else
                {
                    in_use = false;
                    tab = 0;
                }
            }
            else if (this.tab == tab)
            {
                in_use = true;
            }
            else
                in_use = false;

            if (in_use)
            {
                BGcolor = Color.LightSlateGray;
                for (int i = 0; i < rectangle.Length; ++i)
                {
                    if (Mouse_rec.Intersects(rectangle[i]))
                    {
                        surlig = i;
                        color[oldsur] = Color.White;// desurligne              
                        color[surlig] = Color.Yellow;// surligner  
                        oldsur = surlig;
                    }
                    else
                        color[i] = Color.White;
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
                            for (int i = 0; i < rectangle.Length; ++i)
                                rectangle[i].Y -= rectangle[i].Height;


                        oldsur = surlig;

                    }
                    else if (keyboard.IsKeyDown(Keys.Up))
                    {
                        if (surlig != 0)
                            surlig = (surlig - 1) % level.Length;
                        latence = lat;
                        color[oldsur] = Color.White;
                        if (!fond.Contains(rectangle[surlig]))
                            for (int i = 0; i < rectangle.Length; ++i)
                                rectangle[i].Y += rectangle[i].Height;
                        oldsur = surlig;

                    }

                    color[surlig] = Color.Yellow;// surligner
     
                }
                // selection
                if ((mouse.LeftButton == ButtonState.Pressed && Mouse_rec.Intersects(rectangle[surlig]) && Mouse_rec.Intersects(fond ))
                    ||keyboard.IsKeyDown(Keys.Enter ) )
                {                  
                        selected = surlig;
                        color[oldselected] = Color.White;
                        selectedItem = level[selected];
                        oldselected = selected;                    
                    // UP Down
           
                }
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (Down.Intersects(Mouse_rec))
                        Downlist();
                    if (UP.Intersects(Mouse_rec))
                        Uplist();
                }
                color[selected] = Color.Azure;
            }
            else
                BGcolor = Color.DarkSlateGray;
        }

        public void Draw(SpriteBatch spritbach)
        {
            spritbach.Draw(fondT, fond,Color.White );
            for (int i = 0; i < rectangle.Length; ++i)
            {
                if (fond.Contains(rectangle[i]))
                {
                    spritbach.Draw(fondT, rectangle[i], color[i]);
                    spritbach.DrawString(font, level[i], new Vector2(rectangle[i].X, rectangle[i].Y), Color.Black);
                    spritbach.Draw(fondT, new Rectangle(rectangle[i].X, rectangle[i].Top, rectangle[i].Width, 1), Color.Black);
                    spritbach.Draw(fondT, new Rectangle(rectangle[i].X, rectangle[i].Bottom , rectangle[i].Width, 1), Color.Black);
                }
            }
            // vecteur a modif
            spritbach.Draw(fondT, Top, BGcolor);
            spritbach.Draw(fondT,Botom  , BGcolor);
            spritbach.Draw(fondT, right, BGcolor);
            spritbach.Draw(fondT, left, BGcolor);
            spritbach.Draw(fondT, new Rectangle(fond.Left , fond.Y , 1, fond.Height), Color.Black);
            spritbach.Draw(fondT, new Rectangle(fond.Right , fond.Y, 1, fond.Height), Color.Black);
            spritbach.Draw(fondT, new Rectangle(fond.Left, fond.Top , fond.Width  , 1), Color.Black);
            spritbach.Draw(fondT, new Rectangle(fond.Left, fond.Bottom, fond.Width, 1), Color.Black);
            spritbach.Draw(fleche, UP, new Rectangle(0, 0, fleche.Width, fleche.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
            spritbach.Draw(fleche, Down, new Rectangle(0, 0, fleche.Width, fleche.Height), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0);
            spritbach.DrawString(font, "selected Item : " + selectedItem, new Vector2(Top.X+right.Width , Top.Y), Color.Black);
        }
        private void Uplist()
        {
            if (rectangle[0].Top <= fond.Top)
                for (int i = 0; i < rectangle.Length; ++i)
                    rectangle[i].Y += 1;
        }
        private void Downlist()
        {           
            if (rectangle[rectangle.Length -2].Bottom >= fond.Bottom )
            {
                for (int i = 0; i < rectangle.Length; ++i)
                    rectangle[i].Y -= 1;

            }
        }

    }
}
