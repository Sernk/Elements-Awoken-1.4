using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Infernace
{
    public class InfernaceWifeSoul : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            var e = ModContent.GetInstance<EALocalization>();
            for (int l = 0; l < 5; l++)
            {
                Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width * 0.5f, Projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.velocity.Y = Main.rand.NextFloat(-6, -1);
                dust.noGravity = true;
                dust.fadeIn = 1.1f;
                dust = Dust.NewDustPerfect(position, 31, Vector2.Zero);
                dust.velocity.Y = Main.rand.NextFloat(-10, -5);
                dust.noGravity = true;
                dust.fadeIn = 0.9f;
            }
            int between = 270;
            Projectile.ai[0]++;
            if (Projectile.ai[0] == between) CombatText.NewText(Projectile.getRect(), Color.OrangeRed, e.InfernaceWifeSoul, false, false);
            else if (Projectile.ai[0] == between *2 ) CombatText.NewText(Projectile.getRect(), Color.OrangeRed, e.InfernaceWifeSoul1, false, false);
            else if (Projectile.ai[0] == between * 3) CombatText.NewText(Projectile.getRect(), Color.OrangeRed, e.InfernaceWifeSoul2, false, false);
            else if (Projectile.ai[0] == between * 4 && Projectile.ai[1] == 1) CombatText.NewText(Projectile.getRect(), Color.OrangeRed, e.InfernaceWifeSoul3, false, false);
            else if (Projectile.ai[0] > between * 4) Projectile.velocity.Y -= 0.66f;
        }
    }
}