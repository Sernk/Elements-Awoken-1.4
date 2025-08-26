using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    [AutoloadEquip(EquipType.Shield)]
    public class FireShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = 4;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.accessory = true;
            Item.defense = 4;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            player.buffImmune[24] = true;
            player.fireWalk = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.FireEssence, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.ObsidianShield, 1);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}