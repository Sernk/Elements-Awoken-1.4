using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Buffs.PetBuffs
{
    public class VoidCrawler : PetBuffsClass
    {
        public override int ProjType => ProjectileType<Projectiles.Pets.VoidCrawler>();
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().voidCrawler = true;
        }
    }
}