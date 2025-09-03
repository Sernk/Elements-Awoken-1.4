using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Testing
{
    public class CreditsSetup : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item60;
            Item.consumable = false;
        }
        public override bool? UseItem(Player player)
        {
            if (MyWorld.credits)
            {
                MyWorld.credits = false;               
                Main.NewText(ModContent.GetInstance<EALocalization>().CreditsSetup, Color.Purple.R, Color.Purple.G, Color.Purple.B);
            }
            else
            {
                MyWorld.credits = true;
                MyWorld.creditsCounter = 1200;
                Main.NewText(ModContent.GetInstance<EALocalization>().CreditsSetup1, Color.Purple.R, Color.Purple.G, Color.Purple.B);
            }
            return true;
        }
    }
}