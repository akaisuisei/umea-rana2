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
    class Initialisateur :GameState 
    {

        Texture2D background;
        Rectangle rectangle;

        int timer, taille_logo;

        public Initialisateur(Game1 game1, GraphicsDeviceManager graphics, ContentManager content)
        {
            taille_logo = Math.Min(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            rectangle = new Rectangle(graphics.PreferredBackBufferWidth/2-taille_logo/2,graphics.PreferredBackBufferHeight/2-taille_logo/2 ,
                taille_logo ,taille_logo  );
            game1.IsMouseVisible = false;

            timer = 10;//550;

        }

        public override void Initialize(GraphicsDeviceManager graphics)
        {
        }
        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("Menu//logofin");
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            timer--;
            if (timer <= 0)
                game.ChangeState(Game1.gameState.MainMenuState);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(background, rectangle, Color.White );
        }

    }
}
