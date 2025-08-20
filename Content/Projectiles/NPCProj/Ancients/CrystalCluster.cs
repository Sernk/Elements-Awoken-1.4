using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients
{
    public class CrystalCluster : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.ignoreWater = true;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.localAI[0] <= 90)
            {
                return false;
            }

            return base.CanHitPlayer(target);

        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.scale = 0.1f;
            }
            
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] <= 60)
            {
                Projectile.scale += 0.01f;
            }
            else
            {
                Projectile.scale += 0.05f;
            }
            if (Projectile.scale >= 1.5f)
            {
                Projectile.scale = 1.5f;

                Projectile.localAI[1]++;
                if (Projectile.localAI[1] > 15)
                {
                    Projectile.Kill();
                    SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
                }
            }
            if (Main.rand.Next(3) == 0 && !ModContent.GetInstance<Config>().lowDust)
            {
                int dustBoxWidth = (int)(Projectile.width * Projectile.scale);
                int dust = Dust.NewDust(Projectile.Center - new Vector2(dustBoxWidth / 2, dustBoxWidth / 2), dustBoxWidth, dustBoxWidth, EAU.GetDustID(), 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }      
        public override void OnKill(int timeLeft)
        {
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                for (int k = 0; k < 12; k++)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.GetDustID(), 0f, 0f, 100, default(Color));
                    Main.dust[dust].noGravity = true;
                }
            }
        }
    }
}
    