﻿using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Buffs.Other;
using ElementsAwoken.Content.Buffs.PotionBuffs;
using ElementsAwoken.Content.Dusts;
using ElementsAwoken.Content.Items.Accessories;
using ElementsAwoken.Content.Items.Donator.Buildmonger;
using ElementsAwoken.Content.Items.Donator.Crow;
using ElementsAwoken.Content.Items.Weapons.Thrown;
using ElementsAwoken.Content.NPCs.Town;
using ElementsAwoken.EAUtilities;
using ElementsAwoken.NPCs.Town;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.EASystem.EAPlayer
{
    public class NPCsGLOBAL : GlobalNPC, ILocalizedModType
    {
        public string LocalizationCategory => "NPCsGLOBALLocalization";

        int PinkFlame = DustID.Firework_Pink;
        public bool iceBound = false;
        public bool extinctionCurse = false;
        public bool handsOfDespair = false;
        public bool endlessTears = false;
        public bool ancientDecay = false;
        public bool soulInferno = false;
        public bool dragonfire = false;
        public bool discordDebuff = false;
        public bool chaosBurn = false;
        public bool electrified = false;
        public bool acidBurn = false;
        public bool corroding = false;
        public bool fastPoison = false;
        public bool starstruck = false;
        public bool impishCurse = false;
        public bool variableLifeDrain = false;
        public int lifeDrainAmount = 0;
        public float generalTimer = 0;
        public bool hasHands = false;
        public bool delete = false;
        public bool shrinking = false;
        public bool shrunk = false;
        public bool growing = false;
        public bool grown = false;
        public bool storedScale = false;
        public float initialScale = 1f;

        public override void ResetEffects(NPC npc)
        {
            iceBound = false;
            extinctionCurse = false;
            handsOfDespair = false;
            endlessTears = false;
            ancientDecay = false;
            soulInferno = false;
            dragonfire = false;
            discordDebuff = false;
            chaosBurn = false;
            electrified = false;
            acidBurn = false;
            corroding = false;
            fastPoison = false;
            starstruck = false;
            impishCurse = false;
            variableLifeDrain = false;
        }
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override void Load()
        {
            _ = this.GetLocalization("NoHit.Nohit").Value;
            _ = this.GetLocalization("Chat.Say").Value;
            _ = this.GetLocalization("Chat.Say1").Value;
            _ = this.GetLocalization("Chat.Say2").Value;
            _ = this.GetLocalization("Chat.Say3").Value;
            _ = this.GetLocalization("Chat.Say4").Value;
            _ = this.GetLocalization("Chat.Say5").Value;
            _ = this.GetLocalization("Chat.Say6").Value;
            _ = this.GetLocalization("Chat.Say7").Value;
            _ = this.GetLocalization("Chat.Say8").Value;
            _ = this.GetLocalization("Chat.Say9").Value;
        }
        public static void ImmuneAllEABuffs(NPC npc)
        {
            npc.buffImmune[BuffType<IceBound>()] = true;
            npc.buffImmune[BuffType<ExtinctionCurse>()] = true;
            npc.buffImmune[BuffType<HandsOfDespair>()] = true;
            npc.buffImmune[BuffType<EndlessTears>()] = true;
            npc.buffImmune[BuffType<AncientDecay>()] = true;
            npc.buffImmune[BuffType<SoulInferno>()] = true;
            npc.buffImmune[BuffType<Dragonfire>()] = true;
            npc.buffImmune[BuffType<Discord>()] = true;
            npc.buffImmune[BuffType<FastPoison>()] = true;
        }
        public override void AI(NPC npc)
        {
            generalTimer++;
            if (extinctionCurse)
            {
                if (!GetInstance<Config>().lowDust)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        float speedScale = 1;
                        if (j == 1) speedScale = 1.2f;
                        else if (j == 2) speedScale = 0.5f;
                        else if (j == 3) speedScale = 0.9f;
                        else if (j == 4) speedScale = 1.5f;

                        int distance = (int)(npc.width / 2 + 20);
                        double rad = ((generalTimer / 30) * speedScale) + npc.whoAmI + j * 60 * (Math.PI / 180); // angle to radians
                        Vector2 dustCenter = npc.Center - new Vector2((int)(Math.Cos(rad) * distance), (int)(Math.Sin(rad) * distance));

                        int maxDist = 8;
                        if (j == 1) maxDist = 10;
                        else if (j == 2) maxDist = 15;
                        else if (j == 3) maxDist = 5;
                        else if (j == 4) maxDist = 10;

                        int numDusts = (int)(maxDist / 2);
                        for (int i = 0; i < numDusts; i++)
                        {
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                            Dust dust = Main.dust[Dust.NewDust(dustCenter + offset - Vector2.One * 4, 0, 0, PinkFlame, 0, 0, 100)];
                            dust.noGravity = true;
                            dust.velocity *= 0.2f;
                        }
                    }
                }
            }
            if (starstruck)
            {
                for (int j = 0; j < 5; j++)
                {
                    int distance = (int)(npc.width * 1.5f * ((Math.Sin(generalTimer / 10) + 1) / 2));
                    double rad = (generalTimer * 2 * (Math.PI / 180)) + (MathHelper.ToRadians(360 / 5) * j) + MathHelper.ToRadians(npc.whoAmI * 15);
                    Vector2 dustCenter = npc.Center - new Vector2((int)(Math.Cos(rad) * distance), (int)(Math.Sin(rad) * distance));

                    Dust dust = Main.dust[Dust.NewDust(dustCenter, 4, 4, PinkFlame)];
                    dust.noGravity = true;
                    dust.velocity *= 0.2f;
                    dust.fadeIn = 1f;
                    dust.scale = 0.3f;
                }
            }
            bool immune = false;
            foreach (int k in ElementsAwoken.instakillImmune)
            {
                if (npc.type == k)
                {
                    immune = true;
                }
            }
            if (!immune && npc.active && npc.damage > 0 && !npc.dontTakeDamage && !npc.boss && npc.lifeMax < 1000)
            {
                if (shrinking)
                {
                    if (!storedScale)
                    {
                        initialScale = npc.scale;
                        if (grown)
                        {
                            initialScale = npc.scale / 1.5f;
                        }
                        storedScale = true;
                    }
                    if (npc.scale > initialScale * 0.75)
                    {
                        npc.scale *= 0.99f;
                    }
                    else
                    {
                        shrinking = false;
                    }
                    shrunk = true;
                    grown = false;
                }
                if (growing)
                {
                    if (!storedScale)
                    {
                        initialScale = npc.scale;
                        if (shrunk)
                        {
                            initialScale = npc.scale / 0.75f;
                        }
                        storedScale = true;
                    }
                    if (npc.scale < initialScale * 1.5)
                    {
                        npc.scale *= 1.01f;
                    }
                    else
                    {
                        growing = false;
                    }
                    grown = true;
                    shrunk = false;
                }
                if (!shrinking && !growing)
                {
                    storedScale = false;
                }
                if (shrinking && growing)
                {
                    npc.scale = 1f;
                    shrinking = false;
                    growing = false;
                    shrunk = false;
                    grown = false;
                    // make dust in an expanding circle
                    int numDusts = 36;
                    for (int i = 0; i < numDusts; i++)
                    {
                        Vector2 position = (Vector2.Normalize(new Vector2(5, 5)) * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + npc.Center;
                        Vector2 velocity = position - npc.Center;
                        int dust = Dust.NewDust(position + velocity, 0, 0, 131, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                    }
                }
            }
        }
        public static bool AnyBoss()
        {
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC boss = Main.npc[i];
                if (boss.boss && boss.active)
                {
                    return true;
                }
            }
            return false;
        }
        public static void GoThroughPlatforms(NPC npc)
        {
            Vector2 platform = npc.Bottom / 16;
            if (TileID.Sets.Platforms[Framing.GetTileSafely((int)platform.X, (int)platform.Y).TileType]) npc.noTileCollide = true;
            else npc.noTileCollide = false;
        }
        public static StatModifier ReducePierceDamage(StatModifier damage, Projectile projectile)
        {
            if (projectile.type == ProjectileID.LastPrismLaser && ModContent.GetInstance<Config>().vItemChangesDisabled) return (damage * 0.1f);
            else if (projectile.type == ProjectileID.LastPrismLaser && !ModContent.GetInstance<Config>().vItemChangesDisabled) return (damage * 0.85f);
            else if (projectile.maxPenetrate == -1 && ProjectileID.Sets.YoyosMaximumRange[projectile.type] == 0) return (damage * 0.5f);
            else if (projectile.maxPenetrate > 10) return (damage * 0.5f);
            else if (projectile.maxPenetrate > 6) return (damage * 0.75f);
            else if (projectile.maxPenetrate > 3) return (damage * 0.9f);
            else return damage;
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && npc.boss)
                {
                    spawnRate = 0;
                    maxSpawns = 0;
                    break;
                }
            }
            if (player.FindBuffIndex(BuffType<CalamityPotionBuff>()) != -1)
            {
                spawnRate = (int)(spawnRate / 12.5f);
                maxSpawns = (int)(maxSpawns * 12.5f);
            }
            else if (player.FindBuffIndex(BuffType<ChaosPotionBuff>()) != -1 || player.FindBuffIndex(BuffType<CalamityBannerBuff>()) != -1)
            {
                spawnRate = (int)(spawnRate / 7.5f);
                maxSpawns = (int)(maxSpawns * 7.5f);
            }
            else if (player.FindBuffIndex(BuffType<ChaosBannerBuff>()) != -1)
            {
                spawnRate = (int)(spawnRate / 5f);
                maxSpawns = (int)(maxSpawns * 5f);
            }
            else if(player.FindBuffIndex(BuffType<HavocPotionBuff>()) != -1)
            {
                spawnRate = (int)(spawnRate / 4f);
                maxSpawns = (int)(maxSpawns * 4f);
            }
            else if (player.FindBuffIndex(BuffType<HavocBannerBuff>()) != -1)
            {
                spawnRate = (int)(spawnRate / 2f);
                maxSpawns = (int)(maxSpawns * 2f);
            }
            if (MyWorld.aggressiveEnemies)
            {
                spawnRate = (int)(spawnRate / 5f);
                maxSpawns = (int)(maxSpawns * 5f);
            }
            if (MyWorld.credits)
            {
                maxSpawns = 0;
            }
        }
        public override bool SpecialOnKill(NPC npc)
        {
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.glassHeart && npc.boss)
            {
                CombatText.NewText(npc.getRect(), Color.Red, this.GetLocalization("NoHit.Nohit").Value, true);
                npc.NPCLoot();
            }
            return base.SpecialOnKill(npc);
        }
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (AnyBoss()) pool.Clear();
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (iceBound)
            {
                npc.velocity.Y = 0f;
                npc.velocity.X = 0f;
            }
            if (endlessTears)
            {
                npc.velocity *= 0.8f;
            }
            if (acidBurn)
            {
                npc.lifeRegen -= 20;
                if (damage < 3) damage = 3;
            }
            if (dragonfire)
            {
                npc.lifeRegen -= 40;
                if (damage < 2)  damage = 2;
            }
            if (electrified)
            {
                npc.lifeRegen -= 40;
                if (damage < 4) damage = 4;
            }
            if (ancientDecay)
            {
                npc.lifeRegen -= 50;
                if (damage < 5)  damage = 5;
            }
            if (corroding || soulInferno)
            {
                npc.lifeRegen -= 75;
                if (damage < 8) damage = 8;
            }
            if (handsOfDespair)
            {
                npc.lifeRegen -= 120;
                if (damage < 15) damage = 15;
                if (!hasHands && !npc.boss)
                {
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center.X, npc.Center.Y, 0f, 0f, ProjectileType<Content.Projectiles.Other.HandsOfDespair>(), 0, 0f, 0, 1f, npc.whoAmI);
                    hasHands = true;
                }
            }
            else hasHands = false;
            if (extinctionCurse)
            {
                npc.lifeRegen -= 150;
                if (damage < 30)   damage = 30;
            }
            if (chaosBurn || discordDebuff || fastPoison || starstruck)
            {
                npc.lifeRegen -= 300;
                if (damage < 50) damage = 50;
            }
            if (variableLifeDrain && lifeDrainAmount > 0 && !npc.SpawnedFromStatue)
            {
                npc.lifeRegen -= lifeDrainAmount;
                if (damage < lifeDrainAmount / 2)  damage = lifeDrainAmount / 2;
            }
            if (delete)
            {
                npc.active = false;
                if (npc.active)
                {
                    npc.StrikeNPC(new NPC.HitInfo() { Damage = npc.life, Knockback = 0f, HitDirection = 0, Crit = false });
                }
                npc.netUpdate = true;
            }
            Player player = Main.LocalPlayer;
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.buffDPS += Math.Abs(npc.lifeRegen / 2);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (fastPoison) drawColor = new Color(14, 150, 45);
            if (ancientDecay)
            {
                drawColor = Color.LightYellow;
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustType<AncientDust>(), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.25f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.35f;
                    }
                }
                Lighting.AddLight(npc.position, 0.025f, 0f, 0f);
            }
            if (acidBurn)
            {
                npc.color = new Color(178, 244, 124, 120);
            }
            if (MyWorld.swearingEnemies && npc.damage > 0)
            {
                if (Main.rand.Next(600) == 0)
                {
                    string s = "";
                    for (int l = 0; l < 4; l++)
                    {
                        int choice = Main.rand.Next(7);
                        if (choice == 0)
                        {
                            s = s + "$";
                        }
                        if (choice == 1)
                        {
                            s = s + "#";
                        }
                        if (choice == 2)
                        {
                            s = s + "!";
                        }
                        if (choice == 3)
                        {
                            s = s + "$";
                        }
                        if (choice == 4)
                        {
                            s = s + "@";
                        }
                        if (choice == 5)
                        {
                            s = s + "*";
                        }
                        if (choice == 6)
                        {
                            s = s + "?";
                        }
                    }
                    CombatText.NewText(npc.getRect(), Color.Red, s, false, false);
                }
            }
            if (impishCurse)
            {
                if (!npc.ichor) drawColor = new Color(255, 80, 80);
                else drawColor = new Color(255, 180, 40);
                Lighting.AddLight(npc.Center, 0.6f, 0.2f, 0.3f);
            }
        }
        public override void SetDefaults(NPC npc)
        {
            // all vanilla bosses + Deerclops, QueenSlimeBoss + [QueenSlimeMinionBlue, QueenSlimeMinionPink, QueenSlimeMinionPurple], HallowBoss
            #region buff immunity
            if (npc.type == NPCID.EyeofCthulhu || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail || npc.type == NPCID.BrainofCthulhu ||
                npc.type == NPCID.Creeper || npc.type == NPCID.SkeletronHead || npc.type == NPCID.SkeletronHand || npc.type == NPCID.QueenBee || npc.type == NPCID.KingSlime || npc.type == NPCID.WallofFlesh ||
                npc.type == NPCID.WallofFleshEye || npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail || npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism ||
                npc.type == NPCID.SkeletronPrime || npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice || npc.type == NPCID.PrimeLaser || npc.type == NPCID.Plantera ||
                npc.type == NPCID.PlanterasTentacle || npc.type == NPCID.Golem || npc.type == NPCID.GolemHead || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.DukeFishron ||
                npc.type == NPCID.CultistBoss || npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordHand || npc.type == NPCID.MoonLordCore)
            {
                npc.buffImmune[BuffType<IceBound>()] = true;
                npc.buffImmune[BuffType<EndlessTears>()] = true;
                npc.buffImmune[BuffType<HandsOfDespair>()] = true;
            }
            // later bosses
            if (npc.type == NPCID.DukeFishron ||npc.type == NPCID.CultistBoss ||npc.type == NPCID.MoonLordHead ||npc.type == NPCID.MoonLordHand ||npc.type == NPCID.MoonLordCore)  
            {
                npc.buffImmune[BuffType<AncientDecay>()] = true;
                npc.buffImmune[BuffType<SoulInferno>()] = true;
                npc.buffImmune[BuffType<Dragonfire>()] = true;
            }
            //1.4
            if (npc.type == NPCID.Deerclops)
            {
                npc.buffImmune[BuffType<EndlessTears>()] = true;
                npc.buffImmune[BuffType<Dragonfire>()] = true;
            }
            if (npc.type == NPCID.QueenSlimeBoss || npc.type == NPCID.QueenSlimeMinionBlue || npc.type == NPCID.QueenSlimeMinionPink || npc.type == NPCID.QueenSlimeMinionPurple)
            {
                npc.buffImmune[BuffType<EndlessTears>()] = true;
            }
            if (npc.type == NPCID.HallowBoss)
            {
                npc.buffImmune[BuffType<AncientDecay>()] = true;
                npc.buffImmune[BuffType<EndlessTears>()] = true;
                npc.buffImmune[BuffType<Dragonfire>()] = true;
            }
            #endregion
            if (MyWorld.aggressiveEnemies)
            {
                npc.damage = (int)(npc.damage * 1.25f);
            }
            if (npc.SpawnedFromStatue)
            {
                // LifeDrain == ?
                //npc.buffImmune[BuffType<LifeDrain>()] = true;
            }         
        }
        public override void ModifyShop(NPCShop shop)
        {
            Condition BossName1 = new(EALocalization.BossName(1), () => NPC.downedBoss1);
            Condition BossName4 = new(EALocalization.BossName(1), () => Main.hardMode);
            Condition MBossName = new(EALocalization.MBossName(12), () => MyWorld.downedAncients);
            Condition BloodMoon = new(EALocalization.BloodMoon, () => Main.bloodMoon);

            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add(new Item(ItemType<ThrowableBook>()) { value = 80 }, BossName1);
                shop.Add(new Item(ItemType<RainMeter>()) { value = Item.buyPrice(0, 5, 0, 0) }, BossName1);
            }
            if (shop.NpcType == NPCID.Dryad)
            {
                shop.Add(new Item(ItemType<DryadsRadar>()), BloodMoon);
            }
            if (shop.NpcType == NPCID.Wizard)
            {
                shop.Add(new Item(ItemType<Dictionary>()) { value = 800 }, BossName4);
                shop.Add(new Item(ItemType<Dictionary>()) { value = Item.buyPrice(0, 25, 0, 0) }, MBossName);
            }
            if (shop.NpcType == NPCID.Steampunker)
            {
                shop.Add(new Item(ItemType<SonicArm>()) { value = Item.buyPrice(0, 25, 0, 0) });
                shop.Add(new Item(ItemType<FeatheredGoggles>()) { value = Item.buyPrice(2, 0, 0, 0) });
            }
            if (shop.NpcType == NPCID.Cyborg)
            {
                shop.Add(new Item(ItemType<Content.Items.Placeable.Computer>()) { value = Item.buyPrice(0, 5, 0, 0) });
                shop.Add(new Item(ItemType<Content.Items.Placeable.Desk>()) { value = Item.buyPrice(0, 1, 50, 0) });
                shop.Add(new Item(ItemType<Content.Items.Placeable.OfficeChair>()) { value = Item.buyPrice(0, 1, 50, 0) });
                shop.Add(new Item(ItemType<Content.Items.Placeable.LabLightFunctional>()) { value = Item.buyPrice(0, 1, 50, 0) });
            }
        }
        public override void GetChat(NPC npc, ref string chat)
        {
            int storyteller = NPC.FindFirstNPC(NPCType<Storyteller>());
            int alchemist = NPC.FindFirstNPC(NPCType<Alchemist>());
            if (npc.type == NPCID.Guide && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = string.Format(this.GetLocalization("Chat.Say").Value, Main.npc[storyteller].GivenName);
                }
            }
            if (npc.type == NPCID.Nurse && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = string.Format(this.GetLocalization("Chat.Say1").Value, Main.npc[storyteller].GivenName);
                }
            }
            if (npc.type == NPCID.Dryad && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = string.Format(this.GetLocalization("Chat.Say2").Value, Main.npc[storyteller].GivenName);
                }
                if (NPC.downedBoss1 && !MyWorld.downedWasteland)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        switch (Main.rand.Next(3))
                        {
                            case 0: chat = this.GetLocalization("Chat.Say3").Value; break;
                            case 1: chat = this.GetLocalization("Chat.Say4").Value; break;
                            case 2: chat = this.GetLocalization("Chat.Say5").Value; break;
                        }
                    }
                }
            }
            if (npc.type == NPCID.ArmsDealer && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = string.Format(this.GetLocalization("Chat.Say6").Value, Main.npc[storyteller].GivenName);
                }
                if (Main.rand.Next(10) == 0)
                {
                    chat = string.Format(this.GetLocalization("Chat.Say7").Value, Main.npc[storyteller].GivenName);
                }
            }
            if (npc.type == NPCID.Truffle && storyteller >= 0)
            {
                if (Main.rand.Next(10) == 0)
                {
                    chat = string.Format(this.GetLocalization("Chat.Say8").Value, Main.npc[storyteller].GivenName);
                }
            }
            if (npc.type == NPCID.Cyborg)
            {
                if (Main.rand.Next(15) == 0)
                {
                    chat = this.GetLocalization("Chat.Say9").Value;
                }
            }
        }
    }
}