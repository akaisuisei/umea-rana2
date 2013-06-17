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
        public int damage;
        public int firerate;
        public int bullet_Speed;
        public string trajectory;
    }
    public struct couple
    {
        public float X;
        public float Y;
        public int vie;
        public int speed;
        public int seconde;
        public int damage;
    }
    public struct levelProfile
    {
        public string levelname;
        public string next_level;
        public string background_name;
        public int fc_speed;
        public string second_background;
        public string third_bacground;
        public int playerLife;
        public int player_speed;
        public int firerate;
        public Color color;
        public int damage;
        public int bullet_speed;
        public string[] musique;
        public string IAcolor;
        public bool image_sprite { get; set; }//vrai= allem, faux = yoh
        public housesafe house { get; set; }
        public bossP bossPlatforme { get; set; }
    }
    public struct Bonus
    {
        public char type;
        public int speed,angle,launch;
        public float X, Y;
    }

    public struct IA_AA
    {
        public float X;
        public float Y;
        public int Vie;
        public int Puissance;
        public int Speed;
    }

    public struct IA_AR
    {
        public float X;
        public float Y;
        public int Vie;
        public int Puissance;
        public int Speed;
    }

    public struct IA_S
    {
        public float X;
        public float Y;
        public int Vie;
        public int Puissance;
        public int Speed;
    }

    public struct Plat
    {
        public float X;
        public float Y;
        public int nbr, speed;
        public float distance;
        public char type;
        public string name { get; set; }

    }

    public struct savefile
    {
        public List<IA_AA> ia_AA;
        public List<IA_AR> ia_AR;
        public List<IA_S> ia_S;
        public List<Plat> plat_f;
        public List<quaintuplet> ia_viseur;
        public List<quaintuplet> ia_tireur;
        public List<couple> ia_Kamikaze;
        public List<Bonus> bonus;
        public levelProfile levelProfile;

        public Boss_setting bossSEU { get; set; }

    }
    public struct PlayerProfile
    {
        int level;
        int life;
        string playername;

        public int _level
        {
            get { return level; }
            set { if (_level > level)level = _level; }
        }
        public int _life
        {
            get { return life; }
            set { if (_life > 0) life = _life; }
        }
        public string _playername
        {
            get { return playername; }
            set { if (_playername != string.Empty) playername = _playername; }
        }
    }
    public struct housesafe
    {
       public float X { get; set; }
       public float Y { get; set; }
    }
    public struct OptionProfile
    {
    }
    public struct unlocked
    {
        public string level { get; set; }
        public bool locked { get; set; }
        public unlocked (string level, bool locked):this()
        {
            this.locked = locked;
            this.level = level;
        }
    }
    public class unlocklevel
    {
        unlocked[] lockedlevel;
        public unlocklevel()
        {
            lockedlevel = new unlocked[12] ; 
        }
        public void load()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SavedGames\\GameMenu\\MyApplication\\Player1";

            FileStream file1 = null;
            XmlSerializer f = null;
            DirectoryInfo dir = new DirectoryInfo(path );
            if (!dir.Exists)
                dir.Create();
            else
            {
                if (File.Exists(dir.FullName + "\\block.carambar"))
                {
                    file1 = new FileStream(dir.FullName + "\\block.carambar", FileMode.Open, FileAccess.Read);

                    f = new XmlSerializer(typeof(unlocked[]));
                    lockedlevel = (unlocked[])f.Deserialize(file1);
                    file1.Close();
                }
                if (lockedlevel == null)
                    lockedlevel = new unlocked[12];// a finir avec les nom des niveau
            }
        }
        public void endlevel(string level)
        {
            load();
            for (int i = 0; i < lockedlevel.Length - 2; i++)
            {
                if (lockedlevel[i].level == level)
                    lockedlevel[i + 1].locked = true;
            }
            save();
        }
        public List<string> unlocklevellist()
        {
            List<string > res= new List<string> (){""};
            load();
            foreach (unlocked u in lockedlevel)
                if (u.locked)
                    res.Add(u.level);
            return res;
        }
        public void save()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SavedGames\\GameMenu\\MyApplication\\Player1";

            FileStream file1 = null;
            XmlSerializer f = null;
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();
            else
            {
                file1 = new FileStream(dir.FullName + "\\block.carambar", FileMode.Create, FileAccess.Write);
                f = new XmlSerializer(typeof(unlocked[]));
                f.Serialize(file1, this.lockedlevel );
                file1.Close();
            }
        }
    }
        
    public struct  scoring 
        {
           public  string level{ get; set ;}
            public int high{ get; set;}
            public scoring (string level, int high): this()
            {
                this.level= level;
                this.high = high;
            }

        }
    public class Highscore
    {

        List<scoring > highScore;
       
        public Highscore()
        {
           highScore= new List<scoring>();
        }
        public void load()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SavedGames\\GameMenu\\MyApplication\\Player1";
          
            FileStream file1 = null;
            XmlSerializer f = null;
            DirectoryInfo dir = new DirectoryInfo(path );
            if (!dir.Exists)
                dir.Create();
            else
            {
                if (File.Exists(dir.FullName + "\\high.carambar"))
                {
                    file1 = new FileStream(dir.FullName + "\\high.carambar", FileMode.Open , FileAccess.Read);
                    f = new XmlSerializer(typeof(List<scoring>));
                    highScore= (List<scoring>)f.Deserialize(file1);
                    file1.Close();
                }
                if (highScore == null)
                    highScore = new List<scoring> ();// a finir avec les nom des niveau
            }
        }
        public int endlevel(string level, int j1, int j2)
        {
          int i=0;
            load();
            while (i<highScore.Count )
            {
                if(highScore[i].level==level )
                    break;
                i++;
            }
           if(i ==highScore.Count )
               highScore.Add (new scoring (level ,Math.Max (j1,j2)));
           else
               highScore[i]= new scoring (level ,Math.Max (j1,Math.Max(j2,highScore[i].high ))); 
            save();
            return highScore[i].high ;
        }
        public void save()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SavedGames\\GameMenu\\MyApplication\\Player1";

            FileStream file1 = null;
            XmlSerializer f = null;
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();
            else
            {
                file1 = new FileStream(dir.FullName + "\\high.carambar", FileMode.Create, FileAccess.Write);
                f = new XmlSerializer(typeof(List<scoring>));
                f.Serialize(file1, highScore );
                file1.Close();
            }
        }
    
    }
}
