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
    public class VoidWalkersHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ModContent.RarityType<Rarity12>();
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.defense = 9;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsVoidWalkersHelm = true;
        }
        public override void Load()
        {
            _ = this.GetLocalization("VoidWalkersHelmSetBonus").Value;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) *= 1.4f;
            player.maxMinions += 6;
            player.GetKnockback(DamageClass.Summon).Base += 8f;
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
            player.setBonus = this.GetLocalization("VoidWalkersHelmSetBonus").Value; //= "Press the armour ability key to activate psychosis aura\nThe psychosis aura confuses enemies and inflicts extinction curse";
            player.GetModPlayer<MyPlayer>().voidWalkerArmor = 4;
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