using Terraria;
using Terraria.ModLoader;
using ElementsAwoken.Content.Buffs.Debuffs;

namespace ElementsAwoken.Content.Items.Tech.Generators
{
    public class LunarWaste : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 44;
            Item.rare = -1;
            Item.maxStack = 9999;
        }
        public override void UpdateInventory(Player player)
        {
            if (Item.stack >= 10)
            {
                player.AddBuff(ModContent.BuffType<Irradiated>(), 20);
                player.lifeRegen--;
            }
            if (Item.stack >= 15) player.lifeRegen -= 2;
            if (Item.stack >= 20) player.lifeRegen -= 3;
            if (Item.stack >= 30) player.lifeRegen -= 5;
            if (Item.stack >= 40) player.lifeRegen -= 10;
            if (Item.stack >= 50) player.lifeRegen -= 20;
        }
    }
}