using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    class FlaskOfExtinction : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 24;
            Item.maxStack = 9999;
            Item.rare = 8;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<ExtinctionCurseImbue>();
            Item.buffTime = 72000;
            //item.potion = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 1);
            recipe.AddTile(TileID.ImbuingStation);
            recipe.Register();
        }
    }
}