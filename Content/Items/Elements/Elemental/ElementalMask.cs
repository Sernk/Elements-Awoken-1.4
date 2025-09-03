using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Elemental
{
    [AutoloadEquip(EquipType.Head)]
    public class ElementalMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.defense = 26;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 35;
            player.GetCritChance(DamageClass.Melee) += 35;
            player.GetCritChance(DamageClass.Ranged) += 35;
            player.GetCritChance(DamageClass.Throwing) += 35;
            player.maxMinions += 5;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ElementalBreastplate>() && legs.type == ModContent.ItemType<ElementalLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ModContent.GetInstance<EALocalization>().ElementalSetBonus;
            player.GetModPlayer<MyPlayer>().elementalArmor = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElementalEssence>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 6);
            recipe.AddIngredient(ItemID.LunarBar, 16);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}