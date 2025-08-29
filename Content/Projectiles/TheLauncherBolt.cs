using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class TheLauncherBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
            }
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);

            float numDusts = 3;
            for (int i = 0; i < numDusts; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 15)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / numDusts * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.velocity.Y -= 10 * target.knockBackResist;
        }
    }
}