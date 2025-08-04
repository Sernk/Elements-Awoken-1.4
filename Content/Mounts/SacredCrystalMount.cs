using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Mounts
{
    public class SacredCrystalMount : ModMount
    {
        public float speed = 10f;
        public override void SetStaticDefaults()
        {
            MountData.spawnDust = 239;

            //mountData.buff = ModContent.BuffType<SacredCrystalBuff>();

            MountData.heightBoost = 0;
            MountData.fallDamage = 0f;
            MountData.runSpeed = speed;
            MountData.dashSpeed = speed;
            MountData.swimSpeed = speed;
            MountData.usesHover = true;
            MountData.flightTimeMax = 450;
            MountData.fatigueMax = 0;
            MountData.jumpHeight = 5;
            MountData.acceleration = 0.19f;
            MountData.jumpSpeed = 4f;

            MountData.blockExtraJumps = false;
            MountData.totalFrames = 4;
            MountData.constantJump = true;
            int[] array = new int[MountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 20;
            }
            MountData.playerYOffsets = new int[] { 0 };

            MountData.xOffset = 0;
            MountData.bodyFrame = 3;
            MountData.yOffset = 4;

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
                MountData.textureWidth = MountData.backTexture.Width();
                MountData.textureHeight = MountData.backTexture.Height();
            }
        }
        public int shootTimer = 0;
        public int shootCooldown = 0;
        public int type = 0;
        public override void UpdateEffects(Player player)
        {
            shootTimer--;
            shootCooldown--;
            if (shootCooldown <= 0)
            {
                shootCooldown = 80;
                type = Main.rand.Next(4);
            }
            float max = 400f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                    if (shootTimer <= 0 && shootCooldown <= 24)
                    {
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        SoundEngine.PlaySound(SoundID.Item12, player.position);
                        Projectile.NewProjectile(EAU.Play(player), player.Center.X - 4f, player.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<SacredCrystalLaser>(), 30, 5f, player.whoAmI, type);
                        shootTimer = 6;
                    }
                }
            }
        }
    }
}