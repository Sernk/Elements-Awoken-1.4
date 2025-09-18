using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Flower
{
    public class MoonglowWand : FlowerClass
    {
        public override int Damage => 20;
        public override int ProjType => ModContent.ProjectileType<MoonglowLight>();
        public override int Materials => ItemID.Moonglow;
    }
}