using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Infernace
{
    public class InfernalMonolithSpawn : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            float numDusts = 64f;
            Vector2 shape = new Vector2(50f, 10f);

            for (int k = 0; k < numDusts; k++)
            {
                Vector2 vector11 = Vector2.UnitX * 0f;
                vector11 += -Vector2.UnitY.RotatedBy((double)((float)k * (6.28318548f / numDusts)), default(Vector2)) * shape;
                vector11 = vector11.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                Dust dust = Main.dust[Dust.NewDust(Projectile.Center, 0, 0, 6, 0f, 0f, 0, default(Color), 1f)];
                dust.scale = 1f;
                dust.noGravity = true;
                dust.position = Projectile.Center + vector11;
                dust.velocity = new Vector2(0, Main.rand.NextFloat(-4f, 0f));
            }
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y + 40, 0f, 0f, ModContent.ProjectileType<InfernalMonolith>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
            SoundEngine.PlaySound(SoundID.Item69, Projectile.position);
        }
    }
}