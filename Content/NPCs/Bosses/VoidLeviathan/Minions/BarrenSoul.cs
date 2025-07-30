using ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan.Minions
{
    public class BarrenSoul : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 44;

            NPC.damage = 0;
            NPC.defense = 20;
            NPC.lifeMax = 1000;
            NPC.knockBackResist = 0f;

            NPC.noGravity = true;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;

            AnimationType = 5;
            NPC.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Barren Soul");
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, 1f, 0.2f, 0.55f);
            NPC.ai[2]++;
            int minAlpha = 50;
            int projDamage = Main.expertMode ? 160 : 120;
            if (MyWorld.awakenedMode) projDamage = 140;
            if (NPC.ai[2] < 60)
            {
                if (NPC.alpha > minAlpha) NPC.alpha -= (255 - minAlpha) / 60;
            }
            else if (NPC.ai[2] == 150)
            {
                if (MyWorld.awakenedMode)
                {
                    float Speed = 10f;
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile beam = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<BarrenBeam>(), projDamage, 0f, 0)];
                    //beam.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
                else
                {
                    NPC.ai[0] = P.Center.X;
                    NPC.ai[1] = P.Center.Y;
                }
            }
            else if (NPC.ai[2] == 180)
            {
                if (!MyWorld.awakenedMode)
                {
                    float Speed = 10f;
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - NPC.ai[1], NPC.Center.X - NPC.ai[0]);
                    Projectile blast = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), Mod.Find<ModProjectile>("ExtinctionBlast").Type, projDamage, 0f, 0)];
                    //blast.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
            }
            else if (NPC.ai[2] > 180)
            {
                NPC.alpha += (255 - minAlpha) / 60;
                if (NPC.alpha >= 255) NPC.active = false;
            }
            NPC.rotation *= 0f;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}