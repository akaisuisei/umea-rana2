using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
namespace Umea_rana
{
    public class Housse
    {
        Rectangle rect;
        Texture2D texture;
        int fcspeed;
        int line, column;
        int Timer;
        int Frameline;
        int FrameColumn;
        int AnimationSpeed = 10;
        SpriteEffects Effects;
        int windowH { get; set; }
        int WindowW { get; set; }
       
        public Housse()
        {

        }
        public void loadContent(ContentManager content,  string filepath, Rectangle rectangle, int heightW, int widthW)
        {
            this.Frameline = 1;
            this.FrameColumn = 1;
            texture = content.Load<Texture2D>(filepath);
            this.rect = rectangle;
            line = 100;
            column = 100;
            Effects = SpriteEffects.FlipHorizontally;
            windowH = heightW;
            WindowW = widthW;
        }
        public void loadContent(ContentManager content, int fcspeed, string filepath)
        {
            this.Frameline = 1;
            this.FrameColumn = 1;
            this.fcspeed = fcspeed;
            texture = content.Load<Texture2D>(filepath);
            this.rect = new Rectangle(2000, 500, 100, 100);
            line = 100;
            column = 100;
            Effects = SpriteEffects.FlipHorizontally;

        }
        public void parrametrage(levelProfile levelprofile)
        {
            rect.X = (int)(levelprofile.house.X * WindowW );
            rect.Y = (int)(levelprofile.house.Y * windowH );
            fcspeed = levelprofile.fc_speed;
           
        }
        public void Update(KeyboardState keybord, Game1 game, Sprite_PLA sprite)
        {
            if (keybord.IsKeyDown(Keys.Right))
            {
                 rect.X -= fcspeed;
            }
            if (keybord.IsKeyDown(Keys.Left))
            {
                rect.X += fcspeed;
            }
            if (rect.Intersects(sprite.rectangle_C))
                game.ChangeState(Game1.gameState.win);
            Animated();
        }

        public void Update(KeyboardState keybord)
        {
            if (keybord.IsKeyDown(Keys.Right))
            {
                rect.X -= fcspeed;
            }
            if (keybord.IsKeyDown(Keys.Left))
            {
                rect.X += fcspeed;
            }
           
            Animated();
        }
        public void Update(KeyboardState keybord, Game1 game, Sprite_PLA sprite,Sprite_PLA p2)
        {
            if (keybord.IsKeyDown(Keys.Right))
            {
                rect.X -= fcspeed;
            }
            if (keybord.IsKeyDown(Keys.Left))
            {
                rect.X += fcspeed;
            }
            if (rect.Intersects(sprite.rectangle_C) || rect.Intersects(p2.rectangle_C))
                game.ChangeState(Game1.gameState.win);
            Animated();
        }

        private void Animated()
        {
            this.Timer++;
            if (this.Timer == this.AnimationSpeed)
            {
                this.Timer = 0;
                this.FrameColumn++;
                if (FrameColumn > 3)
                {
                    FrameColumn = 1;
                }
            }
        }

        public void draw(SpriteBatch sp)
        {

            sp.Draw(texture, rect, new Rectangle((this.FrameColumn - 1) * column, (this.Frameline - 1) * line, column, line), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
        }

        public void parrametrage(housesafe house)
        {
            fcspeed = 3;
            rect.X = (int)(house.X * WindowW );
            rect.Y = (int)(house.Y * windowH );
        }
    }
}
