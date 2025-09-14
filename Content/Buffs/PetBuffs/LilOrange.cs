using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PetBuffs
{
    public class LilOrange : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.LilOrange>()] > 0) modPlayer.lilOrange = true;
            if (!modPlayer.lilOrange)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else player.buffTime[buffIndex] = 18000;
        }
    }
}