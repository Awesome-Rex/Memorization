using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ITF_Material
{
    public static class Moves
    {
        public static string[] stances;

        public static string[] punchElevations = { "najunde", "kuande", "nopunde" };
        public static string[] blockElevations = { "najunde", "kuande", "chukyo" };

        public static string[] blockFacings = { "inner", "outer" };

        public static string generateOpenPunch(int rank)
        {

        }

        public static string generateSidedPunch(int rank)
        {

        }

        public static string generateOpenBlock(int rank)
        {

        }

        public static string generateSidedBlock(int rank)
        {

        }

        public static string generateKick(int rank)
        {

        }
    }

    public static class Terminology
    {
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

                //naviagtion stance
            {"walking", "gunnon"},
            {"sitting", "annun"},   //open
            {"L-", "niunja"},
            {"gojung", "fixed"},
                //mid movement
            {"gobooryo", "bending"},

            //attack and defence

                   //movement types
            {"punch", "jirugi"},
            {"block", "makgi"},
            {"kick", "busigi"},
            {"strike", "taerigi"},
            {"straigt fingertip thrust", "sun sonkut tulgi"},

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
            {"backfist", "joomuk"},

            //arms vs legs
            {"reverse", "bande"},
        };

        public enum ITFLanguages { Korean, English }

        public static string translate(string input, ITFLanguages from = ITFLanguages.English)
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
            
        };

        public static Dictionary<string, string> bows = new Dictionary<string, string>
        {

        };
    }

    namespace Ranking
    {

        public class Belt
        {
            public static Belt[] Ranks;

            public string name;

            public float[] colour;

            public int gup;
            public int dan;

            Color colourize(float[] code)
            {
                Color colour = new Color();
                colour.R = (byte)code[0];
                colour.G = (byte)code[1];
                colour.B = (byte)code[2];

                return colour;
            }
        }

        public class ColouredBelt : Belt
        {
            public float[] stripe;

            public ColouredBelt(string name, float[] primary, float[] stripe, int gup)
            {
                this.name = name;

                this.colour = primary;
                this.stripe = stripe;

                this.gup = gup;


                this.dan = 0;
            }
        }

        public class BlackBelt : Belt
        {
            public int degrees;

            public BlackBelt(string name, int dan)
            {
                this.name = name;

                this.colour = new float[] { 0f, 0f, 0f };

                this.dan = dan;


                this.gup = 0;
            }
        }
    }
}
