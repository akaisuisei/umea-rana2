﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Umea_rana
{
    [Serializable]
    public class GameConfiguration
    {
        public int width;
        public int height;
        public bool isfullscreen;
        public float volume_BGM;
        public float sound_effect_volume;
        public string langue;
        DisplayMode displaymode;
        public string difficulte;
        public GameConfiguration()
        {
            displaymode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            height = 720;
            width = 960;
            isfullscreen = false;
            difficulte = "normal";
            volume_BGM = 1.0f;
            sound_effect_volume = 0.5f;
            langue = "en-US";
        }
        public GameConfiguration(int _width, int _height, bool fullscreen, float _volume_BGM, float _sound_effect_volume, string _langue, string _difficulte)
        {
            difficulte = _difficulte;
            width = _width;
            height = _height;
            isfullscreen = fullscreen;
            volume_BGM = _volume_BGM;
            sound_effect_volume = _sound_effect_volume;
            langue = _langue;
        }
    }
}
