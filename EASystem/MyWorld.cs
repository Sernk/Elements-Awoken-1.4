using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Events.RadiantRain.Enemies;
using ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase2.ShadeWyrm;
using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.Items.Tech.Accessories.Tier2;
using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Items.Tech.Weapons.Tier1;
using ElementsAwoken.Content.Items.Tech.Weapons.Tier2;
using ElementsAwoken.Content.Items.Weapons.Melee.Whips;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.Content.Tiles;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.Events.VoidEvent;
using ElementsAwoken.Utilities;
using ElementsAwoken.Utilities.Structures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.Generation;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken
{
    public class MyWorld : ModSystem, ILocalizedModType
    {
        public string LocalizationCategory => "MyWorldLocalization";
        public static bool awakenedMode = false;
        public static bool credits = false;
        public static int creditsCounter = 0;
        public static int moonlordKills = 0;
        public static int voidLeviathanKills = 0;
        public static int ancientSummons = 0;
        public static int ancientKills = 0;
        public static bool genVoidite = false;
        public static bool genLuminite = false;
        public static string[] mysteriousPotionColours = new string[10];
        public static bool downedToySlime = false;
        public static bool downedWasteland = false;
        public static bool downedInfernace = false;
        public static bool downedScourgeFighter = false;
        public static bool downedRegaroth = false;
        public static bool downedPermafrost = false;
        public static bool downedObsidious = false;
        public static bool downedAqueous = false;
        public static bool downedEye = false;
        public static bool downedAncientWyrm = false;
        public static bool downedGuardian = false;
        public static bool downedVoidEvent = false;
        public static bool downedShadeWyrm = false;
        public static bool downedVolcanox = false;
        public static bool downedVoidLeviathan = false;
        public static bool downedAzana = false;
        public static bool sparedAzana = false;
        public static bool downedAncients = false;
        public static bool downedCosmicObserver = false;
        public static bool completedRadiantRain = false;
        public static bool downedRadiantMaster = false;
        public static bool darkMoon = false;
        public static bool voidInvasionUp = false;
        public static bool voidInvasionWillStart = false;
        public static int voidInvasionFinished = 0;
        public static int hailStormTime = 0;
        public static int SkyTiles = 0;
        public static int lizardTiles = 0;
        public static int corruptionTiles = 0;
        public static int crimsonTiles = 0;
        public static int hallowedTiles = 0;
        public static int labPosition = 0;
        public static int sizeMult = (int)(Math.Floor(Main.maxTilesX / 4200f));
        public static bool generatedLabs = false;
        public static bool aggressiveEnemies = false;
        public static bool swearingEnemies = false;
        public static bool ancientsDrive = false;
        public static bool aqueousDrive = false;
        public static bool azanaDrive = false;
        public static bool celestialDrive = false;
        public static bool guardianDrive = false;
        public static bool infernaceDrive = false;
        public static bool obsidiousDrive = false;
        public static bool permafrostDrive = false;
        public static bool regarothDrive = false;
        public static bool scourgeFighterDrive = false;
        public static bool voidLeviathanDrive = false;
        public static bool volcanoxDrive = false;
        public static bool wastelandDrive = false;
        public static int desertPrompt = 0;
        public static int firePrompt = 0;
        public static int skyPrompt = 0;
        public static int frostPrompt = 0;
        public static int waterPrompt = 0;
        public static int voidPrompt = 0;
        public static int skyPromptRainCD = 0;
        public static bool radiantRain = false;
        public static bool prevTickRaining = false;
        public override void OnWorldLoad()
        {
            awakenedMode = false;
            credits = false;
            voidLeviathanKills = 0;
            moonlordKills = 0;
            ancientKills = 0;
            ancientSummons = 0;
            sizeMult = (int)(Math.Floor(Main.maxTilesX / 4200f));
            generatedLabs = false;
            downedToySlime = false;
            downedWasteland = false;
            downedInfernace = false;
            downedScourgeFighter = false;
            downedRegaroth = false;
            downedPermafrost = false;
            downedObsidious = false;
            downedAqueous = false;
            downedEye = false;
            downedAncientWyrm = false;
            downedGuardian = false;
            downedVoidEvent = false;
            downedShadeWyrm = false;
            downedVolcanox = false;
            downedVoidLeviathan = false;
            downedAzana = false;
            sparedAzana = false;
            downedAncients = false;
            downedCosmicObserver = false;
            completedRadiantRain = false;
            downedRadiantMaster = false;
            Main.invasionSize = 0;
            voidInvasionUp = false;
            voidInvasionWillStart = false;
            voidInvasionFinished = 0;
            darkMoon = false;
            genVoidite = false;
            genLuminite = false;
            aggressiveEnemies = false;
            swearingEnemies = false;
            ElementsAwoken.encounter = 0;
            ElementsAwoken.encounterTimer = 0;
            ElementsAwoken.encounterSetup = false;
            aqueousDrive = false;
            azanaDrive = false;
            celestialDrive = false;
            guardianDrive = false;
            infernaceDrive = false;
            obsidiousDrive = false;
            permafrostDrive = false;
            regarothDrive = false;
            scourgeFighterDrive = false;
            voidLeviathanDrive = false;
            volcanoxDrive = false;
            wastelandDrive = false;
            desertPrompt = 0;
            firePrompt = 0;
            skyPrompt = 0;
            frostPrompt = 0;
            waterPrompt = 0;
            voidPrompt = 0;
            skyPromptRainCD = 0;
            hailStormTime = 0;
            radiantRain = false;
            prevTickRaining = false;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            tag["awakenedMode"] = awakenedMode;
            tag["downedInfernace"] = downedInfernace;
            tag["downedScourgeFighter"] = downedScourgeFighter;
            tag["downedRegaroth"] = downedRegaroth;
            tag["downedAqueous"] = downedAqueous;
            tag["downedVoidLeviathan"] = downedVoidLeviathan;
            tag["downedWasteland"] = downedWasteland;
            tag["downedPermafrost"] = downedPermafrost;
            tag["downedGuardian"] = downedGuardian;
            tag["downedEye"] = downedEye;
            tag["downedAncientWyrm"] = downedAncientWyrm;
            tag["downedObsidious"] = downedObsidious;
            tag["downedShadeWyrm"] = downedShadeWyrm;
            tag["downedVolcanox"] = downedVolcanox;
            tag["downedToySlime"] = downedToySlime;
            tag["downedAzana"] = downedAzana;
            tag["downedAncients"] = downedAncients;
            tag["downedCosmicObserver"] = downedCosmicObserver;
            tag["completedRadiantRain"] = completedRadiantRain;
            tag["downedRadiantMaster"] = downedRadiantMaster;
            tag["downedVoidEvent"] = downedVoidEvent;
            tag["generatedLabs"] = generatedLabs;
            tag["moonlordKills"] = moonlordKills;
            tag["voidLeviathanKills"] = voidLeviathanKills;
            tag["ancientKills"] = ancientKills;
            tag["ancientSummons"] = ancientSummons;
            tag["aqueousDrive"] = aqueousDrive;
            tag["azanaDrive"] = azanaDrive;
            tag["celestialDrive"] = celestialDrive;
            tag["guardianDrive"] = guardianDrive;
            tag["infernaceDrive"] = infernaceDrive;
            tag["obsidiousDrive"] = obsidiousDrive;
            tag["permafrostDrive"] = permafrostDrive;
            tag["regarothDrive"] = regarothDrive;
            tag["scourgeFighterDrive"] = scourgeFighterDrive;
            tag["voidLeviathanDrive"] = voidLeviathanDrive;
            tag["volcanoxDrive"] = volcanoxDrive;
            tag["wastelandDrive"] = wastelandDrive;
            tag["desertPrompt"] = desertPrompt;
            tag["firePrompt"] = firePrompt;
            tag["skyPrompt"] = skyPrompt;
            tag["frostPrompt"] = frostPrompt;
            tag["waterPrompt"] = waterPrompt;
            tag["voidPrompt"] = voidPrompt;
            tag["radiantRain"] = radiantRain;
            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                tag["potColours" + i] = mysteriousPotionColours[i];
            }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            awakenedMode = tag.GetBool("awakenedMode");
            downedInfernace = tag.GetBool("downedInfernace");
            downedAqueous = tag.GetBool("downedAqueous");
            downedVoidLeviathan = tag.GetBool("downedVoidLeviathan");
            downedScourgeFighter = tag.GetBool("downedScourgeFighter");
            downedRegaroth = tag.GetBool("downedRegaroth");
            completedRadiantRain = tag.GetBool("completedRadiantRain");
            downedWasteland = tag.GetBool("downedWasteland");
            downedPermafrost = tag.GetBool("downedPermafrost");
            downedGuardian = tag.GetBool("downedGuardian");
            downedEye = tag.GetBool("downedEye");
            downedAncientWyrm = tag.GetBool("downedAncientWyrm");
            downedObsidious = tag.GetBool("downedObsidious");
            downedShadeWyrm = tag.GetBool("downedShadeWyrm");
            downedVolcanox = tag.GetBool("downedVolcanox");
            downedToySlime = tag.GetBool("downedToySlime");
            downedAzana = tag.GetBool("downedAzana");
            sparedAzana = tag.GetBool("sparedAzana");
            downedAncients = tag.GetBool("downedAncients");
            downedCosmicObserver = tag.GetBool("downedCosmicObserver");
            downedRadiantMaster = tag.GetBool("downedRadiantMaster");
            downedVoidEvent = tag.GetBool("downedVoidEvent");
            generatedLabs = tag.GetBool("generatedLabs");
            moonlordKills = tag.GetInt("moonlordKills");
            voidLeviathanKills = tag.GetInt("voidLeviathanKills");
            ancientKills = tag.GetInt("ancientKills");
            ancientSummons = tag.GetInt("ancientSummons");
            aqueousDrive = tag.GetBool("aqueousDrive");
            azanaDrive = tag.GetBool("azanaDrive");
            celestialDrive = tag.GetBool("celestialDrive");
            guardianDrive = tag.GetBool("guardianDrive");
            infernaceDrive = tag.GetBool("infernaceDrive");
            obsidiousDrive = tag.GetBool("obsidiousDrive");
            permafrostDrive = tag.GetBool("permafrostDrive");
            regarothDrive = tag.GetBool("regarothDrive");
            scourgeFighterDrive = tag.GetBool("scourgeFighterDrive");
            voidLeviathanDrive = tag.GetBool("voidLeviathanDrive");
            volcanoxDrive = tag.GetBool("volcanoxDrive");
            wastelandDrive = tag.GetBool("wastelandDrive");
            desertPrompt = tag.GetInt("desertPrompt");
            firePrompt = tag.GetInt("firePrompt");
            skyPrompt = tag.GetInt("skyPrompt");
            frostPrompt = tag.GetInt("frostPrompt");
            waterPrompt = tag.GetInt("waterPrompt");
            voidPrompt = tag.GetInt("voidPrompt");
            radiantRain = tag.GetBool("radiantRain");
            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                mysteriousPotionColours[i] = tag.GetString("potColours" + i);
            }
        }
        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags1 = new BitsByte();
            flags1[0] = downedAqueous;
            flags1[1] = downedVoidLeviathan;
            flags1[2] = downedInfernace;
            flags1[3] = downedScourgeFighter;
            flags1[4] = downedRegaroth;
            flags1[5] = completedRadiantRain;
            flags1[6] = downedWasteland;
            flags1[7] = downedPermafrost;
            writer.Write(flags1);
            BitsByte flags2 = new BitsByte();
            flags2[0] = downedGuardian;
            flags2[1] = downedEye;
            flags2[2] = downedAncientWyrm;
            flags2[3] = downedObsidious;
            flags2[4] = downedShadeWyrm;
            flags2[5] = downedVolcanox;
            flags2[6] = downedVoidEvent;
            flags2[7] = downedToySlime;
            writer.Write(flags2);
            BitsByte flags3 = new BitsByte();
            flags3[0] = downedAzana;
            flags3[1] = downedAncients;
            flags3[2] = downedCosmicObserver;
            flags3[3] = sparedAzana;
            flags3[4] = downedRadiantMaster;
            writer.Write(flags3);
            BitsByte flags4 = new BitsByte();
            flags4[0] = generatedLabs;
            flags4[1] = aqueousDrive;
            flags4[2] = azanaDrive;
            flags2[3] = celestialDrive;
            flags2[4] = guardianDrive;
            flags2[5] = infernaceDrive;
            flags2[6] = obsidiousDrive;
            flags2[7] = permafrostDrive;
            writer.Write(flags4);
            BitsByte flags5 = new BitsByte();
            flags5[0] = regarothDrive;
            flags5[1] = scourgeFighterDrive;
            flags5[2] = voidLeviathanDrive;
            flags5[3] = volcanoxDrive;
            flags5[4] = wastelandDrive;
            writer.Write(flags5);
            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                writer.Write(mysteriousPotionColours[i]);
            }
            BitsByte flags6 = new BitsByte();
            flags6[0] = awakenedMode;
            flags6[1] = genLuminite;
            flags6[2] = genVoidite;
            flags6[3] = radiantRain;
            flags6[4] = prevTickRaining;
            flags6[5] = voidInvasionUp;
            writer.Write(flags6);
            writer.Write(desertPrompt);
            writer.Write(firePrompt);
            writer.Write(skyPrompt);
            writer.Write(frostPrompt);
            writer.Write(waterPrompt);
            writer.Write(voidPrompt);
            writer.Write(moonlordKills);
            writer.Write(voidLeviathanKills);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags1 = reader.ReadByte();
            downedAqueous = flags1[0];
            downedVoidLeviathan = flags1[1];
            downedInfernace = flags1[2];
            downedScourgeFighter = flags1[3];
            downedRegaroth = flags1[4];
            completedRadiantRain = flags1[5];
            downedWasteland = flags1[6];
            downedPermafrost = flags1[7];
            BitsByte flags2 = reader.ReadByte();
            downedGuardian = flags2[0];
            downedEye = flags2[1];
            downedAncientWyrm = flags2[2];
            downedObsidious = flags2[3];
            downedShadeWyrm = flags2[4];
            downedVolcanox = flags2[5];
            downedVoidEvent = flags2[6];
            downedToySlime = flags2[7];
            BitsByte flags3 = reader.ReadByte();
            downedAzana = flags3[0];
            downedAncients = flags3[1];
            downedCosmicObserver = flags3[2];
            sparedAzana = flags3[3];
            downedRadiantMaster = flags3[4];
            BitsByte flags4 = reader.ReadByte();
            generatedLabs = flags4[0];
            aqueousDrive = flags4[1];
            azanaDrive = flags4[2];
            celestialDrive = flags4[3];
            guardianDrive = flags4[4];
            infernaceDrive = flags4[5];
            obsidiousDrive = flags4[6];
            permafrostDrive = flags4[7];
            BitsByte flags5 = reader.ReadByte();
            regarothDrive = flags5[0];
            scourgeFighterDrive = flags5[1];
            voidLeviathanDrive = flags5[2];
            volcanoxDrive = flags5[3];
            wastelandDrive = flags5[4];            
            for (int i = 0; i < mysteriousPotionColours.Length; i++)
            {
                mysteriousPotionColours[i] = reader.ReadString();
            }
            BitsByte flags6 = reader.ReadByte();
            awakenedMode = flags6[0];
            genLuminite = flags6[1];
            genVoidite = flags6[2];
            radiantRain = flags6[3];
            prevTickRaining = flags6[4];
            voidInvasionUp = flags6[5];
            desertPrompt = reader.ReadInt32();
            firePrompt = reader.ReadInt32();
            skyPrompt = reader.ReadInt32();
            frostPrompt = reader.ReadInt32();
            waterPrompt = reader.ReadInt32();
            voidPrompt = reader.ReadInt32();
            moonlordKills = reader.ReadInt32();
            voidLeviathanKills = reader.ReadInt32();
        }
        public override void Load()
        {
            _ = this.GetLocalization("voidInvasio").Value;
            _ = this.GetLocalization("voidInvasio1").Value;
            _ = this.GetLocalization("voidInvasio2").Value;
            _ = this.GetLocalization("Azana").Value;
            _ = this.GetLocalization("Azana1").Value;
        }
        // adding item in chests
        public override void PostWorldGen()
        {
            RandomisePotions();
            int[] dungeonItems = new int[] { ItemType<CrashingWave>() };
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 2 * 36)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {

                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(Main.rand.Next(dungeonItems));
                                break;
                            }
                        }
                    }
                }
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 1 * 36)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(ItemType<MysteriousPotion>());
                                MysteriousPotion pot = (MysteriousPotion)chest.item[inventoryIndex].ModItem;
                                pot.potionNum = Main.rand.Next(10);
                                break;
                            }
                        }
                    }
                }
            }
        }
        public override void PostUpdateWorld()
        {
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            string VoidInvasio = this.GetLocalization("voidInvasio").Value;
            string VoidInvasio1 = this.GetLocalization("voidInvasio1").Value;
            string VoidInvasio2 = this.GetLocalization("voidInvasio2").Value;
            string Azana = this.GetLocalization("Azana").Value;
            string Azana1 = this.GetLocalization("Azana1").Value;

            if (!ModContent.GetInstance<Config>().promptsDisabled)
            {
                if (NPC.downedBoss1 && !downedWasteland) desertPrompt++;
                else desertPrompt = 0;
                if (NPC.downedBoss3 && !downedInfernace) firePrompt++;
                else firePrompt = 0;
                if (NPC.downedMechBossAny && !downedRegaroth) skyPrompt++;
                else skyPrompt = 0;
                if (NPC.downedPlantBoss && !downedPermafrost) frostPrompt++;
                else frostPrompt = 0;
                if (NPC.downedFishron && !downedAqueous) waterPrompt++;
                else waterPrompt = 0;
                if (downedVolcanox && !downedVoidLeviathan) voidPrompt++;
                else voidPrompt = 0;

                if (skyPrompt > ElementsAwoken.bossPromptDelay)
                {
                    if (!Main.raining && skyPromptRainCD <= 0)
                    {
                        skyPromptRainCD = 3600;
                        Main.raining = true;
                        Main.rainTime = 18000;
                        ElementsAwoken.DebugModeText("Sky Prompt Rain Started");
                    }
                    if (Main.raining) skyPromptRainCD--;
                }
                if (frostPrompt > ElementsAwoken.bossPromptDelay)
                {
                    if (hailStormTime <= 0 && Main.rand.Next(40000) == 0)
                    {
                        hailStormTime = Main.rand.Next(1800, 7200);
                    }
                }
                if (hailStormTime > 0)
                {
                    hailStormTime--;
                }
            }
            if (modPlayer.voidEnergyTimer > 0)
            {
                MoonlordShake(1f);
            }
            if (modPlayer.infinityDeath)
            {
                MoonlordShake(0.6f);
            }
            #region credits
            #endregion
            #region DotV
            voidInvasionFinished--;
            if (voidInvasionFinished <= 0)
            {
                voidInvasionFinished = 0;
            }
            if (voidInvasionUp)
            {
                VoidEvent.UpdateInvasion();
                if (!Main.dayTime && Main.time > 12620 && Main.time < 16220)
                {
                    Lighting.GlobalBrightness = 0.75f;
                }
                if (!Main.dayTime && Main.time > 15620 && Main.time < 15620)
                {
                    Lighting.GlobalBrightness = 0.4f;
                }
                if (!Main.dayTime && Main.time == 16220)
                {
                    Main.NewText(VoidInvasio, 31, 34, 66);
                }
                if (!NPC.AnyNPCs(NPCType<ShadeWyrmHead>()) && (Main.time == 19800 || Main.time == 27000)) NPC.SpawnOnPlayer(player.whoAmI, NPCType<ShadeWyrmHead>());
                if (!Main.dayTime && Main.time > 16220)
                {
                    MoonlordShake(1f);
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        Player p = Main.player[i];
                        if (p.active)
                        {
                            p.AddBuff(BuffID.Blackout, 60);
                            p.AddBuff(BuffID.Slow, 60);
                            Lighting.GlobalBrightness = 0.5f;
                            p.AddBuff(BuffType<DeterioratingWings>(), 60, false);
                        }
                    }
                    for (int k = 0; k < Main.npc.Length; k++)
                    {
                        NPC other = Main.npc[k];
                        if (VoidEvent.phase1NPCs.Contains(other.type))
                        {
                            other.alpha += 5;
                            if (other.alpha >= 255)
                            {
                                other.active = false;
                            }
                        }

                    }
                }
                bool allDead = true;
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (!Main.player[i].dead)
                    {
                        allDead = false;
                    }
                    if (allDead)
                    {
                        if (Main.netMode == 0) Main.NewText(VoidInvasio1, 182, 15, 15);
                        if (Main.netMode == 2) ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(VoidInvasio2), new Color(182, 15, 15));
                        SoundEngine.PlaySound(SoundID.Zombie105, new Vector2(Main.player[i].position.X, Main.player[i].position.Y));
                        voidInvasionUp = false;
                        voidInvasionFinished = 200;
                    }
                }
            }
            else if (voidInvasionFinished > 0)
            {
                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (VoidEvent.phase1NPCs.Contains(other.type) || VoidEvent.phase2NPCs.Contains(other.type))
                    {
                        other.alpha += 3;
                        if (other.alpha >= 255)
                        {
                            other.active = false;
                        }
                    }
                }
            }
            if (voidInvasionWillStart)
            {
                if (!Main.dayTime)
                {
                    VoidEvent.StartInvasion();
                    Main.invasionX = Main.spawnTileX;
                    voidInvasionWillStart = false;
                    Filters.Scene.Deactivate("MoonLordShake", new object[0]);
                }
                float intensity = MathHelper.Clamp((float)Main.time / 54000f, 0f, 1f);
                MoonlordShake(intensity);
            }
            if (genLuminite)
            {
                GenLuminite();
                genLuminite = false;
            }
            if (genVoidite)
            {
                GenVoidite();
                genVoidite = false;
            }
            #endregion
            if (downedAzana || sparedAzana)
            {
                if (Main.raining && !prevTickRaining)
                {
                    if (Main.rand.NextBool(4) || !completedRadiantRain)
                    {
                        radiantRain = true;
                        Main.NewText(Azana, Color.HotPink);
                    }
                }
            }
            if (radiantRain)
            {
                if (Main.rainTime == 7200) NPC.SpawnOnPlayer(player.whoAmI, NPCType<RadiantMaster>());
                if (!Main.raining)
                {
                    radiantRain = false;
                    completedRadiantRain = true;
                    Main.NewText(Azana1, Color.HotPink);
                }
                int amount = Main.expertMode ? awakenedMode ? 100 : 120 : 240;
                if (Main.rand.NextBool(120))
                {
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player starShowerP = Main.player[i];
                        if (starShowerP.active)
                        {
                            Vector2 pos = new Vector2(starShowerP.Center.X - Main.rand.Next(-1000,1000), starShowerP.Center.Y - 1000);
                            Vector2 vel = new Vector2(Main.windSpeedCurrent * 10, 10f);
                            Projectile proj = Main.projectile[Projectile.NewProjectile(player.GetSource_FromThis(), pos.X, pos.Y, vel.X, vel.Y, ProjectileType<RadiantStarRain>(), Main.expertMode ? awakenedMode ? 150 : 100 : 75, 10f, Main.myPlayer, 0f, 0f)];
                            proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                        }
                    }
                }
            }
            prevTickRaining = Main.raining;
        }
        // thanks laugic !
        #region lab gen
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            if (!ModContent.GetInstance<Config>().labsDisabled)
            {
                int genIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Buried Chests"));
                tasks.Insert(genIndex2 + 2, new PassLegacy("Generating Labs", delegate (GenerationProgress progress, GameConfiguration configuration)
                {
                    progress.Message = "Killing Scientists";
                    LabStructures();
                }));
            }
        }
        private void LabStructures()
        {
            int s = 0;
            generatedLabs = true;
            for (int q = 0; q < 13; q++)
            {
                int xMin = Main.maxTilesX / 2 - Main.maxTilesX / 5;
                int xMax = Main.maxTilesX / 2 + Main.maxTilesX / 5;
                int labPosX = WorldGen.genRand.Next(xMin, xMax);

                int yMin = (int)(GenVars.worldSurfaceHigh + 200.0);
                int yMax = Main.maxTilesY - 230;
                int labPosY = WorldGen.genRand.Next(yMin, yMax);

                s = GenerateLab(s, labPosX, labPosY);
            }
        }
        private int GenerateLab(int s, int structX, int structY)
        {
            if (TileCheckSafe(structX, structY))
            {
                if (!Chest.NearOtherChests(structX, structY))
                {
                    if (structY < Main.maxTilesY - 220)
                    {
                        bool mirrored = false;
                        if (Main.rand.Next(2) == 0)
                            mirrored = true;

                        PickLab(s, structX, structY, mirrored);
                        s++;
                        if (s > 12)
                        {
                            s = 0;
                        }
                    }
                }
            }
            return s;
        }
        private void PickLab(int s, int structX, int structY, bool mirrored)
        {
            if (s == 0)
            {
                WastelandLab.StructureGen(structX, structY, mirrored);
                WastelandLabPlatforms.StructureGen(structX, structY, mirrored);
                WastelandLabFurniture.StructureGen(structX, structY, mirrored);
            }
            else if (s == 12)
            {
                AncientsLab.Generate(structX, structY, mirrored);
            }
            else
            {
                Lab.StructureGen(structX, structY, mirrored);
                LabPlatforms.StructureGen(structX, structY, mirrored);
                LabFurniture.StructureGen(structX, structY, mirrored, s - 1);
            }
        }
        public static void PlaceLabLocker(int x, int y, ushort floorType)
        {
            ClearSpaceForChest(x, y, floorType);
            int chestIndex = WorldGen.PlaceChest(x, y, (ushort)TileType<Content.Tiles.Lab.Locker>(), false, 0);

            int specialItem = GetLabLoot();
            labPosition++;
            int[] oreLoot = GetOreLoot();
            int[] potionLoot = GetPotionLoot();
            int[] money = GetMoneyLoot();
            int[] miscLoot = GetMiscLoot();

            int[] itemsToPlaceInChests = new int[] { specialItem, oreLoot[0], potionLoot[0], money[0], miscLoot[0] };
            int[] itemCounts = new int[] { 1, oreLoot[1], potionLoot[1], money[1], miscLoot[1] };

            FillChest(chestIndex, itemsToPlaceInChests, itemCounts);
        }
        private static int GetLabLoot()
        {
            int[] labLoot = new int[] { ItemType<Coilgun>(), ItemType<Electrozzitron>(), ItemType<TeslaRod>(), ItemType<BassBooster>(), ItemType<Taser>(), ItemType<RustedMechanism>(), };
            if (labPosition < labLoot.GetLength(0))
                return labLoot[labPosition];
            else
            {
                labPosition = 0;
                return labLoot[labPosition];
            }
        }
        private static int[] GetOreLoot()
        {
            int[] oreLoot = new int[] { ItemID.GoldBar, ItemID.PlatinumBar, ItemID.TungstenBar, ItemID.SilverBar };
            int orePos = Main.rand.Next(oreLoot.GetLength(0));
            int oreCount = Main.rand.Next(6, 16);
            int[] ore = { 0, 0 };
            ore[0] = oreLoot[orePos];
            ore[1] = oreCount;
            return ore;
        }

        private static int[] GetPotionLoot()
        {
            int[] potLoot = new int[] { ItemID.InfernoPotion, ItemID.LifeforcePotion, ItemID.WrathPotion };
            int potPos = Main.rand.Next(potLoot.GetLength(0));
            int potCount = Main.rand.Next(2, 5);
            int[] pot = { 0, 0 };
            pot[0] = potLoot[potPos];
            pot[1] = potCount;
            return pot;
        }

        private static int[] GetMoneyLoot()
        {
            int monType = 0;
            int monCount = 0;
            if (Main.rand.Next(2) == 0)
            {
                monType = ItemID.GoldCoin;
                monCount = Main.rand.Next(1, 4);
            }
            else
            {
                monType = ItemID.SilverCoin;
                monCount = Main.rand.Next(60, 99);
            }
            int[] mon = { 0, 0 };
            mon[0] = monType;
            mon[1] = monCount;
            return mon;
        }
        private static int[] GetMiscLoot()
        {
            int[] mscLoot = new int[] { ItemType<Capacitor>(), ItemType<CopperWire>(), ItemType<GoldWire>() };
            int mscPos = Main.rand.Next(mscLoot.GetLength(0));
            int mscCount = Main.rand.Next(2, 6);
            int[] msc = { 0, 0 };
            msc[0] = mscLoot[mscPos];
            msc[1] = mscCount;
            return msc;
        }
        private static void ClearSpaceForChest(int x, int y, ushort floorType)
        {
            WorldGen.KillTile(x, y);
            WorldGen.KillTile(x, y - 1);
            WorldGen.KillTile(x + 1, y - 1);
            WorldGen.KillTile(x + 1, y);
            WorldGen.PlaceTile(x + 1, y + 1, floorType, true, true);
            WorldGen.PlaceTile(x, y + 1, floorType, true, true);
            Main.tile[x, y].LiquidAmount = 0;
            Main.tile[x + 1, y].LiquidAmount = 0;
            Main.tile[x, y + 1].LiquidAmount = 0;
            Main.tile[x + 1, y + 1].LiquidAmount = 0;
        }
        private static void FillChest(int chestIndex, int[] itemsToPlaceInChests, int[] itemCounts)
        {
            if (chestIndex < Main.chest.GetLength(0) && chestIndex >= 0)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == 0 && inventoryIndex < itemsToPlaceInChests.GetLength(0))
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChests[inventoryIndex]);
                            chest.item[inventoryIndex].stack = itemCounts[inventoryIndex];
                        }
                    }
                }
            }
        }
        public static bool TileCheckSafe(int i, int j)
        {
            if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY)
                return true;
            return false;
        }
        #endregion
        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            SkyTiles = tileCounts[TileID.Cloud];
            lizardTiles = tileCounts[TileID.LihzahrdBrick];

            corruptionTiles = tileCounts[TileID.CorruptGrass] + tileCounts[TileID.CorruptHardenedSand] + tileCounts[TileID.CorruptIce] + tileCounts[TileID.CorruptSandstone] + tileCounts[TileID.CorruptThorns] + tileCounts[TileID.Ebonsand] + tileCounts[TileID.Ebonstone];
            crimsonTiles = tileCounts[TileID.FleshBlock] + tileCounts[TileID.CrimsonHardenedSand] + tileCounts[TileID.FleshIce] + tileCounts[TileID.CrimsonSandstone] + tileCounts[TileID.Crimtane] + tileCounts[TileID.Crimsand] + tileCounts[TileID.Crimstone];
            hallowedTiles = tileCounts[TileID.HallowedGrass] + tileCounts[TileID.HallowHardenedSand] + tileCounts[TileID.HallowedIce] + tileCounts[TileID.HallowedPlants] + tileCounts[TileID.HallowedPlants2] + tileCounts[TileID.HallowSandstone] + tileCounts[TileID.Pearlsand] + tileCounts[TileID.Pearlstone];
        }
        private void MoonlordShake(float intensity)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Player player = Main.LocalPlayer;
                if (!Filters.Scene["MoonLordShake"].IsActive())
                {
                    Filters.Scene.Activate("MoonLordShake", player.position, new object[0]);
                }
            }
            else
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player p = Main.player[i];
                    if (p.active)
                    {
                        if (!Filters.Scene["MoonLordShake"].IsActive())
                        {
                            Filters.Scene.Activate("MoonLordShake", p.position, new object[0]);
                        }
                    }
                }
            }
            Filters.Scene["MoonLordShake"].GetShader().UseIntensity(intensity);
        }
        public static void GenVoidite()
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();

            if (Main.netMode == 1 || WorldGen.noTileActions || WorldGen.gen)
            {
                return;
            }
            string text = EALocalization.Voidite;
            if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.DeepPink);
            else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.DeepPink);
            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 4E-05); k++)
            {
                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int y = WorldGen.genRand.Next((int)(Main.maxTilesY * .4f), Main.maxTilesY - 200);

                WorldGen.OreRunner(x, y, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 7), (ushort)TileType<Voidite>());
            }
        }
        public static void GenLuminite()
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            if (Main.netMode == 1 || WorldGen.noTileActions || WorldGen.gen)
            {
                return;
            }
            string text = EALocalization.Luminite;
            if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.GreenYellow);
            else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.GreenYellow);
            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 5E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)(Main.maxTilesY * .6f), Main.maxTilesY - 200);

                WorldGen.OreRunner(x, y, WorldGen.genRand.Next(3, 5), WorldGen.genRand.Next(4, 7), TileID.LunarOre);
            }
        }
        public static void RandomisePotions()
        {
            var nums = Enumerable.Range(0, 10).ToArray();

            for (int i = 0; i < nums.Length; ++i)
            {
                int randomIndex = Main.rand.Next(nums.Length);
                int temp = nums[randomIndex];
                nums[randomIndex] = nums[i];
                nums[i] = temp;
            }
            for (int i = 0; i < nums.Length; ++i)
            {
                switch (nums[i])
                {
                    case 0:
                        mysteriousPotionColours[i] = "Red";
                        break;
                    case 1:
                        mysteriousPotionColours[i] = "Cyan";
                        break;
                    case 2:
                        mysteriousPotionColours[i] = "Lime";
                        break;
                    case 3:
                        mysteriousPotionColours[i] = "Fuschia";
                        break;
                    case 4:
                        mysteriousPotionColours[i] = "Pink";
                        break;
                    case 5:
                        mysteriousPotionColours[i] = "Brown";
                        break;
                    case 6:
                        mysteriousPotionColours[i] = "Orange";
                        break;
                    case 7:
                        mysteriousPotionColours[i] = "Yellow";
                        break;
                    case 8:
                        mysteriousPotionColours[i] = "Blue";
                        break;
                    case 9:
                        mysteriousPotionColours[i] = "Purple";
                        break;
                    default:
                        mysteriousPotionColours[i] = "Red";
                        break;
                }
                ElementsAwoken.DebugModeText(nums[i] + " " + mysteriousPotionColours[i]);
            }
        }
    }
    public class CreditsLight : GlobalWall
    {
        public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
        {
            if (MyWorld.credits)
            {
                Player player = Main.player[Main.myPlayer];
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (Main.tile[i, j].HasTile == false &&  MyWorld.creditsCounter >= modPlayer.screenTransDuration / 2)
                {
                    r = 0.7f;
                    g = 0.7f;
                    b = 0.7f;
                }
            }
        }
    }
}