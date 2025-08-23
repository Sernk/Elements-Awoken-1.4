using ElementsAwoken.Content.Buffs.PotionBuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    public class PinkJelly : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.useTurn = true;
            Item.consumable = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = 1;
            Item.buffType = ModContent.BuffType<Energized>();
            Item.buffTime = 600;
            //item.potion = true;
            return;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.PinkGel, 5);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}