using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CosmicCrusherSpawner : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 30;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;

            Projectile.localAI[1]++;
            if (Projectile.localAI[1] % 6 == 0)
            {
                float shootSpeed = 9f;
                Vector2 targetPos = new Vector2(Projectile.ai[0], Projectile.ai[1]);
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 shootVel = targetPos - Projectile.Center;
                    if (shootVel == Vector2.Zero)
                    {
                        shootVel = new Vector2(0f, 1f);
                    }
                    shootVel.Normalize();
                    shootVel *= shootSpeed;
                    Vector2 perturbedSpeed = new Vector2(shootVel.X, shootVel.Y).RotatedByRandom(MathHelper.ToRadians(20));
                    int proj = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CosmicCrusherBlade>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].timeLeft = 300;
                    Main.projectile[proj].netUpdate = true;
                    Projectile.netUpdate = true;
                }
            }
        }      
    }
}