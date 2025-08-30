using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    [AutoloadEquip(EquipType.Body)]
    public class VoidBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            Item.defense = 33;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Throwing) *= 1.12f;
            player.GetDamage(DamageClass.Melee) *= 1.12f;
            player.GetDamage(DamageClass.Magic) *= 1.12f;
            player.GetDamage(DamageClass.Ranged) *= 1.12f;
            player.GetDamage(DamageClass.Summon) *= 1.12f;
            player.statLifeMax2 += 75;
            player.statManaMax2 += 75;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 16);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}