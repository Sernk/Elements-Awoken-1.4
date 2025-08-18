using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousRockLarge : ModProjectile
    {   	
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Projectile.rotation += 3;

            for (int i = 0; i < 2; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 75, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 0f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 75, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}