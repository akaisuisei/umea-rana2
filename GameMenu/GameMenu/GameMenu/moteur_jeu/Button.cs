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
        string[,] name, levelname;
        int WindowH, WindowW;
        float height, width;
        SpriteFont font;
        int X, Y, G_latence = 200, tab;
        Texture2D selection;
        Texture2D test;
        Rectangle  select;
        bool intecep, oldintercept;
        /// <summary>
        /// bouton du jeu 
        /// </summary>
        /// <param name="lenghtX">nombre de button en X</param>
        /// <param name="lengthY">nombre de button en Y</param>
        /// <param name="Wwidt"></param>
        /// <param name="Wheigth"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="tab"></param>
        public Button(int lenghtX, int lengthY, int Wwidt, int Wheigth, float height, float width, int tab)
        {
            rect = new Rectangle[lenghtX, lengthY];
            gameState = new string[lenghtX, lengthY];
            name = new string[lenghtX, lengthY];
            levelname = new string[lenghtX, lengthY];
            this.WindowH = Wheigth;
            this.WindowW = Wwidt;
            this.height = height * (float)Wheigth;
            this.width = width * (float)Wwidt;
            X = 0;
            Y = 0;
            select = new Rectangle(-30, 0,(int)this.width, (int)this.height); 
            this.tab = tab;
        }
        public void LoadContent(ContentManager Content)
        {
            this.font = Content.Load<SpriteFont>("FontList");
            selection = Content.Load<Texture2D>("Menu//selection");
            test = Content.Load<Texture2D>("ListBoxBG");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="gameState">"SEU","Checkpause","EditSEU","Init","LevelSelect","Level2","Main","Null"
        /// "Option","Pause","Play", "LevelEdit","LevelSelect_P","Exit", "Last","Resume", "Next","Replay"</param>
        /// <param name="name">nom du button</param>
        public void activate(int i, int j, float X, float Y, string gameState, string name)
        {
            this.rect[i, j] = new Rectangle((int)(WindowW * X), (int)(WindowH * Y), (int)width, (int)height);
            this.gameState[i, j] = gameState;
            this.name[i, j] = name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="gameState">"SEU","Checkpause","EditSEU","Init","LevelSelect","Level2","Main","Null"
        /// "Option","Pause","Play", "LevelEdit","LevelSelect_P","Exit","Last","Resume", "Next", "Replay"</param>
        /// <param name="name">nom du button</param>
        /// <param name="levelname">le niveau associer</param>
        public void activate(int i, int j, float X, float Y, string gameState, string name, string levelname)
        {
            this.rect[i, j] = new Rectangle((int)(WindowW * X), (int)(WindowH * Y), (int)width, (int)height);
            this.gameState[i, j] = gameState;
            this.name[i, j] = name;
            this.levelname[i, j] = levelname;
        }
        public void disable(int i, int j)
        {
            this.rect[i, j] = new Rectangle(0, 0, 0, 0); this.gameState[i, j] = null;
            this.name[i, j] = "";
        }
        public void Update(ref KeyboardState Key, ref KeyboardState old, ref MouseState mouse, ref Rectangle mouse_rec, ref Game1 game, ref int tab, string name1)
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
                            break; 
                        }
                    }
                if (intecep != oldintercept)
                    if(intecep )
                    game.menu_cursor.Play(); 
                if (old.IsKeyDown(Keys.Up) && Key.IsKeyUp(Keys.Up))
                {
                    Y = (Y - 1) % rect.GetLength(1);
                    if (gameState[X, Y] == null)
                        Y = (Y - 1) % rect.GetLength(1);
                    game.menu_cursor.Play();

                }
                else if (old.IsKeyDown(Keys.Down) && Key.IsKeyUp(Keys.Down))
                {
                    Y = (Y + 1) % rect.GetLength(1);

                    if (gameState[X, Y] == null)
                        Y = (Y + 1) % rect.GetLength(1);
                    game.menu_cursor.Play();
                }
                else if (old.IsKeyDown(Keys.Right) && Key.IsKeyUp(Keys.Right))
                {
                    X = (X + 1) % rect.GetLength(0);
                    if (gameState[X, Y] == null)
                        X = (X + 1) % rect.GetLength(0);
                    game.menu_cursor.Play();
                }
                else if (old.IsKeyDown(Keys.Left) && Key.IsKeyUp(Keys.Left))
                {
                    X = (X - 1) % rect.GetLength(0);
                    if (gameState[X, Y] == null)
                        X = (X - 1) % rect.GetLength(0);
                    game.menu_cursor.Play();
                }
                select.Y = rect[X, Y].Y;
                select.X = rect[X, Y].X - select.Width;
           
                if ((intecep && mouse.LeftButton == ButtonState.Pressed) || Key.IsKeyDown(Keys.Enter))
                {
                    game.menu_select.Play();
                    if (gameState[X, Y] == "SEU")
                    {
                        game.level = levelname[X, Y];
                        game.ChangeState(Game1.gameState.SEU);
                    }
                    else if (gameState[X, Y] == "Checkpause")
                        game.ChangeState(Game1.gameState.Checkpause);
                    else if (gameState[X, Y] == "EditSEU")
                    {
                        game.level = "edit";
                        game.ChangeState(Game1.gameState.Editeur_mapVV);
                    }
                    else if (gameState[X, Y] == "Init")
                        game.ChangeState(Game1.gameState.Initialisateur);
                    else if (gameState[X, Y] == "LevelSelect")
                        game.ChangeState(Game1.gameState.Level_select_state);
                    else if (gameState[X, Y] == "Level2")
                    {
                        game.level = levelname[X, Y];
                        game.ChangeState(Game1.gameState.Level2);
                    }
                    else if (gameState[X, Y] == "Main")
                        game.ChangeState(Game1.gameState.MainMenuState);
                    else if (gameState[X, Y] == "Null")
                        game.ChangeState(Game1.gameState.Null);
                    else if (gameState[X, Y] == "Option")
                        game.ChangeState(Game1.gameState.OptionState);
                    else if (gameState[X, Y] == "Pause")
                        game.ChangeState(Game1.gameState.Pause);
                    else if (gameState[X, Y] == "Play")
                    {
                        game.level = "edit";
                        game.ChangeState(Game1.gameState.PlayingState);
                    }
                    else if (gameState[X, Y] == "LevelEdit")
                    {
                        if (name1 != string.Empty)
                            game.level = name1;
                        else
                            game.level = name[X, Y];
                        if (game.level != "LevelEdit")
                            game.ChangeState(Game1.gameState.leveleditor);
                    }
                    else if (gameState[X, Y] == "LevelSelect_P")
                        game.ChangeState(Game1.gameState.level_Pselect);
                    else if (gameState[X, Y] == "Exit")
                        game.Exit();
                    else if (gameState[X, Y] == "Last")
                        game.GetPreviousState();
                    else if (gameState[X, Y] == "Resume")
                        game.ChangeState2(Game1.gameState.Null);
                    else if (gameState[X, Y] == "Next")
                        game.nextgame();
                    else if (gameState[X, Y] == "Replay")
                        game.replay();
                    else
                        game.ChangeState(Game1.gameState.MainMenuState);

                    System.Threading.Thread.Sleep(G_latence);
                }
                oldintercept = intecep;
            }
        }
        public void Update(ref KeyboardState Key, ref KeyboardState old, ref MouseState mouse, ref Rectangle mouse_rec, ref Game1 game, ref int tab, string name1, ref bool pause)
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
                if (old.IsKeyDown(Keys.Up) && Key.IsKeyUp(Keys.Up))
                {
                    Y = (Y - 1) % rect.GetLength(1);
                    if (gameState[X, Y] == null)
                        Y = (Y - 1) % rect.GetLength(1);

                }
                else if (old.IsKeyDown(Keys.Down) && Key.IsKeyUp(Keys.Down))
                {
                    Y = (Y + 1) % rect.GetLength(1);

                    if (gameState[X, Y] == null)
                        Y = (Y + 1) % rect.GetLength(1);
                }
                else if (old.IsKeyDown(Keys.Right) && Key.IsKeyUp(Keys.Right))
                {
                    X = (X + 1) % rect.GetLength(0);
                    if (gameState[X, Y] == null)
                        X = (X + 1) % rect.GetLength(0);

                }
                else if (old.IsKeyDown(Keys.Left) && Key.IsKeyUp(Keys.Left))
                {
                    X = (X - 1) % rect.GetLength(0);
                    if (gameState[X, Y] == null)
                        X = (X - 1) % rect.GetLength(0);

                }

                select.Y = rect[X, Y].Y;
                select.X = rect[X, Y].X - select.Width;
           
                if ((intecep && mouse.LeftButton == ButtonState.Pressed) || Key.IsKeyDown(Keys.Enter))
                {
                    if (gameState[X, Y] == "SEU")
                    {
                        game.level = levelname[X, Y];
                        game.ChangeState(Game1.gameState.SEU);
                    }
                    else if (gameState[X, Y] == "Checkpause")
                        game.ChangeState(Game1.gameState.Checkpause);
                    else if (gameState[X, Y] == "EditSEU")
                    {
                        game.level = "edit";
                        game.ChangeState(Game1.gameState.Editeur_mapVV);
                    }
                    else if (gameState[X, Y] == "Init")
                        game.ChangeState(Game1.gameState.Initialisateur);
                    else if (gameState[X, Y] == "LevelSelect")
                        game.ChangeState(Game1.gameState.Level_select_state);
                    else if (gameState[X, Y] == "Level2")
                    {
                        game.level = levelname[X, Y];
                        game.ChangeState(Game1.gameState.Level2);
                    }
                    else if (gameState[X, Y] == "Main")
                        game.ChangeState(Game1.gameState.MainMenuState);
                    else if (gameState[X, Y] == "Null")
                        game.ChangeState(Game1.gameState.Null);
                    else if (gameState[X, Y] == "Option")
                        game.ChangeState(Game1.gameState.OptionState);
                    else if (gameState[X, Y] == "Pause")
                        game.ChangeState(Game1.gameState.Pause);
                    else if (gameState[X, Y] == "Play")
                        game.ChangeState(Game1.gameState.PlayingState);
                    else if (gameState[X, Y] == "LevelEdit")
                    {
                        if (name1 != string.Empty)
                            game.level = name1;
                        else
                            game.level = name[X, Y];
                        if (game.level != "LevelEdit")
                            game.ChangeState(Game1.gameState.leveleditor);
                    }
                    else if (gameState[X, Y] == "LevelSelect_P")
                        game.ChangeState(Game1.gameState.level_Pselect);
                    else if (gameState[X, Y] == "Exit")
                        game.Exit();
                    else if (gameState[X, Y] == "Last")
                        game.GetPreviousState();
                    else if (gameState[X, Y] == "Resume")
                        game.ChangeState2(Game1.gameState.Null);
                            else if (gameState[X, Y] == "Next")
                        game.nextgame();
                    else if (gameState[X, Y] == "Replay")
                        game.replay();
                    else
                        game.ChangeState(Game1.gameState.MainMenuState);

                                       pause = false;
                    System.Threading.Thread.Sleep(G_latence);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < rect.GetLength(0); ++i)
                for (int j = 0; j < rect.GetLength(1); ++j)
                {
              //      spriteBatch.Draw(test, rect[i, j], Color.BlueViolet);
                    spriteBatch.DrawString(font, name[i, j], new Vector2(rect[i, j].X, rect[i, j].Y), Color.White  );

                }

            spriteBatch.Draw(selection, select, Color.White );
        }
        public void Dispose()
        {
            rect = null;
            gameState = null;
            name = null;
            levelname = null;
            selection.Dispose();
        //   test.Dispose();
            
        }
    }
}
