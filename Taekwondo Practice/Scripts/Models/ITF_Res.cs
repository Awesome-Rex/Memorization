using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading;
using System.IO;

namespace ITF_Res
{
    #region "Global Values"
    public enum ITFLanguages { Korean, English }

    public interface INamed
    {
        public string name { get; set; }
    }

    public interface IRanked
    {
        public int minRank { get; set; }
    }

    public enum DifficultyLevel { Easy, Medium, Hard }
    #endregion


    public static class Material
    {

        #region "Values"
        public static Ranking.Belt[] Ranks;

        //training
        public static Pattern[] patterns = { };
        public static Excercise[] excercises = { };
        public static StepSparring[] stepSparring = { };
        #endregion

        #region "Types"
        public class Excercise : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public int averageCount;

            public float averageCountTime;

            //public Excercise (string name, int average)
        }

        public class Pattern : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public int moves;

            public string meaning;

            public Pattern(string name, int moves, string meaning, int minRank)
            {
                this.name = name;
                this.moves = moves;
                this.meaning = meaning;
                this.minRank = minRank;
            }
        }

        public class StepSparring : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public int steps;

            public int version;

            // (move information) public int moves;
        }
        #endregion
    }

    public static class TechniqueGenerator
    {
        public static MoveTemplate[] moveTemplates = {

        };

        public static Move[] Moves;

        #region "Ranked Class"
        /// <summary>
        /// White (0, 1), 
        /// Yellow (2, 3), 
        /// Green (4, 5), 
        /// Blue (6, 7), 
        /// Red (8, 9) >  
        /// Black (10, 11, 12, 13, 14)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minRank"></param>
        /// <returns></returns>
        public class Ranked : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public Ranked(string name, int minRank = 0)
            {
                this.name = name;
                this.minRank = minRank;
            }
        }
        #endregion

        #region "Types"
        public class MoveTemplate : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public object[] template;

            public List<string> combinations;

            public MoveTemplate(dynamic[] template, string name, int minRank)
            {
                this.name = name;
                this.minRank = minRank;

                this.template = template;

                this.combinations = new List<string>();
            }

            public void generateCombinations()
            {
                combinationCycle(new List<string>(), 0);
            }

            public void combinationCycle(List<string> newComb, int index)
            {
                List<string> newCombination = newComb;

                for (int i = index; i < template.Length; i++)
                {
                    object word = template[i];

                    if (word.GetType().IsArray)
                    {
                        foreach (Action possibility in (word as IEnumerable<Action>))
                        {
                            List<string> tempNewComb = newCombination;
                            tempNewComb.Add((word as INamed).name);

                            combinationCycle(tempNewComb, i + 1);
                        }
                        return;
                    }
                    else if (word.GetType() == typeof(Action))
                    {
                        newCombination.Add((word as INamed).name);
                    }
                }

                combinations.Add(newCombination.Aggregate((a, b) => a + " " + b));
            }
        }

        public class Move : IRanked
        {
            public int minRank { get; set; }

            public MoveTemplate template;

            public float tick;
        }

        public class Stance : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public StancePurpose purpose;
            public FaceBalance balance;

            public Stance (string name, StancePurpose purpose, FaceBalance balance, int minRank)
            {
                this.name = name;
                this.minRank = minRank;

                this.purpose = purpose;
                this.balance = balance;
            }
        }

        public class Action : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public ActionPurpose purpose;
            public ActionType type;

            public Action (string name, ActionType type, ActionPurpose purpose, int minRank)
            {
                this.name = name;
                this.minRank = minRank;

                this.purpose = purpose;
                this.type = type;
            }
        }

        #region "Value Types"

        //action / stance specific

        //move instance properties

        public enum SideFacing { Left, Right };

        //induvidual non string
        //  stance
        public enum StancePurpose { Stationary, Ready, Movement }
        public enum FaceBalance { Sided, Open };

        //  action
        public enum ActionPurpose { Attack, Defence };
        public enum ActionType { Punch, Block, Strike, Kick, Thrust };
        
        
        //induvidual action / stance modifiers / attributes
        public static Stance[] stances = { };

        public static IRanked[] PunchElevations = { r("low"/*, */), r("middle"), r("high") };
        public static IRanked[] BlockElevations = { r("low", 0), r("middle"), r("rising") };

        public static IRanked[] BlockFacings = { r("inner"), r("outer") };

        public static IRanked[] ActionBalance = { r(""), r("reverse") };

        #endregion

        #region "shortcuts"
        private static Ranked r(string name, int minRank = 0)
        {
            return new Ranked(name, minRank);
        }

        private static MoveTemplate m(dynamic[] template, string name, int minRank)
        {
            return new MoveTemplate(template, name, minRank);
        }

        private static Stance s (string name, StancePurpose purpose, FaceBalance balance, int minRank)
        {
            return new Stance(name, purpose, balance, minRank);
        }

        private static Action a(string name, ActionType type, ActionPurpose purpose, int minRank)
        {
            return new Action(name, type, purpose, minRank);
        }
        #endregion
        #endregion
    }

    public static class Terminology
    {
        #region "Dictionary"
        public static Dictionary<string, string> dictionary =
        new Dictionary<string, string> {
            //common
            {"teacher", "sabumnim"},
            {"instructor", "sabumnim"},

            {"yes", "ne"},
            {"no", "aniyo"},

            //command
            {"ready", "junbi"},
            {"bow", "kyong ye"},

            //stance
            {"stance", "sogi"},
            {"stance ", "so "},

                //stationary ready stance
            {"attention", "charyot"},
            {"parallel", "narani"},
            {"close", "moa"},
            {"gobooryo", "bending"}, //mid movement

                //naviagtion stance
            {"walking", "gunnon"},
            {"sitting", "annun"},   //open
            {"L-", "niunja"},
            {"gojung", "fixed"},

            //attack and defence

                   //movement types
            {"punch", "jirugi"},
            {"block", "makgi"},
            {"kick", "busigi"},
            {"strike", "taerigi"},
            {"straight fingertip thrust", "sun sonkut tulgi"},

                //blocks
            {"forearm", "palmok"},
            {"knifehand", "sonkal"},

            //positioning

                //vertical
            {"low", "najunde"},
            {"middle", "kuande"},
            {"high", "nopunde"},
            {"rising", "chukyo"},

                //horizontal
            {"side", "yop"},
            {"wedge", "hechyo"},
            {"inward", "anuro"},

                //other
            {"guarding", "daebi"},
            {"circular", "dollimyo"},
            {"twin", "sang"},

            //direction
            {"outer", "bakat"},
            {"inner", "an"},
            {"backfist", "dung joomuk"},


            //arms vs legs (balance)
            {"reverse", "bande"},
        };
        #endregion

        public static string Translate(string input, ITFLanguages from = ITFLanguages.English)
        {
            string output = input;

            if (from == ITFLanguages.English)
            {
                foreach (KeyValuePair<string, string> word in dictionary)
                {
                    output.Replace(word.Key, word.Value);
                }
            } else if (from == ITFLanguages.Korean)
            {
                foreach (KeyValuePair<string, string> word in dictionary)
                {
                    output.Replace(word.Value, word.Key);
                }
            }

            return output;
        }

        public static ITFLanguages checkKorean(string input)
        {
            foreach (KeyValuePair<string, string> word in dictionary)
            {
                if (word.Key != word.Value && input.Contains(word.Key))
                {
                    return ITFLanguages.Korean;
                }
            }

            return ITFLanguages.English;
        }

        public static Dictionary<string, string> beltMeanings = new Dictionary<string, string>
        {
            { "white" ,
                "White signifies innocence, as that of a beginning student who has no previous knowledge of Taekwon-Do." },
            { "yellow" ,
                "Yellow signifies the Earth from which a plant sprouts and takes root as the Taekwon-do foundation is being laid." },
            { "green" ,
                "Green signifies the plant’s growth as Taekwon-do skill begins to develop." },
            { "blue" ,
                "Blue signifies the Heaven, towards which the plant matures into a towering tree as training in Taekwon-do progresses." },
            { "red" ,
                "Red signifies danger, cautioning the student to exercise control and warning the opponent to stay away." },
            { "black" ,
                "Black is the opposite of white, therefore, signifying the maturity and proficiency in taekwon-do. It also indicates the wearer’s imperviousness to darkness and fear." }
        };

        public static Dictionary<string, string> bows = new Dictionary<string, string>
        {

        };
    }

    [Serializable]
    public abstract class Command
    {
        public abstract void Execute();
    }

    public static class Timer
    {
        public static void StartTicks (float tickTime, Action ETick)
        {
            throw new NotImplementedException();
        }
    }

    public static class Commands
    {
        public static void bow ()
        {

        }

        public static void ready ()
        {

        }

        public class Oath : Command
        {
            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public static class Training
        {
            //interface IVariety
            //{
            //    public int variety { get; set; }
            //}
            //interface INav
            //{
            //    public Action next { get; set; }
            //    public Action prev { get; set; }
            //}

            interface ITimed
            {
                public TimeSpan startTime { get; set; }
                public TimeSpan tick { get; set; }

                public TimeSpan currentTime { get; set; }

                public Action<TimeSpan> ETick { get; set; }
                public Action EEnd { get; set; }
            }

            public class WarmUp : Command
            {
                public int Variety = 2;

                public override void Execute()
                {
                    throw new NotImplementedException();
                }
            }

            public class Technique : Command
            {
                public int steps = 15;

                public override void Execute()
                {
                    throw new NotImplementedException();
                }
            }

            public class Patterns : Command
            {
                public int variety;

                public override void Execute()
                {
                    throw new NotImplementedException();
                }
            }

            public class Sparring : Command, ITimed
            {
                public TimeSpan startTime { get; set; }
                public TimeSpan tick { get; set; }
                public TimeSpan currentTime { get; set; }
                public Action<TimeSpan> ETick { get; set; }
                public Action EEnd { get; set; }

                public override void Execute()
                {
                    currentTime = startTime;
                    ETick(currentTime);

                    while (currentTime <= TimeSpan.Zero)
                    {
                        Thread.Sleep(tick);

                        currentTime.Subtract(tick);
                        ETick(currentTime);
                    }

                    EEnd();
                }
            }

            public class SelfDefence : Command, ITimed
            {
                public TimeSpan startTime { get; set; }
                public TimeSpan tick { get; set; }
                public TimeSpan currentTime { get; set; }
                public Action<TimeSpan> ETick { get; set; }
                public Action EEnd { get; set; }

                public override void Execute()
                {
                    currentTime = startTime;
                    ETick(currentTime);

                    while (currentTime <= TimeSpan.Zero)
                    {
                        Thread.Sleep(tick);

                        currentTime.Subtract(tick);
                        ETick(currentTime);
                    }

                    EEnd();
                }
            }

            public class StepSparring : Command
            {
                public int variety;

                public override void Execute()
                {
                    throw new NotImplementedException();
                }
            }

            public class Theory : Command
            {
                public int variety = 7;

                public Anki.Card currentCard;

                public bool revealed = false;


                public static Action<string> EFlip;
                public static Action ENext;

                public override void Execute()
                {
                    for (int i = 0; i < variety; i++)
                    {
                        revealed = false;
                    }
                }
            }
        }
    }

    public static class Anki
    {
        public class Card : IRanked
        {
            public string Front;
            public string Back;

            public int minRank
            {
                get; set;
            }

            public Card(string Front, string Back, int minRank)
            {
                this.Front = Front;
                this.Back = Back;

                this.minRank = minRank;
            }
        }

        public static string GetLine(string text, int lineNo)
        {
            string[] lines = text.Replace("\r", "").Split('\n');
            return lines.Length >= lineNo ? lines[lineNo - 1] : null;
        }
        public static int Lines(string text)
        {
            string[] lines = text.Replace("\r", "").Split('\n');
            return lines.Length;
        }

        public static Card SelectCard (FileInfo path)
        {
            StreamReader sr = File.OpenText(path.FullName);

            string full = sr.ReadToEnd();

            sr.Close();

            return GenerateCard(GetLine(full, new Random().Next(0, Lines(full))), 0);
        }

        public static Card GenerateCard (string contents, int minRank)
        {
            string[] formated = contents.Split("\t", StringSplitOptions.None);

            return new Card(formated[0], formated[1], minRank);
        }
    }

    namespace Ranking
    {
        [Serializable]
        public abstract class Belt : INamed, IRanked
        {
            public abstract string name { get; set; }
            public abstract int minRank { get; set; }

            public float[] colour;

            public int gup;
            public int dan;

            public Color Colourize(float[] code)
            {
                Color colour = new Color();
                colour.R = (byte)code[0];
                colour.G = (byte)code[1];
                colour.B = (byte)code[2];

                return colour;
            }
        }

        public class ColouredBelt : Belt, INamed, IRanked
        {
            public override string name { get; set; }
            public override int minRank { get; set; }

            public float[] stripe;

            public ColouredBelt(string name, float[] primary, float[] stripe, int gup, int minRank)
            {
                this.name = name;
                this.minRank = minRank;

                this.colour = primary;
                this.stripe = stripe;

                this.gup = gup;


                this.dan = 0;
            }
        }

        public class BlackBelt : Belt, INamed, IRanked
        {
            public override string name { get; set; }
            public override int minRank { get; set; }

            public int degrees;

            public BlackBelt(string name, int dan, int minRank)
            {
                this.name = name;
                this.minRank = minRank;

                this.colour = new float[] { 0f, 0f, 0f };

                this.dan = dan;


                this.gup = 0;
            }
        }
    }
}
