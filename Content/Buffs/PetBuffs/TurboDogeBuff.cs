using ElementsAwoken.Content.Projectiles.Pets;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PetBuffs
{
    public class TurboDogeBuff : PetBuffsClass
    {
        public override int ProjType => ModContent.ProjectileType<TurboDoge>();
        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<MyPlayer>().turboDoge = true;
        }
    }
}