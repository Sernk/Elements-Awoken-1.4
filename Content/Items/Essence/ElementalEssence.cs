using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.Essence
{
    public class ElementalEssence : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ModContent.RarityType<Rarity12>();
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 18));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertEssence>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FrostEssence>(), 1);
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 1);
            recipe.AddIngredient(ModContent.ItemType<VoidEssence>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}