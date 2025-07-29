using ElementsAwoken.Content.Items.Chaos;
using ElementsAwoken.Content.Tiles.Crafting;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class DiscordantBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(12, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DiscordantOre>(), 3);
            recipe.AddIngredient(ModContent.ItemType<ChaoticFlare>(), 1);
            recipe.AddTile(ModContent.TileType<ChaoticCrucible>());
            recipe.Register();
        }
    }
}