using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PurgeBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 10;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 320;
        }
        public override void AI()
        {
            if (Projectile.velocity.X != Projectile.velocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = -Projectile.velocity.X;
            }
            if (Projectile.velocity.Y != Projectile.velocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = -Projectile.velocity.Y;
            }
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Vector2 vector = Projectile.position;
                        vector -= Projectile.velocity * ((float)i * 0.25f);
                        Projectile.alpha = 255;
                        int dust = Dust.NewDust(vector, 1, 1, 127, 0f, 0f, 0, default(Color), 0.75f);
                        Main.dust[dust].position = vector;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                        Main.dust[dust].velocity *= 0.05f;
                    }
                }
            }
            return;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 127, damageType: "ranged");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ProjectileUtils.Explosion(Projectile, 127, damageType: "ranged");
            target.AddBuff(ModContent.BuffType<ChaosBurn>(), 180);
        }
    }
}