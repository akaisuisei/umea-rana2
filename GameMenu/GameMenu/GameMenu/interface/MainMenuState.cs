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
using Umea_rana.LocalizedStrings;

namespace Umea_rana
{
    public class MainMenuState : GameState
    //code du menu
    {
        Texture2D background;
        Rectangle rectangle;
        Button button;
        Rectangle rect;
        int tab = 0;
        KeyboardState old;
        SpriteFont font;

        public MainMenuState(Game1 game1, GraphicsDeviceManager graphics, ContentManager Content)
        {
            Audio.play("Menu");
            game1.IsMouseVisible = true;
        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {

            old = Keyboard.GetState();
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {

            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            titre_P = new Vector2(width * 0.8f, height * 0.1f);
            button = new Button(1, 4, width, height, 0.1f, 0.05f, tab);
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            button.LoadContent(Content);
            button.activate(0, 0, 0.1f, 0.1f, "LevelSelect", LocalizedString.Play);
            button.activate(0, 1, 0.1f, 0.2f, "LevelSelect_P", LocalizedString.Map_Editor);
            button.activate(0, 2, 0.1f, 0.3f, "Option", LocalizedString.Option);
            button.activate(0, 3, 0.1f, 0.4f, "Exit", LocalizedString.Exit);
            background = Content.Load<Texture2D>("Menu//background menu");
            font = Content.Load<SpriteFont>("FontList");
        }
        public override void UnloadContent()
        {
            background.Dispose();
            button.Dispose();
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();

            MouseState mouse = Mouse.GetState();
            rect = new Rectangle(mouse.X, mouse.Y, 1, 1);
            button.Update(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab, "");
            old = keyboard;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, rectangle, Color.White);
            button.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Menu", titre_P, Color.White);
        }

    }
}
