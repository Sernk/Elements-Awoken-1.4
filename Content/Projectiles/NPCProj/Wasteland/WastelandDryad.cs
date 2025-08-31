using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Wasteland
{
    public class WastelandDryad : ModProjectile
    {  	
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 52;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 12000;
            Projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Wasteland Dryad");
            Main.projFrames[Projectile.type] = 2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] >= 360) Projectile.frame = 1;
            else Projectile.frame = 0;
            return true;
        }
        public override void AI()
        {
            Projectile.ai[1]++;
            int wastelandID = NPC.FindFirstNPC(NPCType<NPCs.Bosses.Wasteland.Wasteland>());
            Vector2 desiredLoc = Projectile.Center;
            if (wastelandID >= 0)
            {
                NPC parent = Main.npc[wastelandID];
                desiredLoc = parent.Center + new Vector2(200, -200 + (float)Math.Sin(Projectile.ai[1] / 20) * 20f);
            }
            if (!NPC.AnyNPCs(NPCType<NPCs.Bosses.Wasteland.Wasteland>()))
            {
                Projectile.ai[0] = 0;
                NPC dryad = Main.npc[NPC.FindFirstNPC(NPCID.Dryad)];
                desiredLoc = dryad.Center;
                Vector2 toTarget = new Vector2(desiredLoc.X - Projectile.Center.X, desiredLoc.Y - Projectile.Center.Y);
                toTarget.Normalize();
                Projectile.velocity = toTarget * 9;
                if (Vector2.Distance(desiredLoc, Projectile.Center) < 8)
                {
                    Projectile.Kill();
                    dryad.alpha = 0;
                }
            }
            else
            {
                NPC dryad = Main.npc[NPC.FindFirstNPC(NPCID.Dryad)];
                if (Projectile.ai[0] == 0)
                {
                    Vector2 toTarget = new Vector2(desiredLoc.X - Projectile.Center.X, desiredLoc.Y - Projectile.Center.Y);
                    toTarget.Normalize();
                    Projectile.velocity = toTarget * 9;
                    if (Vector2.Distance(desiredLoc, Projectile.Center) < 8) Projectile.ai[0] = 1;
                }
                else
                {
                    var e = ModContent.GetInstance<EALocalization>();
                    NPC parent = Main.npc[wastelandID];
                    Projectile.ai[0]++;
                    Projectile.Center = desiredLoc;
                    if (Projectile.ai[0] == 90) CombatText.NewText(Projectile.getRect(), Color.GreenYellow, e.WastelandDryad, false, false);
                    else if (Projectile.ai[0] == 180) CombatText.NewText(Projectile.getRect(), Color.GreenYellow, e.WastelandDryad1, false, false);
                    else if (Projectile.ai[0] >= 360)
                    {
                        Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 75)];
                        Vector2 toTarget = new Vector2(parent.Center.X - Projectile.Center.X, parent.Center.Y - Projectile.Center.Y);
                        toTarget.Normalize();
                        dust.velocity = toTarget * 24f;
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;

                        Dust dust2 = Main.dust[Dust.NewDust(parent.position, parent.width, parent.height, 75)];
                        dust2.noGravity = true;
                        dust2.fadeIn = 0.8f;
                        dust2.scale *= 2f;
                    }
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.OutwardsCircleDust(Projectile, 75, 36, 5f);
        }
    }
}