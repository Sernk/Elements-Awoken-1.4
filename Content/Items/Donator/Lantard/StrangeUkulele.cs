using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Lantard
{
    [AutoloadEquip(EquipType.Back)]
    public class StrangeUkulele : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = 2;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.strangeUkulele = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("Wood", 35);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }
    }
}
