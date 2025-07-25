using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class Bloodbath : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloodbath");
            // Description.SetDefault("Damage is increased by 20");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1.2f;
        }
    }
}