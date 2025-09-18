using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PlasmaThrowerBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 320;
        }
        public override void AI()
        {
            if (Projectile.ai[1] % 2 == 0)
            {
                Projectile.height += 1;
                Projectile.width += 1;
            }
            if (Projectile.ai[1] % 4 == 0)
            {
                Projectile.Center -= new Vector2(1, 1);
            }
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
            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 7)
            {
                float dustAmount = 4;
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 vector = Projectile.position;
                    vector -= Projectile.velocity * ((float)i / dustAmount);
                    Projectile.alpha = 255;
                    int dust = Dust.NewDust(vector, Projectile.width, Projectile.height, Main.rand.NextBool() ? 6 : 127, 0f, 0f, 0, default(Color), 0.35f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.05f;
                    Main.dust[dust].fadeIn = 1.4f;
                }
            }
            return;
        }     
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}