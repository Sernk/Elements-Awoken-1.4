using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using System.IO;
//using ElementsAwoken.Projectiles.NPCProj;
//using ElementsAwoken.Projectiles.GlobalProjectiles;
//using ElementsAwoken.Items.ItemSets.Radia;
//using ElementsAwoken.Buffs.Debuffs;

namespace ElementsAwoken.Content.Events.RadiantRain.Enemies
{
    public class RadiantMasterDeath : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 50;

            NPC.immortal = true;
            NPC.dontTakeDamage = true;

            NPC.aiStyle = -1;
            NPC.lifeMax = 5;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("rw death sound");
        }

        public override void AI()
        {
            NPC.ai[0]++;
            if (NPC.ai[0] > 650)
            {
                NPC.active = false;
            }
        }
    }
}
