using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Azana
{
    public class AzanaLaserWarning : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 3600;
            Projectile.tileCollide = false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] <= 20)
            {
                if (Projectile.alpha <= 255) Projectile.alpha -= 255 / 20;            
            }
            else if (Projectile.ai[0] == 60)
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, -24, ModContent.ProjectileType<AzanaInfectionPillar>(), Projectile.damage, 0f, Main.myPlayer, 0, 40)];
                proj.localAI[1] = 125;
                proj.Center = Projectile.Center;
            }
            else if (Projectile.ai[0] >= 60)
            {
                Projectile.alpha += 255 / 30;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {      
            EAU.Sb.Draw(TextureAssets.MagicPixel.Value, Projectile.position - new Vector2(3,4000)- Main.screenPosition, null, new Color(217, 107, 84) * (1 - ((float)Projectile.alpha /255f)), Projectile.rotation, Vector2.Zero, new Vector2(6,4), SpriteEffects.None, 0f);
            return false;
        }
    }
}