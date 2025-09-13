using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    [AutoloadEquip(EquipType.Body)]
    public class PutridBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.defense = 18;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) *= 1.1f;
            player.maxMinions++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<PutridBar>(), 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}