using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    public class CalamityPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 9;
            Item.buffType = ModContent.BuffType<CalamityPotionBuff>();
            Item.buffTime = 10800;
            return;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChaosPotion>(), 1);
            recipe.AddIngredient(ItemID.LunarOre, 1);
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
