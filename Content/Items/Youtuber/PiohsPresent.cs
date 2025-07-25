using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Youtuber
{
    public class PiohsPresent : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item79;
            Item.noMelee = true;
            //item.mountType = mod.MountType("ElementalDragonBunny");
            //item.GetGlobalItem<EATooltip>().youtuber = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pioh's Present");
            // Tooltip.SetDefault("Summons a ridable Elemental Dragon Bunny\nBabeElena's Youtuber item");
        }
    }
}