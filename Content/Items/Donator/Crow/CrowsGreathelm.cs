using ElementsAwoken.Content.Items.BossDrops.Volcanox;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Crow
{
    [AutoloadEquip(EquipType.Head)]
    public class CrowsGreathelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 11;
            Item.defense = 16;
            Item.GetGlobalItem<EATooltip>().donator = true;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) *= 1.15f;
            player.statManaMax2 += 30;
            player.manaCost -= 0.20f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CrowsGreatplate>() && legs.type == ModContent.ItemType<CrowsGreatpants>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ModContent.GetInstance<EALocalization>().CrowsSetBonus;
            player.GetModPlayer<MyPlayer>().crowsArmor = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 30);
            recipe.AddIngredient(ModContent.ItemType<VolcanicStone>(), 4);
            recipe.AddIngredient(ItemID.NebulaHelmet, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}