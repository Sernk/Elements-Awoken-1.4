using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class AwokenHealing : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 20;
            if (Main.rand.NextBool(20))
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 3)];
                dust.scale = Main.rand.NextFloat(0.5f, 1.1f);
                dust.velocity *= 0.5f;
                dust.noGravity = true;
            }
        }
    }
}