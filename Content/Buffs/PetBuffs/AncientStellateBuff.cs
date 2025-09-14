using ElementsAwoken.Content.Projectiles.Pets;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Buffs.PetBuffs
{
    public class AncientStellateBuff : PetBuffsClass
    {
        public override int ProjType => ProjectileType<AncientStellate>();
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().stellate = true;
        }
    }
}