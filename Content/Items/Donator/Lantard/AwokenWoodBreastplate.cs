using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Donator.Lantard
{
    [AutoloadEquip(EquipType.Body)]
    public class AwokenWoodBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 11;
            Item.defense = 22;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Awoken Wooden Breastplate");
            // Tooltip.SetDefault("12% increased damage\nIncreases your max number of minions by 2\nSomething so simple has become so powerful... As have you\nLantard's donator item");
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) *= 1.12f;
            player.maxMinions += 2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WoodBreastplate);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
