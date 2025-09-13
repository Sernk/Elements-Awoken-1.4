using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles
{
    public class PutridGoop : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Putrid Trail");
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.05f;
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.width, 46, 0f, 0f, 150, default(Color), 0.75f)];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffType<FastPoison>(), 60);
        }
    }
}