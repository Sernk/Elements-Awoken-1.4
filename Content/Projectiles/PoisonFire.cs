using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PoisonFire : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 125;
            Projectile.extraUpdates = 3;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.2f, 0.8f, 0.4f);
            if (Projectile.timeLeft > 125)
            {
                Projectile.timeLeft = 125;
            }
            if (Projectile.ai[0] > 12f)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 61, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 2.75f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 2.5f;
                    int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 74, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            return;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 80);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
    }
}