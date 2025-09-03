using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.EARecipeSystem;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Elements.Desert
{
    [AutoloadEquip(EquipType.Head)]
    public class AridHat : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3; 
            Item.defense = 2;
            Item.GetGlobalItem<EASystem.UI.Tooltips.ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 3;
            player.GetDamage(DamageClass.Magic) *= 1.04f;
            player.statLifeMax2 += 40;
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