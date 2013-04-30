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
using System.Windows.Forms;

namespace Umea_rana
{
    public class PlayingState : GameState
    //joindre ici le code du jeu
    {
        Rectangle rectangle, rmouse;
        Texture2D test;
        _Pause _pause;
        KeyboardState oldkey;
        UserControl2 user;
        IA_manager_AA manage_aa;
        IA_manager_AR manage_ar;
        IA_manager_S manage_s;
        Scrolling_ManagerV Scroll_manager;
       
        bool _checkpause = false;
         int latence = 0;
         Rectangle fond;
        
        public PlayingState(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, 30, 30);
            fond = new Rectangle(0, 0, width, height);
            _pause = new _Pause(game1, graphics, content);
        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
            user = new UserControl2();
            Application.Run(user);
            oldkey = Keyboard.GetState();
        }
        public override void LoadContent(ContentManager content, GraphicsDevice graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            _pause.LoadContent(content);
            _pause.initbutton(ref level);

            manage_aa = new IA_manager_AA(content.Load<Texture2D>("IA/tuc_jaune"), rectangle, 3, 0, height, width);
            manage_ar = new IA_manager_AR(content.Load<Texture2D>("IA/eve"), rectangle, 3, 0, height, width);
            manage_s = new IA_manager_S(content.Load<Texture2D>("IA/naruto"), rectangle, 3, 0, height, width);
            Scroll_manager = new Scrolling_ManagerV(fond);
            user.LoadContent(manage_s, manage_ar, manage_aa, Scroll_manager, content);
        }
        public override void UnloadContent()
        {
            test.Dispose();
            _pause.Dispose();
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape) && oldkey.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                _pause.checkpause(keyboard, ref _checkpause);
            }
            if (!_checkpause)
            {
                // ici le code de jeu
                if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape) && latence <= 0)
                {

                    _pause.checkpause(keyboard, ref _checkpause);
                    latence = 30;
                    user.Hide();
                }
                if (latence > 0)
                    --latence;
                if (!_checkpause)
                {
                    if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !user.IHave_control && rectangle.Contains(rmouse))
                    {
                        //   spawn = existcheck(ref iaType, rmouse);

                        user._show(mouse.X, mouse.Y, "iaType", -1);
                    }
                    if (user.IHave_control)
                        user.TopMost = true;
                    else
                    {
                      user.update(ref manage_aa, ref manage_ar, ref manage_s, ref keyboard, game, ref Scroll_manager);
                        //        scrolling1.update_ophelia(keyboard);

                    }
                }
                else
                {
                    game.ChangeState2(Game1.gameState.Checkpause);
                    MediaPlayer.Stop();
                    ParticleAdder.adder(game, Game1.gameState.Checkpause, height, width);
                    _pause.Update(game, audio, ref _checkpause);
                }
                oldkey = keyboard;

            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Scroll_manager.Draw(spriteBatch);
            manage_aa.Draw(spriteBatch);
            manage_ar.Draw(spriteBatch);
            manage_s.Draw(spriteBatch);
            //ici le dessin
            if (_checkpause)
                _pause.Draw(spriteBatch);
        }
    }
}
