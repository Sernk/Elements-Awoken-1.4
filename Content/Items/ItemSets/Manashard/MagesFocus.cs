using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class MagesFocus : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) *= 1.1f;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.statManaMax2 += 50;
            player.manaCost -= 0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Manashard>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}