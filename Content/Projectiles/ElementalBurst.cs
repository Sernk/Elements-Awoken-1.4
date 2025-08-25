using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ElementalBurst : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 40;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.9f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 5f)
            {
                Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 63, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 3.75f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Slow, 200);
            target.AddBuff(BuffID.OnFire, 200);
            target.AddBuff(BuffID.VortexDebuff, 200);
            target.AddBuff(BuffID.Frostburn, 200);
            target.AddBuff(BuffID.Wet, 200);
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 200);
        }
    }
}