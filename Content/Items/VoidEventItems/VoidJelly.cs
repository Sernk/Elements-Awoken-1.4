using ElementsAwoken.Content.Buffs.PotionBuffs;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.VoidEventItems
{
    public class VoidJelly : ModItem
    {
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 28;
            Item.value = 2000;
            Item.rare = 2;
            Item.buffType = ModContent.BuffType<VoidJellyBuff>();
            Item.buffTime = EAU.BuffsTime(seconds: 16);
            Item.healLife = 100;
            Item.potion = true;
            return;
        }
    }
}