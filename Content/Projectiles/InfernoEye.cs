using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class InfernoEye : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Center = player.Center - new Vector2(0, 70);

            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
                Main.dust[dust].velocity *= 0.3f;
                Main.dust[dust].fadeIn = 0.9f;
                Main.dust[dust].noGravity = true;
            }
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] % 8 == 0 && Projectile.alpha == 0)
            {
                float shootSpeed = 18f;
                Vector2 targetPos = new Vector2(Projectile.ai[0], Projectile.ai[1]);

                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 shootVel = targetPos - Projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= shootSpeed;
                    if (Main.rand.Next(2) == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                    }

                    int proj = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<InfernoShot>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].timeLeft = 300;
                    Main.projectile[proj].netUpdate = true;
                    Projectile.netUpdate = true;
                }
            }
            if (Projectile.localAI[1] < 15)
            {
                Projectile.alpha -= 26;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
            if (Projectile.localAI[1] > 60)
            {
                Projectile.alpha += 26;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 6, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 360);
        }       
    }
}