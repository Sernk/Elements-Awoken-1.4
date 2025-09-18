using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.OreStaffs
{
    public class TitaniumStaff : OreStaffsProClass
    {
        public override int Damage => 33;
        public override int ProjType => ModContent.ProjectileType<TitaniumSpike>();
        public override int Materials => ItemID.TitaniumBar;
        public override int ToRadiansQuantity => 10;
    }
}
