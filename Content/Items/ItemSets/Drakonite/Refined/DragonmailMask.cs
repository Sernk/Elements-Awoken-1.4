using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined
{
    [AutoloadEquip(EquipType.Head)]
    public class DragonmailMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 7;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7; 
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetKnockback(DamageClass.Summon).Base += 2.5f;
            player.GetDamage(DamageClass.Summon) *= 1.10f;
            player.maxMinions += 2;
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
            player.setBonus = ModContent.GetInstance<EALocalization>().DragonmailMaskSetBonus;
            player.GetModPlayer<MyPlayer>().dragonmailMask = true;
            float minions = 0f;
            for (int i = 0; i < 1000; i++) if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].minion) minions += Main.projectile[i].minionSlots;
            if (minions >= 1 && minions < 4) player.endurance += 0.02f;
            if (minions >= 4 && minions < 7) player.endurance += 0.06f;
            if (minions >= 7 && minions < 10) player.endurance += 0.08f;
            if (minions >= 10 && minions < 13)player.endurance += 0.12f;
            if (minions >= 13 && minions < 16) player.endurance += 0.16f;
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
