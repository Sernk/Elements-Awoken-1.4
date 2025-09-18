using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.OreStaffs
{
    public class MythrilStaff : OreStaffsClass
    {
        public override int Damage => 30;
        public override int ProjType => ModContent.ProjectileType<MythrilBomb>();
        public override int Materials => ItemID.MythrilBar;
    }
}
