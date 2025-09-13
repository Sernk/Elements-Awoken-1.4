using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions.MinionProj
{
    public class GlobuleMinionSmall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Minions/GlobuleMinion"; } }
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.scale = 0.5f;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.X * 0.075f;
            Player player = Main.player[Projectile.owner];
            ProjectileUtils.PushOtherEntities(Projectile);
            if (!ProjectileUtils.Home(Projectile, 8f, 800f))
            {
                Vector2 toTarget = new Vector2(player.Center.X - Projectile.Center.X, player.Center.Y - Projectile.Center.Y);
                toTarget.Normalize();
                Projectile.velocity += toTarget * 0.25f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);
            for (int p = 1; p <= 2; p++)
            {
                float strength = p * 2f;
                int numDusts = p * 10;
                ProjectileUtils.OutwardsCircleDust(Projectile, EAU.PinkFlame, numDusts, strength, randomiseVel: true);
            }
        }
    }
}