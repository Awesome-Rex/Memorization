using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml.Serialization;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ITF_Res
{
    public interface IDefaultable
    {
        public void SetDefault();
    }

    #region "Save Types"
    public class Record
    {
        public string name;
        public int rating;

        public DateTime date;

        public DateTime startTime;
        public DateTime dateTime;
        public DateTime length;

        public Ranking.Belt rank;
        public DifficultyLevel difficulty;

        public Record ()
        {
            name = "Not Specified";
            rating = 0;
            
            //date = null

            //startTime = null;
            //dateTime = null;
            //length = null;

            rank = Material.Ranks[0];
            difficulty = DifficultyLevel.Medium;
        }
    }

    public class Preferences : IDefaultable
    {
        public void SetDefault()
        {

        }

        public Preferences ()
        {
            SetDefault();
        }
    }

    public class Settings : IDefaultable
    {
        public float noiseVolume;
        public float SFXVolume;

        public void SetDefault()
        {

        }

        public Settings()
        {
            SetDefault();
        }
    }

    public class Preset
    {
        public string name;

        public List<Command> SessionStructure = new List<Command>();

        public Preset (string name, List<Command> SessionStructure)
        {
            this.name = name;
            this.SessionStructure = SessionStructure;
        }
    }
    #endregion

    public static class Current 
    {
        public static Record record = new Record();
        public static Preferences preferences
        {
            get
            {
                return Data.preferences;
            }
            set
            {
                Data.preferences = value;
            }
        }
        public static Settings settings
        {
            get
            {
                return Data.settings;
            }
            set
            {
                Data.settings = value;
            }
        }

        public static List<Command> SessionStructure = new List<Command>();
        public static Command currentStrand;

        #region "Adding to saved"
        public static void addRecord()
        {
            Data.records.Add(record);
        }

        public static void addPreset(string name)
        {
            Data.presets.Add(new Preset(name, SessionStructure));
        }
        #endregion

        public static void ExecuteSession()
        {
            foreach (Command strand in SessionStructure)
            {
                strand.Execute();
            }

            EndSession();
        }

        public static void EndSession ()
        {

        }
    }

    public static class Data
    {
        public static List<Record> records = new List<Record>();
        public static Preferences preferences = new Preferences();
        public static Settings settings = new Settings();
        public static List<Preset> presets = new List<Preset>();

        public static void SET()
        {
            records = new List<Record>();
            preferences = new Preferences();
            settings = new Settings();
            presets = new List<Preset>();
        }

        #region "SaveFiles"
        public static void Save ()
        {
            DirectoryInfo location = new DirectoryInfo(".");
            DirectoryInfo folder = new DirectoryInfo(@"C:\REX_SaveFiles\YongGiPracticeDojang");

            settings = Current.settings;
            preferences = Current.preferences;

            /// save each to file
        }

        public static void Load (string directory = @"/\/\")
        {
            if (true) // directory doesnt exist
            {
                SET();
            }
            else if (true) { // directory does exist
                //load from files
                //set preferences (on current)
                //set presets
                //set settings (on current)
                //set records
            }
        }
        #endregion
    }
}
