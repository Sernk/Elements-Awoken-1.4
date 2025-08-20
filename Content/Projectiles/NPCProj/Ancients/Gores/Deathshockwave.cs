using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Gores
{
    public class DeathShockwave : ModProjectile
    {
        public bool[] hasPushed = new bool[255];

        private int rippleCount = 3;
        private int rippleSize = 15;
        private int rippleSpeed = 15;
        private float distortStrength = 300f;

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.timeLeft = 400;
            Projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("");
        }
        public override void AI()
        {
            Projectile.ai[0] += 8;
            Player player = Main.player[Main.myPlayer];
            if (!hasPushed[player.whoAmI] && Vector2.Distance(player.Center, Projectile.Center) < Projectile.ai[0])
            {
                Vector2 toTarget = new Vector2(Projectile.Center.X - player.Center.X, Projectile.Center.Y - player.Center.Y);
                toTarget.Normalize();
                player.velocity -= toTarget * 15f;
                hasPushed[player.whoAmI] = true;
            }
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = 1;
                if (!Filters.Scene["Shockwave"].IsActive())
                {
                    Filters.Scene.Activate("Shockwave", Projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Projectile.Center);
                }
            }
            else
            {
                Projectile.ai[1]++;
                float progress = Projectile.ai[1] / 60f;
                float distortStrength = 200;
                Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
            }
        }
        public override void OnKill(int timeLeft)
        {
            Filters.Scene["Shockwave"].Deactivate();
        }
    }
}