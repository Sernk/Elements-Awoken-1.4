using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class BurningHands : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            EAU.Longer(Type);
        }	
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().dragonfire = true;

            int rand = 1;
            if (player.wet) rand = 4;
            if (Main.rand.NextBool(rand))
            {
                int num1 = Dust.NewDust(player.position, player.width, player.height, 6);
                Main.dust[num1].scale = Main.rand.NextFloat(1.4f,2.2f);
                Main.dust[num1].velocity *= 2f;
                Main.dust[num1].noGravity = true;
            }         
        }
    }
}