using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.OreStaffs
{
    public class AdamantiteStaff : OreStaffsClass
    {
        public override int Damage => 42;
        public override int ProjType => ModContent.ProjectileType<AdamantiteLaser>();
        public override int Materials => ItemID.AdamantiteBar;
    }
}