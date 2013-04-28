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
	public class OptionState:GameState
	{
        Song songMenu;
        public static float _soundeffect_vol = 1.0f;
        public static float _vol = 1.0f;
        public static string _langue = "français";
        Texture2D background;
        Color volume_BGM { get; set; }
        Color langue { get; set; }
        int active_item = 0;
        state_in_option _current;
        Vector2 Audio;
        Vector2 Langue;
        Rectangle rectangle;
        SpriteFont spriteFont;
        int select = 0;
        int latence = 0;
        public OptionState(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            game1.IsMouseVisible = true;
            Curseur curseur= new Curseur(new Vector2(200,200),new Vector2(graphics.PreferredBackBufferHeight * 30 / 100),OptionState._vol);
        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
            Audio = new Vector2(graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight * 30 / 100);
            Langue = new Vector2(graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight * 40 / 100);
            volume_BGM = Color.White;
            langue = Color.Black;
        }
        public override void LoadContent(ContentManager content, GraphicsDevice graph, ref string level, ref string next)
        {
            background = content.Load<Texture2D>("Menu//background menu");
            spriteFont = content.Load<SpriteFont>("FontList");
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game,Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            latence--;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            spriteBatch.DrawString(spriteFont, "Volume BGM", Audio, volume_BGM);
            spriteBatch.DrawString(spriteFont, "Langue",Langue , langue);
        }
        public void sub_update(KeyboardState keyboard,MouseState mouse,state_in_option current)
        {
            switch (current)
            {
                case (state_in_option.volume_BGM):
                    Curseur.update(keyboard,mouse);
                    break;
                case (state_in_option.langage):
                    break;
            }
        }
        public enum state_in_option
        {
            volume_BGM,
            langage
        }
	}
}
