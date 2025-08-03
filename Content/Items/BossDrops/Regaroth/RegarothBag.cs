using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using ElementsAwoken.Utilities;
using ElementsAwoken.EASystem;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    public class RegarothBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.expert = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 5;
        }
        public override bool CanRightClick()
        {
            return true;
        }
		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			itemLoot.Add(ItemDropRule.OneFromOptions(1, Masiv.RegLoot));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StoneOfHope>()));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RegarothMask>(), 10));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RegarothTrophy>(), 10));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnergyWeaversHelm>(), 2));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnergyWeaversBreastplate>(), 2));
			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnergyWeaversLeggings>(), 2));
		}
        public override void RightClick(Player player)
        {
            player.GetModPlayer<MyPlayer>().TryGettingDevArmor();
        }
    }
}