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
    public class _Pause
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;
        Texture2D continuer;
        Texture2D selection;
        Texture2D menu;
        Texture2D quitter;
        Rectangle rectangle;
        Vector2 coordonnees_continuer;
        Vector2 coordonnees_selection;
        Vector2 coordonnees_menu;
        Vector2 coordonnees_quitter;
        int select = 0;
        int latence = 0;

        public _Pause(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            game1.IsMouseVisible = true;
            MediaPlayer.Volume = vol;
            songMenu = content.Load<Song>("Menu//songMenu");
            coordonnees_continuer = new Vector2(100, 80);
            coordonnees_selection = new Vector2(50, 80);
            coordonnees_menu = new Vector2(100, 180);
            coordonnees_quitter = new Vector2(100, 280);
        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("Menu//pause//fond_pause");
            continuer = content.Load<Texture2D>("Menu//pause//continuer");
            selection = content.Load<Texture2D>("Menu//selection");
            menu = content.Load<Texture2D>("Menu//pause//Menu");
            quitter = content.Load<Texture2D>("Menu//quitter");
        }

        public void Update(Game1 game, Audio audio, ref bool checkpause)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (mouse.X > coordonnees_continuer.X && mouse.X < coordonnees_continuer.X + continuer.Width &&
                mouse.Y > coordonnees_continuer.Y && mouse.Y < coordonnees_continuer.Y + continuer.Height)
                select = 0;
            else if (mouse.X > coordonnees_menu.X && mouse.X < coordonnees_menu.X + menu.Width &&
                mouse.Y > coordonnees_menu.Y && mouse.Y < coordonnees_menu.Y + menu.Height)
                select = 1;
            else if (mouse.X > coordonnees_quitter.X && mouse.X < coordonnees_quitter.X + quitter.Width &&
                mouse.Y > coordonnees_quitter.Y && mouse.Y < coordonnees_quitter.Y + quitter.Height)
                select = 2;
            if (select == 0)
                coordonnees_selection = new Vector2(50, 80);
            else if (select == 1||select==-2)
                coordonnees_selection = new Vector2(50, 180);
            else if (select==2||select==-1)
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
                if (select == 0)// selection menu
                {
                    game.ChangeState2(Game1.gameState.Null);
                    MediaPlayer.Stop();
                    checkpause = false;
                    System.Threading.Thread.Sleep(200);
                }
                else if (select == 1)// va aux menu
                {
                    checkpause = false;
                    game.ChangeState(Game1.gameState.MainMenuState);
                    System.Threading.Thread.Sleep(200);
                    MediaPlayer.Play(songMenu);
                }
                else// quitte le jeu
                {
                    game.Exit();
                }

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            spriteBatch.Draw(continuer, coordonnees_continuer, Color.White);
            spriteBatch.Draw(selection, coordonnees_selection, Color.White);
            spriteBatch.Draw(menu, coordonnees_menu, Color.White);
            spriteBatch.Draw(quitter, coordonnees_quitter, Color.White);
        }
        public void checkpause(KeyboardState keyboard, ref bool _checkpause)
        {
            if (_checkpause == true)
                _checkpause = false;
            else
                _checkpause = true;
        }
    }
}
        

    


