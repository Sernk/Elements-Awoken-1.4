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
            bool spawned = true;
            if (player.ownedProjectileCounts[ProjType] > 0)
                spawned = false;
            if (!spawned || player.whoAmI != Main.myPlayer)
                return;
            Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0.0f, 0.0f, ProjType, 0, 0.0f, player.whoAmI, 0.0f, 0.0f);
        }
    }
}