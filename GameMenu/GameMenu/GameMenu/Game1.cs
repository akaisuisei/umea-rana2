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
using Umea_rana.LocalizedStrings;
using Umea_rana.jeu;

namespace Umea_rana
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        gameState _checkpause;
        public gameState _currentState { get; private set; }
        public gameState _previousState { get; set; }
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        GameConfiguration gameconfiguration;
        StorageManager storagemanager;
        Dictionary<gameState, GameState> StateManager;
        Audio audio;
        public string path = "test";
        public string level = string.Empty, next = string.Empty;
        public SoundEffect menu_cursor, menu_select;
        public string endlevel { get; set; }
        public Point score { get; set; }
        public int highscor;
        public Game1()
        {
            //display
            graphics = new GraphicsDeviceManager(this);
            storagemanager = new StorageManager();
            gameconfiguration = storagemanager.LoadGameConfiguration(graphics);
            //Content
            Content.RootDirectory = "Content";
            audio = new Audio(Content);
            //state
            SoundEffect.MasterVolume = gameconfiguration.sound_effect_volume;
            _currentState = gameState.Initialisateur;
            StateManager = new Dictionary<gameState, GameState>();
            StateManager.Add(gameState.OptionState, new OptionState(this, graphics, Content, gameconfiguration));
            StateManager.Add(gameState.Initialisateur, new Initialisateur(this, graphics, Content));
            StateManager.Add(gameState.MainMenuState, new MainMenuState(this, graphics, Content));
            StateManager.Add(gameState.Level_select_state, new Level_select_state(this, graphics, Content));
            StateManager.Add(gameState.level_Pselect, new Leveleditorselect(this, graphics, Content));
            StateManager.Add(gameState.Level_select_state2J, new Level_select_state2J(this, graphics, Content));
            StateManager.Add(gameState.PlayingState, new PlayingState(this, graphics, Content));
            StateManager.Add(gameState.Editeur_mapVV, new Editeur_MapVV(this, graphics, Content));

            StateManager.Add(gameState.SEU, new Shoot_Em_Up(this, graphics, Content));
            StateManager.Add(gameState.leveleditor, new leveleditor(this, graphics, Content));

            StateManager.Add(gameState.win, new GameWin(this, graphics, Content));
            StateManager.Add(gameState.Pause, new Pause(this, graphics, Content));

            StateManager.Add(gameState.levelpersoPLA, new PLA1jperso(this, graphics, Content));
            StateManager.Add(gameState.LevelPersoPLA2J, new PLA2jperso(this, graphics, Content));
            StateManager.Add(gameState.levelPLA, new PLA1j(this, graphics, Content));
            StateManager.Add(gameState.LevelPLA2J, new PLA2j(this, graphics, Content));

            StateManager.Add(gameState.Level2, new Level2(this, graphics, Content));
            StateManager.Add(gameState.level3, new Level3(this, graphics, Content));
            graphics.PreferredBackBufferHeight = OptionState._height;
            graphics.PreferredBackBufferWidth = OptionState._width;
            graphics.IsFullScreen = OptionState.fullscreen;
        }

        protected override void Initialize()
        {
            ParticleAdder.adder(this, _currentState, OptionState._width, OptionState._height);


            try
            {
                StateManager[_currentState].Initialize(graphics);

            }
            catch
            {
            }
            base.Initialize();

        }

        protected override void LoadContent()
        {
            LocalizedString.Culture = new System.Globalization.CultureInfo(OptionState.langue);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MediaPlayer.Volume = OptionState.volume_BGM;
            SoundEffect.MasterVolume = OptionState.sound_effect_volume;
            menu_cursor = Content.Load<SoundEffect>("Menu//menu_cursor");
            menu_select = Content.Load<SoundEffect>("Menu//menu_select");
            StateManager[_currentState].LoadContent(Content, GraphicsDevice, ref level, ref next, graphics, audio);
            base.LoadContent();

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager Content here
            StateManager[_currentState].UnloadContent();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            try
            {
                StateManager[_currentState].Update(this, audio);
            }
            catch
            {
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            /*     if (_currentState != gameState.level2 && _currentState != gameState.Level1_state)
                     spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
                 else*/
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);
            StateManager[_currentState].Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public enum gameState
        {
            MainMenuState,
            PlayingState,
            OptionState,
            Level_select_state,
            Level_select_state2J,
            Level2,
            SEU,
            Pause,
            Initialisateur,
            Checkpause,
            Editeur_mapVV,
            leveleditor,
            level_Pselect,
            win,
            level3,
            levelpersoPLA,
            levelPLA,
            LevelPersoPLA2J,
            LevelPLA2J,
            Null,
        }

        public void ChangeState(gameState NewState, gameState previousState = gameState.Null)
        {
            _previousState = _currentState;
            _currentState = NewState;
            is_fullscreen(OptionState.fullscreen);
            this.Initialize();
            endlevel = "";
        }
        public void ChangeState(gameState NewState, string level, gameState previousState = gameState.Null)
        {
            _previousState = _currentState;
            _currentState = NewState;
            is_fullscreen(OptionState.fullscreen);
            this.Initialize();
            unlocklevel unloak = new unlocklevel();
            unloak.endlevel(level);
            endlevel = "";
        }
        public void ChangeState(gameState NewState, string level, int P1, int P2, gameState previousState = gameState.Null)
        {
            int pmax;
            _previousState = _currentState;
            _currentState = NewState;
            is_fullscreen(OptionState.fullscreen);
            this.Initialize();
            unlocklevel unloak = new unlocklevel();
            unloak.endlevel(level);
            Highscore hgh = new Highscore();
            pmax = hgh.endlevel(level, P1, P2);
            if (P2 != 0)
                if (pmax == P1 && pmax == P2)
                    endlevel = "highscore: your are very good gamer";
                else if (pmax == P1)
                    endlevel = "highscore: P1 is the best, p2 is a looser";
                else if (pmax == P2)
                    endlevel = "highscore: P2 is the best, p1 is a looser";
                else if (P1 == P2)
                    endlevel = "Your are friend";
                else if (P1 > P2)
                    endlevel = "P1 Win";
                else
                    endlevel = "P2 Win";
            else
                if (P1 == pmax)
                    endlevel = "highscore: your are very good gamer";
                else
                    endlevel = " your are very good gamer";
            score = new Point(P1, P2);
            highscor = pmax;

        }
        public void GetPreviousState()
        {
            _currentState = this._previousState;
            is_fullscreen(OptionState.fullscreen);
            this.Initialize();
            endlevel = "";
        }
        public void ChangeState2(gameState checkpause)
        {
            _checkpause = checkpause;
            endlevel = "";
        }

        private void is_fullscreen(bool full)
        {
            if (full)
            {
                if (_currentState == gameState.Editeur_mapVV || _currentState == gameState.PlayingState && graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
                else if (_currentState != gameState.Editeur_mapVV && _currentState != gameState.PlayingState && !graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }
        }
        public void nextgame()
        {
            if (next != null && next.Length > 0)
            {
                if (next[0] == 's' || next[0] == 'S')
                {
                    level = next;
                    ChangeState(gameState.SEU);
                }
                else
                {
                    level = next;
                    ChangeState(gameState.Level2);
                }
            }
        }

        public void replay()
        {
            if (level != null && level.Length > 0)
            {
                ChangeState(_currentState);
            }
        }

    }
}
