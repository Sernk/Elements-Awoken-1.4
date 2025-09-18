using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.OreStaffs
{
    public class OrichalcumStaff : OreStaffsProClass
    {
        public override int Damage => 22;
        public override int ProjType => ModContent.ProjectileType<OrichalcumPetal>();
        public override int Materials => ItemID.OrichalcumBar;
    }
}