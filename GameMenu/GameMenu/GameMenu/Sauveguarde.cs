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

        public Sauveguarde()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SavedGames\\GameMenu\\MyApplication\\Player1";
            Path.Combine(path, "SavedGames\\GameMenu\\MyApplication\\Player1");
        }

        public void save(ref savefile save)
        {
            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null, file4 = null;
            XmlSerializer f = null, g = null, i = null, h = null;
            string sav = path + "\\" + "S_" + save.levelProfile.levelname;
            DirectoryInfo dir = new DirectoryInfo(sav);
            if (!dir.Exists)
                dir.Create();

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
        }

        public void load(ref string filename, ref savefile savefile)// S_ pour shoot em up a modif c est faux
        {
            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null;
            FileStream file4 = null;
            XmlSerializer f = null, g = null, i = null, j = null;

            DirectoryInfo dir = null;
            dir = new DirectoryInfo(path + "\\" + "S_" + filename);
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

        public string[] subdirectory(string type)
        {
            string g;
            string[] hello = System.IO.Directory.GetDirectories(path);
            for (int j = 0; j < hello.Length; ++j)
            {
                g = string.Empty;
                for (int i = 0; i < hello[j].Length; ++i)
                {
                    g += hello[j][i];
                    if (hello[j][i] == '\\' || (hello[j][i - 1] == type[0] && hello[j][i] == type[1]))
                        g = string.Empty;
                }
                hello[j] = g;
            }
            return hello;
        }

        public void load_level(ContentManager content, string level, ref IA_manager_K iamanage_K, ref IA_manager_T iamanage_T, ref IA_manager_V iamanage_V)
        {
            List<quaintuplet> ia, ia_V;
            List<couple> ia_K;
            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null;
            XmlSerializer f = null, g = null, i = null;

            DirectoryInfo dir = null;
            dir = new DirectoryInfo(content.RootDirectory + level);
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

                for (int j = 0; j < ia.Count; ++j)
                    iamanage_T.Add(ia[j].X, -0.08f, ia[j].seconde, ia[j].nombre, ia[j].color);
                for (int j = 0; j < ia_V.Count; ++j)
                    iamanage_V.Add(ia_V[j].X, -0.08f, ia_V[j].seconde, ia_V[j].nombre, ia_V[j].color);
                for (int j = 0; j < ia_V.Count; ++j)
                    iamanage_K.Add(ia_V[j].X, -0.08f, ia_V[j].seconde);

            }
        }
        public void load_leveleditor(string level, ref IA_manager_K iamanage_K, ref IA_manager_T iamanage_T, ref IA_manager_V iamanage_V)
        {
            List<quaintuplet> ia, ia_V;
            List<couple> ia_K;
            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null;
            XmlSerializer f = null, g = null, i = null;

            DirectoryInfo dir = null;
            dir = new DirectoryInfo(path + "\\" + level);
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

                for (int j = 0; j < ia.Count; ++j)
                    iamanage_T.Add(ia[j].X, -0.08f, ia[j].seconde, ia[j].nombre, ia[j].color);
                for (int j = 0; j < ia_V.Count; ++j)
                    iamanage_V.Add(ia_V[j].X, -0.08f, ia_V[j].seconde, ia_V[j].nombre, ia_V[j].color);
                for (int j = 0; j < ia_V.Count; ++j)
                    iamanage_K.Add(ia_V[j].X, -0.08f, ia_V[j].seconde);

            }

        }
    }
}
