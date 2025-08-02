using ElementsAwoken.Content.Buffs.Debuffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class EntropicCoating : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 4));
            Const.SetSoul(Type);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<ChaosBurn>()] = true;
        }
    }
}