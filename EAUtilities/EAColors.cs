using Microsoft.Xna.Framework;
using Terraria;

namespace ElementsAwoken.EAUtilities
{
    public class EAColors
    {
        public static readonly Color Gray = new(128, 128, 128);
        public static readonly Color RarityMagenta = new(239, 65, 255);
        public static readonly Color RarityDarkRed = new(135, 14, 14);
        public static readonly Color RarityDarkBlue = new(22, 68, 132);
        public static readonly Color RarityBrightGreen = new(48, 255, 179);

        //Mystic
        public static readonly Color Blue = new(92, 194, 180);
        public static readonly Color Orange = new(255, 163, 93);
        public static readonly Color Green = new(82, 184, 117);
        public static readonly Color Purpal = new(156, 37, 131);

        //BetaTest
        public static readonly Color DarkRed = Color.DarkRed;
        public static readonly Color Red = Color.Red;
        public static readonly Color DarkOrange = Color.DarkOrange;
        public static readonly Color Orage = Color.Orange;
        public static readonly Color Yelow = Color.Yellow;

        public static readonly Color[] AnimatedColors = [Blue, Orange, Green, Purpal, Main.DiscoColor];
        public static readonly Color[] BetaTestColors = [DarkRed, Red, DarkOrange, Orage, Yelow, Green, /*Main.DiscoColor*/ ];
    }
}