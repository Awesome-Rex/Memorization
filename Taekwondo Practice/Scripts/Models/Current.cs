using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml.Serialization;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Xml;
using Newtonsoft.Json;

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

        public Record(SerializationInfo info, StreamingContext ctxt)
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

        public Preferences(SerializationInfo info, StreamingContext ctxt)
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

        public Settings(SerializationInfo info, StreamingContext ctxt)
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

        public TrainingPreset(SerializationInfo info, StreamingContext ctxt)
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
                return Data.preferences.value;
            }
            set
            {
                Data.preferences.value = value;
            }
        }
        public static Settings settings
        {
            get
            {
                return Data.settings.value;
            }
            set
            {
                Data.settings.value = value;
            }
        }

        public static List<Command> SessionStructure = new List<Command>();
        public static Command currentStrand;

        #region "Adding to saved"
        public static void addRecord()
        {
            Data.records.value.Add(record);
        }

        public static void addPreset(string name)
        {
            Data.presets.value.Add(new TrainingPreset(name, SessionStructure));
        }
        #endregion

        public static void ExecuteSession()
        {
            foreach (Command strand in SessionStructure)
            {
                Thread f = new Thread(strand.Execute);
                f.Start();
            }

            EndSession();
        }

        public static void EndSession ()
        {
            //add record
            
        }
    }

    public static class Data
    {
        public enum SerializeMethod { Byte, XML, JSON };

        public static Saved<List<Record>> records = new Saved<List<Record>>(@"C:\REX_Saves\YongGiPracticeDojang\Records.save", SerializeMethod.Byte);
        public static Saved<Preferences> preferences = new Saved<Preferences>(@"C:\REX_Saves\YongGiPracticeDojang\Preferences.json", SerializeMethod.JSON);
        public static Saved<Settings> settings = new Saved<Settings>(@"C:\REX_Saves\YongGiPracticeDojang\Settings.json", SerializeMethod.JSON);
        public static Saved<List<TrainingPreset>> presets = new Saved<List<TrainingPreset>>(@"C:\REX_Saves\YongGiPracticeDojang\TrainingPresets.json", SerializeMethod.JSON);

        public class Saved <T>
        {
            public T value;
            public SerializeMethod method;
            public string path;

            public Saved (string path, SerializeMethod method = SerializeMethod.Byte)
            {
                this.path = path;
                this.method = method;
            }

            public void Create ()
            {
                File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                if (method == SerializeMethod.XML)
                {
                    XmlDocument doc = new XmlDocument();
                    //doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>"); //Your string here

                    // Save the document to a file and auto-indent the output.
                    using (XmlWriter writer = XmlWriter.Create(File.Open(path, FileMode.Open, FileAccess.Write, FileShare.ReadWrite)))
                    {
                        /*writer.WriteStartElement("Root", null);
                        writer.WriteEndElement();*/
                        //writer.Close();
                    }
                }
            }

            public void Save ()
            {
                if (method == SerializeMethod.Byte)
                {
                    Stream stream = File.Open(path, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, value);
                    stream.Close();
                } else if (method == SerializeMethod.XML)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        serializer.Serialize(tw, value);
                    }
                } else if (method == SerializeMethod.JSON)
                {
                    //File.WriteAllText(path, JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented));

                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        writer.Write(JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented));
                    }
                }
            }

            public void Load ()
            {
                if (method == SerializeMethod.Byte)
                {
                    Stream stream = File.Open(path, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();

                    value = (T)bf.Deserialize(stream);
                    stream.Close();

                }
                else if (method == SerializeMethod.XML)
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(T));
                    TextReader reader = new StreamReader(path);
                    //object obj = deserializer.Deserialize(reader);
                    value = (T)deserializer.Deserialize(reader);
                    reader.Close();
                } else if (method == SerializeMethod.JSON)
                {
                    //value = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

                    using (StreamReader reader = new StreamReader(path))
                    {
                        value = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                    }
                }
            }

            public bool Exists ()
            {
                return File.Exists(path);
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

            records.Create();
            preferences.Create();
            settings.Create();
            presets.Create();

            //defaults
            records.value = new List<Record>();
            preferences.value = new Preferences();
            settings.value = new Settings();
            presets.value = new List<TrainingPreset>();
        }

        #region "SaveFiles"
        public static void Save ()
        {
            //saves to files
            records.Save();
            preferences.Save();
            settings.Save();
            presets.Save();
        }

        public static void Load ()
        {
            if (!Directory.Exists(@"C:\REX_Saves\YongGiPracticeDojang") || !Directory.Exists(@"C:\REX_Saves")) // all files don't exist
            {
                //welcome message
                SET();
            } 
            else 
            { 
                // direcrory does exist
                //load from files

                if (settings.Exists())
                {
                    settings.Load();
                } else
                {
                    settings.Create();
                    settings.value = new Settings();
                }
                if (preferences.Exists())
                {
                    preferences.Load();
                } else
                {
                    preferences.Create();
                    preferences.value = new Preferences();
                }
                if (presets.Exists())
                {
                    presets.Load();
                } else
                {
                    presets.Create();
                    presets.value = new List<TrainingPreset>();
                }
                if (records.Exists())
                {
                    records.Load();
                } else
                {
                    records.Create();
                    records.value = new List<Record>();
                }
            }
        }
        #endregion
    }
}
