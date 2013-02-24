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
    public class ParticleAdder
    {
        public static void adder(Game1 game,Game1.gameState _currentState)
        {
            if ((_currentState == Game1.gameState.MainMenuState)||(_currentState==Game1.gameState.Pause))
            {
                game.Components.Clear();
                var settingsBlueFire = new ParticleSettings(300, new Color(100, 147, 237, 255), new Color(0, 1f, 1f, 0f), 200, 30, 1,
                (v, t) => Vector2.UnitY * -5 + Vector2.UnitX * 2,
                pos => pos + ParticleHelper.GetRandomVector() * 10, 2, 0.4f);
                var BlueFire = new ParticlesMgr(game, settingsBlueFire) { Pos = new Vector2(640, 355) };
                game.Components.Add(BlueFire);
            }
            if (_currentState == Game1.gameState.Level1_state)
            {
                game.Components.Clear();
                for (int i = 0; i < 40; i++)
                {
                    var settingsNeige = new ParticleSettings(2500 + i * 300, new Color(255, 255, 255), new Color(255, 255, 255), 10, 1000, 1,
                         (v, t) => Vector2.UnitY * 10 + Vector2.UnitX * ParticleHelper.GetRandomFloat(-10, 10),
                         pos => pos + ParticleHelper.GetRandomVector(),0.3f,0.3f);
                    var Neige = new ParticlesMgr(game, settingsNeige) { Pos = new Vector2(ParticleHelper.GetRandomFloat(0, 1500), i * -100) };
                    game.Components.Add(Neige);
                }
            }
            if (_currentState == Game1.gameState.level2)
            {
                game.Components.Clear();
            }
        }
        public static void adder_in_pause(Game1 game)
        {
            game.Components.Clear();
            var settingsBlueFire = new ParticleSettings(300, new Color(100, 147, 237, 255), new Color(0, 1f, 1f, 0f), 200, 30, 1,
            (v, t) => Vector2.UnitY * -5 + Vector2.UnitX * 2,
            pos => pos + ParticleHelper.GetRandomVector() * 10, 2, 0.4f);
            var BlueFire = new ParticlesMgr(game, settingsBlueFire) { Pos = new Vector2(640, 355) };
            game.Components.Add(BlueFire);
        }
    }
}
