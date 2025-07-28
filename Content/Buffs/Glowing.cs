using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class Glowing : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing");
            // Description.SetDefault("You release blinding light\nEvery monster can see you");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Lighting.AddLight(player.Center, 5f, 5f, 5f);
        }
    }
}