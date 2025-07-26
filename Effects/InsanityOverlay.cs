using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Effects
{
    public class InsanityOverlay
    {
        public static float transparency = 1f;
        public static int gbValues = 255;

        public static void Update()
        {
            if (Main.gameMenu) return;
            var mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();

            transparency = MathHelper.Lerp(0.2f, 0f, modPlayer.sanity / (modPlayer.sanityMax * 0.5f));
            gbValues = (int)Math.Round(MathHelper.Lerp(155, 255, modPlayer.sanity / (modPlayer.sanityMax * 0.5f)));
        }
    }
}
