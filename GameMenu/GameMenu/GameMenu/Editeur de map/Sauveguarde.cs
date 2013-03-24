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
            FileStream file2 = null;
            FileStream file3 = null, file4 = null;
            XmlSerializer f = null, g = null, i = null, h = null;
            string sav = path + "\\SEU\\" + save.levelProfile.levelname;
            DirectoryInfo dir = new DirectoryInfo(sav);
            string ext = "", nam = "" ;
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
            if (nam != save.levelProfile.background_name)
            {
                System.IO.File.Copy(save.levelProfile.background_name, sav + "\\background" + ext, true);
                save.levelProfile.background_name = "background" + ext;
            }
            file1 = new FileStream(dir.FullName + "\\ai_T" + ".xml", FileMode.Create, FileAccess.Write);
            f = new XmlSerializer(typeof(List<quaintuplet>));
            f.Serialize(file1, save.ia_tireur);
            file1.Close();
            file2 = new FileStream(dir.FullName + "\\ai_V" + ".xml", FileMode.Create, FileAccess.Write);
            g = new XmlSerializer(typeof(List<quaintuplet>));
            g.Serialize(file2, save.ia_viseur);
            file2.Close();
            file3 = new FileStream(dir.FullName + "\\ai_K" + ".xml", FileMode.Create, FileAccess.Write);
            i = new XmlSerializer(typeof(List<couple>));
            i.Serialize(file3, save.ia_Kamikaze);
            file3.Close();
            file4 = new FileStream(dir.FullName + "\\level_profile" + ".xml", FileMode.Create, FileAccess.Write);
            h = new XmlSerializer(typeof(levelProfile));
            h.Serialize(file4, save.levelProfile);
            file4.Close();

            // 

        }
        /// <summary>
        /// SEU pour shhot em up a utilier ds l editeur de map non fini manque le recuperation du fond
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="savefile"></param>
        public void load_SEU(ref string filename, ref savefile savefile)
        {
            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null;
            FileStream file4 = null;
            XmlSerializer f = null, g = null, i = null, j = null;

            DirectoryInfo dir = null;
            dir = new DirectoryInfo(path + "\\SEU\\" + filename);
            if (dir.Exists)
            {
                file1 = new FileStream(dir.FullName + "\\ai_T" + ".xml", FileMode.Open, FileAccess.Read);
                f = new XmlSerializer(typeof(List<quaintuplet>));
                savefile.ia_tireur = (List<quaintuplet>)f.Deserialize(file1);
                file1.Close();
                file2 = new FileStream(dir.FullName + "\\ai_V" + ".xml", FileMode.Open, FileAccess.Read);
                g = new XmlSerializer(typeof(List<quaintuplet>));
                savefile.ia_viseur = (List<quaintuplet>)g.Deserialize(file2);
                file2.Close();
                file3 = new FileStream(dir.FullName + "\\ai_K" + ".xml", FileMode.Open, FileAccess.Read);
                i = new XmlSerializer(typeof(List<couple>));
                savefile.ia_Kamikaze = (List<couple>)i.Deserialize(file3);
                file3.Close();
                file4 = file2 = new FileStream(dir.FullName + "\\level_profile" + ".xml", FileMode.Open, FileAccess.Read);
                j = new XmlSerializer(typeof(levelProfile));
                savefile.levelProfile = (levelProfile)j.Deserialize(file4);
                file4.Close();
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

        public string[] filename(ContentManager content)
        {
            string g;
            if (!Directory.Exists(Path.GetDirectoryName(  content.RootDirectory ) + "\\back" ))
                Directory.CreateDirectory(content.RootDirectory  + "\\back"  );
            string[] hello =System.IO.Directory.GetFiles (content.RootDirectory + "\\back");
            for (int j = 0; j < hello.Length; ++j)
            {
                g = string.Empty;
                for (int i = 0; i < hello[j].Length&&hello[j][i]!='.' ; ++i)
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
        /// non fini manque le fond pour la phase de jeu normal
        /// </summary>
        /// <param name="content"></param>
        /// <param name="level">nom du nivau</param>
        /// <param name="iamanage_K"></param>
        /// <param name="iamanage_T"></param>
        /// <param name="iamanage_V"></param>
        public void load_level_SEU(ContentManager content, string level, ref IA_manager_K iamanage_K, 
            ref IA_manager_T iamanage_T, ref IA_manager_V iamanage_V,ref Scrolling_ManagerV scrollM)
        {
            List<quaintuplet> ia, ia_V;
            List<couple> ia_K;
            levelProfile levelprof;
            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null;
            FileStream file4 = null;
            XmlSerializer f = null, g = null, i = null,e=null;


            DirectoryInfo dir = null;
            dir = new DirectoryInfo(content.RootDirectory  +"\\"+ level);
            if (dir.Exists)
            {
                ia = new List<quaintuplet>();
                ia_K = new List<couple>();
                ia_V = new List<quaintuplet>();
                file1 = new FileStream(dir.FullName + "\\ai_T" + ".xml", FileMode.Open, FileAccess.Read);
                f = new XmlSerializer(typeof(List<quaintuplet>));
                ia = (List<quaintuplet>)f.Deserialize(file1);
                file1.Close();
                file2 = new FileStream(dir.FullName + "\\ai_V" + ".xml", FileMode.Open, FileAccess.Read);
                g = new XmlSerializer(typeof(List<quaintuplet>));
                ia_V = (List<quaintuplet>)g.Deserialize(file2);
                file2.Close();
                file3 = new FileStream(dir.FullName + "\\ai_K" + ".xml", FileMode.Open, FileAccess.Read);
                i = new XmlSerializer(typeof(List<couple>));
                ia_K = (List<couple>)i.Deserialize(file3);
                file3.Close();
                file4 = new FileStream(dir.FullName + "\\level_profile" + ".xml", FileMode.Open, FileAccess.Read);
                e = new XmlSerializer(typeof(levelProfile));
                levelprof = (levelProfile)e.Deserialize(file4);
                file4.Close();

                for (int j = 0; j < ia.Count; ++j)
                    iamanage_T.Add(ia[j]);
                for (int j = 0; j < ia_V.Count; ++j)
                    iamanage_V.Add(ia_V[j]);
                for (int j = 0; j < ia_K.Count; ++j)
                    iamanage_K.Add(ia_K[j]);

                scrollM.Load(content, levelprof);

            }
        }
        /// <summary>
        /// non fini manque le fond pour la phase de jeu level perso
        /// </summary>
        /// <param name="level"></param>
        /// <param name="iamanage_K"></param>
        /// <param name="iamanage_T"></param>
        /// <param name="iamanage_V"></param>
        public void load_leveleditor_SEU(ContentManager content, string level, ref IA_manager_K iamanage_K,
            ref IA_manager_T iamanage_T, ref IA_manager_V iamanage_V, ref Scrolling_ManagerV scrollM)
        {
            List<quaintuplet> ia, ia_V;
            List<couple> ia_K;
            levelProfile level_profile;
            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null;
            FileStream file4 = null;
            XmlSerializer f = null, g = null, i = null, h = null;

            DirectoryInfo dir = null;
            dir = new DirectoryInfo(path + "\\SEU\\" + level);
            if (dir.Exists)
            {
                ia = new List<quaintuplet>();
                ia_K = new List<couple>();
                ia_V = new List<quaintuplet>();
                file1 = new FileStream(dir.FullName + "\\ai_T" + ".xml", FileMode.Open, FileAccess.Read);
                f = new XmlSerializer(typeof(List<quaintuplet>));
                ia = (List<quaintuplet>)f.Deserialize(file1);
                file1.Close();
                file2 = new FileStream(dir.FullName + "\\ai_V" + ".xml", FileMode.Open, FileAccess.Read);
                g = new XmlSerializer(typeof(List<quaintuplet>));
                ia_V = (List<quaintuplet>)g.Deserialize(file2);
                file2.Close();
                file3 = new FileStream(dir.FullName + "\\ai_K" + ".xml", FileMode.Open, FileAccess.Read);
                i = new XmlSerializer(typeof(List<couple>));
                ia_K = (List<couple>)i.Deserialize(file3);
                file3.Close();
                file4 = new FileStream(dir.FullName + "\\level_profile" + ".xml", FileMode.Open, FileAccess.Read);
                h = new XmlSerializer(typeof(levelProfile));
                level_profile = (levelProfile)h.Deserialize(file4);
                file4.Close();

                for (int j = 0; j < ia.Count; ++j)
                    iamanage_T.Add(ia[j]);
                for (int j = 0; j < ia_V.Count; ++j)
                    iamanage_V.Add(ia_V[j]);
                for (int j = 0; j < ia_K.Count; ++j)
                    iamanage_K.Add(ia_K[j]);

                scrollM.Load(content, level_profile);
            }
        }

        public void supp_dir(string filename)
        {
            DirectoryInfo dir = new DirectoryInfo(path + "\\SEU\\" + filename);

            if (dir.Exists)
                dir.Delete(true);

        }

    }
}
