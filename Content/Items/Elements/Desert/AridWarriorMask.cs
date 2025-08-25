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
    public class AridWarriorMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3; 
            Item.defense = 5;
            Item.GetGlobalItem<EASystem.UI.Tooltips.ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Arid Warrior Mask");
            // Tooltip.SetDefault("4% increased melee damage\n3% increased melee critical strike chance\n10% increased melee speed");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 3;
            player.GetDamage(DamageClass.Melee) *= 1.04f;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.1f;
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
