using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    [AutoloadEquip(EquipType.Head)]
    public class PutridMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 7;
            Item.defense = 8;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) *= 1.15f;
            player.GetKnockback(DamageClass.Summon).Base *= 1.3f;
            player.maxMinions++;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PutridBreastplate>() && legs.type == ItemType<PutridLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = GetInstance<EALocalization>().PutridSetBonus;
            player.GetModPlayer<MyPlayer>().putridArmour = true;
            player.maxMinions++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<PutridBar>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
