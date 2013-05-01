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
using Umea_rana.LocalizedStrings;

namespace Umea_rana
{
    public class OptionState : GameState
    {
        Texture2D background;
        Color color_volume_BGM { get; set; }
        Color color_langue { get; set; }
        Color color_resolution { get; set; }
        Color color_retour { get; set; }
        Color color_resolution1280_768 { get; set; }
        Color color_resolution1024_768 { get; set; }
        Color color_resolution960_720 { get; set; }
        Color color_resolution800_600 { get; set; }
        Color color_resolutionfullscreen { get; set; }
        Color color_difficulté { get; set; }
        Color color_volume_SE { get; set; }
        Color color_defaut { get; set; }
        Color color_save_apply { get; set; }
        Color color_difficulte_facile, color_difficulte_normal, color_difficulte_difficile, color_difficulte_extreme;
        Color color_français, color_anglais, color_finois, color_espagnol, color_japonais, color_chinois;
        int active_item = 0;
        int select_resolution;
        int select_langue;
        int select_difficulte;
        GameConfiguration gameconfiguration;
        Curseur curseur_BGM, curseur_SE;
        Rectangle rectangle;
        public static int _width;
        public static int _height;
        public static bool fullscreen;
        public static float volume_BGM { get; set; }
        public static float sound_effect_volume { get; set; }
        public static string langue;
        public static string difficulté;
        Vector2 v_volume_BGM, v_volume_soundeffect, v_difficulté, defaut, langage, resolution, retour,
            v_difficulte_facile, v_difficulte_normal, v_difficulte_difficile, v_difficulte_extreme, v_fr, v_eng, v_fin, v_esp, v_jap, v_cn, enregistre_appliquer,
            v_fullscreen, resolution1280_768, resolution1024_768, resolution960_720, resolution800_600;
        SpriteFont spriteFont;
        int latence = 0;
        public OptionState(Game1 game1, GraphicsDeviceManager _graphics, ContentManager Content, GameConfiguration _gameconfiguration)
        {
            gameconfiguration = _gameconfiguration;
            _width = gameconfiguration.width;
            _height = gameconfiguration.height;
            fullscreen = gameconfiguration.isfullscreen;
            langue = gameconfiguration.langue;
            volume_BGM = gameconfiguration.volume_BGM;
            sound_effect_volume = gameconfiguration.sound_effect_volume;
            difficulté = gameconfiguration.difficulte;
        }
        public override void Initialize(GraphicsDeviceManager graphics)
        {
            color_volume_BGM = Color.Black;
            color_volume_SE = Color.Black;
            color_difficulté = Color.Black;
            color_langue = Color.Black;
            color_resolution = Color.Black;
            color_resolutionfullscreen = Color.BlueViolet;
            color_resolution1024_768 = Color.BlueViolet;
            color_resolution1280_768 = Color.BlueViolet;
            color_resolution960_720 = Color.BlueViolet;
            color_resolution800_600 = Color.BlueViolet;
            color_retour = Color.Black;
            color_defaut = Color.Black;
            color_save_apply = Color.Black;
            color_difficulte_facile = Color.BlueViolet;
            color_difficulte_normal = Color.BlueViolet;
            color_difficulte_difficile = Color.BlueViolet;
            color_difficulte_extreme = Color.BlueViolet;
            color_français = Color.BlueViolet;
            color_anglais = Color.BlueViolet;
            color_finois = Color.BlueViolet;
            color_espagnol = Color.BlueViolet;
            color_japonais = Color.BlueViolet;
            color_chinois = Color.BlueViolet;
            if (fullscreen == true)
            {
                color_resolutionfullscreen = Color.White;
                select_resolution = 0;
            }
            else
                if (_width == 1280 && _height == 768)
                {
                    color_resolution1280_768 = Color.White;
                    select_resolution = 1;
                }
                else
                    if (_width == 1024 && _height == 768)
                    {
                        color_resolution1024_768 = Color.White;
                        select_resolution = 2;
                    }
                    else
                        if (_width == 960 && _height == 720)
                        {
                            color_resolution960_720 = Color.White;
                            select_resolution = 3;
                        }
                        else
                        {
                            color_resolution800_600 = Color.White;
                            select_resolution = 4;
                        }
            switch (active_item)
            {
                case 0:
                    color_volume_BGM = Color.White;
                    break;
                case 1:
                    color_volume_SE = Color.White;
                    break;
                case 2:
                    color_difficulté = Color.White;
                    break;
                case 3:
                    color_langue = Color.White;
                    break;
                case 4:
                    color_resolution = Color.White;
                    break;
                case 5:
                    color_save_apply = Color.White;
                    break;
                case 6:
                    color_defaut = Color.White;
                    break;
            }
            switch (difficulté)
            {
                case "facile":
                    color_difficulte_facile = Color.White;
                    select_difficulte = 0;
                    break;
                case "normal":
                    color_difficulte_normal = Color.White;
                    select_difficulte = 1;
                    break;
                case "difficile":
                    color_difficulte_difficile = Color.White;
                    select_difficulte = 2;
                    break;
                case "extreme":
                    color_difficulte_extreme = Color.White;
                    select_difficulte = 3;
                    break;
            }
            switch (langue) //à changer j'ai oublié les string qu'il faut mettre
            {
                case "fr-FR":
                    color_français = Color.White;
                    select_langue = 0;
                    break;
                case "en-US":
                    color_anglais = Color.White;
                    select_langue = 1;
                    break;
                case "es-ES":
                    color_espagnol = Color.White;
                    select_langue = 2;
                    break;
                case "fi-FI":
                    color_finois = Color.White;
                    select_langue = 3;
                    break;
                case "_jap":
                    color_japonais = Color.White;
                    select_langue = 4;
                    break;
                case "_cn":
                    color_chinois = Color.White;
                    select_langue = 5;
                    break;
            }
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics)
        {
            spriteFont = Content.Load<SpriteFont>("FontList");
            background = Content.Load<Texture2D>("Menu//background menu");
            rectangle = new Rectangle(0, 0, _width, _height);

            v_volume_BGM = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 20 / 100);
            curseur_BGM = new Curseur(new Vector2(graphics.PreferredBackBufferWidth * 20 / 100, graphics.PreferredBackBufferHeight * 20 / 100), new Rectangle(), new Rectangle(), OptionState.volume_BGM); //ajouter les dimensions du curseur et de la ligne

