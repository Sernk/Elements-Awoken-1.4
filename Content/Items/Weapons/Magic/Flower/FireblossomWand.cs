using ElementsAwoken.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Magic.Flower
{
    public class FireblossomWand : FlowerClass
    {
        public override int Damage => 18;
        public override int ProjType => ModContent.ProjectileType<FireblossomBall>();
        public override int Materials => ItemID.Fireblossom;
        public override float ShotSpeed => 8f;
    }
}