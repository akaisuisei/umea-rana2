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
    public class PlayingState:GameState
        //joindre ici le code du jeu
    {
        Rectangle rectangle;
        Texture2D test;
        public PlayingState()
        {
            rectangle = new Rectangle(0, 0, 30, 30);
        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
           
        }
        public override void LoadContent(ContentManager content, string level, GraphicsDevice graph)
        {
            test = content.Load<Texture2D>("Untitled");
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
     
     
        }
    }
}
