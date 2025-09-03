using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BlightRang : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/BlightRing"; } }

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 1600;
            AIType = 52;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blight Ring");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Corroding>(), 300);
            target.immune[Projectile.owner] = 4;
        }
        public override void AI()
        {
            for (int l = 0; l < 3; l++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 74, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= Main.rand.NextFloat(0.5f, 0.9f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}