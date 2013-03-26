using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Umea_rana;

namespace Umea_rana
{
    class Button
    {
        Rectangle[,] rect;
        string[,] gameState;
        string[,] name;
        int WindowH, WindowW;
        float height, width;
        SpriteFont font;
        int latence = 0, X, Y, G_latence=200, tab;
        Texture2D selection;
        Texture2D test;
        Vector2 select;
        bool intecep;
        
        public Button(int lenghtX, int lengthY, int Wwidt, int Wheigth, float height, float width, int tab)
        {
            rect = new Rectangle[lenghtX, lengthY];
            gameState = new string[lenghtX, lengthY];
            name = new string[lenghtX, lengthY];

            this.WindowH = Wheigth;
            this.WindowW = Wwidt;
            this.height = height * (float)Wheigth;
            this.width = width * (float)Wwidt;
            X = 0;
            Y = 0;
            select = Vector2.Zero;
            this.tab = tab;
        }
        public void LoadContent(ContentManager Content)
        {
            this.font = Content.Load<SpriteFont>("FontList");
            selection = Content.Load<Texture2D>("Menu//selection");
            test = Content.Load<Texture2D>("ListBoxBG");
        }


        public void activate(int i, int j, float X, float Y, string gameState, string name)
        {
            this.rect[i, j] = new Rectangle((int)(WindowW  * X), (int)(WindowH  * Y), (int)width, (int)height);
            this.gameState[i, j] = gameState;
            this.name[i, j] = name;

        }
        public void Update(ref KeyboardState Key, ref MouseState mouse,ref Rectangle  mouse_rec,   ref Game1  game, ref int tab, string name1)
        {
            if (tab == this.tab)
            {
                intecep = false;
                
                for (int i = 0; i < rect.GetLength(0); ++i)
                    for (int j = 0; j < rect.GetLength(1); ++j)
                    {
                        if (mouse_rec.Intersects(rect[i, j]))
                        {
                            X = i;
                            Y = j;
                            intecep = true;
                        }
                    }

                    latence--;    // sinon les changements sont bien trop rapides
               if (Key.IsKeyDown(Keys.Up))
                {
                    Y = (Y + 1) % rect.GetLength(1);
                    latence = 10;
                }
                else if (Key.IsKeyDown(Keys.Down))
                {
                    Y = (Y - 1) % rect.GetLength(1);
                    latence = 10;
                }
                else if (Key.IsKeyDown(Keys.Right))
                {
                    X = (X + 1) % rect.GetLength(0);
                    latence = 10;
                }
                else if (Key.IsKeyDown(Keys.Left))
                {
                    X = (X - 1) % rect.GetLength(0);
                    latence = 10;
                }
                select = new Vector2(rect[X, Y].X, rect[X, Y].Y);
                if ((intecep && mouse.LeftButton == ButtonState.Pressed) || Key.IsKeyDown(Keys.Enter))
                {
                    if (name1 != string.Empty)
                        game.level = name1;
                    else
                        game.level = name[X, Y];
                    
                    if (gameState[X, Y] == "SEU")
                        game.ChangeState(Game1.gameState.SEU);
                    else if (gameState[X, Y] == "checkpause")
                        game.ChangeState(Game1.gameState.Checkpause);
                    else if (gameState[X, Y] == "EditSEU")
                        game.ChangeState(Game1.gameState.Editeur_mapVV);
                    else if (gameState[X, Y] == "init")
                        game.ChangeState(Game1.gameState.Initialisateur);
                    else if (gameState[X, Y] == "LevelSelect")
                        game.ChangeState(Game1.gameState.Level_select_state);
                    else if (gameState[X, Y] == "level2")
                        game.ChangeState(Game1.gameState.Level2);
                    else if (gameState[X, Y] == "Main")
                        game.ChangeState(Game1.gameState.MainMenuState);
                    else if (gameState[X, Y] == "Null")
                        game.ChangeState(Game1.gameState.Null);
                    else if (gameState[X, Y] == "Option")
                        game.ChangeState(Game1.gameState.OptionsState);
                    else if (gameState[X, Y] == "Pause")
                        game.ChangeState(Game1.gameState.Pause);
                    else if (gameState[X, Y] == "play")
                        game.ChangeState(Game1.gameState.PlayingState);
                    else if (gameState[X, Y] == "LevelEdit")
                    {
                        if (game.level != "")
                            game.ChangeState(Game1.gameState.leveleditor);            
                    }
                    else
                        game.ChangeState(Game1.gameState.MainMenuState);
           
                    System.Threading.Thread.Sleep(G_latence);
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < rect.GetLength(0); ++i)
                for (int j = 0; j < rect.GetLength(1); ++j)
                {
                    spriteBatch.DrawString(font, name[i, j], new Vector2(rect[i, j].X, rect[i, j].Y), Color.Black);
        //            spriteBatch.Draw(test, rect[i,j], Color.BlueViolet);
                }

            spriteBatch.Draw(selection, select, Color.White);
        }

    }
}
