using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class AnarchyP : ModProjectile
    {
        int shootTimer = 0;
        int shootTimer2 = 0;
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 450f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 21f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 3;
            target.AddBuff(ModContent.BuffType<ChaosBurn>(), 180);
        }
        public override void AI()
        {
            var p = EAU.Proj(Projectile);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            shootTimer--;
            shootTimer2--;
            if (shootTimer <= 0)
            {
                int choice = Main.rand.Next(7);

                float speedX = 0;
                float speedY = 0;

                if (choice == 0)
                {
                    speedX = 0;
                    speedY = 8;
                }
                if (choice == 1)
                {
                    speedX = 3;
                    speedY = 8;
                }
                if (choice == 2)
                {
                    speedX = 5;
                    speedY = 8;
                }
                if (choice == 3)
                {
                    speedX = 8;
                    speedY = 8;
                }
                if (choice == 4)
                {
                    speedX = 8;
                    speedY = 5;
                }
                if (choice == 5)
                {
                    speedX = 8;
                    speedY = 3;
                }
                if (choice == 6)
                {
                    speedX = 8;
                    speedY = 0;
                }
                Projectile.NewProjectile(p, Projectile.Center.X, Projectile.Center.Y, speedX, speedY, ModContent.ProjectileType<AnarchyWave>(), Projectile.damage * 4, 0, Projectile.owner);
                Projectile.NewProjectile(p, Projectile.Center.X, Projectile.Center.Y, -speedX, -speedY, ModContent.ProjectileType<AnarchyWave>(), Projectile.damage * 4, 0, Projectile.owner);
                shootTimer = 30 + Main.rand.Next(0, 60);
            }
            if (shootTimer2 <= 0)
            {
                int numberProjectiles = 3;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                    value15.X *= 0.25f;
                    value15.Y *= 0.25f;
                    Projectile.NewProjectile(p, Projectile.Center.X, Projectile.Center.Y, value15.X, value15.Y, ModContent.ProjectileType<AnarchyLaser>(), Projectile.damage, 2f, Projectile.owner, 0f, 0f);
                }
                shootTimer2 = 9 + Main.rand.Next(0, 4);
            }
        }
    }
}