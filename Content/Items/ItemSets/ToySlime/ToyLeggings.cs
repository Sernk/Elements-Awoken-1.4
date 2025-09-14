using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.ToySlime
{
    [AutoloadEquip(EquipType.Legs)]
    public class ToyLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 3;
            Item.defense = 7;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.05f;
            player.GetDamage(DamageClass.Generic) *= 1.02f;
            player.endurance += 0.02f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BrokenToys>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}