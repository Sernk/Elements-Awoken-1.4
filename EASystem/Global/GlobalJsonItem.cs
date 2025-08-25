using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Global
{
    public class GlobalJsonItem : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            if (ElementsAwoken.itemList != null && entity.ModItem != null)
            {
                string itemName = entity.ModItem.Name; // имя класса 

                if (ElementsAwoken.itemList.Materials.Contains(itemName))
                {
                    entity.ResearchUnlockCount = 100;
                }
            }
        }
    }
}