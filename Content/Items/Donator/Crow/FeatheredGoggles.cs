using ElementsAwoken.Content.Mounts;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Crow
{
    public class FeatheredGoggles : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item79;
            Item.noMelee = true;
            Item.mountType = ModContent.MountType<CrowMount>();
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
    }
}