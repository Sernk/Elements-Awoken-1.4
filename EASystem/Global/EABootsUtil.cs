using Terraria;
using Terraria.ModLoader;
using static ElementsAwoken.EAUtilities.EAList;

namespace ElementsAwoken.EASystem.Global
{
    public class EABootsUtil : GlobalItem
    {
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 8;
                if (VBoots.Contains(item.type)) // Vanila
                {
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        if (slot != i && (EABoots.Contains(player.armor[i].type)))
                        {
                            return false;
                        }
                    }
                }
                if (EABoots.Contains(item.type)) // Elements Awoken
                {
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        if (slot != i && (VBoots.Contains(player.armor[i].type)))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}