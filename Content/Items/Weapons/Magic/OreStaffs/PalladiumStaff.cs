using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.OreStaffs
{
    public class PalladiumStaff : OreStaffsProClass
    {
        public override int Damage => 35;
        public override int ProjType => ModContent.ProjectileType<PalladiumSpark>();
        public override int Materials => ItemID.PalladiumBar;
        public override int ToRadiansQuantity => 7;
    }
}