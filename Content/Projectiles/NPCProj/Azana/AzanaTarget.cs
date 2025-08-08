using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Azana
{
    public class AzanaTarget : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 120;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item95, Projectile.Center);

            int numProj = Main.expertMode ? MyWorld.awakenedMode ? 10 : 8 : 6;
            for (int i = 0; i < numProj; i++)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y - 750, Main.rand.NextFloat(-2,2), Main.rand.NextFloat(14,22), ModContent.ProjectileType<AzanaGlob>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
        public override void AI()
        {
            Projectile.rotation += 0.1f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D shell = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/Azana/AzanaTarget1").Value;
            Vector2 shellOrigin = new Vector2(shell.Width * 0.5f, shell.Height * 0.5f);
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, TextureAssets.Projectile[Projectile.type].Value.Height * 0.5f);
            Vector2 shellPos = Projectile.Center  - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Vector2 drawPos = Projectile.Center  - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor);
            EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, 0, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            EAU.Sb.Draw(shell, shellPos, null, color, Projectile.rotation, shellOrigin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}