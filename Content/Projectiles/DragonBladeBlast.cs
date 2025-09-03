using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DragonBladeBlast : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            Projectile.Center = Main.player[Projectile.owner].Center + new Vector2(0, 4);

            float max = 70;
            Projectile.ai[0] += max / 8f;
            if (Projectile.ai[0] > max) Projectile.Kill();
            for (int i = 0; i < 120; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * Projectile.ai[0], (float)Math.Cos(angle) * Projectile.ai[0]);
                Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset, 0, 0, 6, 0, 0, 100)];
                dust.noGravity = true;
            }
            if (Projectile.ai[0] == max)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (!npc.townNPC && npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, Projectile.Center) < max + 20)
                    {
                        npc.AddBuff(BuffID.OnFire, 120);
                        Vector2 toTarget = new Vector2(Projectile.Center.X - npc.Center.X, Projectile.Center.Y - npc.Center.Y);
                        toTarget.Normalize();
                        npc.velocity -= toTarget * 10 * npc.knockBackResist;
                        if (!npc.noGravity) npc.velocity.Y -= 4 * npc.knockBackResist;
                    }
                }
            }
        }
    }
}