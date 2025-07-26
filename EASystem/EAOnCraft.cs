using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ElementsAwoken.EASystem
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
    }
}