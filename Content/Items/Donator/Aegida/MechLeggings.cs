using ElementsAwoken.Content.Items.BossDrops.Volcanox;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Donator.Aegida
{
    [AutoloadEquip(EquipType.Legs)]
    public class MechLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 80, 0);
            Item.rare = 11;
            Item.defense = 22;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) *= 1.11f;
            player.moveSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VortexLeggings);
            recipe.AddIngredient(ItemType<Pyroplasm>(), 20);
            recipe.AddIngredient(ItemType<NeutronFragment>(), 3);
            recipe.AddIngredient(ItemType<VolcanicStone>(), 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}