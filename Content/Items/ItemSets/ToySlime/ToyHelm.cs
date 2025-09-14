using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.ToySlime
{
    [AutoloadEquip(EquipType.Head)]
    public class ToyHelm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 3;
            Item.defense = 6;
            Item.GetGlobalItem<ArmorSetBonusToolTips>().IsHelmet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 4;
            player.GetCritChance(DamageClass.Melee) += 4;
            player.GetCritChance(DamageClass.Ranged) += 4;
            player.GetCritChance(DamageClass.Throwing) += 4;
            player.endurance += 0.01f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<ToyBreastplate>() && legs.type == ItemType<ToyLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = GetInstance<EALocalization>().ToySetBonus;
            player.GetModPlayer<MyPlayer>().toyArmor = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BrokenToys>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}