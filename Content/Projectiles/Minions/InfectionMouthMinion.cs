using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class InfectionMouthMinion : ModProjectile
    {
        public float dashTimer = 0;
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.tileCollide = false;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.minionSlots = 1;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]);
                EAU.Sb.Draw(tex, drawPos, rectangle, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.04f;
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<InfectionMouthBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.azanaMinions = false;
            }
            if (modPlayer.azanaMinions)
            {
                Projectile.timeLeft = 2;
            }
            if (Main.rand.NextBool(120))
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 5)];
                dust.velocity = Vector2.Zero;
            }
            ProjectileUtils.PushOtherEntities(Projectile);
            float targetX = Projectile.position.X;
            float targetY = Projectile.position.Y;
            float targetDist = 1200f;
            bool attacking = false;
            int maxDist = 1200;
            if (Projectile.ai[1] != 0f)
            {
                maxDist = 1400;
            }
            if (Math.Abs(Projectile.Center.X - player.Center.X) + Math.Abs(Projectile.Center.Y - player.Center.Y) > (float)maxDist)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.tileCollide = true;
                NPC ownerMinionAttackTargetNPC10 = Projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC10 != null && ownerMinionAttackTargetNPC10.CanBeChasedBy(this))
                {
                    float num1059 = ownerMinionAttackTargetNPC10.position.X + (float)(ownerMinionAttackTargetNPC10.width / 2);
                    float num1058 = ownerMinionAttackTargetNPC10.position.Y + (float)(ownerMinionAttackTargetNPC10.height / 2);
                    float num1057 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num1059) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num1058);
                    if (num1057 < targetDist && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC10.position, ownerMinionAttackTargetNPC10.width, ownerMinionAttackTargetNPC10.height))
                    {
                        targetDist = num1057;
                        targetX = num1059;
                        targetY = num1058;
                        attacking = true;
                    }
                }
                if (!attacking)
                {
                    for (int num1056 = 0; num1056 < 200; num1056++)
                    {
                        if (Main.npc[num1056].CanBeChasedBy(this))
                        {
                            float num1055 = Main.npc[num1056].position.X + (float)(Main.npc[num1056].width / 2);
                            float num1054 = Main.npc[num1056].position.Y + (float)(Main.npc[num1056].height / 2);
                            float num1053 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num1055) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num1054);
                            if (num1053 < targetDist && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num1056].position, Main.npc[num1056].width, Main.npc[num1056].height))
                            {
                                targetDist = num1053;
                                targetX = num1055;
                                targetY = num1054;
                                attacking = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Projectile.tileCollide = false;
            }
            if (!attacking)
            {
                Projectile.friendly = true;
                float speed = 16f;
                if (Projectile.ai[0] == 1f)
                {
                    speed = 20f;
                }
                Vector2 vector301 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num1050 = player.Center.X - vector301.X;
                if (player.velocity.X != 0)
                {
                    num1050 = player.Center.X + 300 * Math.Sign(player.velocity.X) - vector301.X;
                    speed = Math.Abs(player.velocity.X * 1.4f);
                }
                float num1049 = player.Center.Y - vector301.Y - 60f;
                float num1048 = (float)Math.Sqrt(num1050 * num1050 + num1049 * num1049);
                if (num1048 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                }
                if (num1048 > 2000f)
                {
                    Projectile.position.X = player.Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = player.Center.Y - (float)(Projectile.width / 2);
                }
                if (num1048 > 70f)
                {
                    num1048 = speed / num1048;
                    num1050 *= num1048;
                    num1049 *= num1048;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num1050) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num1049) / 21f;
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
                Projectile.friendly = false;
                Projectile.rotation = Projectile.velocity.X * 0.05f;
                if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                }
                return;
            }
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] -= 1f;
            }
            if (Projectile.ai[1] == 0f)
            {
                dashTimer--;
                Projectile.friendly = true;
                float speed = 8f;
                float num1042 = targetX - Projectile.Center.X;
                float num1041 = targetY - Projectile.Center.Y;
                float dist = (float)Math.Sqrt(num1042 * num1042 + num1041 * num1041);
                if (dist < 100f)
                {
                    speed = 10f;
                }
                if (dist < 300f && dashTimer <= 0)
                {
                    Vector2 toTarget = new Vector2(num1042, num1041);
                    toTarget.Normalize();
                    toTarget *= 20f;
                    Projectile.velocity.X = toTarget.X;
                    Projectile.velocity.Y = toTarget.Y;

                    dashTimer = 30;
                }
                dist = speed / dist;
                num1042 *= dist;
                num1041 *= dist;
                Projectile.velocity.X = (Projectile.velocity.X * 14f + num1042) / 15f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 14f + num1041) / 15f;
            }
            else
            {
                Projectile.friendly = false;
                if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 10f)
                {
                    Projectile.velocity *= 1.05f;
                }
            }
            Projectile.rotation = Projectile.velocity.X * 0.05f;
            if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
            {
                Projectile.spriteDirection = -Projectile.direction;
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 5;
            Projectile.ai[1] = 3f;
            ProjectileUtils.Explosion(Projectile, new int[] { 127 }, Projectile.damage);
        }
    }
}