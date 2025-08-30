using Terraria.ID;

namespace ElementsAwoken.Content.Projectiles.Spears
{
    public class InfernoLanceP : SpearsClass
    {
        public override float HoldoutRangeMin => 50f;
        public override float HoldoutRangeMax => 120f;
        public override int Buffs => BuffID.OnFire;
    }
}