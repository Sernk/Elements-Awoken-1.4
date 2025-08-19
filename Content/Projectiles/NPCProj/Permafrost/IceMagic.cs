using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Permafrost
{
    public class IceMagic : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 44;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 4)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void AI()
        {
            Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(1f));
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 180)
            {
                Projectile.velocity *= 0.96f;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Frostburn, 120, false);
        }
        public override void OnKill(int timeLeft)
        {
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                int numDusts = 30;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (Vector2.One * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                    Vector2 velocity = position - Projectile.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, 135, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 5f;
                }
            }
        }
    }
}