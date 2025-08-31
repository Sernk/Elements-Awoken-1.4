using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Legs)]
    public class OceanicLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8;
            Item.defense = 16;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
            if (player.statLife <= (player.statLifeMax2 * 0.75f))
            {
                player.statDefense += 4;
                if (player.statLife <= (player.statLifeMax2 * 0.5f))
                {
                    player.statDefense += 4;
                    if (player.statLife <= (player.statLifeMax2 * 0.25f))
                    {
                        player.statDefense += 4;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 9);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 14);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}