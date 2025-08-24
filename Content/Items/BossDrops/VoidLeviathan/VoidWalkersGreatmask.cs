using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidWalkersGreatmask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.defense = 45;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void Load()
        {
            _ = this.GetLocalization("VoidWalkersGreatmaskSetBonus").Value;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) *= 1.25f;
            player.GetCritChance(DamageClass.Melee) += 15;
            player.GetAttackSpeed(DamageClass.Melee) += 0.20f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VoidWalkersBreastplate>() && legs.type == ModContent.ItemType<VoidWalkersLeggings>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = this.GetLocalization("VoidWalkersGreatmaskSetBonus").Value;
            player.GetModPlayer<MyPlayer>().voidWalkerArmor = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 4);
            recipe.AddIngredient(ItemID.LunarBar, 6);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}