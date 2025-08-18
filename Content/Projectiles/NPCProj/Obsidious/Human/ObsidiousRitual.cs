using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousRitual : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 108;
            Projectile.height = 108;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hostile = true;
            Projectile.timeLeft = 100000;
        }
        public override void AI()
        {
            NPC npc = Main.npc[(int)Projectile.ai[1]];
            if (npc != Main.npc[0])
            {
                Projectile.Center = npc.Center;
            }
            if (!npc.active)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.timeLeft = 100000;
            }
            Projectile.rotation += 0.1f;
            Projectile.alpha = npc.alpha;
        }
    }
}