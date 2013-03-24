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

namespace Umea_rana
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        gameState _checkpause;
        gameState _currentState;
        public gameState _previousState { get; set; }
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        Dictionary<gameState, GameState> StateManager;
        int height, width;
        Audio audio;
        DisplayMode displaymode;
        public string path = "test";
        public string level=string.Empty ;
        public SoundEffect menu_cursor, menu_select;
        public Game1()
        {
            //display
            displaymode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            graphics = new GraphicsDeviceManager(this);
            height = displaymode.Height;
            width = displaymode.Width;
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.ApplyChanges();
            graphics.IsFullScreen = false;

            //content
            Content.RootDirectory = "Content";
            audio = new Audio(Content);
            //state
            _currentState = gameState.Initialisateur;
            StateManager = new Dictionary<gameState, GameState>();
            StateManager.Add(gameState.PlayingState, new PlayingState());
            StateManager.Add(gameState.MainMenuState, new MainMenuState(this, graphics, Content));
            StateManager.Add(gameState.Level_select_state, new Level_select_state(this, graphics, Content));
            StateManager.Add(gameState.Level2, new Level2(this, graphics, Content));
            StateManager.Add(gameState.SEU, new Shoot_Em_Up(this, graphics, Content));
            StateManager.Add(gameState.Pause, new Pause(this, graphics, Content));
            StateManager.Add(gameState.Initialisateur, new Initialisateur(this, graphics, Content));
            StateManager.Add(gameState.Editeur_mapVV, new Editeur_MapVV(this, graphics, Content));
            StateManager.Add(gameState.leveleditor, new leveleditor(this, graphics, Content));
            StateManager.Add(gameState.level_Pselect, new Leveleditorselect (this,graphics ,Content ));
        }

        protected override void Initialize()
        {
            ParticleAdder.adder(this, _currentState, width, height);
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menu_cursor = Content.Load<SoundEffect>("Menu//menu_cursor");
            menu_select = Content.Load<SoundEffect>("Menu//menu_select");
            StateManager[_currentState].LoadContent(Content, level,GraphicsDevice );
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
            OptionsState,
            Level_select_state,
            Level2,
            SEU,
            Pause,
            Initialisateur,
            Checkpause,
            Editeur_mapVV,
            leveleditor,
            level_Pselect,
            Null,
        }

        public void ChangeState(gameState NewState, gameState previousState = gameState.Null)
        {
            _previousState = _currentState;
            _currentState = NewState;
            is_fullscreen(false);
            this.Initialize();
        }
        public void GetPreviousState()
        {
            _currentState = this._previousState;
            this.Initialize();
        }
        public void ChangeState2(gameState checkpause)
        {
            _checkpause = checkpause;
        }

        private void is_fullscreen(bool full)
        {
            if (full)
            {
                if (_currentState == gameState.Editeur_mapVV && graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
                if (_currentState != gameState.Editeur_mapVV && !graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }
        }
    }
}
