using ElementsAwoken.EASystem;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Mounts
{
    public class WispForm : ModMount
    {
        public override void SetStaticDefaults()
        {
            //mountData.spawnDust = 239;
           // mountData.buff = mod.BuffType("CrowBuff");

            MountData.heightBoost = -32;
            MountData.fallDamage = 0f;
            MountData.runSpeed = 7f;
            MountData.dashSpeed = 7f;
            MountData.usesHover = true;
            MountData.flightTimeMax = Int32.MaxValue;
            MountData.fatigueMax = Int32.MaxValue;
            MountData.acceleration = 0.2f;
            MountData.jumpHeight = 0;
            MountData.jumpSpeed = 0;

            MountData.blockExtraJumps = false;
            MountData.totalFrames = 1;
            MountData.constantJump = true;
            int[] array = new int[MountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 30;
            }
            MountData.playerYOffsets = array;

            MountData.xOffset = 0;
            MountData.bodyFrame = 0;
            MountData.yOffset = 0;
            MountData.playerHeadOffset = 0;

            MountData.standingFrameCount = 0;
            MountData.standingFrameDelay = 0;
            MountData.standingFrameStart = 0;

            MountData.runningFrameCount = 0;
            MountData.runningFrameDelay = 0;
            MountData.runningFrameStart = 0;

            MountData.flyingFrameCount = 0;
            MountData.flyingFrameDelay = 0;
            MountData.flyingFrameStart = 0;

            MountData.inAirFrameCount = 0;
            MountData.inAirFrameDelay = 0;
            MountData.inAirFrameStart = 0;

            MountData.idleFrameCount = 0;
            MountData.idleFrameDelay = 0;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = false;

            MountData.swimFrameCount = 0;
            MountData.swimFrameDelay = 0;
            MountData.swimFrameStart = 0;
            if (Main.netMode != 2)
            {
                MountData.frontTexture = null;
            }
        }
        public override void UpdateEffects(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.wispForm = true;
            modPlayer.wispDust = 242; 
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 242, 0f, 0f)];
                dust.noGravity = true;
                dust.velocity.Y = -6f * Main.rand.NextFloat(0.8f, 1.2f);
                dust.fadeIn = 0.5f;
            }
            player.width = 10;
        }
    }
}