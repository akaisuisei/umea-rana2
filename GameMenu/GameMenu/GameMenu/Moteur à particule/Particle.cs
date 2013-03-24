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
    public class Particle
    {
        private readonly ParticleSettings _settings;
        private readonly ParticlesMgr _mgr;
        public bool Alive;
        public Vector2 Pos { get; set; }
        public double LifeTime { get; set; }
        public Vector2 Velocity { get; set; }
        
        public Particle(ParticlesMgr mgr)
        {
            _settings = mgr.Settings;
            _mgr = mgr;
        }

        public void Reset()
        {
            Pos = _settings.Pos == null ? _mgr.Pos : _settings.Pos(_mgr.Pos);
            LifeTime = _settings.LifeTime;
            Alive = true;
        }
        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            if (!Alive)
                return;
            var pourcentage=(float)((_settings.LifeTime-LifeTime)/_settings.LifeTime);
            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            sb.Draw(texture, Pos, null,ParticleHelper.Interpolate(_settings.ColorStart,_settings.ColorEnd,pourcentage), 0, Vector2.Zero, MathHelper.Lerp(_settings.scalestart,_settings.scaleend,pourcentage), SpriteEffects.None, 0);
            sb.End();
        }

        public void Update(GameTime gameTime)
        {
            if (!Alive)
                return;
            if (_settings.Velocity != null)
                Velocity = _settings.Velocity(Velocity, (_settings.LifeTime - LifeTime) / _settings.LifeTime);
            Pos += Velocity;

            LifeTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (LifeTime < 0)
                Alive = false;
        }
    }
    public struct ParticleSettings
    {
        public Color ColorStart { get; set; }
        public Color ColorEnd { get; set; }
        public Func<Vector2,double,Vector2> Velocity { get; set; }
        public Func<Vector2,Vector2> Pos { get; set; }
        public int Max { get; set; }
        public double LifeTime { get; set; }
        public double AddFrequence { get; set; }
        public int ParticlesPerAdd { get; set; }
        public float scalestart { get; set; }
        public float scaleend { get; set; }

        public ParticleSettings(double lifeTime, Color colorStart,Color colorEnd, int max = 200, double addFrequence = 0, int particlesPerAdd = 1, Func<Vector2, double,Vector2> velocity = null,
            Func<Vector2,Vector2> pos=null,float ScaleStart=1,float ScaleEnd=1)
            : this()
        {
            ColorStart = colorStart;
            ColorEnd = colorEnd;
            Max = max;
            LifeTime = lifeTime;
            AddFrequence = addFrequence;
            ParticlesPerAdd = particlesPerAdd;
            Velocity = velocity;
            Pos = pos;
            scalestart = ScaleStart;
            scaleend = ScaleEnd;
            
        }
    }
}
