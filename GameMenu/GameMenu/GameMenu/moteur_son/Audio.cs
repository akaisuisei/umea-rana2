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
        List<Song> playlist2;
        static double _elapsed;
        private int _playing;
        public int playing { get { return _playing; } }
        int  notupdate;
        
        public Audio(ContentManager Content)
        {
            playlist = new Dictionary<string, Song>();
            _elapsed=0;
            playlist2 = new List<Song>();
            playlist.Add("Menu", Content.Load<Song>("Menu//songMenu"));
            playlist.Add("Loading", null);
            playlist.Add("BGMlevel1", null);
            playlist.Add("BGMlevel2", null);
            playlist.Add("extraBGMlevel1", null);
            playlist.Add("extraBGMlevel2", null);
            playlist.Add("BGMlevel3", null);
            playlist.Add("extraBGMlevel3", null);
            playlist.Add("extraBGM", null);
            notupdate = 30;
        }
        public void PlayMenu()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(playlist["Menu"]);
        }
        public void parrametrage(savefile savefile, ContentManager Content)
        {
            foreach (string st in savefile.levelProfile.musique)
                if (st != "" && st != null)
                    playlist2.Add(Content.Load<Song>(savefile.levelProfile.levelname + "\\" + st));
            
            _playing = 0;
        }
        public void parrametrage(string path, savefile savefile, string type)
        {

            foreach (string st in savefile.levelProfile.musique)
                if (st != "" && st != null)
                    playlist2.Add(Song.FromUri("s", new Uri("file:" + "" + path + "\\" + type + "\\" + savefile.levelProfile.levelname + "\\" + st)));
            _playing = 0;
        }
        public void Play()
        {
            notupdate = 30;
            _playing = 0;
            if (playlist2.Count > 0)
                MediaPlayer.Play(playlist2[(int)playing]);
            else
            {
                MediaPlayer.Stop();
                _playing = -1;
            }
        }
        public void Newplaylist()
        {
            MediaPlayer.Stop();
            _playing = 0;
            playlist2.Clear();

        }
        public void Update()
        {
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
        public static void addMusic(string level, string path, string name_of_song) //prend en paramètres le niveau, le chemin pour charger la musique dans le répertoire du niveau concerné
        {
            MediaPlayer.Stop();
            Song song = Song.FromUri(name_of_song, new Uri(path));
            playlist[level] = song;
        }
        public static void nextMusic(Song song,GameTime gameTime,int playing)
        {
            _elapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (_elapsed > song.Duration.Seconds)//si la musique est terminé
            {
                playing=(playing+1)%5;// tu peux changer selon les fonctions qui existent déja
            }
        }
        public void nextMusique()
        {
            if (notupdate <0&& _playing!=-1 && MediaPlayer.PlayPosition == playlist2[(int)_playing ] .Duration)//si la musique est terminé
            {
                _playing =( (int)(_playing + 1) % playlist2.Count);// tu peux changer selon les fonctions qui existent déja
                MediaPlayer.Play(playlist2[(int)_playing  ]);
                notupdate = 60;
            }
            notupdate--;
        }
        public static void changevolume(float volume)
        {
            OptionState.volume_BGM = volume;
            MediaPlayer.Volume = OptionState.volume_BGM;
        }
        public static void changevolume_temp(float volume)
        {
            MediaPlayer.Volume = volume;
        }
        public static void change_soundeffect_volume(float soundeffect_volume)
        {
            OptionState.sound_effect_volume = soundeffect_volume;
            SoundEffect.MasterVolume = OptionState.sound_effect_volume;
        }
        public static void change_soundeffect_volume_temp(float soundeffect_volume)
        {
            SoundEffect.MasterVolume = soundeffect_volume;
        }
    }
}
