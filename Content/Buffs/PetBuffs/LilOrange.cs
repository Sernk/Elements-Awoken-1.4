using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PetBuffs
{
    public class LilOrange : PetBuffsClass
    {
        public override int ProjType => ModContent.ProjectileType<Projectiles.Pets.LilOrange>();
        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<MyPlayer>().lilOrange = true;
        }
    }
}