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
        int transition;
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
            Entry_Select(keyboard, mouse);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            spriteBatch.DrawString(spriteFont, "Volume BGM", Audio, volume_BGM);
            spriteBatch.DrawString(spriteFont, "Langue",Langue , langue);
        }
        public void Entry_Select(KeyboardState keyboard, MouseState mouse)
        {
            if (keyboard.IsKeyDown(Keys.Down)&&latence<=0)
            {
                if(active_item>1)
                    active_item=0;
                else
                    active_item++;
                latence=30;
            }
            if (keyboard.IsKeyDown(Keys.Up) && latence <= 0)
            {
                if (active_item > 1)
                    active_item =1 ;
                else
                    active_item--;
                latence = 30;
            }
            if (keyboard.IsKeyDown(Keys.Enter) && latence <= 0)
            {
                Select_Menu_items(active_item,keyboard,mouse);
                latence = 30;
            }
        }
        public void Select_Menu_items(int active_item,KeyboardState keyboard, MouseState mouse)
        {
            switch (active_item)
            {
                case 0:
                    volume_BGM=Color.Red;
                    Curseur.update(keyboard, mouse);
                    break;
                case 1:
                    
                    break;
                
            }
        }
        public void Highlight(int active_item)
        {
            switch (active_item)
            {
                case 0:
                    volume_BGM = Color.White;
                    langue = Color.Black;
                    break;
                case 1:
                    volume_BGM = Color.Black;
                    langue = Color.White;
                    break;
            }
        }
	}
}
