using Terraria.ID;

namespace ElementsAwoken.Content.Projectiles.Spears
{
    public class GlacierBreakerP : SpearsClass
    {
        public override float HoldoutRangeMin => 86f;
        public override float HoldoutRangeMax => 170f;
        public override int Buffs => BuffID.Frostburn;
    }
}