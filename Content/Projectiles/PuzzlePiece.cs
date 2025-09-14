using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PuzzlePiece : ModProjectile
    {
        public int type = 0;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = type;
            return true;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                type = Main.rand.Next(0, 3);
                Projectile.localAI[0] = 1;
            }
            Projectile.velocity.Y += 0.16f;

            Projectile.rotation += Main.rand.NextFloat(0.08f, 0.1f);
        }

        public override void OnKill(int timeLeft)
        {        
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}