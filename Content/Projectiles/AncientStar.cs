using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AncientStar : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Starfury);
            AIType = ProjectileID.Starfury;
            Projectile.scale *= 1.5f;
            Projectile.damage *= 2;
        }
        public override bool PreKill(int timeLeft)
        {
            Projectile.type = ProjectileID.Starfury;
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 2; i++)
            {
                int explode = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-10, 11) * .25f, Main.rand.Next(-10, -5) * .25f, ModContent.ProjectileType<AncientStar2>(), (int)(Projectile.damage * .5f), 0, Projectile.owner);
                Main.projectile[explode].aiStyle = 1;
                Main.projectile[explode].scale = 1f;
                Main.projectile[explode].tileCollide = true;
            }
            return true;
        }
    }
}