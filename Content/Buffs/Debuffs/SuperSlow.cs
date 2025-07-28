using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class SuperSlow : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Super Slow");
            // Description.SetDefault("You can hardly step");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().superSlow = true;
        }
    }
}