using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Superbaseball101
{
    [AutoloadEquip(EquipType.Legs)]
    public class FireDemonsLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = 3;
            Item.defense = 4;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 3);
            recipe.AddIngredient(ItemID.HellstoneBar, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}