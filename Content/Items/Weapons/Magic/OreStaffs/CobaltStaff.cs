using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.OreStaffs
{
    public class CobaltStaff : OreStaffsClass
    {
        public override int Damage => 35;
        public override int ProjType => ModContent.ProjectileType<CobaltScythe>();
        public override int Materials => ItemID.CobaltBar;
    }
}