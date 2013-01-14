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
        gameState _currentState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dictionary<gameState, GameState> StateManager;
        int height, width;
        Audio audio;
        DisplayMode displaymode;

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
            graphics.IsFullScreen = true;

            //content
            Content.RootDirectory = "Content";
            audio = new Audio(Content);
            //state
            _currentState = gameState.MainMenuState;
            StateManager = new Dictionary<gameState, GameState>();
            StateManager.Add(gameState.PlayingState, new PlayingState());
            StateManager.Add(gameState.MainMenuState, new MainMenuState(this, graphics, Content));
            StateManager.Add(gameState.Level_select_state, new Level_select_state(this, graphics, Content));
            StateManager.Add(gameState.Level1_state, new Level1_state(this, graphics, Content));
            StateManager.Add(gameState.level2, new level2(this, graphics, Content));
            StateManager.Add(gameState.Pause, new Pause(this, graphics, Content));
        }

        protected override void Initialize()
        {
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
            StateManager[_currentState].LoadContent(Content);
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
            base.Draw(gameTime);
            StateManager[_currentState].Draw(spriteBatch);
        }

        public enum gameState
        {
            MainMenuState,
            PlayingState,
            OptionsState,
            Level_select_state,
            Level1_state,
            level2,
            Pause,
        }

        public void ChangeState(gameState NewState)
        {
            _currentState = NewState;
            base.Initialize();
        }

    }
}