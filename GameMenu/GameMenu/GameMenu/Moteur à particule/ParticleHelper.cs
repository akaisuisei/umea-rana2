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
    public static class ParticleHelper
    {
        private static readonly Random _random = new Random();

        public static Vector2 GetRandomVector()
        {
            return Vector2.Transform(Vector2.UnitX, Quaternion.CreateFromYawPitchRoll(0, 0, (float)(_random.NextDouble() * MathHelper.TwoPi)));
        }
        public static Vector2 GetRandomVector(float a, float b)
        {
            return Vector2.Transform(Vector2.UnitX,
                                     Quaternion.CreateFromYawPitchRoll(0, 0, (float)GetRandomFloat(a, b) * MathHelper.TwoPi));
        }
        public static float GetRandomFloat(float from = 0, float to = 1)
        {
            return (float)((_random.NextDouble() * (to - from)) + from);
        }
        public static Color Interpolate(Color a, Color b, float amount)
        {
            Color res = Color.Lerp(a, b, MathHelper.Clamp(amount * 1.5f, 0, 1));
            res.A = (byte)MathHelper.Lerp(0, 255, 1 - amount);
            return res;
        }
    }
}
