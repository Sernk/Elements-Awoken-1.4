using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class DragonBatFireball : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 0;
            Projectile.alpha = 255;
            Projectile.timeLeft = 150;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Bat");
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                Projectile.localAI[0] = 1f;
            }
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            Main.dust[dust].velocity *= 0.1f;
            if (Projectile.velocity == Vector2.Zero)
            {
                Main.dust[dust].velocity.Y -= 1f;
                Main.dust[dust].scale = 1.5f;
            }
            else
            {
                Main.dust[dust].velocity += Projectile.velocity * 0.2f;
            }
            Main.dust[dust].position.X = Projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].position.Y = Projectile.Center.Y + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.5f;
        }
    }
}