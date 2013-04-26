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

namespace Umea_rana
{
    public abstract class GameState
    {
        protected const int G_latence = 200;
        protected static int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        protected static int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        protected static Texture2D titre;
        protected Vector2 titre_P = new Vector2(width - 200, 0);

        public abstract void Initialize(GraphicsDeviceManager graphics);
        public abstract void LoadContent(ContentManager Content, GraphicsDevice graph, ref string level, ref string next, GraphicsDeviceManager graphics);
        public abstract void UnloadContent();
        public abstract void Update(Game1 game, Audio audio);
        public abstract void Draw(SpriteBatch spriteBatch);

       protected void pause(Game1 game, KeyboardState keybord, Game1.gameState _currentState)
        {
            if (keybord.IsKeyDown(Keys.P) || keybord.IsKeyDown(Keys.Escape))
            {
                game.ChangeState(Game1.gameState.Pause, _currentState);
            }

        }

        protected void fail(Game1 game, sprite_broillon sprite, Game1.gameState _currentState)
        {
            if (sprite.rectangle.Bottom > 2 * height||sprite.dead )
                game.ChangeState(Game1.gameState.Pause, _currentState);
        }
    }
}
