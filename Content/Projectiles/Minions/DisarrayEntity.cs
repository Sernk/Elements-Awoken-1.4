using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.Minions
{   
    public class DisarrayEntity : ModProjectile
    {
        public float shootTimer = 0f;
        public float shootTimer2 = 0f;
        private float shootAi = 0f;
        private float dashAi = 0f;
        private int dashCount = 0;
        Vector2 dashTargetPos = new Vector2();
        NPC dashTarget = null;
        NPC tpTarget = null;
        private float tpAi = -1f;
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 56;
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
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 9;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 16)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.ai[0] == 0 && Projectile.frame != 0) Projectile.frame = 0;
            else if (Projectile.ai[0] == 1)
            {
                if (Projectile.frame < 1) Projectile.frame = 1;
                if (Projectile.frame > 2) Projectile.frame = 1;
            }
            else if (Projectile.ai[0] == 2)
            {
                if (Projectile.frame < 3) Projectile.frame = 3;
                if (Projectile.frame > 5) Projectile.frame = 3;
            }
            else if (Projectile.ai[0] == 3)
            {
                if (Projectile.frame < 6) Projectile.frame = 6;
                if (Projectile.frame > 8) Projectile.frame = 6;
            }

            if (tpAi >= 0)
            {
                Texture2D texture = Request<Texture2D>("ElementsAwoken/Content/Projectiles/Minions/DisarrayCrystal").Value;
                Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) * 0.5f);
                for (int k = 0; k < 4; k++)
                {
                    Vector2 drawPos = new Vector2(Projectile.Center.X + 50 * (k >= 2 ? -1 : 1), Projectile.Center.Y - 50 * (k % 2 == 0 ? -1 : 1)) - Main.screenPosition + drawOrigin;
                    EAU.Sb.Draw(texture, drawPos, null, Color.White, 0f, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer<MyPlayer>();
            player.AddBuff(BuffType<CrystallineEntityBuff>(), 3600);
            if (player.dead) modPlayer.crystalEntity = false;
            if (modPlayer.crystalEntity) Projectile.timeLeft = 2;

            float intensity = 0.2f * (Projectile.ai[0] + 1);
            Lighting.AddLight(Projectile.Center, 1.2f * intensity, 0f * intensity, 1.5f * intensity);

            if (Projectile.ai[0] != 3)
            {
                shootTimer--;
                shootTimer2--;
                if (shootTimer2 <= 0) shootTimer2 = 120;
                if (Projectile.ai[0] < 3 && Projectile.ai[1] == 0)
                {
                    if (shootTimer <= 0)
                    {
                        if (Projectile.owner == Main.myPlayer)
                        {
                            float max = 400f;
                            for (int i = 0; i < Main.npc.Length; i++)
                            {
                                NPC nPC = Main.npc[i];
                                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height))
                                {
                                    float Speed = 9f;
                                    if (Projectile.ai[0] == 1) Speed = 11.5f;
                                    else if (Projectile.ai[0] == 2) Speed = 13.5f;
                                    if ((Projectile.ai[0] != 1 && shootTimer <= 0) || (shootTimer <= 0f && shootTimer2 <= 24))
                                    {
                                        SoundEngine.PlaySound(SoundID.Item28, Projectile.position);
                                        float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));

                                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, projSpeed.X, projSpeed.Y, ProjectileType<DisarrayShard>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                                        if (Projectile.ai[0] == 0) shootTimer = 75;
                                        else if (Projectile.ai[0] == 1)
                                        {
                                            shootTimer = 8;
                                        }
                                        else
                                        {
                                            shootTimer = 12;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (shootTimer == 5)
                    {
                        for (int k = 0; k < 20; k++)
                        {
                            int num5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<AncientPink>(), 0f, 0f, 200, default(Color), 1.2f);
                            Main.dust[num5].noGravity = true;
                            Main.dust[num5].velocity *= 0.75f;
                            Main.dust[num5].fadeIn = 1.8f;
                            Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                            vector.Normalize();
                            vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                            Main.dust[num5].velocity = vector;
                            vector.Normalize();
                            vector *= 34f;
                            Main.dust[num5].position = Projectile.Center - vector;
                        }
                    }
                }


                float targetLocX = Projectile.position.X;
                float targetLocY = Projectile.position.Y;
                float closestTarget = 900f;
                bool attacking = false;
                if (Projectile.ai[1] != 0)
                {
                    attacking = true;
                }
                int maxPlayerDist = 500;

                if (Math.Abs(Projectile.Center.X - player.Center.X) + Math.Abs(Projectile.Center.Y - player.Center.Y) > (float)maxPlayerDist)
                {
                    Projectile.localAI[0] = 1f;
                }
                if (Projectile.localAI[0] == 0f)
                {
                    Projectile.tileCollide = true;
                    NPC targettedNPC = Projectile.OwnerMinionAttackTargetNPC;
                    if (targettedNPC != null && targettedNPC.CanBeChasedBy(Projectile, false))
                    {
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * 200, (float)Math.Cos(angle) * 200);

                        float targetX = targettedNPC.Center.X + offset.X;
                        float targetY = targettedNPC.Center.Y + offset.Y;
                        float npcDist = Math.Abs(Projectile.Center.X - targetX) + Math.Abs(Projectile.Center.Y - targetY);
                        if (npcDist < closestTarget && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, targettedNPC.position, targettedNPC.width, targettedNPC.height))
                        {
                            closestTarget = npcDist;
                            targetLocX = targetX;
                            targetLocY = targetY;
                            attacking = true;
                        }
                    }
                    if (!attacking)
                    {
                        for (int i = 0; i < Main.npc.Length; i++)
                        {
                            NPC nPC = Main.npc[i];
                            if (nPC.CanBeChasedBy(Projectile, false))
                            {
                                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                Vector2 offset = new Vector2((float)Math.Sin(angle) * 200, (float)Math.Cos(angle) * 200);

                                float targetX = nPC.Center.X + offset.X;
                                float targetY = nPC.Center.Y + offset.Y;
                                float npcDist = Math.Abs(Projectile.Center.X - targetX) + Math.Abs(Projectile.Center.Y - targetY);
                                if (npcDist < closestTarget && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height))
                                {
                                    closestTarget = npcDist;
                                    targetLocX = targetX;
                                    targetLocY = targetY;
                                    attacking = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Projectile.tileCollide = false;
                }
                if (!attacking)
                {
                    float speed = 8f;
                    if (Projectile.localAI[0] == 1f) speed = 12f;
                    float goToX = player.Center.X - Projectile.Center.X;
                    float goToY = player.Center.Y - Projectile.Center.Y - 60f;
                    float targetDist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                    if (targetDist < 100f && Projectile.localAI[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                    {
                        Projectile.localAI[0] = 0f;
                    }
                    if (targetDist > 2000f) Projectile.Center = player.Center;
                    if (targetDist > 70f)
                    {
                        targetDist = speed / targetDist;
                        goToX *= targetDist;
                        goToY *= targetDist;
                        Projectile.velocity.X = (Projectile.velocity.X * 20f + goToX) / 21f;
                        Projectile.velocity.Y = (Projectile.velocity.Y * 20f + goToY) / 21f;
                    }
                    else
                    {
                        if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                        {
                            Projectile.velocity.X = -0.15f;
                            Projectile.velocity.Y = -0.05f;
                        }
                        Projectile.velocity *= 1.01f;
                    }
                    Projectile.rotation = Projectile.velocity.X * 0.05f;

                    if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                    {
                        Projectile.spriteDirection = -Projectile.direction;
                        return;
                    }
                }
                else
                {
                    if (Projectile.ai[0] != 3)
                    {
                        if (Projectile.ai[1] == 0)
                        {
                            float speed = 8f;
                            float goToX = targetLocX - Projectile.Center.X;
                            float goToY = targetLocY - Projectile.Center.Y;
                            float targetDist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                            if (targetDist < 100f)
                            {
                                speed = 10f;
                            }
                            targetDist = speed / targetDist;
                            goToX *= targetDist;
                            goToY *= targetDist;
                            Projectile.velocity.X = (Projectile.velocity.X * 14f + goToX) / 15f;
                            Projectile.velocity.Y = (Projectile.velocity.Y * 14f + goToY) / 15f;

                            if (Projectile.ai[0] == 2)
                            {
                                shootAi++;
                                if (shootAi > 300)
                                {
                                    shootAi = 0;
                                    Projectile.ai[1] = 1;
                                }
                            }
                        }
                        else
                        {
                            Projectile.tileCollide = false;
                            if (Projectile.localAI[1] == 0f)
                            {
                                for (int i = 0; i < Main.npc.Length; i++)
                                {
                                    NPC nPC = Main.npc[i];
                                    if (nPC.CanBeChasedBy(Projectile, false))
                                    {
                                        float npcDist = Math.Abs(Projectile.Center.X - nPC.Center.X) + Math.Abs(Projectile.Center.Y - nPC.Center.Y);
                                        if (nPC.active && npcDist < closestTarget && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height))
                                        {
                                            closestTarget = npcDist;
                                            dashTarget = nPC;
                                            dashTargetPos = nPC.Center;
                                        }
                                    }
                                }
                                if (dashTargetPos.X != 0 && dashTargetPos.Y != 0)
                                {
                                    dashAi++;
                                    if (dashAi >= 10)
                                    {
                                        float speed = 14f;
                                        Vector2 toTarget = new Vector2(dashTargetPos.X - Projectile.Center.X, dashTargetPos.Y - Projectile.Center.Y);
                                        toTarget.Normalize();
                                        Projectile.velocity = toTarget * speed;
                                        Projectile.localAI[1] = 1f;
                                        Projectile.netUpdate = true;
                                        dashAi = 0;
                                    }
                                }
                            }
                            else if (Projectile.localAI[1] == 1f)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<AncientPink>());
                                    Main.dust[dust].velocity *= 0.1f;
                                    Main.dust[dust].scale *= 1.2f;
                                    Main.dust[dust].noGravity = true;
                                }

                                dashAi += 1f;
                                if (dashAi >= 20f)
                                {
                                    Projectile.velocity *= 0.96f;
                                    if ((double)Projectile.velocity.X > -0.1 && (double)Projectile.velocity.X < 0.1)
                                    {
                                        Projectile.velocity.X = 0f;
                                    }
                                    if ((double)Projectile.velocity.Y > -0.1 && (double)Projectile.velocity.Y < 0.1)
                                    {
                                        Projectile.velocity.Y = 0f;
                                    }
                                }
                                int dashTime = 30;
                                if (dashAi >= dashTime)
                                {
                                    dashCount += 1;
                                    dashAi = 0f;
                                    if (dashCount >= 3)
                                    {
                                        Projectile.localAI[1] = 0f;
                                        Projectile.ai[1] = 0f;
                                        dashAi = 0f;
                                        dashCount = 0;
                                    }
                                    else
                                    {
                                        Projectile.localAI[1] = 0f;
                                        Projectile.velocity *= 0.1f;
                                    }
                                }
                            }
                            if (dashTarget == null || !dashTarget.active)
                            {
                                Projectile.ai[1] = 0f;
                            }
                        }
                    }
                    else
                    {

                    }
                    Projectile.rotation = Projectile.velocity.X * 0.05f;
                    if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                    {
                        Projectile.spriteDirection = -Projectile.direction;
                        return;
                    }
                }
            }
            if (Projectile.ai[0] == 3)
            {
                bool attacking = false;
                if (tpTarget != null)
                {
                    if (tpTarget.active) attacking = true;                
                    else FindTpTarget();
                }
                else FindTpTarget();
                if (!attacking)
                {
                    tpAi = -1f;

                    float speed = 8f;
                    if (Projectile.localAI[0] == 1f) speed = 12f;
                    float goToX = player.Center.X - Projectile.Center.X;
                    float goToY = player.Center.Y - Projectile.Center.Y - 60f;
                    float targetDist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                    if (targetDist < 100f && Projectile.localAI[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                    {
                        Projectile.localAI[0] = 0f;
                    }
                    if (targetDist > 2000f) Projectile.Center = player.Center;
                    if (targetDist > 70f)
                    {
                        targetDist = speed / targetDist;
                        goToX *= targetDist;
                        goToY *= targetDist;
                        Projectile.velocity.X = (Projectile.velocity.X * 20f + goToX) / 21f;
                        Projectile.velocity.Y = (Projectile.velocity.Y * 20f + goToY) / 21f;
                    }
                    else
                    {
                        if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                        {
                            Projectile.velocity.X = -0.15f;
                            Projectile.velocity.Y = -0.05f;
                        }
                        Projectile.velocity *= 1.01f;
                    }
                    Projectile.rotation = Projectile.velocity.X * 0.05f;

                    if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                    {
                        Projectile.spriteDirection = -Projectile.direction;
                        return;
                    }
                }
                else
                {
                    if (!tpTarget.active) attacking = false;
                    else
                    {
                        Projectile.spriteDirection = Math.Sign(Projectile.Center.X - tpTarget.Center.X);


                        tpAi++;
                        Dust dust2 = Main.dust[Dust.NewDust(new Vector2(tpTarget.Center.X, tpTarget.Center.Y), 25, 25, 6, 0, 0, 100)];
                        dust2.noGravity = true;
                        if (tpAi == 20)
                        {
                            int maxDist = 250;
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);

                            TpDust();
                            Projectile.Center = tpTarget.Center + offset;
                            Projectile.velocity = Vector2.Zero;
                            Projectile.rotation = 0f;
                            TpDust();
                        }
                        if (tpAi == 30)
                        {
                            float Speed = 22f;

                            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
                            float rotation = (float)Math.Atan2(Projectile.Center.Y - tpTarget.Center.Y, Projectile.Center.X - tpTarget.Center.X);
                            Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));

                            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, projSpeed.X, projSpeed.Y, ProjectileType<DisarrayBlast>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                            for (int k = 0; k < 4; k++)
                            {
                                Vector2 crystalPos = new Vector2(Projectile.Center.X + 50 * (k >= 2 ? -1 : 1), Projectile.Center.Y - 50 * (k % 2 == 0 ? -1 : 1));
                                float beamRotation = (float)Math.Atan2(crystalPos.Y - tpTarget.Center.Y, crystalPos.X - tpTarget.Center.X);
                                Vector2 beamSpeed = new Vector2((float)((Math.Cos(beamRotation) * Speed) * -1), (float)((Math.Sin(beamRotation) * Speed) * -1));
                                Projectile.NewProjectile(EAU.Proj(Projectile), crystalPos.X, crystalPos.Y, beamSpeed.X, beamSpeed.Y, ProjectileType<DisarrayBeam>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner);
                            }
                            tpAi = 0;
                        }
                    }
                }
            }
            ProjectileUtils.PushOtherEntities(Projectile);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.localAI[1] == 1f && Projectile.ai[0] != 0)
            {
                target.immune[Projectile.owner] = 10;
            }
        }

        private void FindTpTarget()
        {
            float max = 600f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                {
                    tpTarget = nPC;
                }
            }
        }
        private void TpDust()
        {
            for (int k = 0; k < 30; k++)
            {
                int num5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustType<AncientPink>(), 0f, 0f, 200, default(Color), 1.2f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.75f;
                Main.dust[num5].fadeIn = 1.8f;
                Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                Main.dust[num5].velocity = vector;
                vector.Normalize();
                vector *= 34f;
                Main.dust[num5].position = Projectile.Center - vector;
            }
        }
    }
}