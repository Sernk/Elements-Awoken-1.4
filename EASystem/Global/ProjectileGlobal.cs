using ElementsAwoken.Content.Buffs;
using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Global
{
    public class ProjectileGlobal : GlobalProjectile
    {
        public bool dontScaleDamage = false;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.hostile)
            {
                if (MyWorld.awakenedMode && !dontScaleDamage)
                {
                    projectile.damage = (int)(projectile.damage * 1.5f);
                }
            }
            if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (projectile.type == ProjectileID.GreenLaser)
                {
                    projectile.penetrate = 2;
                }
            }
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            int damage = info.Damage;
            if (target.FindBuffIndex(ModContent.BuffType<FrostShield>()) != -1 && damage > 0 && projectile.active && !projectile.friendly && projectile.hostile)
            {
                if (Main.rand.Next(3) == 0)
                {
                    target.statLife += damage; // to stop damage
                    projectile.velocity.X = -projectile.velocity.X;
                    projectile.velocity.Y = -projectile.velocity.Y;
                }
            }
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            float damage = modifiers.SourceDamage.Base;
            if (Main.expertMode && dontScaleDamage && projectile.hostile)
            {
                damage = (int)(damage * 0.5f); // cut damage in half in expert 
            }
        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            float damage = modifiers.SourceDamage.Base;
            if (target.GetGlobalNPC<NPCsGLOBAL>().impishCurse)
            {
                damage = (int)(damage * 1.75f);
            }
        }
        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer<MyPlayer>();
            if (modPlayer.sonicArm && projectile.GetGlobalProjectile<EAProjectileType>().whip)
            {
                if (projectile.ai[0] == projectile.GetGlobalProjectile<EAProjectileType>().whipAliveTime / 2)
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, ModContent.ProjectileType<WhipCrack>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                    SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/WhipCrack"));
                }
            }
            if (projectile.CountsAsClass(DamageClass.Melee) && modPlayer.eaMagmaStone && !projectile.noEnchantments)
            {
                bool makeDust = Main.rand.Next(3) == 0;
                if (ModContent.GetInstance<Config>().lowDust) makeDust = Main.rand.Next(8) == 0;
                if (makeDust)
                {
                    int num70 = Dust.NewDust(new Vector2(projectile.position.X - 4f, projectile.position.Y - 4f), projectile.width + 8, projectile.height + 8, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num70].scale = 1.5f;
                    }
                    Main.dust[num70].noGravity = true;
                    Dust expr_B54_cp_0 = Main.dust[num70];
                    expr_B54_cp_0.velocity.X = expr_B54_cp_0.velocity.X * 2f;
                    Dust expr_B72_cp_0 = Main.dust[num70];
                    expr_B72_cp_0.velocity.Y = expr_B72_cp_0.velocity.Y * 2f;
                }
            }
            if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (projectile.type == ProjectileID.LastPrismLaser)
                {
                    Projectile laser = Main.projectile[(int)projectile.ai[1]];
                    if (laser.ai[0] >= 180) projectile.damage = (int)(projectile.damage * 0.5f);
                }
            }
            if (projectile.type == ProjectileID.LastPrism && modPlayer.prismPolish)
            {
                if (projectile.ai[0] < 180) projectile.ai[0]++;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            PlayerUtils playerUtils = player.GetModPlayer<PlayerUtils>();

            var source = projectile.GetSource_FromThis();
            bool crit = hit.Crit;

            if (!ModContent.GetInstance<Config>().vItemChangesDisabled)
            {
                if (projectile.type == ProjectileID.LastPrismLaser)
                {
                    target.immune[projectile.owner] = 20;
                }
            }

            if (modPlayer.noDamageCounter > 0) modPlayer.noDamageCounter = 0;

            if (projectile.friendly)
            {
                if (modPlayer.voidArmor)
                {
                    if (target.CanBeChasedBy(this) && !target.SpawnedFromStatue)
                    {
                        if (modPlayer.voidArmorHealCD <= 0)
                        {
                            float healAmount = Main.rand.Next(2, 8);
                            Projectile.NewProjectile(source, projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<VoidHeal>(), 0, 0f, projectile.owner, projectile.owner, healAmount); // ai 1 is how much it heals
                            modPlayer.voidArmorHealCD = Main.rand.Next(15, 45);
                        }
                    }
                }
                if (modPlayer.heartContainer && projectile.minion)
                {
                    if (target.CanBeChasedBy(this) && !target.SpawnedFromStatue)
                    {
                        if (Main.rand.Next(16) == 0)
                        {
                            float healAmount = Main.rand.Next(2, 8);
                            int num1 = projectile.owner;
                            Projectile.NewProjectile(source, projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<HeartContainerHeal>(), 0, 0f, projectile.owner, (float)num1, healAmount); // ai 1 is how much it heals
                        }
                    }
                }
                if (modPlayer.venomSample || modPlayer.vilePower)
                {
                    target.AddBuff(BuffID.Venom, 300);
                    target.AddBuff(BuffID.Poisoned, 300); 
                    for (int k = 0; k < 3; k++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 171, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
                    }
                }
                if (modPlayer.frozenGauntlet)
                {
                    target.AddBuff(BuffID.Frostburn, 200);
                }
                if (modPlayer.eaMagmaStone)
                {
                    if (Main.rand.Next(7) == 0)
                    {
                        target.AddBuff(BuffID.OnFire, 360);
                    }
                    else if (Main.rand.Next(3) == 0)
                    {
                        target.AddBuff(BuffID.OnFire, 120);
                    }
                    else
                    {
                        target.AddBuff(BuffID.OnFire, 60);
                    }
                }
                if (modPlayer.dragonmailGreathelm && projectile.CountsAsClass(DamageClass.Melee))
                {
                    target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
                }
                if (modPlayer.dragonmailHood && projectile.CountsAsClass(DamageClass.Magic))
                {
                    target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
                }
                if (modPlayer.dragonmailVisage && projectile.CountsAsClass(DamageClass.Ranged))
                {
                    target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);

                    if (Main.rand.Next(15) == 0)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 174, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                            Projectile.NewProjectile(source, projectile.position.X, projectile.position.Y, (float)Main.rand.Next(-35, 36) * 0.2f, (float)Main.rand.Next(-35, 36) * 0.2f, Mod.Find<ModProjectile>("GreekFire").Type, projectile.damage / 2, projectile.knockBack * 0.35f, Main.myPlayer, 0f, 0f);
                        }
                        SoundEngine.PlaySound(SoundID.Item14, projectile.position);
                    }
                }
                if (modPlayer.dragonmailMask && projectile.minion)
                {
                    target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
                }
                if (modPlayer.voidWalkerArmor == 1 && projectile.CountsAsClass(DamageClass.Melee))
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
                }
                if (modPlayer.voidWalkerArmor == 2 && projectile.CountsAsClass(DamageClass.Ranged))
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
                }
                if (modPlayer.voidWalkerArmor == 3 && projectile.CountsAsClass(DamageClass.Magic))
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
                }
                if (modPlayer.voidWalkerArmor == 4 && projectile.minion)
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 300);
                }
                if (modPlayer.abyssalRage > 0)
                {
                    target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 420);
                }
                if (modPlayer.neovirtuoBonus && Main.rand.Next(9) == 0 && projectile.type != ModContent.ProjectileType<NeovirtuoHoming>())
                {
                    if (modPlayer.neovirtuoTimer <= 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item84, player.position);
                        int neoDamage = 200;
                        int speed = 8;
                        Projectile.NewProjectile(source, player.Center.X, player.Center.Y, speed, speed, ModContent.ProjectileType<NeovirtuoHoming>(), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(source, player.Center.X, player.Center.Y, speed, -speed, ModContent.ProjectileType<NeovirtuoHoming>(), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(source, player.Center.X, player.Center.Y, -speed, speed, ModContent.ProjectileType<NeovirtuoHoming>(), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(source, player.Center.X, player.Center.Y, -speed, -speed, ModContent.ProjectileType<NeovirtuoHoming>(), neoDamage, 1.25f, Main.myPlayer, 0f, 0f);
                        modPlayer.neovirtuoTimer = 15;
                    }
                }
                if (modPlayer.immortalResolve)
                {
                    if (crit && modPlayer.immortalResolveCooldown <= 0)
                    {
                        int randLife = Main.rand.Next(1, 5);
                        if ((player.GetCritChance(DamageClass.Magic) < 10 && projectile.CountsAsClass(DamageClass.Magic)) && (player.GetCritChance(DamageClass.Melee) < 10 && projectile.CountsAsClass(DamageClass.Melee)) && (player.GetCritChance(DamageClass.Ranged) < 10 && projectile.CountsAsClass(DamageClass.Ranged)) && (player.GetCritChance(DamageClass.Throwing) < 10 && projectile.CountsAsClass(DamageClass.Throwing)))
                        {
                            randLife = Main.rand.Next(1, 18);
                        }
                        if ((player.GetCritChance(DamageClass.Magic) >= 10 && player.GetCritChance(DamageClass.Magic) < 25 && projectile.CountsAsClass(DamageClass.Magic)) && 
                            (player.GetCritChance(DamageClass.Melee) >= 10 && player.GetCritChance(DamageClass.Melee) < 25 && projectile.CountsAsClass(DamageClass.Melee)) && 
                            (player.GetCritChance(DamageClass.Ranged) >= 10 && player.GetCritChance(DamageClass.Ranged) < 25 && projectile.CountsAsClass(DamageClass.Ranged)) && 
                            (player.GetCritChance(DamageClass.Throwing) >= 10 && player.GetCritChance(DamageClass.Throwing) < 25 && projectile.CountsAsClass(DamageClass.Throwing)))
                        {
                            randLife = Main.rand.Next(1, 12);
                        }
                        if ((player.GetCritChance(DamageClass.Magic) >= 25 && player.GetCritChance(DamageClass.Magic) < 75 && projectile.CountsAsClass(DamageClass.Magic)) &&
                           (player.GetCritChance(DamageClass.Melee) >= 25 && player.GetCritChance(DamageClass.Melee) < 75 && projectile.CountsAsClass(DamageClass.Melee)) &&
                           (player.GetCritChance(DamageClass.Ranged) >= 25 && player.GetCritChance(DamageClass.Ranged) < 75 && projectile.CountsAsClass(DamageClass.Ranged)) &&
                           (player.GetCritChance(DamageClass.Throwing) >= 25 && player.GetCritChance(DamageClass.Throwing) < 75 && projectile.CountsAsClass(DamageClass.Throwing)))
                        {
                            randLife = Main.rand.Next(1, 8);
                        }
                        if ((player.GetCritChance(DamageClass.Magic) >= 75 && projectile.CountsAsClass(DamageClass.Magic)) && (player.GetCritChance(DamageClass.Melee) >= 75 && projectile.CountsAsClass(DamageClass.Melee)) && (player.GetCritChance(DamageClass.Ranged) >= 75 && projectile.CountsAsClass(DamageClass.Ranged)) && (player.GetCritChance(DamageClass.Throwing) >= 75 && projectile.CountsAsClass(DamageClass.Throwing)))
                        {
                            randLife = Main.rand.Next(1, 5);
                        }
                        player.statLife += randLife;
                        player.HealEffect(randLife);
                        modPlayer.immortalResolveCooldown = 10;
                    }
                }
                if (modPlayer.crowsArmor && modPlayer.crowsArmorCooldown <= 0)
                {
                    float lightningSpeed = 8f;
                    Vector2 spawnpoint = new Vector2(target.Center.X, target.Center.Y - 100);
                    float rotation = -(float)Math.Atan2(spawnpoint.X - target.Center.Y, spawnpoint.X - target.Center.X);
                    Vector2 speed = new Vector2((float)((Math.Cos(rotation) * lightningSpeed) * -1), (float)((Math.Sin(rotation) * lightningSpeed) * -1));

                    Vector2 vector94 = new Vector2(speed.X, speed.Y);
                    float ai = (float)Main.rand.Next(100);
                    Vector2 vector95 = Vector2.Normalize(vector94) * 2f;
                    Projectile.NewProjectile(source, spawnpoint.X, spawnpoint.Y, vector95.X, vector95.Y, ModContent.ProjectileType<CrowLightning>(), 100, 0f, Main.myPlayer, vector94.ToRotation(), ai);
                    Projectile.NewProjectile(source, spawnpoint.X, spawnpoint.Y, 0f, 0f, ModContent.ProjectileType<CrowStorm>(), 0, 0f, Main.myPlayer);
                    modPlayer.crowsArmorCooldown = 30;
                }
                if (modPlayer.cosmicGlass && crit && modPlayer.cosmicGlassCD <= 0 && projectile.type != ModContent.ProjectileType<ChargeRifleHalf>())
                {
                    if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(player.Center.Y - target.Center.Y, player.Center.X - target.Center.X);

                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        SoundEngine.PlaySound(SoundID.Item12, player.position);
                        Projectile.NewProjectile(source, player.Center.X, player.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<ChargeRifleHalf>(), 30, 3f, projectile.owner, 0f);
                        modPlayer.cosmicGlassCD = 3;
                    }
                }
                if (modPlayer.sufferWithMe && Main.rand.Next(4) == 0)
                {
                    target.AddBuff(ModContent.BuffType<ChaosBurn>(), 300, false);
                }
                int strikeChance = 10;
                if (NPC.downedBoss3) strikeChance = 7;
                if (Main.hardMode) strikeChance = 5;
                if (NPC.downedPlantBoss) strikeChance = 4;
                if (NPC.downedMoonlord) strikeChance = 2;
                if (modPlayer.strangeUkulele && Main.rand.Next(strikeChance) == 0 && projectile.type != ModContent.ProjectileType<UkuleleArc>())
                {
                    List<int> availableNPCs = new List<int>();
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC other = Main.npc[k];
                        if (other.active && !other.friendly && other.damage > 0 && !other.dontTakeDamage && Vector2.Distance(other.Center, player.Center) < 300)
                        {
                            availableNPCs.Add(other.whoAmI);
                        }
                    }
                    if (availableNPCs.Count > 0)
                    {
                        NPC arcTarget = Main.npc[availableNPCs[Main.rand.Next(availableNPCs.Count)]];
                        if (arcTarget.active && !arcTarget.friendly && arcTarget.damage > 0 && !arcTarget.dontTakeDamage)
                        {
                            SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ElectricArcing"));

                            float Speed = 9f;
                            float rotation = (float)Math.Atan2(player.Center.Y - target.Center.Y, player.Center.X - target.Center.X);
                            rotation += MathHelper.ToRadians(Main.rand.Next(-60, 60));
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            Projectile.NewProjectile(source, player.Center.X, player.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<UkuleleArc>(), (int)(projectile.damage * 0.5f), 3f, player.whoAmI, arcTarget.whoAmI);
                        }
                    }
                }
                if (modPlayer.bleedingHeart)
                {
                    if (target.life <= 0 && playerUtils.enemiesKilledLast10Secs >= 4 && !target.SpawnedFromStatue)
                    {
                        player.AddBuff(ModContent.BuffType<Bloodbath>(), 600, false);
                    }
                }
                if (modPlayer.putridArmour)
                {
                    if (projectile.minion && projectile.minionSlots != 0 && Main.rand.NextBool(3))
                    {
                        target.AddBuff(ModContent.BuffType<FastPoison>(), 60, false);
                    }
                }
            }
        }
    }
}