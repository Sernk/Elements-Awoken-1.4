using ElementsAwoken.Content.Projectiles.Pets;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Buffs.PetBuffs
{
    public class CelestialWatcherBuff : PetBuffsClass
    {
        public override int ProjType => ProjectileType<CelestialWatcher>();
        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<MyPlayer>().royalEye = true;
        }
    }
}