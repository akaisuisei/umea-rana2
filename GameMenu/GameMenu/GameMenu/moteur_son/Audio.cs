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
    public class Audio
    {
        static Dictionary<string, Song> playlist;
        

        public Audio(ContentManager Content)
        {
            playlist = new Dictionary<string, Song>();
            playlist.Add("Menu", Content.Load<Song>("Menu//songMenu"));
            playlist.Add("Loading", null);
            playlist.Add("BGMlevel1", null);
            playlist.Add("BGMlevel2", null);
            playlist.Add("extraBGMlevel1", null);
            playlist.Add("extraBGMlevel2", null);
            playlist.Add("BGMlevel3", null);
            playlist.Add("extraBGMlevel3", null);
            playlist.Add("extraBGM", null);
        }
       
        public static void play(string level)
        {
            MediaPlayer.Stop();
            MediaPlayer.Volume = OptionState.volume_BGM;
            switch (level)
            {
                case "Menu":
                    MediaPlayer.Play(playlist["Menu"]);
                    break;
                case "BGMlevel1":
                    if (playlist["extraBGMlevel1"] == null)
                        MediaPlayer.Play(playlist["BGMlevel1"]);
                    else
                        MediaPlayer.Play(playlist["extraBGMlevel1"]);
                    break;
                case "BGMlevel2":
                    if (playlist["extraBGMlevel2"] == null)
                        MediaPlayer.Play(playlist["BGMlevel2"]);
                    else
                        MediaPlayer.Play(playlist["extraBGMlevel2"]);
                    break;
                case "BGMlevel3":
                    if (playlist["extraBGMlevel2"] == null)
                        MediaPlayer.Play(playlist["BGMlevel3"]);
                    else
                        MediaPlayer.Play(playlist["extraBGMlevel3"]);
                    break;
            }
        }
        public static void addMusic(string level,string path,string name_of_song) //prend en paramètres le niveau, le chemin pour charger la musique dans le répertoire du niveau concerné
        {
            MediaPlayer.Stop();
            Song song = Song.FromUri(name_of_song,new Uri(path));
            playlist[level] = song;
        }
        public static void nextMusic(string current_level)
        {
        }
        public static void changevolume(float volume)
        {
            OptionState.volume_BGM = volume;
            MediaPlayer.Volume = OptionState.volume_BGM;
        }
    }
}
