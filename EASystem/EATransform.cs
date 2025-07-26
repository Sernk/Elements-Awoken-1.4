using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ElementsAwoken.EASystem
{
    public class EATransform : ModSystem
    {
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            if (!Main.gameMenu)
            {
                Player player = Main.LocalPlayer;
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (MyWorld.credits)
                {
                    if (!Main.gameMenu)
                    {
                        if (MyWorld.creditsCounter > modPlayer.screenTransDuration / 2)
                        {
                            Main.screenPosition = modPlayer.desiredScPos - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                        }
                    }
                }
                if (!ModContent.GetInstance<Config>().screenshakeDisabled)
                {
                    if (modPlayer.screenshakeAmount >= 0)
                    {
                        modPlayer.screenshakeTimer++;
                        if (modPlayer.screenshakeTimer >= 5) modPlayer.screenshakeAmount -= 0.1f;
                    }
                    if (modPlayer.screenshakeAmount < 0)
                    {
                        modPlayer.screenshakeAmount = 0;
                        modPlayer.screenshakeTimer = 0;
                    }
                    Main.screenPosition += new Vector2(modPlayer.screenshakeAmount * Main.rand.NextFloat(), modPlayer.screenshakeAmount * Main.rand.NextFloat());
                }
            }
        }
    }
}
