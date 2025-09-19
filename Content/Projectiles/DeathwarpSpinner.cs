using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles
{
    public class DeathwarpSpinner : ModProjectile
    {
        private int aiTimer = 0;
        private int shootTimer = 0;
        private float increase = 0f;
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deathwarp");
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Vector2 direction = player.Center - Projectile.Center;
            if (direction.X > 0f)
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = direction.ToRotation();
            }
            if (direction.X < 0f)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = direction.ToRotation() + 1.57f;
            }
            Projectile.rotation += MathHelper.ToRadians(45);
            if (Projectile.localAI[0] == 0)
            {
                SoundEngine.PlaySound(new SoundStyle(EAU.SoundPath("LaserCharge")), Projectile.position);
                Projectile.localAI[0]++;
            }
            aiTimer++;
            shootTimer--;
            Vector2 offset = new Vector2(100, 0);
            if (increase <= 0.25f)
            {
                increase += 0.001f;
                if (aiTimer > 30) increase += 0.001f;
                if (aiTimer > 45) increase += 0.001f;
                if (aiTimer > 50)  increase += 0.001f;
            }
            Projectile.ai[0] += increase;
            Projectile.Center = player.Center + offset.RotatedBy(Projectile.ai[0] * (Math.PI * 2 / 8));

            if (!player.active || player.dead) Projectile.Kill();

            if (increase >= 0.24f && shootTimer <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
                float Speed = -15f;
                float rotation = (float)Math.Atan2(Projectile.Center.Y - player.Center.Y, Projectile.Center.X - player.Center.X);
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ProjectileType<DeathwarpLaser>(), Projectile.damage, 0f, 0);
                shootTimer = 3;
            }
        }
    }
}