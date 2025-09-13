using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.Content.Projectiles.Minions.MinionProj;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class PutridRipper : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 82;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.tileCollide = false;
            Projectile.minionSlots = 1.5f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Putrid Ripper");
            Main.projFrames[Projectile.type] = 6;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 12)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.ai[1] <= 30)
                {
                    if (Projectile.frame > 5)
                        Projectile.frame = 3;
                }
                else
                {
                    if (Projectile.frame > 2)
                        Projectile.frame = 0;
                }
            }
            return true;
        }
        public override void PostDraw(Color lightColor)
        {
            if (ModContent.GetInstance<Config>().debugMode)
            {
                EAU.Sb.DrawString(FontAssets.MouseText.Value, "ai[0]: " + Projectile.ai[0], Projectile.Hitbox.TopLeft() + new Vector2(0, -10), Color.White);
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<PutridRipperBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.putridRipper = false;
            }
            if (modPlayer.putridRipper)
            {
                Projectile.timeLeft = 2;
            }
            float viewDist = 500f;
            float chaseDist = 200f;
            float chaseAccel = 6f;
            float inertia = 40f;

            ProjectileUtils.PushOtherEntities(Projectile);

            Vector2 targetPos = Projectile.position;
            float targetDist = viewDist;
            bool target = false;
            Projectile.tileCollide = true;
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if ((distance < targetDist) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                    }
                }
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > (target ? 1000f : 500f))
            {
                Projectile.ai[0] = 1f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
            }
            if (target && Projectile.ai[0] == 0f)
            {
                Vector2 direction = targetPos - Projectile.Center;
                if (direction.Length() > chaseDist)
                {
                    direction.Normalize();
                    Projectile.velocity = (Projectile.velocity * inertia + direction * chaseAccel) / (inertia + 1);
                }
                else
                {
                    Projectile.velocity *= (float)Math.Pow(0.97, 40.0 / inertia);
                }
            }
            else
            {
                if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
                {
                    Projectile.ai[0] = 1f;
                }            
                float num546 = 8f;
                if (Projectile.ai[0] == 1f)
                {
                    num546 = 12f;
                }
                if (Vector2.Distance(player.Center,Projectile.Center) < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                Vector2 vector42 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num547 = player.Center.X - vector42.X;
                float num548 = player.Center.Y - vector42.Y - 60f;
                float num549 = (float)Math.Sqrt((double)(num547 * num547 + num548 * num548));
                if (num549 > 2000f)
                {
                    Projectile.Center = player.Center;
                }
                if (num549 > 70f)
                {
                    num549 = num546 / num549;
                    num547 *= num549;
                    num548 *= num549;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num547) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num548) / 21f;
                }
                else
                {
                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X = -0.15f;
                        Projectile.velocity.Y = -0.05f;
                    }
                    Projectile.velocity *= 1.01f;
                }
                Projectile.rotation = Projectile.velocity.X * 0.05f;
            }
            Projectile.rotation = Projectile.velocity.X * 0.05f;

            if (Projectile.ai[0] == 0f)
            {
                if (target)
                {
                    Projectile.ai[1]--;
                    if (Projectile.ai[1] == 0f)
                    {
                        SoundEngine.PlaySound(SoundID.NPCDeath13, Projectile.Center);
                    }
                    if (Projectile.ai[1] <= 0f && Projectile.ai[1] % 2 == 0)
                    {
                        if (Projectile.ai[1] < -30)
                        {
                            Projectile.ai[1] = 180f;
                        }
                        if (ModContent.GetInstance<Config>().debugMode)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(targetPos - new Vector2(0, MathHelper.Lerp(0, 90, MathHelper.Clamp(Math.Abs(Projectile.Center.X - targetPos.X), 0, 300) / 300)), 2, 2, 6)];
                                dust.noGravity = true;
                            }
                        }
                        if (Main.myPlayer == Projectile.owner && targetDist < 300)
                        {
                            Vector2 mouth = Projectile.Center + new Vector2(0, -22);

                            Vector2 shootVel = targetPos - Projectile.Center -new Vector2(0, MathHelper.Lerp(0,90,MathHelper.Clamp(Math.Abs(Projectile.Center.X - targetPos.X), 0, 300) / 300)); // to make it aim more up the further away the target is
                            if (shootVel == Vector2.Zero) shootVel = new Vector2(0f, 1f);
                            shootVel.Normalize();
                            shootVel *= 5f; 
                            shootVel = shootVel.RotatedByRandom(MathHelper.ToRadians(12));

                            Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.Proj(Projectile), mouth.X, mouth.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<PutridVomit>(), Projectile.damage / 3, Projectile.knockBack, Main.myPlayer, 0f, 0f)];
                            proj.timeLeft = 300;
                            proj.netUpdate = true;
                            Projectile.netUpdate = true;
                            Projectile.spriteDirection = (Projectile.direction = Math.Sign(shootVel.X));
                        }
                    }
                }
                if (Projectile.ai[1] > 0 || !target)
                {
                    if (Projectile.velocity.X > 0f)
                    {
                        Projectile.spriteDirection = -(Projectile.direction = -1);
                    }
                    else if (Projectile.velocity.X < 0f)
                    {
                        Projectile.spriteDirection = -(Projectile.direction = 1);
                    }
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}