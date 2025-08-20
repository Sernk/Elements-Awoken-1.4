using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Gores
{
    public class Lightbeam : ModProjectile
    {
        public float rotSpeed = 0.2f;
        public override void SetDefaults()
        {
            Projectile.width = 600;
            Projectile.height = 600;
            Projectile.timeLeft = 100000;
            Projectile.light = 3;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.rotation = Main.rand.NextFloat(0, 5);
                rotSpeed = Main.rand.NextFloat(-0.04f, 0.04f);
                Projectile.scale = Main.rand.NextFloat(0.5f, 1.2f);
                Projectile.alpha = Main.rand.Next(150, 220);

                Projectile.localAI[0]++;
            }
            Projectile.rotation += rotSpeed;

            if (!NPC.AnyNPCs(ModContent.NPCType<AncientAmalgamDeath>()))
            {
                Projectile.Kill();
            }
        }
    }
}