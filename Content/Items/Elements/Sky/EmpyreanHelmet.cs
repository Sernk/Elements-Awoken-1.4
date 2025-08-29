using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Sky
{
    [AutoloadEquip(EquipType.Head)]
    public class EmpyreanHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.defense = 9;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 7;
            player.GetDamage(DamageClass.Ranged) *= 1.1f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<EmpyreanPlate>() && legs.type == ModContent.ItemType<EmpyreanLeggings>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = EALocalization.EmpyreanSetBonus;
            player.GetModPlayer<MyPlayer>().empyreanCloudCD--;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyEssence>(), 6);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}