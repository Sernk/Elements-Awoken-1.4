using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions.BreathOfDarkness
{
    public class VleviHead : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.minion = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.timeLeft *= 5;
        }
        public override void AI()
        {
            Player player10 = Main.player[Projectile.owner];
            if ((int)Main.time % 120 == 0)
            {
                Projectile.netUpdate = true;
            }
            if (!player10.active)
            {
                Projectile.active = false;
                return;
            }
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<MiniVleviBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.miniVlevi = false;
            }
            if (modPlayer.miniVlevi )
            {
                Projectile.timeLeft = 2;
            }

            int num1049 = 30;

            Vector2 center14 = player10.Center;
            float maxTargetDist = 700f;
            float num1052 = 1000f;
            int num1053 = -1;
            if (Projectile.Distance(center14) > 2000f)
            {
                Projectile.Center = center14;
                Projectile.netUpdate = true;
            }
            bool flag67 = true;
            if (flag67)
            {
                NPC ownerMinionAttackTargetNPC5 = Projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(Projectile, false))
                {
                    float num1054 = Projectile.Distance(ownerMinionAttackTargetNPC5.Center);
                    if (num1054 < maxTargetDist * 2f)
                    {
                        num1053 = ownerMinionAttackTargetNPC5.whoAmI;
                        if (ownerMinionAttackTargetNPC5.boss)
                        {
                            int whoAmI = ownerMinionAttackTargetNPC5.whoAmI;
                        }
                        else
                        {
                            int whoAmI2 = ownerMinionAttackTargetNPC5.whoAmI;
                        }
                    }
                }
                if (num1053 < 0)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC target = Main.npc[i];
                        if (target.CanBeChasedBy(Projectile, false) && player10.Distance(target.Center) < num1052)
                        {
                            float num1056 = Projectile.Distance(target.Center);
                            if (num1056 < maxTargetDist)
                            {
                                num1053 = i;
                            }
                        }
                    }
                }
            }
            if (num1053 != -1)
            {
                NPC nPC15 = Main.npc[num1053];
                Vector2 vector148 = nPC15.Center - Projectile.Center;
                float num1057 = (float)(vector148.X > 0f).ToDirectionInt();
                float num1058 = (float)(vector148.Y > 0f).ToDirectionInt();
                float scaleFactor15 = 0.4f;
                if (vector148.Length() < 600f)
                {
                    scaleFactor15 = 0.6f;
                }
                if (vector148.Length() < 300f)
                {
                    scaleFactor15 = 0.8f;
                }
                if (vector148.Length() > nPC15.Size.Length() * 0.75f)
                {
                    Projectile.velocity += Vector2.Normalize(vector148) * scaleFactor15 * 1.5f;
                    if (Vector2.Dot(Projectile.velocity, vector148) < 0.25f)
                    {
                        Projectile.velocity *= 0.8f;
                    }
                }
                float num1059 = 30f;
                if (Projectile.velocity.Length() > num1059)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num1059;
                }
            }
            else
            {
                float num1060 = 0.2f;
                Vector2 vector149 = center14 - Projectile.Center;
                if (vector149.Length() < 200f)
                {
                    num1060 = 0.12f;
                }
                if (vector149.Length() < 140f)
                {
                    num1060 = 0.06f;
                }
                if (vector149.Length() > 100f)
                {
                    if (Math.Abs(center14.X - Projectile.Center.X) > 20f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num1060 * (float)Math.Sign(center14.X - Projectile.Center.X);
                    }
                    if (Math.Abs(center14.Y - Projectile.Center.Y) > 10f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num1060 * (float)Math.Sign(center14.Y - Projectile.Center.Y);
                    }
                }
                else if (Projectile.velocity.Length() > 2f)
                {
                    Projectile.velocity *= 0.96f;
                }
                if (Math.Abs(Projectile.velocity.Y) < 1f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - 0.1f;
                }
                float num1061 = 15f;
                if (Projectile.velocity.Length() > num1061)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num1061;
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            int direction = Projectile.direction;
            Projectile.direction = (Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : -1));
            if (direction != Projectile.direction)
            {
                Projectile.netUpdate = true;
            }
            float num1062 = MathHelper.Clamp(Projectile.localAI[0], 0f, 50f);
            Projectile.position = Projectile.Center;
            Projectile.scale = 1f + num1062 * 0.01f;
            Projectile.width = (Projectile.height = (int)((float)num1049 * Projectile.scale));
            Projectile.Center = Projectile.position;
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 42;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                    return;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 7;
        }
    }
}