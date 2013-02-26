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
            DirectoryInfo dir = new DirectoryInfo(sav);
            if (!dir.Exists)
                dir.Create();
          
            

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

        public void load(string foldername, ref List<quaintuplet> ia, ref List<quaintuplet> ia_V, ref List<couple> ia_K)
        {
            FileStream file1 = null;
            FileStream file2 = null;
            FileStream file3 = null;
            XmlSerializer f = null, g = null, i = null;

            DirectoryInfo dir = null;
            dir = new DirectoryInfo(path + "\\" + foldername);
            if (dir.Exists)
            {
                
                
                    file1 = new FileStream(dir.FullName + "\\ai_T" + ".xml", FileMode.Open, FileAccess.Read );
                    f = new XmlSerializer(typeof(List<quaintuplet>));
                    ia = (List<quaintuplet>)f.Deserialize(file1);
                    file1.Close();
                    file2 = new FileStream(dir.FullName + "\\ai_V" + ".xml", FileMode.Open, FileAccess.Read );
                    g = new XmlSerializer(typeof(List<quaintuplet>));
                    ia_V = (List<quaintuplet>)g.Deserialize(file2);
                    file2.Close();
                    file3 = new FileStream(dir.FullName + "\\ai_K" + ".xml", FileMode.Open, FileAccess.Read );
                    i = new XmlSerializer(typeof(List<couple>));
                    ia_K = (List<couple>)i.Deserialize(file3);
                    file3.Close();
                

            }

            
        }

        public string[] subdirectory()
        {
            string g ;
            string [] hello=System.IO.Directory.GetDirectories(path);
            for (int j =0;j<hello.Length ;++j)
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
            return   hello ;
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
            dir = new DirectoryInfo(content.RootDirectory +level );
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
                   iamanage_T .Add(ia[j].X, ia[j].Y, ia[j].seconde, ia[j].nombre, ia[j].color);
                for (int j = 0; j < ia_V.Count; ++j)
                    iamanage_V.Add(ia_V[j].X,ia_V[j].Y, ia_V[j].seconde, ia_V[j].nombre, ia_V[j].color);
                for (int j = 0; j < ia_V.Count; ++j)
                    iamanage_K.Add(ia_V[j].X, ia_V[j].Y, ia_V[j].seconde);
    
            }

        }
    }
}
