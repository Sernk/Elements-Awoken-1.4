using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class BabyPuff : ModProjectile
    {
        public int shootTimer = 0;
        public int hitTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(shootTimer);
            writer.Write(hitTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            shootTimer = reader.ReadInt32();
            hitTimer = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 26;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.tileCollide = false;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.aiStyle = 26;
            AIType = 266;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.frame > 2)
                Projectile.frame = 2;
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<BabyPuffBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.babyPuff = false;
            }
            if (modPlayer.babyPuff)
            {
                Projectile.timeLeft = 2;
            }
            if (Projectile.frame == 2)
            {
                int dustSize = 10;
                for (int i = 0; i < 3; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.Bottom - new Vector2(dustSize / 2, 12), dustSize, dustSize, 21)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / 3 * (float)i;
                    dust.noGravity = true;
                    dust.scale *= 0.7f;
                }
            }
            shootTimer--;
            if (shootTimer <= 0)
            {
                float max = 200f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 3f;
                        float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y - 3f, ModContent.ProjectileType<BabyPuffSpike>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        shootTimer = 60;
                        break;
                    }
                }
            }
            hitTimer--;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (hitTimer > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hitTimer = 20;
        }
    }
}