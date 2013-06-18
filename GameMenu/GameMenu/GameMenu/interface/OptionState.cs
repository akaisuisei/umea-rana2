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
        Button button;
        Button button_difficulté;
        Button button_langage;
        Button button_resolution;
        Rectangle rect;
        KeyboardState old;
        Point _resolution;
        int tab = 0;
        int tab1 = 1;
        int tab2 = 2;
        int tab3 = 3;
        int active_item = 0;
        int active_item2 = 0;
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
        bool canchange = true;
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
            old = Keyboard.GetState();
            curseur_BGM = new Curseur(new Vector2(graphics.PreferredBackBufferWidth * 30 / 100, graphics.PreferredBackBufferHeight * 21 / 100), new Rectangle(graphics.PreferredBackBufferWidth * 20 / 100, graphics.PreferredBackBufferHeight * 21 / 100, graphics.PreferredBackBufferWidth / 38, graphics.PreferredBackBufferHeight / 26), new Rectangle(graphics.PreferredBackBufferWidth * 30 / 100, graphics.PreferredBackBufferHeight * 21 / 100, graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 100), OptionState.volume_BGM); //ajouter les dimensions du curseur et de la ligne
            curseur_SE = new Curseur(new Vector2(graphics.PreferredBackBufferWidth * 30 / 100, graphics.PreferredBackBufferHeight * 31 / 100), new Rectangle(graphics.PreferredBackBufferWidth * 20 / 100, graphics.PreferredBackBufferHeight * 31 / 100, graphics.PreferredBackBufferWidth / 38, graphics.PreferredBackBufferHeight / 26), new Rectangle(graphics.PreferredBackBufferWidth * 30 / 100, graphics.PreferredBackBufferHeight * 31 / 100, graphics.PreferredBackBufferWidth / 4, graphics.PreferredBackBufferHeight / 100), OptionState.sound_effect_volume);
            if (fullscreen == true)
            {
                select_resolution = 0;
            }
            else
                if (_width == 1280 && _height == 768)
                {
                    select_resolution = 1;
                }
                else
                    if (_width == 1024 && _height == 768)
                    {
                        select_resolution = 2;
                    }
                    else
                        if (_width == 960 && _height == 720)
                        {
                            select_resolution = 3;
                        }
                        else
                        {
                            select_resolution = 4;
                        }
            switch (difficulté)
            {
                case "Facile":
                    select_difficulte = 0;
                    break;
                case "Normal":
                    select_difficulte = 1;
                    break;
                case "Difficile":
                    select_difficulte = 2;
                    break;
                case "Extreme":
                    select_difficulte = 3;
                    break;
            }
            switch (langue) //à changer j'ai oublié les string qu'il faut mettre
            {
                case "fr-FR":
                    select_langue = 0;
                    break;
                case "en-US":
                    select_langue = 1;
                    break;
                case "es-ES":
                    select_langue = 2;
                    break;
                case "fi-FI":
                    select_langue = 3;
                    break;
                case "ja-JP":
                    select_langue = 4;
                    break;
            }
        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics, Audio audio)
        {
            spriteFont = Content.Load<SpriteFont>("FontList");
            background = Content.Load<Texture2D>("Menu//background menu");
            rectangle = new Rectangle(0, 0, _width, _height);

            button = new Button(1, 8, _width, _height, 0.1f, 0.05f, tab);
            button.LoadContent(Content);
            button.activate(0, 0, 0.08f, 0.2f, "Volume_BGM", LocalizedString.Volume_BGM);
            button.activate(0, 1, 0.08f, 0.3f, "Volume_SE", LocalizedString.Volume_effet_sonore);
            button.activate(0, 2, 0.08f, 0.4f, "Difficulte", LocalizedString.difficulty);
            button.activate(0, 3, 0.08f, 0.5f, "Langage", LocalizedString.Language);
            button.activate(0, 4, 0.08f, 0.6f, "Resolution", LocalizedString.Resolution);
            button.activate(0, 5, 0.08f, 0.7f, "Appliquer", LocalizedString.save_apply);
            button.activate(0, 6, 0.08f, 0.8f, "Defaut", LocalizedString._default);
            button.activate(0, 7, 0.08f, 0.9f, "Retour", LocalizedString.Back);

            button_difficulté = new Button(4, 1, _width, _height, 0.2f, 0.4f, tab1);
            button_difficulté.LoadContent(Content);
            button_difficulté.activate(0, 0, 0.2f, 0.4f, "Facile", LocalizedString.easy);
            button_difficulté.activate(1, 0, 0.4f, 0.4f, "Normal", LocalizedString.medium);
            button_difficulté.activate(2, 0, 0.6f, 0.4f, "Difficile", LocalizedString.Hard);
            button_difficulté.activate(3, 0, 0.8f, 0.4f, "Extreme", LocalizedString.Extreme);

            button_langage = new Button(5, 1, _width, _height, 0.2f, 0.5f, tab2);
            button_langage.LoadContent(Content);
            button_langage.activate(0, 0, 0.2f, 0.5f, "fr-FR", LocalizedString.French);
            button_langage.activate(1, 0, 0.3f, 0.5f, "en-US", LocalizedString.English);
            button_langage.activate(2, 0, 0.4f, 0.5f, "es-ES", LocalizedString.Spanish);
            button_langage.activate(3, 0, 0.5f, 0.5f, "fi-FI", LocalizedString.Finnish);
            button_langage.activate(4, 0, 0.6f, 0.5f, "ja-JP", LocalizedString.japanese);

            curseur_BGM.LoadContent(Content);
            
            curseur_SE.LoadContent(Content);

            button_resolution = new Button(5, 1, _width, _height, 0.2f, 0.6f, tab3);
            button_resolution.LoadContent(Content);
            button_resolution.activate(0, 0, 0.2f, 0.6f, "fullscreen", LocalizedString.Full_screen);
            button_resolution.activate(1, 0, 0.3f, 0.6f,  "1280X768","1280X768");
            button_resolution.activate(2, 0, 0.4f, 0.6f, "1027X768", "1027X768");
            button_resolution.activate(3, 0, 0.5f, 0.6f,  "960X720", "960X720");
            button_resolution.activate(4, 0, 0.6f, 0.6f,  "800X600","800X600");

        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            StorageManager storage = new StorageManager();
            rect = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (latence < 0)
            {
                button.update(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab, ref active_item, ref active_item2);


                if (active_item2 == 5)
                {
                    active_item2 = 0;
                    game.graphics.IsFullScreen = fullscreen;
                    game.graphics.PreferredBackBufferWidth = _width;
                    game.graphics.PreferredBackBufferHeight = _height;
                    Audio.changevolume(curseur_BGM._volume);
                    Audio.change_soundeffect_volume(curseur_SE._volume);
                    game.graphics.ApplyChanges();
                    storage.SaveGameConfiguration(new GameConfiguration(_width, _height, fullscreen, volume_BGM, sound_effect_volume, langue, difficulté));
                    game.ChangeState(Game1.gameState.OptionState);
                }
                else if (active_item2 == 6)
                {
                    active_item2 = 0;
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
                else if (active_item2 == 7)
                {
                    active_item2 = 0;
                    Audio.changevolume(volume_BGM);
                    Audio.change_soundeffect_volume(sound_effect_volume);
                    game.ChangeState(Game1.gameState.MainMenuState);
                }
                else
                    switch (active_item)
                    {
                        case 0://selection sur volume_BGM
                            curseur_BGM.update(keyboard, mouse);
                            Audio.changevolume_temp(curseur_BGM._volume);
                            break;
                        case 1://selection sur volume_SE
                            curseur_SE.update(keyboard, mouse);
                            Audio.change_soundeffect_volume_temp(curseur_SE._volume);
                            break;
                        case 2://selection sur difficulté
                            button_difficulté.update2(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab1, ref select_difficulte, ref difficulté);

                            break;
                        case 3://selection sur langage                   
                            button_langage.update2(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab2, ref select_langue, ref langue);
                            break;
                        case 4://selection sur résolution
                            button_resolution.update3(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab3, ref select_resolution);
                            switch (select_resolution)
                            {
                                case 0://fix
                                    _width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                                    _height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                                    fullscreen = true;
                                    break;
                                case 1://fix
                                    fullscreen = false;
                                    _width = 1280;
                                    _height = 768;
                                    break;
                                case 2:
                                    fullscreen = false;
                                    _width = 1024;
                                    _height = 768;
                                    break;
                                case 3:
                                    fullscreen = false;
                                    _width = 960;
                                    _height = 720;
                                    break;
                                case 4:
                                    fullscreen = false;
                                    _width = 800;
                                    _height = 600;
                                    break;
                            }
                            break;
                        case 5://selection sur enregistrer/appliquer
                            if (keyboard.IsKeyDown(Keys.Enter))
                            {
                                game.graphics.IsFullScreen = fullscreen;
                                game.graphics.PreferredBackBufferWidth = _width;
                                game.graphics.PreferredBackBufferHeight = _height;
                                Audio.changevolume(curseur_BGM._volume);
                                Audio.change_soundeffect_volume(curseur_SE._volume);
                                game.graphics.ApplyChanges();
                                storage.SaveGameConfiguration(new GameConfiguration(_width, _height, fullscreen, volume_BGM, sound_effect_volume, langue, difficulté));
                                game.ChangeState(Game1.gameState.OptionState);
                            }
                            break;
                        case 6://selection sur paramètre par défaut
                            if (keyboard.IsKeyDown(Keys.Enter))
                            {
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
                            if (keyboard.IsKeyDown(Keys.Enter))
                            {
                                Audio.changevolume(volume_BGM);
                                Audio.change_soundeffect_volume(sound_effect_volume);
                                game.ChangeState(Game1.gameState.MainMenuState);
                            }
                            break;
                    }
            }
            latence--;
            old = keyboard;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            curseur_BGM.Draw(spriteBatch);
            curseur_SE.Draw(spriteBatch);
            button.Draw(spriteBatch, active_item);
            button_difficulté.Draw2(spriteBatch, select_difficulte);
            button_langage.Draw2(spriteBatch, select_langue);
            button_resolution.Draw2(spriteBatch, select_resolution);
        }


    }
}