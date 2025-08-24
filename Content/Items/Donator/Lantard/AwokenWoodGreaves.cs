using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Donator.Lantard
{
    [AutoloadEquip(EquipType.Legs)]
    public class AwokenWoodGreaves : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 11;
            Item.defense = 12;
            Item.defense = 12;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) *= 1.06f;
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WoodGreaves);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}