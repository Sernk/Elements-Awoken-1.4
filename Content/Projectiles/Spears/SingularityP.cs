using Terraria.ID;

namespace ElementsAwoken.Content.Projectiles.Spears
{
    public class SingularityP : SpearsClass
    {
        public override float HoldoutRangeMin => 75f;
        public override float HoldoutRangeMax => 150f;
        public override int Buffs => BuffID.OnFire;
    }
}