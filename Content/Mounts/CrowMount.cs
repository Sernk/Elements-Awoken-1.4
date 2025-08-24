using ElementsAwoken.Content.Buffs.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Mounts
{
    public class CrowMount : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.spawnDust = 239;
            MountData.buff = ModContent.BuffType<CrowBuff>();
            MountData.heightBoost = 22;
            MountData.fallDamage = 0f;
            MountData.runSpeed = 7f;
            MountData.dashSpeed = 7f;
            MountData.usesHover = true;
            MountData.flightTimeMax = 900;
            MountData.fatigueMax = 800;
            MountData.acceleration = 0.2f;
            MountData.jumpHeight = 10;
            MountData.jumpSpeed = 4f;

            MountData.blockExtraJumps = false;
            MountData.totalFrames = 14;
            MountData.constantJump = true;
            int[] array = new int[MountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 30;
            }
            array[2] -= 2;
            array[3] -= 2;
            MountData.playerYOffsets = array;
            MountData.xOffset = 0;
            MountData.bodyFrame = 3;
            MountData.yOffset = 0;
            MountData.playerHeadOffset = 22;
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 5;
            MountData.runningFrameCount = 6;
            MountData.runningFrameDelay = 18;
            MountData.runningFrameStart = 0;
            MountData.flyingFrameCount = 8;
            MountData.flyingFrameDelay = 3;
            MountData.flyingFrameStart = 6;
            MountData.inAirFrameCount = 8;
            MountData.inAirFrameDelay = 10;
            MountData.inAirFrameStart = 6;
            MountData.idleFrameCount = 1;
            MountData.idleFrameDelay = 12;
            MountData.idleFrameStart = 5;
            MountData.idleFrameLoop = false;
            MountData.swimFrameCount = 0;
            MountData.swimFrameDelay = 0;
            MountData.swimFrameStart = 0;
            if (Main.netMode != 2)
            {
                if (Main.netMode != 2)
                {
                    MountData.textureWidth = MountData.backTexture.Value.Width;
                    MountData.textureHeight = MountData.backTexture.Value.Height; //MountData.frontTexture = null;
                }
            }
        }
    }
}