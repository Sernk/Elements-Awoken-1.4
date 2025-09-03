using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Head)]
    public class DragonmailHood : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 9;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 80;
            player.GetCritChance(DamageClass.Magic) += 5;
            player.GetDamage(DamageClass.Magic) *= 1.07f;
            player.manaCost *= 0.85f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DragonmailChestpiece>() && legs.type == ModContent.ItemType<DragonmailLeggings>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ModContent.GetInstance<EALocalization>().DragonmailHoodSetBonus;
            player.GetModPlayer<MyPlayer>().dragonmailHood = true;
            if (player.statLife <= (player.statLifeMax2 * 0.75f)) player.GetDamage(DamageClass.Magic) *= 1.02f;
            if (player.statLife <= (player.statLifeMax2 * 0.5f)) player.GetDamage(DamageClass.Magic) *= 1.08f;
            if (player.statLife <= (player.statLifeMax2 * 0.25f)) player.GetDamage(DamageClass.Magic) *= 1.12f;
            if (player.statLife <= (player.statLifeMax2 * 0.05f)) player.GetDamage(DamageClass.Magic) *= 1.20f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RefinedDrakonite>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}