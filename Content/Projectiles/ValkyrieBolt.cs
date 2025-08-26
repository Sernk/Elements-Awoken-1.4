using ElementsAwoken.Content.Projectiles.Explosions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ValkyrieBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 100;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;

            for (int l = 0; l < 5; l++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 60)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)l;
                dust.noGravity = true;
                dust.scale = 1f;
                dust.color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<RainbowExplosion>(), Projectile.damage, 0, Projectile.owner);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<RainbowExplosion>(), Projectile.damage, 0, Projectile.owner);
            return true;
        }
    }
}