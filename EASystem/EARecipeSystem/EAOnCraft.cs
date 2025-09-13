using ElementsAwoken.EAUtilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.EARecipeSystem
{
    public static class EAOnCraft
    {
        public static void SanityUPOnCraft(Recipe recipe, Item item, List<Item> consumedItems, Item destinationStack)
        {
            Player player = Main.player[item.playerIndexTheItemIsReservedFor];
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (MyWorld.awakenedMode)
            {
                if (item.damage > 0 && modPlayer.craftWeaponCooldown <= 0)
                {
                    modPlayer.sanity += 15;
                    modPlayer.craftWeaponCooldown = 3600;
                }
            }
        }
        public static void SanityBookOnCraft(Recipe recipe, Item item, List<Item> consumedItems, Item destinationStack)
        {
            if (!MyWorld.awakenedMode) Main.NewText(ModContent.GetInstance<EALocalization>().SanityBookOnCraft, EAColors.Gray);
        }
    }
}