using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Wasteland
{
    public class AcidBall : ModProjectile
    {  	
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.hostile = true;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.velocity.Y += 0.16f;

            for (int i = 0; i < 3; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 75, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 0f;
            }
            if (Projectile.timeLeft <= 60)
            {
                if (Main.rand.Next(60) == 0)
                {
                    Projectile.Kill();
                }
            }
            if (Vector2.Distance(Main.player[Main.myPlayer].Center, Projectile.Center) <= 75)
            {
                if (Main.rand.Next(15) == 0)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<AcidAuraBase>(), 0, 0f, Projectile.owner, 0f, 0f);
            SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/AcidHiss"), Projectile.position);
        }
    }
}