using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AbyssalBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 1f;
            Projectile.extraUpdates = 1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
        }
        public override void AI()
        {
            Dust fire = Main.dust[Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, Main.rand.NextBool() ? 234 : DustID.Firework_Pink, Projectile.velocity.X * 0.6f, Projectile.velocity.Y * 0.6f, 130, default(Color), 3.75f)];
            fire.velocity *= 0.6f;
            fire.scale *= 0.6f;
            fire.noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, Main.rand.NextBool() ? 234 : DustID.Firework_Pink, 0f, 0f, 100, default);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}