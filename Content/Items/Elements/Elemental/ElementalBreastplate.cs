using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Elemental
{
    [AutoloadEquip(EquipType.Body)]
    public class ElementalBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.defense = 40;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<HeartsPlayers>().HPBonus += 100;
            player.GetDamage(DamageClass.Generic) *= 1.2f;
            player.statManaMax2 += 150;
            player.endurance += 0.1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElementalEssence>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 12);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}