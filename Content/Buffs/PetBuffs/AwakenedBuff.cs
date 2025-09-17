using ElementsAwoken.Content.Projectiles.Pets;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PetBuffs
{
    public class AwakenedBuff : PetBuffsClass
    {
        public override int ProjType => ModContent.ProjectileType<WOKE>();
        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<MyPlayer>().woke = true;
        }
    }
}