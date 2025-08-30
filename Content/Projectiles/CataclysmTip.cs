using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CataclysmTip : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.TerraBeam);
            Main.projFrames[Projectile.type] = 1;
            Projectile.scale = 1.2f;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 127, damageType: "melee");
            {
                Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
                int damage = 40;
                int type = ModContent.ProjectileType<CataclysmicLaser>();
                Projectile.NewProjectile(EAU.Proj(Projectile), vector8.X, vector8.Y, 0, 12, type, damage, 0f, 0);
                Projectile.NewProjectile(EAU.Proj(Projectile), vector8.X, vector8.Y, 0, -12, type, damage, 0f, 0);
                Projectile.NewProjectile(EAU.Proj(Projectile), vector8.X, vector8.Y, 12, 0, type, damage, 0f, 0);
                Projectile.NewProjectile(EAU.Proj(Projectile), vector8.X, vector8.Y, -12, 0, type, damage, 0f, 0);
            }
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.3f) / 255f, ((255 - Projectile.alpha) * 0.4f) / 255f, ((255 - Projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.2f;

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
    }
}