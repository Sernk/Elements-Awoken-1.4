using ElementsAwoken.Content.Buffs.Debuffs;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CorroderSpit : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.3f, 0.9f, 0.6f);

            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 74)];
                dust.velocity *= 0.6f;
                dust.position -= Projectile.velocity / 8f * (float)i;
                dust.noGravity = true;
                dust.scale = 0.8f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Corroding>(), 300, false);
            SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/AcidHiss"), Projectile.Center);
        }
    }
}