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
        bossPLAT boss;
        Platform_manager manage_p;
        Scrolling_ManagerV Scroll_manager;
        Dictionary<string, Texture2D> T_platform;

        bool _checkpause = false;
        Rectangle fond;
        string iaType;
        int spawn = -1;
        Sauveguarde sauveguarde;
        public PlayingState(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, 30, 30);

            _pause = new _Pause(game1, graphics, content);
        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
            user = new UserControl2();
            Application.Run(user);
            oldkey = Keyboard.GetState();
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            sauveguarde = new Sauveguarde();
            T_platform = new Dictionary<string, Texture2D>();
            string[] platstring = sauveguarde.filename(Content, "platform");
            foreach (string p in platstring)
                T_platform.Add(p, Content.Load<Texture2D>("platform//" + p));
            boss= new bossPLAT ();
            _pause.LoadContent(Content);
            _pause.initbutton(ref level);
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            fond = new Rectangle(0, 0, width, height);
            manage_aa = new IA_manager_AA(Content.Load<Texture2D>("IA/color/tuc_jaune"), rectangle, 3, 0, height, width);
            manage_ar = new IA_manager_AR(Content.Load<Texture2D>("IA/color/eve"), rectangle, 3, 0, height, width);
            manage_s = new IA_manager_S(Content.Load<Texture2D>("IA/color/naruto"), rectangle, 3, 0, height, width);
            manage_p = new Platform_manager(T_platform, 0.07f, 0.03f, height, width);
            Scroll_manager = new Scrolling_ManagerV(fond);
            boss.loadContent (Content,new Rectangle(0,0,width ,height ));
            user.LoadContent(manage_s, manage_ar, manage_aa, Scroll_manager, Content, manage_p, fond,boss );
            iaType = "Kawabunga";
        }
        public override void UnloadContent()
        {
         
            _pause.Dispose();
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            rmouse.X = mouse.X;
            rmouse.Y = mouse.Y;

            // ici le code de jeu
            if ((keyboard.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape) && oldkey.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape)) )
            {
                if (_checkpause)
                {
                    if (user.IHave_control)
                        user.Show();
                }
                else
                    if (user.IHave_control)
                        user.Hide();
                _pause.checkpause(keyboard, ref _checkpause);
            }
   
            if (!_checkpause)
            {
                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !user.IHave_control && fond.Contains(rmouse))
                {
                       spawn = existcheck(ref iaType, rmouse);

                    user._show(mouse.X, mouse.Y, iaType , spawn );
                }
                if (user.IHave_control)
                    user.TopMost = true;
                else
                {
                    manage_aa.UpdateEDIT(keyboard);
                    manage_ar.UpdateEDIT(keyboard);
                    manage_s.UpdateEDIT(keyboard);
                    manage_p.Update(keyboard);
                    boss.UpdateEDIT (ref keyboard);
                    user.update(ref manage_aa, ref manage_ar, ref manage_s, ref keyboard, game, ref Scroll_manager, ref manage_p,ref boss );
                 
                    Scroll_manager.Update(keyboard);
                }
            }
            else
            {
                game.ChangeState2(Game1.gameState.Checkpause);
                MediaPlayer.Stop();
                ParticleAdder.adder(game, Game1.gameState.Checkpause, height, width);
              _pause.Update(game, audio, ref _checkpause, ref keyboard ,ref oldkey );
            }
            oldkey = keyboard;


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Scroll_manager.Draw(spriteBatch);
            manage_p.Draw2(spriteBatch);
            manage_aa.Draw(spriteBatch);
            manage_ar.Draw(spriteBatch);
            manage_s.Draw(spriteBatch);
    
            boss.DrawEDIT (spriteBatch);
            //ici le dessin
            if (_checkpause)
                _pause.Draw(spriteBatch);
        }
        private int existcheck(ref string hello, Rectangle recM)
        {
              foreach (platform  p in manage_p.plato  )
                  if (p.rectangle_C.Intersects(recM))
                  {
                      hello = "plateformes";
                      return (int)p.vie;
                  }
            foreach (Stalker  st in manage_aa.Ia_manage )
                if (st.rectangle_C.Intersects(recM))
                {
                    hello = "IA_AA";
                    return st.spawn;
                }
            foreach (Stalker st in manage_s.Ia_manage  )
                if (st.rectangle_C.Intersects(recM))
                {
                    hello = "IA_S";
                    return st.spawn;
                }
            foreach (Stalker st in manage_ar.Ia_manage)
                if (st.rectangle_C.Intersects(recM))
                {
                    hello = "IA_AR";
                    return st.spawn;
                }
            hello = ""; 
              return -1;
        }
    }
}
