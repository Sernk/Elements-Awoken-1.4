using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    [AutoloadEquip(EquipType.Head)]
    public class AridHeadgear : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3; 
            Item.defense = 1;
            Item.GetGlobalItem<EASystem.UI.Tooltips.ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetKnockback(DamageClass.Summon).Base *= 1.05f;
            player.maxMinions += 1;
            player.GetDamage(DamageClass.Summon) *= 1.04f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<AridBreastplate>() && legs.type == ItemType<AridLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = GetInstance<EALocalization>().AridSetBonus;
            player.GetModPlayer<MyPlayer>().arid = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<DesertEssence>(), 6);
            recipe.AddRecipeGroup(EARecipeGroups.SandGroup, 15);
            recipe.AddRecipeGroup(EARecipeGroups.SandstoneGroup, 5);
            recipe.AddTile(TileType<Tiles.Crafting.ElementalForge>());
            recipe.Register();
        }
    }
}