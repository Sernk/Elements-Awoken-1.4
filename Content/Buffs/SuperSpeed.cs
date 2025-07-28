using ElementsAwoken.EASystem;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class SuperSpeed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Super Speed");
            // Description.SetDefault("You move at uncontrollable speeds");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().superSpeed = true;
        }
    }
}