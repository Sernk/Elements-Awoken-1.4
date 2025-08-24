using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Eoite
{
    [AutoloadEquip(EquipType.Body)]
    public class EoitesRobes : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 10;
            Item.defense = 20;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.18f;
            player.GetDamage(DamageClass.Magic) *= 1.05f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 10);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddIngredient(ItemID.Silk, 24);
            recipe.AddIngredient(ItemID.Amethyst, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}