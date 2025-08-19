using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Permafrost
{
    public class HomingIce : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/NPCProj/Permafrost/IceRain"; } }

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 44;

            Projectile.hostile = true;

            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation += 0.05f;

            Player P = Main.LocalPlayer;
            float goToX = P.Center.X - Projectile.Center.X;
            float goToY = P.Center.Y - Projectile.Center.Y;
            float speed = 0.2f;
            if (Projectile.velocity.X <goToX)
            {
                Projectile.velocity.X = Projectile.velocity.X + speed;
                if (Projectile.velocity.X < 0f &&goToX > 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X + speed;
                }
            }
            else if (Projectile.velocity.X >goToX)
            {
                Projectile.velocity.X = Projectile.velocity.X - speed;
                if (Projectile.velocity.X > 0f &&goToX < 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X - speed;
                }
            }
            if (Projectile.velocity.Y < goToY)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + speed;
                if (Projectile.velocity.Y < 0f && goToY > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + speed;
                    return;
                }
            }
            else if (Projectile.velocity.Y > goToY)
            {
                Projectile.velocity.Y = Projectile.velocity.Y - speed;
                if (Projectile.velocity.Y > 0f && goToY < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - speed;
                    return;
                }
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Frostburn, 120, false);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            float speed = 5f;
            float numberProjectiles = MyWorld.awakenedMode ? 6 : 4;
            float rotation = MathHelper.ToRadians(360);
            float rotateExtra = MathHelper.ToRadians(Main.rand.Next(90));
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                perturbedSpeed.RotatedBy(rotateExtra);
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<FrostExplosion>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            }
        }
    }
}