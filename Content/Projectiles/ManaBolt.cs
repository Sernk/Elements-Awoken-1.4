using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ManaBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 3)
            {
                float numDust = 4;
                for (int l = 0; l < numDust; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 234)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / numDust * (float)l;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-9, 9), (float)Main.rand.Next(-9, 9));
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, value15.X, value15.Y, ModContent.ProjectileType<Manashatter>(), Projectile.damage / 2, 2f, Projectile.owner, 0f, 0f);
            }

            ProjectileUtils.Explosion(Projectile, 234, damageType: "ranged");
        }
    }
}