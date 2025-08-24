using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Buildmonger
{
    [AutoloadEquip(EquipType.Legs)]
    public class ForgedGreaves : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 80, 0);
            Item.rare = 3;
            Item.defense = 6;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 6;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ForgedIronBar>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
