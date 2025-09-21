using ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1;
using ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase2;
using ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase2.ShadeWyrm;
using ElementsAwoken.Content.NPCs.Elementals;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Events.VoidEvent
{
    public class VoidEventNPCsGLOBAL : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) // run on clients only
        {
            if (MyWorld.voidInvasionUp && (Main.invasionX == Main.spawnTileX)) //If the invasion is up and the invasion has reached the spawn pos
            {
                pool.Clear();
                if (!Main.dayTime && Main.time <= 16220) // before midnight
                {
                    int numCasters = 0;
                    int numSkulls = 0;
                    int numSkullettes = 0;
                    int numHives = 0;
                    int numFlies = 0;
                    int numImmolators = 0;
                    int numSlimes = 0;
                    int numKnights = 0;
                    int numElementals = 0;
                    int numFliers = 0;

                    for (int i = 0; i < Main.maxNPCs; i++) // to reduce the amount of loops we are doing 
                    {
                        NPC checkNPC = Main.npc[i];
                        if (checkNPC.active)
                        {
                            if (checkNPC.type == NPCType<ZergCaster>()) numCasters++;
                            else if (checkNPC.type == NPCType<AbyssSkull>()) numSkulls++;
                            else if (checkNPC.type == NPCType<AbyssSkullette>()) numSkullettes++;
                            else if (checkNPC.type == NPCType<DimensionalHive>()) numHives++;
                            else if (checkNPC.type == NPCType<VoidFly>()) numFlies++;
                            else if (checkNPC.type == NPCType<Immolator>()) numImmolators++;
                            else if (checkNPC.type == NPCType<ReaverSlime>()) numSlimes++;
                            else if (checkNPC.type == NPCType<VoidKnight>()) numKnights++;
                            else if (checkNPC.type == NPCType<VoidElemental>()) numElementals++;
                            else if (checkNPC.type == NPCType<AccursedFlier>()) numFliers++;
                        }
                    }
                    pool.Clear();
                    if (Main.time >= 5400)
                    {
                        pool.Add(NPCType<ZergCaster>(), 0.1f);

                        pool.Add(NPCType<AbyssSkull>(), 0.2f);

                        pool.Add(NPCType<AbyssSkullette>(), 0.05f);
                    }
                    if (Main.time >= 7200)
                    {
                        float hiveRate = MathHelper.Lerp(0.05f, 0f, MathHelper.Clamp(numHives / 2, 0, 1));
                        pool.Add(NPCType<DimensionalHive>(), hiveRate);
                        pool.Add(NPCType<VoidFly>(), 0.1f);
                        pool.Add(NPCType<AccursedFlier>(), 0.1f);
                    }
                    if (Main.time >= 9000)
                    {
                        pool.Add(NPCType<Immolator>(), 0.09f);
                    }

                    pool.Add(NPCType<ReaverSlime>(), 0.5f);
                    pool.Add(NPCType<VoidKnight>(), 0.4f);
                    pool.Add(NPCType<VoidElemental>(), 0.2f);
                }
                else // after midnight
                {
                    pool.Clear();

                    if (!NPC.AnyNPCs(NPCType<ShadeWyrmHead>())) pool.Add(NPCType<ShadeWyrmHead>(), 0.0001f);

                    int numGolems = NPC.CountNPCS(NPCType<VoidGolem>());
                    if (numGolems > 10) pool.Add(NPCType<VoidGolem>(), 0.0f);
                    else if (numGolems > 5) pool.Add(NPCType<VoidGolem>(), 0.1f);
                    else pool.Add(NPCType<VoidGolem>(), 0.5f);

                    int numHunters = NPC.CountNPCS(NPCType<EtherealHunter>());
                    if (numHunters > 10) pool.Add(NPCType<EtherealHunter>(), 0.0f);
                    else if (numHunters > 5) pool.Add(NPCType<EtherealHunter>(), 0.1f);
                    else pool.Add(NPCType<EtherealHunter>(), 0.5f);

                    int numCrawlers = NPC.CountNPCS(NPCType<VoidCrawler>());
                    if (numCrawlers > 10) pool.Add(NPCType<VoidCrawler>(), 0.0f);
                    else if (numCrawlers > 5) pool.Add(NPCType<VoidCrawler>(), 0.1f);
                    else pool.Add(NPCType<VoidCrawler>(), 0.5f);
                }
            }
        }
        //public void Phase1Drops(NPC npc)
        //{
        //    if (Main.rand.Next(3) == 0)
        //    {
        //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"), 1);
        //    }
        //    if (Main.rand.Next(2) == 0)
        //    {
        //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidStone"), Main.rand.Next(3, 5));
        //    }
        //}
        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (MyWorld.voidInvasionUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                if (Main.time <= 16220)
                {
                    if (Main.time < 5400)
                    {
                        spawnRate = 50;
                        maxSpawns = 100;
                    }
                    if (Main.time >= 5400)
                    {
                        spawnRate = 40;
                        maxSpawns = 100;
                    }
                    if (Main.time >= 7200)
                    {
                        spawnRate = 30;
                        maxSpawns = 100;
                    }
                    if (Main.time >= 9000)
                    {
                        spawnRate = 25;
                        maxSpawns = 100;
                    }
                }
                else
                {
                    spawnRate = 100; //Lower the number, the more spawns
                    maxSpawns = 50; //Max spawns of NPCs depending on NPC value
                }
            }
        }

        public override void PostAI(NPC npc)
        {
            if (MyWorld.voidInvasionUp)
            {
                //Changes NPCs so they do not despawn
                bool tooFar = false;
                if (npc.type != NPCType<ShadeWyrmHead>() || npc.type != NPCType<ShadeWyrmBody>() || npc.type != NPCType<ShadeWyrmTail>())
                {
                    if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) >= 2000)
                    {
                        tooFar = true;
                    }
                    else
                    {
                        tooFar = false;
                    }
                }
                if (Main.invasionX == (double)Main.spawnTileX)
                {
                    if (!tooFar)
                    {
                        npc.timeLeft = 500;
                    }
                    else if (tooFar && !npc.townNPC)
                    {
                        npc.active = false;
                    }
                }
            }
        }
    }
}