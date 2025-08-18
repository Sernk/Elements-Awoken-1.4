using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousIceCrystalSpin : ModProjectile
    {	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 220;
            DrawOffsetX = -10;
            DrawOriginOffsetY = -10;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] < 30f)
            {
                Player player = Main.player[Projectile.owner];
                Vector2 direction = player.Center - Projectile.Center;
                Projectile.rotation = direction.ToRotation() + 1.57f;

                NPC parent = Main.npc[(int)Projectile.ai[1]];
                Vector2 offset = new Vector2(45, 0);

                    int distance = 50;
                    double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
                    Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
                    Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;
                
                if (!parent.active)
                {
                    Projectile.Kill();
                }
            }
            else if (Projectile.localAI[0] == 30f)
            {
                float speed = 15f;
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - Projectile.position.Y, Main.player[Main.myPlayer].position.X - Projectile.position.X);
                Projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
                SoundEngine.PlaySound(SoundID.DD2_SonicBoomBladeSlash, Projectile.position);
            }
            else if (Projectile.localAI[0] > 30f)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 135, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].velocity *= 0f;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 135, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}