            v_volume_soundeffect = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 30 / 100);
            curseur_SE = new Curseur(new Vector2(graphics.PreferredBackBufferWidth * 20 / 100, graphics.PreferredBackBufferHeight * 20 / 100), new Rectangle(), new Rectangle(), OptionState.sound_effect_volume); //idem

            v_difficulté = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 40 / 100);
            v_difficulte_facile = new Vector2(graphics.PreferredBackBufferWidth * 20 / 100, graphics.PreferredBackBufferHeight * 40 / 100);
            v_difficulte_normal = new Vector2(graphics.PreferredBackBufferWidth * 40 / 100, graphics.PreferredBackBufferHeight * 40 / 100);
            v_difficulte_difficile = new Vector2(graphics.PreferredBackBufferWidth * 60 / 100, graphics.PreferredBackBufferHeight * 40 / 100);
            v_difficulte_extreme = new Vector2(graphics.PreferredBackBufferWidth * 80 / 100, graphics.PreferredBackBufferHeight * 40 / 100);

            langage = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 50 / 100);
            v_fr = new Vector2(graphics.PreferredBackBufferWidth * 20 / 100, graphics.PreferredBackBufferHeight * 50 / 100);
            v_eng = new Vector2(graphics.PreferredBackBufferWidth * 30 / 100, graphics.PreferredBackBufferHeight * 50 / 100);
            v_esp = new Vector2(graphics.PreferredBackBufferWidth * 40 / 100, graphics.PreferredBackBufferHeight * 50 / 100);
            v_fin = new Vector2(graphics.PreferredBackBufferWidth * 50 / 100, graphics.PreferredBackBufferHeight * 50 / 100);
            v_jap = new Vector2(graphics.PreferredBackBufferWidth * 60 / 100, graphics.PreferredBackBufferHeight * 50 / 100);
            v_cn = new Vector2(graphics.PreferredBackBufferWidth * 70 / 100, graphics.PreferredBackBufferHeight * 50 / 100);

            resolution = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 60 / 100);
            v_fullscreen = new Vector2(graphics.PreferredBackBufferWidth * 20 / 100, graphics.PreferredBackBufferHeight * 60 / 100);
            resolution1280_768 = new Vector2(graphics.PreferredBackBufferWidth * 35 / 100, graphics.PreferredBackBufferHeight * 60 / 100);
            resolution1024_768 = new Vector2(graphics.PreferredBackBufferWidth * 50 / 100, graphics.PreferredBackBufferHeight * 60 / 100);
            resolution960_720 = new Vector2(graphics.PreferredBackBufferWidth * 65 / 100, graphics.PreferredBackBufferHeight * 60 / 100);
            resolution800_600 = new Vector2(graphics.PreferredBackBufferWidth * 80 / 100, graphics.PreferredBackBufferHeight * 60 / 100);

            enregistre_appliquer = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 70 / 100);
            defaut = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 80 / 100);
            retour = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 90 / 100);
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            StorageManager storage = new StorageManager();
            if (latence <= 0)
            {
                if (keyboard.IsKeyDown(Keys.Down))
                {
                    active_item++;
                    if (active_item > 7)
                        active_item = 0;
                    latence = 20;
                }
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    active_item--;
                    if (active_item < 0)
                        active_item = 7;
                    latence = 20;
                }
                switch (active_item)
                {
                    case 0://selection sur volume_BGM
                        color_volume_BGM = Color.White;
                        color_volume_SE = Color.Black;
                        color_retour = Color.Black;
                        curseur_BGM.update(keyboard, mouse);
                        break;
                    case 1://selection sur volume_SE
                        color_volume_BGM = Color.Black;
                        color_volume_SE = Color.White;
                        color_difficulté = Color.Black;
                        curseur_SE.update(keyboard, mouse);
                        break;
                    case 2://selection sur difficulté
                        color_volume_SE = Color.Black;
                        color_difficulté = Color.White;
                        color_langue = Color.Black;

                        if (keyboard.IsKeyDown(Keys.Left))
                        {
                            select_difficulte--;
                            if (select_difficulte < 0)
                                select_difficulte = 3;
                            latence = 20;
                        }
                        if (keyboard.IsKeyDown(Keys.Right))
                        {
                            select_difficulte++;
                            if (select_difficulte > 3)
                                select_difficulte = 0;
                            latence = 20;
                        }
                        switch (select_difficulte)
                        {
                            case 0:
                                color_difficulte_extreme = Color.BlueViolet;
                                color_difficulte_normal = Color.BlueViolet;
                                color_difficulte_facile = Color.White;
                                difficulté = "facile";
                                break;
                            case 1:
                                color_difficulte_facile = Color.BlueViolet;
                                color_difficulte_normal = Color.White;
                                color_difficulte_difficile = Color.BlueViolet;
                                difficulté = "normal";
                                break;
                            case 2:
                                color_difficulte_normal = Color.BlueViolet;
                                color_difficulte_difficile = Color.White;
                                color_difficulte_extreme = Color.BlueViolet;
                                difficulté = "difficile";
                                break;
                            case 3:
                                color_difficulte_facile = Color.BlueViolet;
                                color_difficulte_difficile = Color.BlueViolet;
                                color_difficulte_extreme = Color.White;
                                difficulté = "extreme";
                                break;
                        }
                        break;
                    case 3://selection sur langage                   
                        color_difficulté = Color.Black;
                        color_langue = Color.White;
                        color_resolution = Color.Black;
                        if (keyboard.IsKeyDown(Keys.Left))
                        {
                            select_langue--;
                            if (select_langue < 0)
                                select_langue = 5;
                            latence = 20;
                        }
                        if (keyboard.IsKeyDown(Keys.Right))
                        {
                            select_langue++;
                            if (select_langue > 5)
                                select_langue = 0;
                            latence = 20;
                        }
                        switch (select_langue) //à changer j'ai oublié les string qu'il faut mettre
                        {
                            case 0:
                                color_chinois = Color.BlueViolet;
                                color_anglais = Color.BlueViolet;
                                color_français = Color.White;
                                langue = "fr-FR";
                                break;
                            case 1:
                                color_français = Color.BlueViolet;
                                color_anglais = Color.White;
                                color_espagnol = Color.BlueViolet;
                                langue = "en-US";
                                break;
                            case 2:
                                color_anglais = Color.BlueViolet;
                                color_finois = Color.BlueViolet;
                                color_espagnol = Color.White;
                                langue = "es-ES";
                                break;
                            case 3:
                                color_espagnol = Color.BlueViolet;
                                color_japonais = Color.BlueViolet;
                                color_finois = Color.White;
                                langue = "fi-FI";
                                break;
                            case 4:
                                color_japonais = Color.White;
                                color_chinois = Color.BlueViolet;
                                color_finois = Color.BlueViolet;
                                break;
                            case 5:
                                color_japonais = Color.BlueViolet;
                                color_français = Color.BlueViolet;
                                color_chinois = Color.White;
                                break;
                        }
                        break;
                    case 4://selection sur résolution
                        color_langue = Color.Black;
                        color_resolution = Color.White;
                        color_save_apply = Color.Black;
                        if (keyboard.IsKeyDown(Keys.Left))
                        {
                            select_resolution--;
                            if (select_resolution < 0)
                                select_resolution = 4;
                            latence = 20;
                        }
                        if (keyboard.IsKeyDown(Keys.Right))
                        {
                            select_resolution++;
                            if (select_resolution > 4)
                                select_resolution = 0;
                            latence = 20;
                        }
                        switch (select_resolution)
                        {
                            case 0://fix
                                color_resolutionfullscreen = Color.White;
                                color_resolution800_600 = Color.BlueViolet;
                                color_resolution1280_768 = Color.BlueViolet;
                                _width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                                _height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                                fullscreen = true;
                                break;
                            case 1://fix
                                color_resolution1280_768 = Color.White;
                                color_resolution1024_768 = Color.BlueViolet;
                                color_resolutionfullscreen = Color.BlueViolet;
                                fullscreen = false;
                                _width = 1280;
                                _height = 768;
                                break;
                            case 2:
                                color_resolution1280_768 = Color.BlueViolet;
                                color_resolution1024_768 = Color.White;
                                color_resolution960_720 = Color.BlueViolet;
                                fullscreen = false;
                                _width = 1024;
                                _height = 768;
                                break;
                            case 3:
                                color_resolution1024_768 = Color.BlueViolet;
                                color_resolution960_720 = Color.White;
                                color_resolution800_600 = Color.BlueViolet;
                                fullscreen = false;
                                _width = 960;
                                _height = 720;
                                break;
                            case 4:
                                color_resolution800_600 = Color.White;
                                color_resolutionfullscreen = Color.BlueViolet;
                                color_resolution960_720 = Color.BlueViolet;
                                fullscreen = false;
                                _width = 800;
                                _height = 600;
                                break;
                        }
                        break;
                    case 5://selection sur enregistrer/appliquer
                        color_resolution = Color.Black;
                        color_defaut = Color.Black;
                        color_save_apply = Color.White;
                        if (keyboard.IsKeyDown(Keys.Enter))
                        {
                            latence = 20;
                            game.graphics.IsFullScreen = fullscreen;
                            game.graphics.PreferredBackBufferWidth = _width;
                            game.graphics.PreferredBackBufferHeight = _height;
                            Audio.changevolume(volume_BGM);
                            SoundEffect.MasterVolume = sound_effect_volume;
                            game.graphics.ApplyChanges();
                            storage.SaveGameConfiguration(new GameConfiguration(_width, _height, fullscreen, volume_BGM, sound_effect_volume, langue, difficulté));
                            game.ChangeState(Game1.gameState.OptionState);
                        }
                        break;
                    case 6://selection sur paramètre par défaut
                        color_defaut = Color.White;
                        color_save_apply = Color.Black;
                        color_retour = Color.Black;
                        if (keyboard.IsKeyDown(Keys.Enter))
                        {
                            latence = 20;
                            _height = game.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
                            _width = game.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
                            fullscreen = true;
                            volume_BGM = 1.0f;
                            sound_effect_volume = 0.5f;
                            langue = "fr-FR";
                            difficulté = "normal";
                            Audio.changevolume(volume_BGM);
                            SoundEffect.MasterVolume = sound_effect_volume;
                            game.graphics.PreferredBackBufferHeight = _height;
                            game.graphics.PreferredBackBufferWidth = _width;
                            game.graphics.IsFullScreen = fullscreen;
                            game.graphics.ApplyChanges();
                            storage.SaveGameConfiguration(new GameConfiguration(_width, _height, fullscreen, volume_BGM, sound_effect_volume, langue, difficulté));
                            game.ChangeState(Game1.gameState.OptionState);
                        }
                        break;
                    case 7://selection sur retour
                        color_volume_BGM = Color.Black;
                        color_defaut = Color.Black;
                        color_retour = Color.White;
                        if (keyboard.IsKeyDown(Keys.Enter))
                        {
                            game.ChangeState(Game1.gameState.MainMenuState);
                        }
                        break;
                }
            }

            latence--;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            spriteBatch.DrawString(spriteFont, LocalizedString.Volume_BGM, v_volume_BGM, color_volume_BGM);

            spriteBatch.DrawString(spriteFont, LocalizedString.Volume_effet_sonore , v_volume_soundeffect, color_volume_SE);

            spriteBatch.DrawString(spriteFont, LocalizedString.difficulty , v_difficulté, color_difficulté);
            spriteBatch.DrawString(spriteFont, LocalizedString.easy , v_difficulte_facile, color_difficulte_facile);
            spriteBatch.DrawString(spriteFont, LocalizedString.medium , v_difficulte_normal, color_difficulte_normal);
            spriteBatch.DrawString(spriteFont, LocalizedString.Hard , v_difficulte_difficile, color_difficulte_difficile);
            spriteBatch.DrawString(spriteFont, LocalizedString.Extreme , v_difficulte_extreme, color_difficulte_extreme);

            spriteBatch.DrawString(spriteFont, LocalizedString.Language, langage, color_langue);
            spriteBatch.DrawString(spriteFont, "Francais", v_fr, color_français);
            spriteBatch.DrawString(spriteFont, "English", v_eng, color_anglais);
            spriteBatch.DrawString(spriteFont, "Catalan", v_esp, color_espagnol);
            spriteBatch.DrawString(spriteFont, "Suomi", v_fin, color_finois);
            spriteBatch.DrawString(spriteFont, "nihongo", v_jap, color_japonais);
            spriteBatch.DrawString(spriteFont, "Chinois", v_cn, color_chinois);

            spriteBatch.DrawString(spriteFont, LocalizedString.Resolution , resolution, color_resolution);
            spriteBatch.DrawString(spriteFont, LocalizedString.Full_screen , v_fullscreen, color_resolutionfullscreen);
            spriteBatch.DrawString(spriteFont, "1280 X 768", resolution1280_768, color_resolution1280_768);
            spriteBatch.DrawString(spriteFont, "1024 X 768", resolution1024_768, color_resolution1024_768);
            spriteBatch.DrawString(spriteFont, "960 X 720", resolution960_720, color_resolution960_720);
            spriteBatch.DrawString(spriteFont, "800 X 600", resolution800_600, color_resolution800_600);

            spriteBatch.DrawString(spriteFont, LocalizedString.save_apply , enregistre_appliquer, color_save_apply);
            spriteBatch.DrawString(spriteFont, LocalizedString._default , defaut, color_defaut);

            spriteBatch.DrawString(spriteFont, LocalizedString.Back , retour, color_retour);
        }


    }
}