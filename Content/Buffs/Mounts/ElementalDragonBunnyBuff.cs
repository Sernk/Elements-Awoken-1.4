using ElementsAwoken.Content.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Mounts
{
    public class ElementalDragonBunnyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elemental Dragon Bunny");
            // Description.SetDefault("It happily hops around");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<ElementalDragonBunny>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
