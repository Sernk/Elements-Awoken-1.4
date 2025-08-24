using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Buildmonger
{
    [AutoloadEquip(EquipType.Head)]
    public class ForgedMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 60, 0);
            Item.rare = 3;
            Item.defense = 5;
            Item.GetGlobalItem<EATooltip>().donator = true;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) *= 1.12f;
            player.magmaStone = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ForgedBreastplate>() && legs.type == ModContent.ItemType<ForgedGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = ModContent.GetInstance<EALocalization>().ForgedSetBonus;
            player.GetModPlayer<MyPlayer>().forgedArmor = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ForgedIronBar>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}