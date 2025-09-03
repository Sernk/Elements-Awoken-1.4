using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Gores
{
    public class IzarisShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 30;
            Projectile.timeLeft = 100000;
            Projectile.scale *= 1.3f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = (int)Projectile.ai[0];
            return true;
        }
        public override void AI()
        {
            Projectile.rotation += 0.1f;

            NPC parent = null;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<NPCs.Bosses.Ancients.ShardBase>())
                {
                    parent = Main.npc[i];
                }
            }
            if (parent != null)
            {
                float movespeed = 5f;
                if (Vector2.Distance(parent.Center, Projectile.Center) >= 60)
                {
                    movespeed = 12f;
                }
                Vector2 toTarget = new Vector2(parent.Center.X - Projectile.Center.X, parent.Center.Y - Projectile.Center.Y);
                toTarget = new Vector2(parent.Center.X - Projectile.Center.X, parent.Center.Y - Projectile.Center.Y);
                toTarget.Normalize();
                if (Vector2.Distance(parent.Center, Projectile.Center) >= 40)
                {
                    Projectile.velocity = toTarget * movespeed;
                }

                ProjectileUtils.PushOtherEntities(Projectile, new List<int>() { ModContent.ProjectileType<KirveinShard>(), ModContent.ProjectileType<XernonShard>(), ModContent.ProjectileType<KrecheusShard>() });

            }
            else if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0]++;
                ElementsAwoken.DebugModeText("Error: Shard Base not found");
            }
        }
    }
}