using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Body)]
    public class EmpyreanPlate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.defense = 17;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Melee) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
            player.wingTimeMax += 30;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 8);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 24);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}