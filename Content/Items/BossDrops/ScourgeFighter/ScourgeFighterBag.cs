using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.ScourgeFighter
{
    /// <summary>
    /// No Item = ModContent.ItemType<ScourgeFighterMask>()
    /// fixes this
    /// </summary>
    public class ScourgeFighterBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(1, Masiv.ScoLoot));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeDrive>()));
            //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterMask>(), 10));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterTrophy>(), 10));

            IItemDropRule weaponDrop = new LeadingConditionRule(new EAIDRC.ScourgeLootCondition());
            weaponDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterRocketLauncher>()));
            weaponDrop.OnSuccess(ItemDropRule.Common(ItemID.RocketI, 1, 50, 150));
            itemLoot.Add(weaponDrop);
        }
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}