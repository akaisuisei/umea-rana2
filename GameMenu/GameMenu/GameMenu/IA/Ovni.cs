using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Umea_rana
{
    public class cercle
    {
        public int x { get; set; }
        public int Y { get; set; }
        public int R { get; set; }

        public bool is_in_bound(Rectangle rec)
        {
            return (Math.Pow(rec.Right - x, 2) + Math.Pow(rec.Top - Y, 2) <= R ||
                Math.Pow(rec.Right - x, 2) + Math.Pow(rec.Bottom - Y, 2) <= R ||
                Math.Pow(rec.Left - x, 2) + Math.Pow(rec.Top - Y, 2) <= R ||
                Math.Pow(rec.Left - x, 2) + Math.Pow(rec.Bottom - Y, 2) <= R);
        }
    }

    public class ovnis
    {
        public int FrameLine { get; set; }
        public int maxframecolumn { get; set; }
        public int minframecolunm { get; set; }
        public int FrameColumn { get; set; }
        public ovnis()
        {
            circle = new cercle();
            FrameColumn = 1;
            FrameLine = 1;
            timer = 10;
            minframecolunm = 1;
            dirX = 1;
            dirY = 1;
        }
        public char type;
        public int damage, vie, miss, power, bomb, launch;
        public int dirX, dirY;
        public int speed;
        public Vector2 dir;
        public Rectangle rectangle;
        public cercle circle;
        public int timer { get; set; }
        public int timer_dead = 60;
    }

    public class Ovni
    {
        int line, colunm;
        public List<ovnis> ovni { get; set; }
        int R1, R2, R3, R4;
        int WindoW, WindowH, fc;
        int width1, width2, width3, width4;
        int height1, height2, height3, height4;
        Texture2D texture;
        Rectangle fond;
        public Ovni(int width, int height)
        {
            WindoW = width; WindowH = height;
            width1 = (int)(WindoW * 0.05);
            width2 = (int)(WindoW * 0.07);
            width3 = (int)(WindoW * 0.09);
            width4 = (int)(WindoW * 0.13);
            height1 = (int)(WindowH * 0.05);
            height2 = (int)(WindowH * 0.07);
            height3 = (int)(WindowH * 0.09);
            height4 = (int)(WindowH * 0.13);
            R1 = width1 / 2;
            R2 = width2 / 2;
            R3 = width3 / 2;
            R4 = width4 / 2;
            ovni = new List<ovnis>();
            line = 64;
            colunm = 64;

        }
        public Ovni(Rectangle fond)
        {
            this.fond = fond;
            WindoW = fond.Width;
            WindowH = fond.Height;
            width1 = (int)(WindoW * 0.05);
            width2 = (int)(WindoW * 0.07);
            width3 = (int)(WindoW * 0.09);
            width4 = (int)(WindoW * 0.13);
            height1 = (int)(WindowH * 0.05);
            height2 = (int)(WindowH * 0.07);
            height3 = (int)(WindowH * 0.09);
            height4 = (int)(WindowH * 0.13);
            R1 = width1/2;
            R2 = width2/2;
            R3 = width3/2;
            R4 = width4/2;
            ovni = new List<ovnis>();
            line = 64;
            colunm = 64;
        }
        public void param(int fc)
        {
            this.fc = fc;
        }
        public void Load(Texture2D texture)
        {
            this.texture = texture;
        }
        public void Update(ref int time)
        {
            for (int i = 0; i < ovni.Count; ++i)
            {
                if (ovni[i].timer_dead == 60)
                {
                    if (ovni[i].launch < time)
                    {
                        this.ovni[i].rectangle.X += (int)(ovni[i].dirX * ovni[i].dir.X * ovni[i].speed);
                        this.ovni[i].rectangle.Y += (int)(ovni[i].dirY * ovni[i].dir.Y * ovni[i].speed);
                        ovni[i].circle.x = ovni[i].rectangle.Center.X;
                        ovni[i].circle.Y = ovni[i].rectangle.Center.Y;
                        if (ovni[i].type == 'v' || ovni[i].type == 'm' || ovni[i].type == 'p' || ovni[i].type == 'b')
                        {
                            if (ovni[i].rectangle.Left < fond.Left)
                            {
                                this.ovni[i].dirX = 1;
                                this.ovni[i].rectangle.X = fond.Left + 1;
                            }
                            else if (ovni[i].rectangle.Right > fond.Right)
                            {
                                this.ovni[i].dirX = -1;
                                this.ovni[i].rectangle.X = fond.Right - this.ovni[i].rectangle.Width - 5;
                            }
                            if (ovni[i].rectangle.Top < fond.Top)
                            {
                                this.ovni[i].dirY = 1;
                                this.ovni[i].rectangle.Y = fond.Top;
                            }
                            else if (ovni[i].rectangle.Bottom > fond.Bottom)
                            {
                                this.ovni[i].dirY = -1;
                                this.ovni[i].rectangle.Y = fond.Bottom - this.ovni[i].rectangle.Height - 10;
                            }

                        }
                    }
                }
                else
                {
                    ovni[i].FrameLine = 10;
                    ovni[i].timer_dead--;
                    if (ovni[i].timer_dead == 0)
                        ovni.RemoveAt(i);
                }
                if (ovni[i].timer == 0)
                {
                    ovni[i].FrameColumn++;
                    if (ovni[i].FrameColumn == ovni[i].maxframecolumn)
                        ovni[i].FrameColumn = ovni[i].minframecolunm;
                    ovni[i].timer = 10;
                }
                ovni[i].timer--;
            }
        }

        public void Update(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.Down))
                for (int i = 0; i < this.ovni.Count; ++i)
                {
                    ovni[i].rectangle.Y -= fc;
                    ovni[i].circle.x = ovni[i].rectangle.Center.X;
                    ovni[i].circle.Y = ovni[i].rectangle.Center.Y;
                }
            else if (key.IsKeyDown(Keys.Up))
                for (int i = 0; i < this.ovni.Count; ++i)
                {
                    ovni[i].rectangle.Y += fc;
                    ovni[i].circle.x = ovni[i].rectangle.Center.X;
                    ovni[i].circle.Y = ovni[i].rectangle.Center.Y;
                }

        }
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (ovnis v in ovni)
                spritebatch.Draw(texture, v.rectangle, new Rectangle((v.FrameColumn - 1) * colunm, (v.FrameLine - 1) * line, colunm, line), Color.White);
        }
        /// <summary>
        /// add pour SEU
        /// </summary>
        /// <param name="type">v:vie,m:missile,p:power,b:bomb,a:asteroide,c:comet,default:sun</param>
        /// <param name="time"></param>
        /// <param name="speed"></param>
        /// <param name="angle"></param>
        /// <param name="X"></param>
        public void Add(Bonus bonus)
        {
            Random rnd = new Random();
            ovnis ov = new ovnis();
            Vector2 dir;
            if (bonus.angle != 0)
                dir = new Vector2((float)Math.Cos((bonus.angle * Math.PI) / 180), (float)Math.Sin(bonus.angle * Math.PI / 180));
            else
                dir = new Vector2(rnd.Next(), rnd.Next());
            int x = fond.Left + (int)(bonus.X * fond.Width);
            dir.Normalize();

            ov.dir = dir;
            if (bonus.speed != 0)
                ov.speed = bonus.speed;
            else
                ov.speed = rnd.Next(2, 10);
            ov.launch = bonus.launch;

            ov.type = bonus.type;
            switch (bonus.type)
            {
                case 'v':
                    ov.vie = 1;
                    ov.rectangle = new Rectangle(x, -height1 - 2, width1, height1);
                    ov.circle.R = R1;

                    ov.FrameColumn = 21;
                    ov.FrameLine = 5;
                    ov.maxframecolumn = 25;
                    ov.minframecolunm = 21;
                    break;
                case 'm':
                    ov.miss = 1;
                    ov.rectangle = new Rectangle(x, -height1 - 2, width1, height1);
                    ov.circle.R = R1;

                    ov.FrameColumn = 21;
                    ov.minframecolunm = 21;
                    ov.maxframecolumn = 24;
                    ov.FrameLine = 2;
                    break;
                case 'p':
                    ov.power = 1;
                    ov.rectangle = new Rectangle(x, -height1 - 2, width1, height1);
                    ov.circle.R = R1;

                    ov.FrameColumn = 21;
                    ov.minframecolunm = 21;
                    ov.maxframecolumn = 24;
                    ov.FrameLine = 3;
                    break;
                case 'b':
                    ov.bomb = 1;
                    ov.rectangle = new Rectangle(x, -height1 - 2, width1, height1);
                    ov.circle.R = R1;

                    ov.FrameColumn = 21;
                    ov.minframecolunm = 21;
                    ov.maxframecolumn = 24;
                    ov.FrameLine = 4;
                    break;
                case 'a':
                    ov.damage = 30;
                    ov.rectangle = new Rectangle(x, -height2 - 2, width2, height2);
                    ov.circle.R = R2;

                    ov.FrameLine = rnd.Next(1, 6);
                    switch (ov.FrameLine)
                    {
                        case 1:
                            ov.maxframecolumn = 25;
                            break;
                        case 2:
                            ov.maxframecolumn = 19;
                            break;
                        default:
                            ov.maxframecolumn = 20;
                            break;
                    }
                    break;
                case 'c':
                    ov.damage = 60;
                    ov.rectangle = new Rectangle(x, -height3 - 2, width3, height3);
                    ov.circle.R = R3;

                    ov.FrameLine = rnd.Next(8, 10);
                    ov.maxframecolumn = 30;
                    break;
                default:
                    ov.damage = 100;
                    ov.circle.R = R4;
                    ov.rectangle = new Rectangle(x, -height3 - 2, width4, height4);

                    ov.FrameLine = rnd.Next(6, 8);
                    ov.maxframecolumn = 30;
                    break;
            }

            ovni.Add(ov);
        }
        /// <summary>
        /// add for map's editor
        /// </summary>
        /// <param name="type">v:vie,m:missile,p:power,b:bomb,a:asteroide,c:comet,default:sun</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="spawn"></param>
        public void Add(char type, float X, float Y, int spawn)
        {
            int x = (int)(X * WindoW), y = (int)(Y * WindowH);
            ovnis ov = new ovnis();

            switch (type)
            {
                case 'v':
                    ov.rectangle = new Rectangle(x, y, width1, height1);
                    ov.circle.R = R1;

                    ov.FrameColumn = 21;
                    ov.FrameLine = 5;
                    break;
                case 'm':
                    ov.rectangle = new Rectangle(x, y, width1, height1);
                    ov.circle.R = R1;

                    ov.FrameColumn = 21;

                    ov.FrameLine = 2;
                    break;
                case 'p':
                    ov.rectangle = new Rectangle(x, y, width1, height1);
                    ov.circle.R = R1;

                    ov.FrameColumn = 21;
                    ov.FrameLine = 3;
                    break;
                case 'b':
                    ov.rectangle = new Rectangle(x, y, width1, height1);
                    ov.circle.R = R1;
                    ov.FrameLine = 1;
                    ov.FrameColumn = 21;
                    ov.FrameLine = 4;
                    break;
                case 'a':
                    ov.rectangle = new Rectangle(x, y, width2, height2);
                    ov.circle.R = R2;
                    ov.FrameLine = 1;
                    break;
                case 'c':
                    ov.rectangle = new Rectangle(x, y, width3, height3);
                    ov.circle.R = R3;
                    ov.FrameLine = 8;
                    break;
                default:
                    ov.rectangle = new Rectangle(x, y, width4, height4);
                    ov.circle.R = R4;
                    ov.FrameLine = 7;
                    break;
            }
            ov.circle.x = ov.rectangle.Center.X;
            ov.circle.Y = ov.rectangle.Center.Y;
            ov.type = type;
            ov.launch = spawn;
            ovni.Add(ov);
        }

        public void remove_all()
        {
            this.ovni.Clear();
        }
        public void Dispose()
        {
            ovni = null;
            texture.Dispose();
        }
    }
    public class Boss : objet
    {

        int lauchtime { get; set; }
        Rectangle fond { get; set; }
        Texture2D texture { get; set; }
        Texture2D T_munition { get; set; }
        string type { get; set; }
       public int damage { get; set; }
        int speed { get; set; }
        int RPM { get; set; }
        Vector2 dir { get; set; }
        Point sens { get; set; }
        int colunm, line;
        Color color;
        bool avance;
        int timeAvance;
        Double angle;
        Bullet_manager bulletM;
        public List<munition> munition;
        public Boss()
        {


        }
        public void LoadContent(Rectangle fond, Rectangle rectangle,ContentManager Content)
        {
            this.fond = fond;
            this.rectangle = rectangle;
            this.rectangle_C = rectangle;
            dir = new Vector2(0, 1);
            sens = new Point (1, 1);
            avance = true;
            timeAvance = 200;
            angle = 0;

            munition = new List<munition>();
            T_munition = Content.Load<Texture2D>("bullet//bullet");
        }
        public void update( int time, spripte_V J1)
        {
            if (type != "")
            {
                if (this.lauchtime < time)
                {
                    if (avance)
                    {
                        dir = new Vector2(0, 1);
                        if (timeAvance < 0)
                            avance = false;
                        else
                            timeAvance -= speed;
                    }
                    else
                    {
                        dir = new Vector2((float)Math.Sin(angle), 1);

                        angle = (angle + 0.05) % (2 * Math.PI);
                        if (rectangle_C.Left < fond.Left)
                        {
                            sens = new Point(1, sens.Y);
                            rectangle.X = fond.Left;
                        }
                        else if (rectangle_C.Right > fond.Right)
                        {
                            sens = new Point(-1, sens.Y);
                            rectangle.X = fond.Right - rectangle_C.Width - 5;
                        }

                        else if (rectangle_C.Top < fond.Top)
                        {
                            sens = new Point(sens.X, 1);
                            rectangle.Y = fond.Top;
                        }

                        else if (rectangle_C.Bottom > fond.Bottom)
                        {
                            sens = new Point(sens.X, -1);
                            rectangle.Y = fond.Bottom - rectangle_C.Height - 10;
                        }

                    }

                    this.rectangle.X += (int)(sens.X * dir.X * speed);
                    this.rectangle.Y += (int)(sens.Y * dir.Y * speed);

                    bulletM.patternpdate(this, J1, ref munition);
                    Update_rec_collision();
                }
                // sinon rien ps da nimation
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (munition m in munition)
            {
                spriteBatch.Draw(T_munition , m.rectangle, Color.White );
            }
            spriteBatch.Draw(texture, rectangle, Color.White);
    
        }
        public void parametrage(ContentManager Content, Boss_setting boss)
        {
        
            rectangle.X = (int)(boss.pos.X * fond.Width + fond.Right);
            rectangle.Y = fond.Top - rectangle.Height;
            lauchtime = boss.timer;
            this.type = boss.type;
            if (type != null && type != "")
                this.texture = Content.Load<Texture2D>("bossSEU//" + this.type);
            else
            {
                this.texture = Content.Load<Texture2D>("bossSEU//null");
                type = "";
                this.rectangle = new Rectangle();
                this.rectangle_C = new Rectangle();
            }
            vie = boss.life;
            this.speed = boss.speed;
            this.color = boss.color;
            this.RPM = boss.RPM;
            this.damage = boss.damage;

            this.hauteurY = rectangle_C.Height;
            this.largeurX = rectangle_C.Width;
            decalageX = 0;
            decalageY = 0;
                bulletM = new Bullet_manager(new Rectangle(0, 0, rectangle_C.Width / 2, rectangle_C.Height / 2),
    20, this.speed, Content.Load<SoundEffect>("hero//vaisseau//tir2"), color, fond.Width, this.RPM);
            
        }
    }
    public struct Boss_setting
    {
        public int damage { get; set; }
        public Color color { get; set; }
        public int RPM { get; set; }
        public int life { get; set; }
        public int speed { get; set; }
        public int timer { get; set; }
        public Vector2 pos { get; set; } //spawning point
        public string type { get; set; }
        public Boss_setting(Vector2 pos, int timer, int damage, int rpm, int life, int speed, System.Drawing.Color color)
            : this()
        {
            this.pos = pos;
            this.timer = timer;
            this.damage = damage;
            this.color = new Color(color.R, color.G, color.B, color.A);
            this.RPM = rpm;
            this.life = life;
            this.speed = speed;

        }
        public Boss_setting(Vector2 pos, int timer, int damage, int rpm, int life, int speed, System.Drawing.Color color, string type)
            : this()
        {
            this.pos = pos;
            this.timer = timer;
            this.damage = damage;
            this.color = new Color(color.R, color.G, color.B, color.A);
            this.RPM = rpm;
            this.life = life;
            this.speed = speed;
            this.type = type;
        }
        public Boss_setting(Vector2 pos, int timer, int damage, int rpm, int life, int speed, Color color, string type)
            : this()
        {
            this.pos = pos;
            this.timer = timer;
            this.damage = damage;
            this.color = color;
            this.RPM = rpm;
            this.life = life;
            this.speed = speed;
            this.type = type;
        }

    }
    public class Pattern
    {
        Boss_setting boss_setting;
        PatternMgr mgr;
        PatternSettings setting;
        public Vector2 direction { get; set; }
        public bool exist;
        public double LifeTime { get; set; }
        public Rectangle taille_missile { get; set; }
        public Vector2 Pos { get; set; }
        public Vector2 speed { get; set; }
        public int r_height { get; set; }
        public int r_width { get; set; }

        public Pattern(PatternMgr _mgr)
        {
            mgr = _mgr;
            setting = mgr.Setting;
            r_height = setting.height;
            r_width = setting.width;
        }
        public void reset()
        {
            Pos = setting.pos == null ? mgr.Pos : setting.pos(mgr.Pos);
            exist = true;
            LifeTime = setting.LifeTime;
        }
        public void update(GameTime gameTime)
        {
            if (!exist)
                return;
            Pos += setting.direction * speed;
            taille_missile = new Rectangle((int)Pos.X, (int)Pos.Y, r_width, r_height);
            LifeTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (LifeTime < 0 || Pos.Y > OptionState._height)
                exist = false;
        }

        public void draw(SpriteBatch spriteBatch, Texture2D texture, Color color)
        {
            if (!exist)
                return;
            spriteBatch.Begin();
            spriteBatch.Draw(texture, taille_missile, color);
            spriteBatch.End();
        }
    }
    public class PatternMgr
    {
        private readonly List<Pattern> _pattern;
        public PatternSettings Setting { get { return setting; } }
        private readonly PatternSettings setting;
        private double _elapsed;
        private Texture2D missile;
        public Vector2 Pos { get; set; }
        public PatternMgr(PatternSettings _setting)
        {
            setting = _setting;
            _pattern = new List<Pattern>();
            for (int i = 0; i < setting.max; i++)
                _pattern.Add(new Pattern(this));
        }
        public void LoadContent(ContentManager Content)
        {
            Content.Load<Texture2D>("Moteur à particule//particle2");
        }
        public void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var pattern in _pattern)
                pattern.update(gameTime);
            foreach (var pattern in _pattern.Where(p => !p.exist))
                pattern.reset();
            //gérer la collision ici
            if (_elapsed >= 30)
                return;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_elapsed >= 30)
                return;
            foreach (Pattern p in _pattern)
                p.draw(spriteBatch, missile, setting.couleur);
        }
    }
    public struct PatternSettings
    {
        public Color couleur { get; set; } //couleur des missiles
        public int max { get; set; } //maximum de missile présent
        public double frequence { get; set; }
        public Vector2 direction { get; set; }//direction
        public int speed { get; set; }//vitesse du missile
        public double LifeTime { get; set; }//durée de vie du missile
        public Func<Vector2, Vector2> pos { get; set; }//position du missile
        public int width { get; set; }//longueur du rectangle de collision
        public int height { get; set; }//largeur du rectangle de collision

        public PatternSettings(double _Lifetime, Vector2 _direction, int _speed, Color _couleur, double _frequence, int _max = 200, Func<Vector2, Vector2> _pos = null, int _width = 5, int _height = 5)
            : this()
        {
            LifeTime = _Lifetime;
            speed = _speed;
            pos = _pos;
            couleur = _couleur;
            max = _max;
            frequence = _frequence;
            direction = _direction;
            width = _width;
            height = _height;
        }
    }
}
