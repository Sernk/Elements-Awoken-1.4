using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    public class DemonPhilter : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = 2;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 28;
            Item.value = 2000;
            Item.rare = 1;
            Item.buffType = ModContent.BuffType<DemonSkinBuff>();
            Item.buffTime = 7200;
            //item.potion = true;
            return;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddRecipeGroup(EARecipeGroups.EvilOre, 4);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}