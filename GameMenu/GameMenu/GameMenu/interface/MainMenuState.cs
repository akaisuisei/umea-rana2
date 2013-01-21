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
    public class MainMenuState : GameState
    //code du menu
    {
        Song songMenu;
        public static float vol = 1.0f;
        Texture2D background;
        Texture2D jouer;
        Texture2D selection;
        Texture2D options;
        Texture2D quitter;
        Rectangle rectangle;
        Vector2 coordonnees_jouer;
        Vector2 coordonnees_selection;
        Vector2 coordonnees_options;
        Vector2 coordonnees_quitter;

        int select = 0;
        int latence = 0;

        public MainMenuState(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            game1.IsMouseVisible = true;
            MediaPlayer.Volume = vol;
            songMenu = content.Load<Song>("Menu//songMenu");
            MediaPlayer.Play(songMenu);


        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            coordonnees_jouer = new Vector2(100, 80);
            coordonnees_selection = new Vector2(50, 80);
            coordonnees_options = new Vector2(100, 180);
            coordonnees_quitter = new Vector2(100, 280);
        }
        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("Menu//background menu");
            jouer = content.Load<Texture2D>("Menu//jouer");
            selection = content.Load<Texture2D>("Menu//selection");
            options = content.Load<Texture2D>("Menu//options");
            quitter = content.Load<Texture2D>("Menu//quitter");
            titre = content.Load<Texture2D>("Menu//pause//Menu");
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();

            MouseState mouse = Mouse.GetState();

            if (mouse.X > coordonnees_jouer.X && mouse.X < coordonnees_jouer.X + jouer.Width &&
                mouse.Y > coordonnees_jouer.Y && mouse.Y < coordonnees_jouer.Y + jouer.Height)
                select = 0;
            else if (mouse.X > coordonnees_options.X && mouse.X < coordonnees_options.X + options.Width &&
                mouse.Y > coordonnees_options.Y && mouse.Y < coordonnees_options.Y + options.Height)
                select = 1;
            else if (mouse.X > coordonnees_quitter.X && mouse.X < coordonnees_quitter.X + quitter.Width &&
                mouse.Y > coordonnees_quitter.Y && mouse.Y < coordonnees_quitter.Y + quitter.Height)
                select = 2;
            if (select == 0)
                coordonnees_selection = new Vector2(50, 80);
            else if (select == 1 || select == -2)
                coordonnees_selection = new Vector2(50, 180);
            else
                coordonnees_selection = new Vector2(50, 280);
            if (latence > 0)  // la latence créé un temps d'attente avant de pouvoir changer à nouveau de boutton
                latence--;    // sinon les changements sont bien trop rapides
            else
            {
                if (keyboard.IsKeyDown(Keys.Down))  //la selection est faite grâce à un modulo égal au nombre total de bouttons
                {
                    select = (select + 1) % 3;
                    latence = 10;
                }
                else if (keyboard.IsKeyDown(Keys.Up))
                {
                    select = (select - 1) % 3;
                    latence = 10;
                }
            }
            if (keyboard.IsKeyDown(Keys.Enter) || mouse.LeftButton == ButtonState.Pressed)
            {
                if (select == 0)// lance la selecteur partie
                {
                    game.ChangeState(Game1.gameState.Level_select_state);
                    System.Threading.Thread.Sleep(G_latence);
                }
                else if (select == 1)// va aux options
                {
                    System.Threading.Thread.Sleep(G_latence);
                }
                else// quitte le jeu
                {
                    game.Exit();
                }

            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, rectangle, Color.White);
            spriteBatch.Draw(jouer, coordonnees_jouer, Color.White);
            spriteBatch.Draw(selection, coordonnees_selection, Color.White);
            spriteBatch.Draw(options, coordonnees_options, Color.White);
            spriteBatch.Draw(quitter, coordonnees_quitter, Color.White);
            spriteBatch.Draw(titre, titre_P, Color.White);
            spriteBatch.End();
        }

    }
}
