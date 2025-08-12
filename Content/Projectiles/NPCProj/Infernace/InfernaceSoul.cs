using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Infernace
{
    public class InfernaceSoul : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            for (int l = 0; l < 5; l++)
            {
                Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width * 0.5f, Projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.velocity.Y = Main.rand.NextFloat(-6, -1);
                dust.noGravity = true;
                dust.fadeIn = 1.1f;
                dust = Dust.NewDustPerfect(position, 31, Vector2.Zero);
                dust.velocity.Y = Main.rand.NextFloat(-10, -5);
                dust.noGravity = true;
                dust.fadeIn = 0.9f;
            }
            Projectile.velocity.Y -= 0.66f;
        }
    }
}