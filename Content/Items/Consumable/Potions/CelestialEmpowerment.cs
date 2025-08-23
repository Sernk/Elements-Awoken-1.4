using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable.Potions
{
    public class CelestialEmpowerment : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = 2;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 28;
            Item.value = 15000;
            Item.rare = 9;
            Item.buffType = ModContent.BuffType<LuminiteBuff>();
            Item.buffTime = 7200;
            return;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 2);
            recipe.AddIngredient(ItemID.LunarOre, 4);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}