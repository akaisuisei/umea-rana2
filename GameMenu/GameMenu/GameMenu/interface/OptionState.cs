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
    public class OptionState3 : GameState
    {
        Texture2D background;
        Button button;
        Rectangle rect;
        KeyboardState old;
        int tab = 0;
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
        bool canchange = true;
        SpriteFont spriteFont;
        int latence = 0;
        public OptionState3(Game1 game1, GraphicsDeviceManager _graphics, ContentManager Content, GameConfiguration _gameconfiguration)
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
                case "ja-JP":
                    color_japonais = Color.White;
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

            v_volume_BGM = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 20 / 100);
            curseur_BGM.LoadContent(Content);

            v_volume_soundeffect = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 30 / 100);
            curseur_SE.LoadContent(Content);

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
            rect = new Rectangle(mouse.X, mouse.Y, 1, 1);
            //  button.update(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab, ref active_item, ref canchange);
            switch (active_item)
            {
                case 0://selection sur volume_BGM
                    color_volume_BGM = Color.White;
                    color_volume_SE = Color.Black;
                    color_retour = Color.Black;
                    curseur_BGM.update(keyboard, mouse);
                    Audio.changevolume_temp(curseur_BGM._volume);
                    break;
                case 1://selection sur volume_SE
                    color_volume_BGM = Color.Black;
                    color_volume_SE = Color.White;
                    color_difficulté = Color.Black;
                    curseur_SE.update(keyboard, mouse);
                    Audio.change_soundeffect_volume_temp(curseur_SE._volume);
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
                            langue = "ja-JP";
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
                        Audio.changevolume(curseur_BGM._volume);
                        Audio.change_soundeffect_volume(curseur_SE._volume);
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
                        Audio.changevolume(volume_BGM);
                        Audio.change_soundeffect_volume(sound_effect_volume);
                        game.ChangeState(Game1.gameState.MainMenuState);
                    }
                    break;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            curseur_BGM.Draw(spriteBatch);
            curseur_SE.Draw(spriteBatch);
            button.Draw(spriteBatch, active_item);

            spriteBatch.DrawString(spriteFont, LocalizedString.easy, v_difficulte_facile, color_difficulte_facile);
            spriteBatch.DrawString(spriteFont, LocalizedString.medium, v_difficulte_normal, color_difficulte_normal);
            spriteBatch.DrawString(spriteFont, LocalizedString.Hard, v_difficulte_difficile, color_difficulte_difficile);
            spriteBatch.DrawString(spriteFont, LocalizedString.Extreme, v_difficulte_extreme, color_difficulte_extreme);

            spriteBatch.DrawString(spriteFont, "Français", v_fr, color_français);
            spriteBatch.DrawString(spriteFont, "English", v_eng, color_anglais);
            spriteBatch.DrawString(spriteFont, "Catalán", v_esp, color_espagnol);
            spriteBatch.DrawString(spriteFont, "Suomi", v_fin, color_finois);
            spriteBatch.DrawString(spriteFont, "日本語", v_jap, color_japonais);

            spriteBatch.DrawString(spriteFont, LocalizedString.Full_screen, v_fullscreen, color_resolutionfullscreen);
            spriteBatch.DrawString(spriteFont, "1280 X 768", resolution1280_768, color_resolution1280_768);
            spriteBatch.DrawString(spriteFont, "1024 X 768", resolution1024_768, color_resolution1024_768);
            spriteBatch.DrawString(spriteFont, "960 X 720", resolution960_720, color_resolution960_720);
            spriteBatch.DrawString(spriteFont, "800 X 600", resolution800_600, color_resolution800_600);


        }


    }
    public class OptionState : GameState
    {
        Texture2D background;
        Button button_resolution, button_dificulte, button_langue, button_apply, button_action;
        Rectangle rect, rectangle;
        KeyboardState old;
        int tab = 0;
        string apply, apply_default;


        GameConfiguration gameconfiguration;
        Curseur curseur_BGM, curseur_SE;
        public Point resolution;
        public static int _width;
        public static int _height;
        public static bool fullscreen;
        public static float volume_BGM { get; set; }
        public static float sound_effect_volume { get; set; }
        public static string langue;
        public static string difficulté;
        Vector2 v_volume_BGM, v_volume_soundeffect;
        SpriteFont spriteFont;
        Rectangle[] fond;
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


        }
        public override void LoadContent(ContentManager Content, GraphicsDevice Graph, ref string level, ref string next, GraphicsDeviceManager graphics, Audio audio)
        {
            apply_default = "";
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            spriteFont = Content.Load<SpriteFont>("FontList");
            background = Content.Load<Texture2D>("Menu//background menu");
            rectangle = new Rectangle(0, 0, _width, _height);

            button_dificulte = new Button(4, 1, width, height, 0.1f, 0.1f, 2, "dificulty", 0f, 0.45f);
            button_dificulte.LoadContent(Content);
            button_dificulte.activateoption(0, 0, 0.1f, 0.45f, "facile", "easy", OptionState.difficulté);
            button_dificulte.activateoption(1, 0, 0.3f, 0.45f, "normal", "moyen", OptionState.difficulté);
            button_dificulte.activateoption(2, 0, 0.5f, 0.45f, "difficile", "hard", OptionState.difficulté);
            button_dificulte.activateoption(3, 0, 0.7f, 0.45f, "extreme", "extrem", OptionState.difficulté);

            button_langue = new Button(5, 1, width, height, 0.1f, 0.1f, 3, "Langue", 0f, 0.55f);
            button_langue.LoadContent(Content);
            button_langue.activateoption(0, 0, 0.1f, 0.55f, "fr-FR", "Francais", OptionState.langue);
            button_langue.activateoption(1, 0, 0.3f, 0.55f, "en-US", "English", OptionState.langue);
            button_langue.activateoption(2, 0, 0.5f, 0.55f, "es-ES", "Catalán", OptionState.langue);
            button_langue.activateoption(3, 0, 0.7f, 0.55f, "fi-FI", "Suomi", OptionState.langue);
            button_langue.activateoption(4, 0, 0.9f, 0.55f, "ja-JP", "日本語", OptionState.langue);


            button_resolution = new Button(5, 1, width, height, 0.1f, 0.1f, 4, "resolution", 0f, 0.65f, 'j');
            button_resolution.LoadContent(Content);
            button_resolution.activate(0, 0, 0.1f, 0.65f, new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), "Full screen", new Point(0, 0));
            button_resolution.activate(1, 0, 0.3f, 0.65f, new Point(1280, 768), "1280 X 768", new Point(width, height));
            button_resolution.activate(2, 0, 0.5f, 0.65f, new Point(1024, 768), "1024 X 768", new Point(width, height));
            button_resolution.activate(3, 0, 0.7f, 0.65f, new Point(960, 720), "960 X 720", new Point(width, height));
            button_resolution.activate(4, 0, 0.9f, 0.65f, new Point(800, 600), "800 X 600", new Point(width, height));

            button_apply = new Button(1, 1, width, height, 0.1f, 0.1f, 5);
            button_apply.LoadContent(Content);
            button_apply.activate(0, 0, 0.2f, 0.75f, "apply", "apply");

            button_action = new Button(2, 1, width, height, 0.1f, 0.1f, 6);
            button_action.LoadContent(Content);
            button_action.activate(0, 0, 0.2f, 0.85f, "default", "par default");
            button_action.activate(1, 0, 0.7f, 0.85f, "back", "retour");

            v_volume_BGM = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 20 / 100);
            curseur_BGM.LoadContent(Content);

            v_volume_soundeffect = new Vector2(graphics.PreferredBackBufferWidth * 8 / 100, graphics.PreferredBackBufferHeight * 30 / 100);
            curseur_SE.LoadContent(Content);
            rect = new Rectangle(0, 0, 1, 1);

            fond = new Rectangle[7] {
                new Rectangle (0,(int)(height*0.21 ),width ,4 ),
                new Rectangle (0,(int)(height*0.31 ) ,width ,4),
                new Rectangle (0,(int)(height*0.45 ),width ,4 ),
                new Rectangle (0,(int)(height*0.55 ) ,width ,4 ),
                new Rectangle (0,(int)(height*0.65 ) ,width ,4),
                new Rectangle (0,(int)(height*0.75 ),width ,4 ),
                new Rectangle (0,(int)(height*0.85 ),width ,4 )};
        }
        public override void UnloadContent()
        {
        }
        public override void Update(Game1 game, Audio audio)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            rect.X = mouse.X;
            rect.Y = mouse.Y;
            //  button.update(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab, ref active_item, ref canchange);
            if (old.IsKeyDown(Keys.Up) && keyboard.IsKeyUp(Keys.Up))
            {
                tab--;
                if (tab == -1)
                    tab = 6;

            }
            if (old.IsKeyDown(Keys.Down) && keyboard.IsKeyUp(Keys.Down))
            {
                tab = (tab + 1) % 7;
            }
            for (int i = 0; i < fond.GetLength(0); i++)
                if (fond[i].Intersects(rect))
                    tab = i;
            switch (tab)
            {
                case 0://selection sur volume_BGM

                    curseur_BGM.update(keyboard, mouse);
                    Audio.changevolume_temp(curseur_BGM._volume);
                    break;
                case 1://selection sur volume_SE

                    curseur_SE.update(keyboard, mouse);
                    Audio.change_soundeffect_volume_temp(curseur_SE._volume);
                    break;


                default:
                    resolution = button_resolution.Update2(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab);
                    langue = button_langue.update2(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab);
                    difficulté = button_dificulte.update2(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab);
                    button_action.update(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab, ref apply);
                    button_apply.update(ref keyboard, ref old, ref mouse, ref rect, ref game, ref tab, ref apply);

                    if (apply != apply_default)
                    {
                        StorageManager storage = new StorageManager();
                        _width = resolution.X; _height = resolution.Y;
                        switch (apply)
                        {
                            case "apply":
                                game.graphics.IsFullScreen = fullscreen;
                                game.graphics.PreferredBackBufferWidth = _width;
                                game.graphics.PreferredBackBufferHeight = _height;
                                Audio.changevolume(curseur_BGM._volume);
                                Audio.change_soundeffect_volume(curseur_SE._volume);
                                game.graphics.ApplyChanges();
                                storage.SaveGameConfiguration(new GameConfiguration(_width, _height, fullscreen, volume_BGM, sound_effect_volume, langue, difficulté));
                                game.ChangeState(Game1.gameState.OptionState);
                                break;
                            case "default":
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
                                break;
                            case "back":
                                game.ChangeState(Game1.gameState.MainMenuState);
                                System.Threading.Thread.Sleep(200);
                                break;
                        }


                        apply = "";
                    }

                    break;



            }
            old = keyboard;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, rectangle, Color.White);
            curseur_BGM.Draw(spriteBatch);
            curseur_SE.Draw(spriteBatch);
            //  button_apply.Draw(spriteBatch);
            button_dificulte.draw(spriteBatch);
            button_langue.draw(spriteBatch);
            button_resolution.draw(spriteBatch);
            button_apply.draw(spriteBatch);
            button_action.draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, "volume bac", v_volume_BGM, Color.White);
            spriteBatch.DrawString(spriteFont, "volume bgm", v_volume_soundeffect, Color.White);

        }


    }
}