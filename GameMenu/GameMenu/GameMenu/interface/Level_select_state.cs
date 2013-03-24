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
    class Level_select_state : GameState
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;
        Texture2D level1;
        Texture2D selection;
        Texture2D level2;
        Texture2D retour;
        Rectangle rectangle;
        Vector2 coordonnees_level1;
        Vector2 coordonnees_selection;
        Vector2 coordonnees_level2;
        Vector2 coordonnees_retour;
        int select = 0;
        int latence = 0;

        public Level_select_state(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            game1.IsMouseVisible = true;
            MediaPlayer.Volume = vol;
            songMenu = content.Load<Song>("Menu//songMenu");
            coordonnees_level1 = new Vector2(100, 80);
            coordonnees_selection = new Vector2(50, 80);
            coordonnees_level2 = new Vector2(100, 180);
            coordonnees_retour = new Vector2(100, 280);
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {

        }
        public override void LoadContent(ContentManager content, string level)
        {
            background = content.Load<Texture2D>("Menu//background menu");
            level1 = content.Load<Texture2D>("Menu//LevelSelect//level1");
            selection = content.Load<Texture2D>("Menu//selection");
            level2 = content.Load<Texture2D>("Menu//LevelSelect//level2");
            retour = content.Load<Texture2D>("Menu//LevelSelect//retour");
            titre = content.Load<Texture2D>("Menu//LevelSelect//niveau");
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (mouse.X > coordonnees_level1.X && mouse.X < coordonnees_level1.X + level1.Width &&
                mouse.Y > coordonnees_level1.Y && mouse.Y < coordonnees_level1.Y + level1.Height)
                select = 0;
            else if (mouse.X > coordonnees_level2.X && mouse.X < coordonnees_level2.X + level2.Width &&
                mouse.Y > coordonnees_level2.Y && mouse.Y < coordonnees_level2.Y + level2.Height)
                select = 1;
            else if (mouse.X > coordonnees_retour.X && mouse.X < coordonnees_retour.X + retour.Width &&
                mouse.Y > coordonnees_retour.Y && mouse.Y < coordonnees_retour.Y + retour.Height)
                select = 2;
            if (select == 0)
                coordonnees_selection = new Vector2(50, 80);
            else if (select == 1||select==-2)
                coordonnees_selection = new Vector2(50, 180);
            else if(select ==2 || select==-1)
                coordonnees_selection = new Vector2(50, 280);
            if (latence > 0)  // la latence créé un temps d'attente avant de pouvoir changer à nouveau de boutton
                latence--;    // sinon les changements sont bien trop rapides
            else
            {
                if (keyboard.IsKeyDown(Keys.Down))  //la selection est faite grâce à un modulo égal au nombre total de bouttons
                {
                    select = (select + 1) % 3;
                    game.menu_cursor.Play();
                    latence = 10;
                }
                else if (keyboard.IsKeyDown(Keys.Up))
                {
                    select = (select - 1) % 3;
                    game.menu_cursor.Play();
                    latence = 10;
                }
            }
            if (keyboard.IsKeyDown(Keys.Enter) || mouse.LeftButton == ButtonState.Pressed)
            {
                game.menu_select.Play();
                if (select == 0)
                {
                    game.level = "SEU1";
                    game.ChangeState(Game1.gameState.SEU );
                    MediaPlayer.Stop();
                    System.Threading.Thread.Sleep(G_latence );
                } // level1
                else if (select == 1)
                {
                   game.level = "level1"; 
                    game.ChangeState(Game1.gameState.Level2, Game1.gameState.Level2);
                    MediaPlayer.Stop();
                    System.Threading.Thread.Sleep(G_latence);
                }
                else
                {
                    game.ChangeState(Game1.gameState.MainMenuState);
                    System.Threading.Thread.Sleep(G_latence);
                }// retour au menu
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            spriteBatch.Draw(level1, coordonnees_level1, Color.White);
            spriteBatch.Draw(selection, coordonnees_selection, Color.White);
            spriteBatch.Draw(level2, coordonnees_level2, Color.White);
            spriteBatch.Draw(retour, coordonnees_retour, Color.White);
            spriteBatch.Draw(titre, titre_P, Color.White);
        }
    }
}
