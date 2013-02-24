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
    public class ParticlesMgr : DrawableGameComponent
    {
        public Vector2 Pos;
        public ParticleSettings Settings { get { return _settings; } }
        private readonly ParticleSettings _settings;
        private double _elapsed;
        private readonly Game1 _game;
        private readonly List<Particle> _particles;
        private Texture2D _texture;
        private const int Max = 500;

        public ParticlesMgr(Game1 game, ParticleSettings settings)
            : base(game)
        {
            _game = game;
            _settings = settings;
            _particles = new List<Particle>();
            for (int i = 0; i < _settings.Max; i++)
                _particles.Add(new Particle(this));
        }

        protected override void LoadContent()
        {
            _texture = _game.Content.Load<Texture2D>("Moteur à particule//particle2");
        }
        public override void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            foreach (var particle in _particles)
                particle.Update(gameTime);
            int nb = Max - _particles.Count(p => p.Alive);
            int add = _settings.ParticlesPerAdd == 0 ? nb : _settings.ParticlesPerAdd;
            if (_settings.ParticlesPerAdd != 0 && nb < _settings.ParticlesPerAdd)
                return;
            if (_settings.AddFrequence == 0)
                for (int i = 0; i < add; i++)
                    _particles.Find(p => !p.Alive).Reset();
            else
                if (_elapsed > _settings.AddFrequence && nb > 0)
                {
                    foreach (var particle in _particles.Where(p => !p.Alive).Take(add))
                        particle.Reset();
                    _elapsed = 0;
                }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var particle in _particles)
                particle.Draw(_game.spriteBatch, _texture);
        }
    }
}
