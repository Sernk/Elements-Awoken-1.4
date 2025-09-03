namespace ElementsAwoken.EASystem.EAMinion
{
    public abstract class Minion : Terraria.ModLoader.ModProjectile
    {
        public override void AI()
        {
            CheckActive();
            Behavior();
        }
        public abstract void CheckActive();
        public abstract void Behavior();
    }
}