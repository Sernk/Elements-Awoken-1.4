using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.Content.Projectiles.Minions.MinionProj;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class GlobuleMinion : ModProjectile
    {
        public float hitNum = 0;
        public float splitCD = 900;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(hitNum);
            writer.Write(splitCD);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            hitNum = reader.ReadSingle();
            splitCD = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.tileCollide = false;
            Projectile.minionSlots = 2f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(BuffType<GlobuleMinionBuff>(), 3600);
            if (player.dead) modPlayer.globule = false;
            if (modPlayer.globule)  Projectile.timeLeft = 2;

            Projectile.rotation = Projectile.velocity.X * 0.075f;
            ProjectileUtils.PushOtherEntities(Projectile);
            splitCD--;
            if (splitCD == 0)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), ProjectileType<GlobuleMinionSmall>(), (int)(Projectile.damage * 1.25f), Projectile.knockBack, Main.myPlayer, 0f, Projectile.whoAmI);
                        SoundEngine.PlaySound(SoundID.Item94, Projectile.position);
                    }
                }
            }
            else if (splitCD <= 0)
            {
                Projectile.alpha = 255;
                Projectile.Center = player.Center + new Vector2(0, -80);
                if (CountProjectiles() <= 0)
                {
                    splitCD = 900;
                    int numDusts = 36;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.One * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                        Vector2 velocity = position - Projectile.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, EAU.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 4f;
                    }
                }
            }
            else
            {
                Projectile.alpha = 0;
                Vector2 targetPos = Projectile.position;
                float targetDist = 500;
                bool target = false;
                Projectile.tileCollide = true;
                NPC manualTarget = Projectile.OwnerMinionAttackTargetNPC;
                if (manualTarget != null)
                {
                    targetDist = Vector2.Distance(manualTarget.Center, Projectile.Center);
                    targetPos = manualTarget.Center;
                    target = true;
                }
                else
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC npc = Main.npc[k];
                        if (npc.CanBeChasedBy(this, false))
                        {
                            float distance = Vector2.Distance(npc.Center, Projectile.Center);
                            if ((distance < targetDist) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                            {
                                targetDist = distance;
                                targetPos = npc.Center;
                                target = true;
                            }
                        }
                    }
                }
                if (!target)
                {
                    Projectile.friendly = true;
                    float speed = 16f;
                    Vector2 toTarget = player.Center - Projectile.Center;
                    float dist = (float)Math.Sqrt(toTarget.X * toTarget.X + toTarget.Y * toTarget.Y);
                    if (dist < 100)
                    {
                        Projectile.velocity *= 0.97f;
                    }
                    else if (dist < 2000)
                    {
                        dist = speed / dist;
                        toTarget.X *= dist;
                        toTarget.Y *= dist;
                        Projectile.velocity.X = (Projectile.velocity.X * 20f + toTarget.X) / 21f;
                        Projectile.velocity.Y = (Projectile.velocity.Y * 20f + toTarget.Y) / 21f;
                    }
                    else
                    {
                        Projectile.Center = player.Center;
                    }
                  
                    Projectile.friendly = false;
                    return;
                }
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 1f;
                }
                if (Projectile.ai[1] == 0f)
                {
                    Projectile.ai[0]--;
                    Projectile.friendly = true;
                    float speed = 8f;
                    float num1042 = targetPos.X - Projectile.Center.X;
                    float num1041 = targetPos.Y - Projectile.Center.Y;
                    float dist = (float)Math.Sqrt(num1042 * num1042 + num1041 * num1041);
                    if (dist < 100f)
                    {
                        speed = 10f;
                    }
                    if (dist < 300f && Projectile.ai[0] <= 0)
                    {
                        Vector2 toTarget = new Vector2(num1042, num1041);
                        toTarget.Normalize();
                        toTarget *= 20f;
                        Projectile.velocity.X = toTarget.X;
                        Projectile.velocity.Y = toTarget.Y;

                        Projectile.ai[0] = 30;
                    }
                    dist = speed / dist;
                    num1042 *= dist;
                    num1041 *= dist;
                    Projectile.velocity.X = (Projectile.velocity.X * 14f + num1042) / 15f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 14f + num1041) / 15f;
                }
                else
                {
                    Projectile.friendly = false;
                    if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 10f)
                    {
                        Projectile.velocity *= 1.05f;
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffType<Buffs.Debuffs.Starstruck>(), 300);
            target.immune[Projectile.owner] = 5;

            hitNum++; 
            if (hitNum > 10)
            {
                hitNum = 0;
            }
            else if (hitNum > 3)
            {
                Projectile.ai[1] = 1f;
            }
            else
            {
                Projectile.ai[1] = 8f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
        private int CountProjectiles()
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == ProjectileType<GlobuleMinionSmall>() && Main.projectile[i].ai[1] == Projectile.whoAmI) num++;
            }
            return num;
        }
    }
}