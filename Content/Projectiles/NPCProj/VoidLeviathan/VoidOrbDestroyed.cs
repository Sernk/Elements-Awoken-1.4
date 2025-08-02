using ElementsAwoken.Content.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidOrbDestroyed : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 86;
            Projectile.height = 86;
            Projectile.tileCollide = false;
            Projectile.scale = 1.2f;
            Projectile.timeLeft = 300;
            Projectile.alpha = 60;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.2f, 0.55f);
            Projectile.ai[0]++;
            int deathDuration = 20;
            Projectile.scale += 3f / deathDuration;
            Projectile.alpha += (255 - 60) / deathDuration;
            if (Projectile.alpha >= 255) Projectile.Kill();
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 80, true);
        }
    }
}