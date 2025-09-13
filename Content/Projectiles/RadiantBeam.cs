using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles
{
    public class RadiantBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 4;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 170;
            Projectile.tileCollide = false;
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
            int dustLength = 8;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.scale *= 1.5f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life <= 0 && Main.myPlayer == Projectile.owner)
            {
                ProjectileUtils.OutwardsCircleDust(Projectile, EAU.PinkFlame, 36, 6f, randomiseVel: true, dustScale: 2f, dustFadeIn: 2.4f);
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), ProjectileType<RadiantStarHoming>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Main.myPlayer, 0f, Projectile.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item94, Projectile.position);
                }
            }
        }
    }
}