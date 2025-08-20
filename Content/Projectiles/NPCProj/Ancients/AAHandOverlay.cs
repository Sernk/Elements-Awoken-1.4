using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients
{
    public class AAHandOverlay : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 72;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
            Projectile.scale *= 1.3f;
        }
        public override void AI()
        {
            NPC npc = Main.npc[(int)Projectile.ai[1]];
            Vector2 offset = new Vector2(30, 0);
            if (npc != Main.npc[0])
            {
                Projectile.rotation = npc.rotation;
                Projectile.direction = npc.direction;
                Projectile.spriteDirection = -npc.spriteDirection;
                Projectile.alpha = (int)npc.ai[2];

                Projectile.position.X = npc.position.X;
                Projectile.position.Y = npc.position.Y;
            }
            if (!npc.active)
            {
                Projectile.Kill();
            }
        }
    }
}
    