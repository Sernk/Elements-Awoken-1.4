using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DrakoniteEruption : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.alpha = 0;
            Projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Drakonite Eruption");
        }
        public override void AI()
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }

            if (ProjectileUtils.CountProjectiles(Projectile.type,Projectile.owner) > 3 && ProjectileUtils.HasLeastTimeleft(Projectile.whoAmI)) Projectile.Kill();

            Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.BottomLeft.X, Projectile.BottomLeft.Y), Projectile.width, 2, DustID.Torch)];
            dust.scale *= 1.2f;
            dust.velocity = new Vector2(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-4, -2.5f));
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}