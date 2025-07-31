using Terraria;

namespace ElementsAwoken.Utilities
{
    public class ProjUtils
    {
        public static int CountProjectiles(int type, int owner)
        {
            int count = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.owner == owner && proj.type == type)
                {
                    count++;
                }
            }
            return count;
        }

        public static bool HasLeastTimeleft(int whoAmI)
        {
            Projectile proj = Main.projectile[whoAmI];
            int minTimeLeft = proj.timeLeft;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (i == whoAmI)
                    continue;

                Projectile other = Main.projectile[i];
                if (other.active && other.owner == proj.owner && other.type == proj.type)
                {
                    if (other.timeLeft < minTimeLeft)
                        return false;
                }
            }

            return true;
        }
    }
}
