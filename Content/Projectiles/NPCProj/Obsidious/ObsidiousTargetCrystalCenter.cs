using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousTargetCrystalCenter : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            Player player = Main.player[(int)Projectile.ai[1]];

            Projectile.localAI[1]++;
            if (Projectile.localAI[1] < 120)
            {
                Projectile.position.X = player.Center.X;
                Projectile.position.Y = player.Center.Y;
            }

            if (!player.active)
            {
                Projectile.Kill();
            }

            if (Projectile.localAI[0] == 0)
            {
                int swirlCount = 5;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    int orbital = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<ObsidiousTargetCrystal>(), Projectile.damage, Projectile.knockBack, 0, l * distance, Projectile.whoAmI);
                    Projectile Orbital = Main.projectile[orbital];

                }
                Projectile.localAI[0] = 1;
            }
        }
    }
}
    