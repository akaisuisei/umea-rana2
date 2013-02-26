
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;

namespace Umea_rana
{
    class Editeur_MapVV: GameState 
    {
         Scrolling scrolling1;
        sripte_V vaisseau;

        KeyboardState oldkey;
        Texture2D bacgkround1, background2, aster_t, planet1, star;
        List<Texture2D> T_sprite;
        Collision collision;
        IA_manager_T manage_T;
        IA_manager_V manage_V;
        IA_manager_K manage_k;
        int taille_sprt;
        int game_time;
        _Pause _pause;
        bool _checkpause = false;
        int latence = 0;
     

        UserControl1 user;
        string backGround;

        public Editeur_MapVV(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            game1.IsMouseVisible = false;
            oldkey = Keyboard.GetState();
            T_sprite = new List<Texture2D>();
            collision = new Collision();
            _pause = new _Pause(game1, graphics, content);
           
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
            // TODO: Add your initialization logic here

            taille_sprt = (int)(Math.Min(width, height) * 0.05);
            game_time = 0;
            backGround = "level2//fond";
            // ajout IA 
                        Application.Run(user);
        }

        public override void LoadContent(ContentManager Content)
        {

            //charge le fond
            bacgkround1 = Content.Load<Texture2D>(backGround );
            //charge le sprite
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman1"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman1d"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman1g"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman2"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman2d"));
            T_sprite.Add(Content.Load<Texture2D>("hero//vaisseau//sazabiHaman2g"));

            //charge l IA
            aster_t = Content.Load<Texture2D>("IA/asteroid/asteroide-sprite");
            aster_t = Content.Load<Texture2D>("IA/asteroid/asteroide-sprite");
            planet1 = Content.Load<Texture2D>("IA/asteroid/planet4");
            star = Content.Load<Texture2D>("IA/asteroid/star");
            //instancie le scolling

            scrolling1 = new Scrolling(bacgkround1, new Rectangle(0, 0, width, height), 1, height);

           
            manage_T = new IA_manager_T(planet1 , new Rectangle(0, 0, taille_sprt, taille_sprt), Content, height, width, Color.Red);
            manage_V = new IA_manager_V(star , new Rectangle(0, 0, taille_sprt, taille_sprt), Content, height, width, Color.Green);
            manage_k = new IA_manager_K(aster_t, new Rectangle(0, 0, taille_sprt, taille_sprt), 0, 4, height);


            //intancie le vaisseau
            vaisseau = new sripte_V(T_sprite,
                new Rectangle(height / 2 + taille_sprt / 2, width / 2 + taille_sprt / 2, taille_sprt, taille_sprt), Content, height, width, Color.Gray, 9);

 
            //instancie les donnees de la pause
            _pause.LoadContent(Content);
            user = new UserControl1(manage_T, manage_V, manage_k);
        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            user.destroy();
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard;
            MouseState mouse;
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape) && latence <= 0)
            {
                _pause.checkpause(keyboard, ref _checkpause );
                latence = 30;
                user.Hide();
            }
            if (latence > 0)
                --latence;
            if (_checkpause)
                _pause.Update(game, audio, ref _checkpause);

            if (mouse.LeftButton  == Microsoft.Xna.Framework.Input.ButtonState.Pressed&& !user.IHave_control )
            {
                user.Hide();
                user._show(mouse.X, mouse.Y);
            }
            if (user.IHave_control)
                user.TopMost = true;

            //update interface
            user.update(ref manage_T,ref manage_V,ref manage_k,ref keyboard );
            scrolling1.update_ophelia(keyboard);
            manage_k.Update_ophelia(keyboard);
            manage_T.Update_ophelia(keyboard);
            manage_V.Update_ophelia(keyboard);

            oldkey = keyboard;
            game_time++;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_checkpause == false)
            {
        
                //scrolling
                scrolling1.Draw(spriteBatch);
                vaisseau.Draw(spriteBatch);
               
                manage_T.Draw(spriteBatch);
                manage_V.Draw(spriteBatch);
                manage_k.Draw(spriteBatch);
    
            }
            else
                _pause.Draw(spriteBatch);
        }
        
    }
}
