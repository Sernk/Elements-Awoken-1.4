using ElementsAwoken.Content.NPCs.Bosses.TheGuardian;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.TheGuardian
{
    public class GuardianCircle : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 408;
            Projectile.height = 408;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
        }
        public override void AI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<TheGuardianFly>()))
            {
                Projectile.Kill();
            }
            //NPC parent = Main.npc[NPC.FindFirstNPC(mod.NPCType("TheGuardianFly"))];
            NPC parent = Main.npc[(int)Projectile.ai[1]];

            Projectile.Center = parent.Center;

            Projectile.rotation += 0.05f;
        }
    }
}