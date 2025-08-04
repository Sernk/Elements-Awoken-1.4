using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CelestiaPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
            Main.projFrames[Projectile.type] = 4;
        }
        public override void AI()
        {
            Projectile.velocity.X *= 0.98f;
            Projectile.velocity.Y *= 0.98f;

            Lighting.AddLight(Projectile.Center, 0.6f, 0.6f, 0.6f);

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= 250)
                {
                    if (Projectile.ai[0] <= 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item122, Projectile.position);
                        Vector2 vector94 = nPC.Center - Projectile.Center;
                        float ai = (float)Main.rand.Next(100);
                        float speed = 5f;
                        Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * speed;
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, vector95.X, vector95.Y, ModContent.ProjectileType<CelestiaArc>(), Projectile.damage, 0f, 0, vector94.ToRotation(), ai);
                        Projectile.ai[0]++;
                    }
                }
            }
            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 120)
            {
                Projectile.alpha += 10;
            }
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
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