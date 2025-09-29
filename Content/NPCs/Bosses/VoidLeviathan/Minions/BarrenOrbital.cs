using ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan;
using ElementsAwoken.EASystem.EAPlayer;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan.Minions
{
    public class BarrenOrbital : ModNPC
    {
        int projectileBaseDamage = 0;

        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Bosses/VoidLeviathan/Minions/BarrenSoul"; } }
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
        }
        public override void SetStaticDefaults()    
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, 1f, 0.2f, 0.55f);
            if (Main.masterMode)
            {
                if (MyWorld.awakenedMode) projectileBaseDamage = 22;
                else projectileBaseDamage = 16;
            }
            if (Main.expertMode)
            {
                if (MyWorld.awakenedMode) projectileBaseDamage = 64;
                else projectileBaseDamage = 64;
            }
            else projectileBaseDamage = 70;
            NPC parent = Main.npc[(int)NPC.ai[1]];
            NPC.ai[0] += 3f;
            int distance = 125;
            double rad = NPC.ai[0] * (Math.PI / 180); // angle to radians
            NPC.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - NPC.width / 2;
            NPC.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - NPC.height / 2;
            if (!parent.active) NPC.active = false;

            NPC.ai[2]--;
            if (NPC.ai[2] <= 0)
            {
                int projDamage = projectileBaseDamage;

                float Speed = 10f;
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);

                Projectile blast = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<ExtinctionBlast>(), projDamage, 0f, 0)];
                blast.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;

                NPC.ai[2] = Main.rand.Next(600, 800);
                if (Main.expertMode) NPC.ai[2] -= 100;
                if (MyWorld.awakenedMode) NPC.ai[2] -= 100;
            }
            NPC.rotation *= 0f;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}