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
    public class PlayingState : GameState
    //joindre ici le code du jeu
    {
        Rectangle rectangle;
        Texture2D test;
        _Pause _pause;
        KeyboardState oldkey;
        bool _checkpause = false;
        public PlayingState(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, 30, 30);
            _pause = new _Pause(game1, graphics, content);
        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
            oldkey = Keyboard.GetState();
        }
        public override void LoadContent(ContentManager content, GraphicsDevice graph, ref string level, ref string next)
        {
            _pause.LoadContent(content);
            _pause.initbutton(ref level);
            test = content.Load<Texture2D>("Untitled");
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

            if (keyboard.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape) && oldkey.IsKeyDown(Keys.Escape))
            {
                _pause.checkpause(keyboard, ref _checkpause);
            }
            if (!_checkpause)
            {
                // ici le code de jeu
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            //ici le dessin
            if (_checkpause)
                _pause.Draw(spriteBatch);
        }
    }
}
