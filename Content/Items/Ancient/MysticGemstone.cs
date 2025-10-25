using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.Ancient
{
    public class MysticGemstone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ModContent.RarityType<Mystic>();
        }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(7, 53));
            EAU.SetSoul(Type);
        }
    }
}