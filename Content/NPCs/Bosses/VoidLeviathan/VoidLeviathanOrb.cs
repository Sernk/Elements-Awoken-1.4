//using ElementsAwoken.Projectiles.GlobalProjectiles;
using ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan
{
    [AutoloadBossHead]

    public class VoidLeviathanOrb : ModNPC
    {
        private float shootTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 40;

            NPC.aiStyle = -1;

            NPC.damage = 0;
            NPC.lifeMax = 30000;
            NPC.defense =  90;
            NPC.knockBackResist = 0f;

            NPC.scale = 1.5f;
            NPC.boss = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath56;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            //NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;

//            NPCsGLOBAL.ImmuneAllEABuffs(NPC);
            // all vanilla buffs
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void's Orb");
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            if (MyWorld.awakenedMode)
            {
                NPC.defense = 120;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0) Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<VoidOrbDestroyed>(), 0, 0f, Main.myPlayer);
        }
        public override bool PreKill()
        {
            return false;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            if (!NPC.AnyNPCs(ModContent.NPCType<VoidLeviathanHead>())) NPC.active = false;

            shootTimer--;
            if (shootTimer <= 0 && Vector2.Distance(P.Center, NPC.Center) < 600 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int projectileBaseDamage = 70;
                int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 1.8f);

                Projectile strike = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), ModContent.ProjectileType<VoidOrbProj>(), projDamage, 0f, Main.myPlayer)];
                //strike.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                shootTimer = 60;
            }

            if (NPC.ai[2] == 0)
            {
                if (!ModContent.GetInstance<Config>().lowDust)
                {
                    int numDusts = 20;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.One * new Vector2((float)NPC.width / 2f, (float)NPC.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + NPC.Center;
                        Vector2 velocity = position - NPC.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, DustID.Firework_Pink, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 6f;
                    }
                }
                NPC.scale = 0.1f;
                NPC.ai[2]++;
            }
            if (NPC.scale < 1.5) NPC.scale += 1f / 30f;
            else NPC.scale = 1.5f;

        }
    }
}