using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class GalacticaBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.TerraBeam);
            Main.projFrames[Projectile.type] = 1;
            Projectile.scale = 1.2f;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Galactica Blade");
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.3f) / 255f, ((255 - Projectile.alpha) * 0.4f) / 255f, ((255 - Projectile.alpha) * 1f) / 255f);
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 135);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.2f;
            int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 242);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].scale = 1.2f;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 135, 242 }, damageType: "melee");

            //MORE BLADES
            Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
            int damage = 40;
            int type = ModContent.ProjectileType<GalacticaBlade2>();
            IEntitySource p = EAU.Proj(Projectile);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, 0, 12, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, 0, -12, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, 12, 0, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, -12, 0, type, damage, 0f, 0);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
    }
}