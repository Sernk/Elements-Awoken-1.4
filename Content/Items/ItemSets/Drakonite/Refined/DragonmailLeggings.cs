using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Legs)]
    public class DragonmailLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 16;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.09f;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.12f;
            player.lifeRegen += 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RefinedDrakonite>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}