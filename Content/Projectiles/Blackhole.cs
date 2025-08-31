using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Blackhole : ModProjectile
    {
        public bool hasOrbital = false;

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1.3f;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 80);
            target.immune[Projectile.owner] = 5;
        }
        public override void AI()
        {
            Projectile.velocity.X = 0f;
            Projectile.velocity.Y = 0f;
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
            }
            if (!hasOrbital)
            {
                int swirlCount = 12;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    int orbital = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<BlackholeOrbit>(), Projectile.damage, Projectile.knockBack, 0, l * distance, Projectile.whoAmI);
                }
                hasOrbital = true;
            }
            if (ProjectileUtils.CountProjectiles(Projectile.type, Projectile.owner) > 3)
            {
                if (ProjectileUtils.HasLeastTimeleft(Projectile.whoAmI))
                {
                    Projectile.alpha += 10;
                    if (Projectile.alpha >= 255)
                    {
                        Projectile.Kill();
                    }
                }
            }
            int maxDist = 400;
            float gravStength = 0.3f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                bool immune = false;
                foreach (int k in ElementsAwoken.instakillImmune)
                {
                    if (npc.type == k)
                    {
                        immune = true;
                    }
                }
                if (!immune && !npc.townNPC && npc.active && npc.damage > 0 && !npc.boss && npc.lifeMax < 10000 && Vector2.Distance(npc.Center, Projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(Projectile.Center.X - npc.Center.X, Projectile.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity.X += toTarget.X * gravStength;
                    npc.velocity.Y += toTarget.Y * gravStength * 5;
                }
            }
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item item = Main.item[i];
                if (item.active && Vector2.Distance(item.Center, Projectile.Center) < maxDist)
                {
                    Vector2 toTarget = new Vector2(Projectile.Center.X - item.Center.X, Projectile.Center.Y - item.Center.Y);
                    toTarget.Normalize();
                    item.velocity += toTarget * gravStength;
                }
            }

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}