using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Testing
{
    public class SanityChanger : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.useAnimation = 3;
            Item.useTime = 3;
            Item.useStyle = 4;
            Item.autoReuse = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("sanitinator");
            // Tooltip.SetDefault("this game drives me crazy");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            AwakenedPlayer modPlayer = player.GetModPlayer<AwakenedPlayer>();
            if (player.altFunctionUse != 2)
            {
                modPlayer.sanity++;
            }
            else
            {
                modPlayer.sanity--;
            }
            return true;
        }
    }
}
