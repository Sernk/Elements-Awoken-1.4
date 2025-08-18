using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousHandOverlay : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 76;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
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