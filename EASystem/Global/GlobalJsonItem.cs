using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.EAPlayer
{
    public class GlobalJsonItem : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            if (ElementsAwoken.itemList != null && entity.ModItem != null)
            {
                string itemName = entity.ModItem.Name; // имя класса 
             
              //if (ElementsAwoken.itemList.TheNecessaryItemsToOpen.Contains(itemName)) { entity.ResearchUnlockCount = 0; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_3.Contains(itemName)) { entity.ResearchUnlockCount = 3; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_5.Contains(itemName)) { entity.ResearchUnlockCount = 5; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_10.Contains(itemName)) { entity.ResearchUnlockCount = 10; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_15.Contains(itemName)) { entity.ResearchUnlockCount = 15; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_25.Contains(itemName)) { entity.ResearchUnlockCount = 25; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_20.Contains(itemName)) { entity.ResearchUnlockCount = 20; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_75.Contains(itemName)) { entity.ResearchUnlockCount = 75; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_50.Contains(itemName)) { entity.ResearchUnlockCount = 50; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_100.Contains(itemName)) { entity.ResearchUnlockCount = 100; }
                if (ElementsAwoken.itemList.TheNecessaryItemsToOpen_400.Contains(itemName)) { entity.ResearchUnlockCount = 400; }
            }
        }
    }
}