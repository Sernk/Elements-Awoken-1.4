using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ImbalancerMine : ModProjectile
    {
        public int shootTimer = 0;

        public override void SendExtraAI(BinaryWriter writer) => writer.Write(shootTimer);
        public override void ReceiveExtraAI(BinaryReader reader) => shootTimer = reader.ReadInt32();
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            int maxDist = 150;
            if (ProjectileUtils.CountProjectiles(Projectile.type, Projectile.owner) > 6 && ProjectileUtils.HasLeastTimeleft(Projectile.whoAmI)) Projectile.Kill();
            if (Projectile.ai[1] == 1)
            {
                NPC stick = Main.npc[(int)Projectile.ai[0]];
                if (stick.active)
                {
                    Projectile.Center = stick.Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = stick.gfxOffY;
                    stick.AddBuff(ModContent.BuffType<ElectrifiedNPC>(), 300);
                }
                else Projectile.Kill();
            }
            else if (Projectile.ai[1] == 2)
            {
                Projectile.velocity = Vector2.Zero;

                if ((Projectile.velocity.X > 0.5f || Projectile.velocity.X < -0.5f) || (Projectile.velocity.Y > 0.5f || Projectile.velocity.Y < -0.5f))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226)];
                        dust.velocity = Vector2.Zero;
                        dust.position -= Projectile.velocity / 6f * (float)i;
                        dust.noGravity = true;
                        dust.scale = 1f;
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                        Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset - Vector2.One * 4, 0, 0, 226, 0, 0, 100)];
                        dust.noGravity = true;
                        dust.velocity *= 0.2f;
                        if (Collision.SolidCollision(dust.position, 4, 4)) dust.active = false;
                    }
                    shootTimer--;
                    if (shootTimer <= 0)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC nPC = Main.npc[i];
                            if (nPC.CanBeChasedBy(this) && Collision.CanHit(Projectile.Center, 2, 2, nPC.Center, 2, 2) && Vector2.Distance(Projectile.Center, nPC.Center) <= maxDist)
                            {
                                SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ElectricArcing"), Projectile.position);

                                float Speed = 9f;
                                float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                                Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, projSpeed.X, projSpeed.Y, ModContent.ProjectileType<ImbalancerLightning>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner);
                                shootTimer = 20;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                Projectile.velocity.Y += 0.13f;
                Projectile.rotation += Projectile.velocity.X * 0.02f;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] == 2) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = target.whoAmI;
            Projectile.ai[1] = 1;
            Projectile.velocity =(target.Center - Projectile.Center) * 0.75f;
            Projectile.netUpdate = true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile exp = Main.projectile[Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<Explosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f)];
            exp.DamageType = DamageClass.Ranged;
            int numDusts = 30;
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = (Vector2.One * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) / 2f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                Vector2 velocity = position - Projectile.Center;
                int dust = Dust.NewDust(position + velocity, 0, 0, 226, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 2;
            return false;
        }
    }
}