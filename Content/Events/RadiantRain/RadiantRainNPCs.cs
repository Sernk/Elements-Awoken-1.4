using ElementsAwoken.Content.Events.RadiantRain.Enemies;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Events.RadiantRain
{
    public class RadiantRainNPCs : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (MyWorld.radiantRain && spawnInfo.Player.position.Y / 16 < Main.worldSurface) 
            {
                pool.Clear();
                pool.Add(NPCType<SparklingSlime>(), 0.1f);
                pool.Add(NPCType<RadiantWarrior>(), 0.05f);
                pool.Add(NPCType<StellarStarfish>(), 0.05f);
                pool.Add(NPCType<AllKnowerHead>(), 0.025f);
                pool.Add(NPCType<StarlightGlobule>(), 0.02f);
            }
        }
        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (MyWorld.radiantRain && player.position.Y / 16 < Main.worldSurface)
            {
                spawnRate = 300; //Lower the number, the more spawns
                maxSpawns = 15; //Max spawns of NPCs depending on NPC value             
            }
        }
    }
}