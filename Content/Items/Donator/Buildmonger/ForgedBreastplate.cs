using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Buildmonger
{
    [AutoloadEquip(EquipType.Body)]
    public class ForgedBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 3;
            Item.defense = 7;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ForgedIronBar>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}