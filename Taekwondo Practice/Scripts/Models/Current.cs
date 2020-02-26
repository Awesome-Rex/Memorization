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
    [Serializable]
    public class Record : ISerializable
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class Preferences : IDefaultable, ISerializable
    {
        public void SetDefault()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public Preferences ()
        {
            SetDefault();
        }
    }

    [Serializable]
    public class Settings : IDefaultable, ISerializable
    {
        public float noiseVolume;
        public float SFXVolume;

        public void SetDefault()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public Settings()
        {
            SetDefault();
        }
    }

    [Serializable]
    public class TrainingPreset : ISerializable
    {
        public string name;

        public List<Command> SessionStructure = new List<Command>();

        public TrainingPreset (string name, List<Command> SessionStructure)
        {
            this.name = name;
            this.SessionStructure = SessionStructure;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
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
            Data.presets.Add(new TrainingPreset(name, SessionStructure));
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
        public enum SerializeMethod { Byte, XML };

        public static List<Record> records;
        public static Preferences preferences;
        public static Settings settings;
        public static List<TrainingPreset> presets;

        public static List<Saved<List<Record>>> saves = new List<Saved<List<Record>>>
        {
            new Saved<List<Record>>(() => records, @"C:\REX_SaveFiles\YongGiPracticeDojang\Records.save", SerializeMethod.Byte, n => records = n)
        };

        public class Saved <T>
        {
            public SerializeMethod method;

            public Func<T> getVal;
            public Action<T> setVal;

            public string path;

            public Saved (Func<T> getVal, string path, SerializeMethod method, Action<T> setVal)
            {
                this.getVal = getVal;
                this.setVal = setVal;

                this.path = path;
                this.method = method;
            }

            public void Save ()
            {
                if (method == SerializeMethod.Byte)
                {
                    Stream stream = File.Open(path, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, getVal());
                    stream.Close();
                } else if (method == SerializeMethod.XML)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        serializer.Serialize(tw, getVal());
                    }
                }
            }

            public void Load ()
            {
                if (method == SerializeMethod.Byte)
                {
                    Stream stream = File.Open(path, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();

                    setVal((T)bf.Deserialize(stream));
                    stream.Close();

                }
                else if (method == SerializeMethod.XML)
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(T));
                    TextReader reader = new StreamReader(path);
                    //object obj = deserializer.Deserialize(reader);
                    setVal((T)deserializer.Deserialize(reader));
                    reader.Close();
                }
            }
        }

        public static void SET()
        {
            System.Diagnostics.Debug.WriteLine("Welcome Message!");


            // creates files, directories
            if (!Directory.Exists(@"C:\REX_Saves"))
            {
                DirectoryInfo dir = new DirectoryInfo(@"C:\REX_Saves");
                dir.Create();
            }
            if (!Directory.Exists(@"C:\REX_Saves\YongGiPracticeDojang"))
            {
                DirectoryInfo dir = new DirectoryInfo(@"C:\REX_Saves\YongGiPracticeDojang");
                dir.Create();
            }

            Stream IOsettings = File.Open(@"C:\REX_SaveFiles\YongGiPracticeDojang\Settings.xaml", FileMode.Create);
            Stream IOpreferences = File.Open(@"C:\REX_SaveFiles\YongGiPracticeDojang\Preferences.xaml", FileMode.Create);
            Stream IOrecords = File.Open(@"C:\REX_SaveFiles\YongGiPracticeDojang\Records.save", FileMode.Create);
            Stream IOpresets = File.Open(@"C:\REX_SaveFiles\YongGiPracticeDojang\TrainingPresets.xaml", FileMode.Create);

            //defaults
            records = new List<Record>();
            preferences = new Preferences();
            settings = new Settings();
            presets = new List<TrainingPreset>();
        }

        #region "SaveFiles"
        public static void Save ()
        {
            //saves to files

            XmlSerializer serializer;
            ///
            serializer = new XmlSerializer(typeof(Settings));
            using (TextWriter tw = new StreamWriter(@"C:\REX_Saves\YongGiPracticeDojang\Settings.xml"))
            {
                serializer.Serialize(tw, settings);
            }
            ///
            serializer = new XmlSerializer(typeof(Preferences));
            using (TextWriter tw = new StreamWriter(@"C:\REX_Saves\YongGiPracticeDojang\Preferences.xml"))
            {
                serializer.Serialize(tw, preferences);
            }
            ///
            serializer = new XmlSerializer(typeof(TrainingPreset));
            using (TextWriter tw = new StreamWriter(@"C:\REX_Saves\YongGiPracticeDojang\TrainingPresets.xml"))
            {
                serializer.Serialize(tw, presets);
            }
            ///
            Stream stream = File.Open(@"C:\REX_SaveFiles\YongGiPracticeDojang\Records.save", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, records);
            stream.Close();
        }

        public static void Load ()
        {
            if (!Directory.Exists(@"C:\REX_Saves\YongGiPracticeDojang") || !Directory.Exists(@"C:\REX_Saves")) // all files don't exist
            {
                //welcome message
                SET();
            }
            else { // direcrory does exist
                //load from files

                if (File.Exists(@"C:\REX_Saves\YongGiPracticeDojang\Settings.xaml"))
                {
                    //also get files!!!!!!!
                } else
                {
                    //create new file
                    File.Create(@"C:\REX_SaveFiles\YongGiPracticeDojang\Settings.xaml");
                    settings = new Settings();
                }
                if (File.Exists(@"C:\REX_Saves\YongGiPracticeDojang\Preferences.xaml"))
                {
                    
                } else
                {
                    File.Create(@"C:\REX_SaveFiles\YongGiPracticeDojang\Preferences.xaml");
                    preferences = new Preferences();
                }
                if (File.Exists(@"C:\REX_Saves\YongGiPracticeDojang\TrainingPresets.xaml"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<TrainingPreset>));
                    using (FileStream reader = File.OpenRead(@"C:\REX_Saves\YongGiPracticeDojang\TrainingPresets.xaml"))
                    {
                        presets = (List<TrainingPreset>)(deserializer.Deserialize(reader));
                        reader.Close();
                    }
                } else
                {
                    File.Create(@"C:\REX_SaveFiles\YongGiPracticeDojang\TrainingPresets.xaml");
                    presets = new List<TrainingPreset>();
                }
                if (File.Exists(@"C:\REX_Saves\YongGiPracticeDojang\Records.save"))
                {
                    Stream stream = File.Open(@"C:\REX_SaveFiles\YongGiPracticeDojang\Records.save", FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();

                    records = (List<Record>)bf.Deserialize(stream);
                    stream.Close();
                } else
                {
                    File.Create(@"C:\REX_SaveFiles\YongGiPracticeDojang\Records.save");
                    records = new List<Record>();
                }
            }
        }
        #endregion
    }
}
