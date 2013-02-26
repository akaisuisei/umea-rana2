using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Umea_rana
{
    public struct quaintuplet
    {
        public float X;
        public float Y;
        public int seconde;
        public int nombre;
        public int vie;
        public int speed;
        public Color color;
    }
    public struct couple
    {
        public float X;
        public float Y;
        public int vie;
        public int speed;
        public int seconde;
    }
    public struct savefile
    {
        public List<quaintuplet> ia_viseur;
        public List<quaintuplet> ia_tireur;
        public List<couple> ia_Kamikaze;
    }

    class Sauveguarde
    {

        string path;

        public Sauveguarde()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SavedGames\\GameMenu\\MyApplication\\Player1";
            Path.Combine(path, "SavedGames\\GameMenu\\MyApplication\\Player1");
        }

        public void save(string foldername, ref List<quaintuplet> ia, ref List<quaintuplet> ia_V, ref List<couple> ia_K)
        {


            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null;
            XmlSerializer f = null, g = null, i = null;
            string sav = path + "\\" + foldername;
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();
            if (dir.Exists)
            {

                file1 = new FileStream(dir.FullName + "\\ai_T" + ".xml", FileMode.Create, FileAccess.Write);
                f = new XmlSerializer(typeof(List<quaintuplet>));
                f.Serialize(file1, ia);
                file1.Close();
                file2 = new FileStream(dir.FullName + "\\ai_V" + ".xml", FileMode.Create, FileAccess.Write);
                g = new XmlSerializer(typeof(List<quaintuplet>));
                g.Serialize(file2, ia_V);
                file2.Close();
                file3 = new FileStream(dir.FullName + "\\ai_K" + ".xml", FileMode.Create, FileAccess.Write);
                i = new XmlSerializer(typeof(List<couple>));
                i.Serialize(file3, ia_K);
                file3.Close();
            }
        }

    }
}
