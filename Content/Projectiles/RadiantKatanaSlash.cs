using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class RadiantKatanaSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 48;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void AI()
        {
            float addRad = 0;
            if (Projectile.direction == -1) addRad = 3.14f;
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + addRad;
            Vector2 dir = Projectile.velocity;
            dir.Normalize();
            Player player = Main.player[Projectile.owner];
            player.velocity = dir * 25f;
            player.immune = true;
            player.direction = Math.Sign(Projectile.velocity.X);
            Projectile.Center = player.Center + dir * 7;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Starstruck>(), 300);
            target.immune[Projectile.owner] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 4)
                {
                    Projectile.Kill();
                    float maxSpeed = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                    if (Math.Abs(player.velocity.X) > maxSpeed) player.velocity.X = maxSpeed * player.direction;
                }
               
            }
            return true;
        }
    }
}
