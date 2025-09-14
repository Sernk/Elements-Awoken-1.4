using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PetBuffs
{
    public abstract class PetBuffsClass : ModBuff
    {
        public abstract int ProjType { get; }
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjType] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(EAU.Play(player), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjType, 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}