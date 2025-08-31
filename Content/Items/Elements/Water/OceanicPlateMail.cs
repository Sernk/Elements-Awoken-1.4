using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Water
{
    [AutoloadEquip(EquipType.Body)]
    public class OceanicPlateMail : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8;
            Item.defense = 19;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Throwing) *= 1.06f;
            player.GetDamage(DamageClass.Melee) *= 1.06f;
            player.GetDamage(DamageClass.Magic) *= 1.06f;
            player.GetDamage(DamageClass.Ranged) *= 1.06f;
            player.GetDamage(DamageClass.Summon) *= 1.06f;
            player.wingTimeMax += 25;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 10);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 22);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}