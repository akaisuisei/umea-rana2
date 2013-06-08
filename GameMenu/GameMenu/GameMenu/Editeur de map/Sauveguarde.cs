using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Umea_rana
{


    class Sauveguarde
    {

        string path;
        public string _path { get { return path; } }
        public Sauveguarde()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SavedGames\\GameMenu\\MyApplication\\Player1";
        }
        /// <summary>
        /// save sEU pour editeur de map pas fini manque la copie du fond ds le dossier
        /// </summary>
        /// <param name="save"></param>
        public void save_SEU(ref savefile save)
        {
            FileStream file1 = null;
            XmlSerializer f = null;
            string sav = path + "\\SEU\\" + save.levelProfile.levelname;
            DirectoryInfo dir = new DirectoryInfo(sav);
            string ext = "", nam = "", name = "";
            string[] res = new string[save.levelProfile.musique.Length];
            if (!dir.Exists)
                dir.Create();

            for (int j = 0; j < save.levelProfile.background_name.Length; ++j)
            {
                if (save.levelProfile.background_name[j] == '.')
                    ext = "";
                nam += save.levelProfile.background_name[j];
                ext += save.levelProfile.background_name[j];

                if (save.levelProfile.background_name[j] == '\\')
                    nam = "";
            }

            // copie de backgroung et verif k il n est pas ds le dossier
            for (int j = 0; j < nam.Length && nam[j] != '.'; ++j)
                name += nam[j];
            if (name != "BackgrounD")
                System.IO.File.Copy(save.levelProfile.background_name, sav + "\\BackgrounD" + ext, true);

            save.levelProfile.background_name = "BackgrounD" + ext;
            // copie des musique + verif pas ds le dossier
            for (int w = 0; w < res.Length; w++)
            {
                for (int j = 0; j < save.levelProfile.musique[w].Length; ++j)
                {
                    res[w] += save.levelProfile.musique[w][j];
                    if (save.levelProfile.musique[w][j] == '\\')
                        res[w] = "";
                }
                if (save.levelProfile.musique[w] != null && save.levelProfile.musique[w] != "")
                {
                    System.IO.File.Copy(save.levelProfile.musique[w], sav + "\\" + res[w], true);
                    save.levelProfile.musique[w] = res[w];
                }
            }
            file1 = new FileStream(dir.FullName + "\\level.lvl", FileMode.Create, FileAccess.Write);
            f = new XmlSerializer(typeof(savefile));
            f.Serialize(file1, save);
            file1.Close();
        }
        /// <summary>
        ///  pour shot em up a utilier ds l editeur de map
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="savefile"></param>
        public void load_SEU(ref string filename, ref savefile savefile)
        {
            FileStream file1 = null;
            XmlSerializer f = null;

            DirectoryInfo dir = null;
            dir = new DirectoryInfo(path + "\\SEU\\" + filename);
            if (dir.Exists)
            {
                file1 = new FileStream(dir.FullName + "\\level.lvl", FileMode.Open, FileAccess.Read);
                f = new XmlSerializer(typeof(savefile));
                savefile = (savefile)f.Deserialize(file1);
                file1.Close();
            }
        }

        public string[] subdirectory(string type) // nom des dossier ds le dossier gamesave\\...\\ type
        {
            string g;
            if (!Directory.Exists(path + "\\" + type))
                Directory.CreateDirectory(path + "\\" + type);
            string[] hello = System.IO.Directory.GetDirectories(path + "\\" + type);
            for (int j = 0; j < hello.Length; ++j)
            {
                g = string.Empty;
                for (int i = 0; i < hello[j].Length; ++i)
                {
                    g += hello[j][i];
                    if (hello[j][i] == '\\')
                        g = string.Empty;
                }
                hello[j] = g;
            }
            return hello;
        }

        public string[] filename(ContentManager Content)
        {
            string g;
            if (!Directory.Exists(Path.GetDirectoryName(Content.RootDirectory) + "\\back"))
                Directory.CreateDirectory(Content.RootDirectory + "\\back");
            string[] hello = System.IO.Directory.GetFiles(Content.RootDirectory + "\\back");
            for (int j = 0; j < hello.Length; ++j)
            {
                g = string.Empty;
                for (int i = 0; i < hello[j].Length && hello[j][i] != '.'; ++i)
                {
                    g += hello[j][i];
                    if (hello[j][i] == '\\')
                        g = string.Empty;
                }
                hello[j] = g;
            }
            return hello;
        }
        /// <summary>
        ///  jeu normal chargement
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="level">nom du nivau</param>
        /// <param name="iamanage_K"></param>
        /// <param name="iamanage_T"></param>
        /// <param name="iamanage_V"></param>
        public void load_level_SEU(ContentManager Content, ref string level, ref string next, ref IA_manager_K iamanage_K,
            ref IA_manager_T iamanage_T, ref IA_manager_V iamanage_V, ref Scrolling_ManagerV scrollM, ref GraphicsDevice grash, ref spripte_V sprite, ref Ovni ovni)
        {
            savefile savefil = new savefile();
            FileStream file1 = null;
            XmlSerializer f = null;

            DirectoryInfo dir = null;
            dir = new DirectoryInfo(Content.RootDirectory + "\\" + level);
            if (dir.Exists)
            {
                file1 = new FileStream(dir.FullName + "\\level.lvl", FileMode.Open, FileAccess.Read);
                f = new XmlSerializer(typeof(savefile));
                savefil = (savefile)f.Deserialize(file1);
                file1.Close();
            }
            for (int j = 0; j < savefil.ia_tireur.Count; ++j)
                iamanage_T.Add(savefil.ia_tireur[j]);
            for (int j = 0; j < savefil.ia_viseur.Count; ++j)
                iamanage_V.Add(savefil.ia_viseur[j]);
            for (int j = 0; j < savefil.ia_Kamikaze.Count; ++j)
                iamanage_K.Add(savefil.ia_Kamikaze[j]);
            for (int j = 0; j < savefil.bonus.Count; ++j)
                ovni.Add(savefil.bonus[j]);
            scrollM.Load(Content, savefil.levelProfile, grash);
            sprite.parametrage(ref savefil.levelProfile);
            ovni.param(savefil.levelProfile.fc_speed);
            next = savefil.levelProfile.next_level;

        }
        /// <summary>
        ///jeu level perso chargement
        /// </summary>
        /// <param name="level">nom du niveau</param>
        /// <param name="iamanage_K">manager de ia</param>
        /// <param name="iamanage_T"></param>
        /// <param name="iamanage_V"></param>
        public void load_leveleditor_SEU(ContentManager Content, string level, ref IA_manager_K iamanage_K,
            ref IA_manager_T iamanage_T, ref IA_manager_V iamanage_V, ref Scrolling_ManagerV scrollM, ref GraphicsDevice grap, ref spripte_V sprite, ref Ovni ovni)
        {
            savefile savefile = new savefile();
            load_SEU(ref level, ref savefile);
            for (int j = 0; j < savefile.ia_tireur.Count; ++j)
                iamanage_T.Add(savefile.ia_tireur[j]);
            for (int j = 0; j < savefile.ia_viseur.Count; ++j)
                iamanage_V.Add(savefile.ia_viseur[j]);
            for (int j = 0; j < savefile.ia_Kamikaze.Count; ++j)
                iamanage_K.Add(savefile.ia_Kamikaze[j]);
            for (int j = 0; j < savefile.bonus.Count; ++j)
                ovni.Add(savefile.bonus[j]);

            scrollM.Load(Content, savefile.levelProfile, grap);
            sprite.parametrage(ref savefile.levelProfile);
            ovni.param(savefile.levelProfile.fc_speed);
        }

        public void supp_dir(string filename)
        {
            DirectoryInfo dir = new DirectoryInfo(path + "\\SEU\\" + filename);
            if (dir.Exists)
                dir.Delete(true);
        }

    }
}
