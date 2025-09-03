namespace ElementsAwoken.Content.Projectiles.Spears
{
    public class DragonFangP : SpearsClass
    {
        public override float HoldoutRangeMin => 80f;
        public override float HoldoutRangeMax => 140f;
        public override int Buffs => EAU.Dragonfire;
    }
}