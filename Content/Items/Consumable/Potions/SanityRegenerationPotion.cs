using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    public class SanityRegenerationPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.rare = 3;
            Item.buffType = ModContent.BuffType<SanityRegenerationBuff>();
            Item.buffTime = 7200;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddRecipeGroup(EARecipeGroups.EvilOre, 4);
            recipe.AddIngredient(ItemID.SpecularFish, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}