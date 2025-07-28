using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class GoldenWeapons : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Golden Weapons");
            // Description.SetDefault("You deal 10% extra damage");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1.1f;
            Lighting.AddLight(player.Center, 0.5f, 0.4f, 0.1f);
        }
    }
}