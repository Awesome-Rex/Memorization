using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ITF_Material
{
    public enum ITFLanguages { Korean, English }

    public static class Res
    {
        //extra
        public static Ranking.Belt[] Ranks;

        //divisions
        public static A.Pattern[] patterns = { };

        public static A.Excercise[] excercises = { };

        public static A.StepSparring[] stepSparring = { };


        //translation
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


        //action / stance specific

        //move properties
        public enum FaceBalance { Sided, Open };
        public enum SideFacing { Left, Right };
        public enum ActionType { Attack, Defence };

        //induvidual non string
        public enum StancePurpose { Stationary, Ready, Movement }

        //induvidual action / stance modifiers
        public static string[] PunchElevations = { "low", "middle", "high" };
        public static string[] BlockElevations = { "low", "middle", "rising" };

        public static string[] BlockFacings = { "inner", "outer" };

        public static string[] ActionBalance = { "", "reverse" };


    }

    namespace A //Actions
    {
        /*public class Stance : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public string side;

            public string prefix;
            public string version;

            public bool stationary;

            public Stance ()
            {

            }
        }

        public class Action : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public string side;
            public Res.ActionType type;

            public Stance[] incompatible;

            public Action ()
            {

            }
        }

        public class Move : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public Stance stance;
            public Res.SideFacing stanceFacing;

            public Action action;
            public Res.SideFacing actionFacing;

            public bool reverse;

            public Move(string name, int minRank)
            {
                this.name = name;
                this.minRank = minRank;
            }

            public string[] generateCombinations();
        }*/

        public class Move : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }

            public object[] template;

            public List<string> combinations;

            public Move (dynamic[] template, string name, int minRank)
            {
                this.name = name;
                this.minRank = minRank;

                this.template = template;

                this.combinations = new List<string>();
            }

            public void generateCombinations ()
            {
                combinationCycle(new List<string>(), 0);
            }

            public void combinationCycle (List<string> newComb, int index)
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
                    else if (word is INamed && word is IRanked)
                    {
                        newCombination.Add((word as INamed).name);
                    }
                }

                combinations.Add(newCombination.Aggregate((a, b) => a + " " + b));
            }
        }

        public class Action : INamed, IRanked
        {
            public string name { get; set; }
            public int minRank { get; set; }


        }

        public class Excercise : INamed
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
    }

    public static class S //Shortcuts
    {
        private static A.Move m(string name, int minRank)
        {
            return new A.Move(name, minRank);
        }
        private static A.Excercise e()
        {
            throw new NotImplementedException();
        }
        private static A.Pattern p()
        {
            throw new NotImplementedException();
        }
    }

    public interface INamed
    {
        public string name { get; set; }
    }

    public interface IRanked
    {
        public int minRank { get; set; }
    }

    public class Ranked : IRanked
    {
        public int minRank { }
    }

    public static class TechniqueGenerator
    {
        /*public static string str (dynamic val)
        {
            string text;
            Terminology.actionToText.TryGetValue(val, out text);
            return text;
        }*/
    }

    public static class Terminology
    {
        public static Dictionary<string, string> dictionary =
        new Dictionary<string, string> {
            #region "Dictionary"

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

        public static Dictionary<dynamic, string> actionToText = new Dictionary<dynamic, string>
        {
            /*{ BlockFacing.Inner, "inner" },
            { BlockFacing.Outer, "outer"}*/
        };

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
    }

    namespace Divisions
    {
        public abstract class Division
        {
            public abstract void Execute();
        }

        public class Oath : Division
        {
            

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public class WarmUp : Division
        {
            public int Variety = 2;

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public class Technique : Division
        {
            public int steps = 15;

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public class Patterns : Division
        {
            public int variety;

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public class Sparring : Division
        {
            public float time;

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public class SelfDefence : Division
        {
            public float time;

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public class Sparring3Step : Division
        {
            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

        public class Theory : Division
        {
            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }
    }

    namespace Ranking
    {
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
