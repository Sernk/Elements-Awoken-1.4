using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class IceWaveCheck : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 100;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Bottom;
            if (player.itemAnimation <= 5)
            {
                Point tilePoint = new Point((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16);
                if (Framing.GetTileSafely(tilePoint.X, tilePoint.Y).HasTile)
                {
                    SoundEngine.PlaySound(SoundID.Item69, Projectile.Center);
                    Projectile.NewProjectile(EAU.Proj(Projectile), tilePoint.X * 16 + 8, tilePoint.Y * 16 - 16, 0f, 0f, ModContent.ProjectileType<IceWave>(), Projectile.damage, 0f, Main.myPlayer, 24f, player.direction);
                }
                Projectile.Kill();
            }
        }
    }
}