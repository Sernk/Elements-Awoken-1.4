using ElementsAwoken.Content.Items.BossDrops.Volcanox;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Crow
{
    // the cape doesnt draw if its in the armour slot
    [AutoloadEquip(EquipType.Body, EquipType.Front, EquipType.Back)]
    public class CrowsGreatplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 11;
            Item.defense = 20;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.05f;
            player.GetCritChance(DamageClass.Magic) += 15;
            player.statManaMax2 += 30;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 75);
            recipe.AddIngredient(ModContent.ItemType<VolcanicStone>(), 12);
            recipe.AddIngredient(ItemID.NebulaBreastplate, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}