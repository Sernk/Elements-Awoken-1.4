using ElementsAwoken.Content.Items.BossDrops.Volcanox;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Crow
{
    [AutoloadEquip(EquipType.Legs)]
    public class CrowsGreatpants : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 11;
            Item.defense = 18;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.15f;
            player.GetDamage(DamageClass.Magic) *= 1.15f;
            player.statManaMax2 += 30;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddIngredient(ModContent.ItemType<VolcanicStone>(), 8);
            recipe.AddIngredient(ItemID.NebulaLeggings, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}