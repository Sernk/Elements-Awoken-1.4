using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Flower
{
    public class ShiverthornWand : FlowerClass
    {
        public override int Damage => 14;
        public override int ProjType => ModContent.ProjectileType<ShiverthornIcicle>();
        public override int Materials => ItemID.Shiverthorn;
    }
}