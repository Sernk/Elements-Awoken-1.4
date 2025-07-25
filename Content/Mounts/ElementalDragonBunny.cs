using ElementsAwoken.Content.Buffs.Mounts;
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
    public class ElementalDragonBunny : ModMount
    {
        public float speed = 10f;
        public override void SetStaticDefaults()
        {
            MountData.spawnDust = 15;
            MountData.buff = ModContent.BuffType<ElementalDragonBunnyBuff>();

            MountData.heightBoost = 20;
            MountData.flightTimeMax = 0;
            MountData.fallDamage = 0.8f;
            MountData.runSpeed = 11f;
            MountData.dashSpeed = 7.5f;
            MountData.acceleration = 0.07f;
            MountData.jumpHeight = 30;
            MountData.jumpSpeed = 9f;
            MountData.totalFrames = 7;

            MountData.blockExtraJumps = false;
            MountData.totalFrames = 7;
            MountData.constantJump = true;

            int[] array = new int[MountData.totalFrames];
            for (int k = 0; k < array.Length; k++)
            {
                array[k] = 14;
            }
            array[2] += 2;
            array[3] += 4;
            array[4] += 8;
            array[5] += 8;
            MountData.playerYOffsets = array;
            MountData.xOffset = -7;
            MountData.bodyFrame = 3;
            MountData.yOffset = 4;
            MountData.playerHeadOffset = 22;

            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;

            MountData.runningFrameCount = 7;
            MountData.runningFrameDelay = 12;
            MountData.runningFrameStart = 0;

            MountData.flyingFrameCount = 6;
            MountData.flyingFrameDelay = 6;
            MountData.flyingFrameStart = 1;

            MountData.inAirFrameCount = 1;
            MountData.inAirFrameDelay = 12;
            MountData.inAirFrameStart = 5;

            MountData.idleFrameCount = 0;
            MountData.idleFrameDelay = 0;
            MountData.idleFrameStart = 0;
            if (Main.netMode != 2)
            {
                MountData.textureWidth = MountData.backTexture.Value.Width;
                MountData.textureHeight = MountData.backTexture.Value.Height; //MountData.frontTexture = null;
            }
        }

        public int shootTimer = 0;
        public int type = 0;
        public override void UpdateEffects(Player player)
        {
            shootTimer--;
            float max = 400f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(player.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                    if (shootTimer <= 0)
                    {
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        SoundEngine.PlaySound(SoundID.Item20, player.position);
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X - 4f, player.Center.Y, speed.X, speed.Y - 3.5f, ModContent.ProjectileType<BunnyBreath>(), 25, 5f, player.whoAmI, type);
                        shootTimer = 90;
                    }
                }
            }
            if (player.controlJump && player.velocity.Y == 0f)
            {
                for (int l = 0; l < 12; l++)
                {
                    Vector2 vector3 = Vector2.UnitX * (float)(-(float)player.width) / 2f;
                    vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    vector3 = vector3.RotatedBy(1.57079637f, default(Vector2));
                    int num9 = Dust.NewDust(player.Center, 0, 0, 181, 0f, 0f, 160, default(Color), 1f);
                    Main.dust[num9].scale = 1.1f;
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].fadeIn = 1f;
                    Main.dust[num9].position = player.Center + vector3;
                    Main.dust[num9].velocity = player.velocity * 0.1f;
                    Main.dust[num9].velocity = Vector2.Normalize(player.Bottom - new Vector2(0f, 24f) - player.velocity * 3f - Main.dust[num9].position) * 1.25f;
                }
            }
        }
    }
}