using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AbyssalTentacle : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
        }
        public override void AI()
        {
            Vector2 center10 = Projectile.Center;
            Projectile.scale = 1f - Projectile.localAI[0];
            Projectile.width = (int)(20f * Projectile.scale);
            Projectile.height = Projectile.width;
            Projectile.position.X = center10.X - (float)(Projectile.width / 2);
            Projectile.position.Y = center10.Y - (float)(Projectile.height / 2);
            if ((double)Projectile.localAI[0] < 0.1)
            {
                Projectile.localAI[0] += 0.01f;
            }
            else
            {
                Projectile.localAI[0] += 0.025f;
            }
            if (Projectile.localAI[0] >= 0.95f)
            {
                Projectile.Kill();
            }
            Projectile.velocity.X = Projectile.velocity.X + Projectile.ai[0] * 1.5f;
            Projectile.velocity.Y = Projectile.velocity.Y + Projectile.ai[1] * 1.5f;
            if (Projectile.velocity.Length() > 16f)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= 16f;
            }
            Projectile.ai[0] *= 1.05f;
            Projectile.ai[1] *= 1.05f;
            if (Projectile.scale < 1f)
            {
                int num892 = 0;
                while ((float)num892 < Projectile.scale * 10f)
                {
                    int num893 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Main.rand.NextBool() ? 54 : DustID.Firework_Pink, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.1f);
                    Main.dust[num893].position = (Main.dust[num893].position + Projectile.Center) / 2f;
                    Main.dust[num893].noGravity = true;
                    Dust dust = Main.dust[num893];
                    dust.velocity *= 0.1f;
                    dust = Main.dust[num893];
                    dust.velocity -= Projectile.velocity * (1.3f - Projectile.scale);
                    Main.dust[num893].fadeIn = (float)(100 + Projectile.owner);
                    dust = Main.dust[num893];
                    dust.scale += Projectile.scale * 0.75f;
                    int num3 = num892;
                    num892 = num3 + 1;
                }
                return;
            }
        }
    }
}