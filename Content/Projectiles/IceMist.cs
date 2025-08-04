using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class IceMist : ModProjectile
    {	
        public override void SetDefaults()
        {
            Projectile.width = 92;
            Projectile.height = 102;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 220;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            {
                if (Projectile.ai[0] >= 130f)
                {
                    Projectile.alpha += 10;
                }
                else
                {
                    Projectile.alpha -= 10;
                }
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
                if (Projectile.alpha > 255)
                {
                    Projectile.alpha = 255;
                }
                if (Projectile.ai[0] >= 150f)
                {
                    Projectile.Kill();
                    return;
                }
                if (Projectile.ai[0] % 30f == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item120, Projectile.position);

                    float numberProjectiles = 8;
                    float rotation = MathHelper.ToRadians(360);
                    float speed = 2f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(2, 2).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                        int num1 = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + Projectile.velocity.X, Projectile.Center.Y + Projectile.velocity.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IceMistSpike>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                }
                Projectile.rotation += 0.104719758f;
                Lighting.AddLight(Projectile.Center, 0.3f, 0.75f, 0.9f);
                return;
            }
        }       
    }
}