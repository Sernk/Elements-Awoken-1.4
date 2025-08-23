using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    public class HellFury : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 28;
            Item.value = 2500;
            Item.rare = 3;
            Item.buffType = ModContent.BuffType<HellFuryBuff>();
            Item.buffTime = 1800;
            //item.potion = true;
            return;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddIngredient(ItemID.Hellstone, 4);
            recipe.AddIngredient(ItemID.Obsidian, 4);
            recipe.AddIngredient(ModContent.ItemType<MagmaCrystal>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}