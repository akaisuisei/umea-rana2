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
        public static float vol = 1.0f;
        Texture2D background;
        Texture2D retour;
        Texture2D quitter;
        Texture2D editeur_de_map;
        Rectangle rectangle;
        int select = 0;
        int latence = 0;
        public OptionState(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            rectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            game1.IsMouseVisible = true;
            MediaPlayer.Volume = vol;
            songMenu = content.Load<Song>("Menu//songMenu");
            MediaPlayer.Play(songMenu);
        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
        }
        public override void LoadContent(ContentManager content, string level)
        {
            background = content.Load<Texture2D>("Menu//background menu");
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
        }
	}
}
