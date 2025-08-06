using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class NyanBootsTrail : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10000;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = (Projectile.velocity.X > 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            Main.instance.LoadProjectile(250);
            Texture2D texture2D32 = TextureAssets.Projectile[250].Value;
            Vector2 origin9 = new Vector2((float)(texture2D32.Width / 2), 0f);
            Vector2 value36 = new Vector2((float)Projectile.width, (float)Projectile.height) / 2f;
            Color white2 =Color.White;
            white2.A = 127;
            for (int num271 = Projectile.oldPos.Length - 1; num271 > 0; num271--)
            {
                Vector2 vector54 = Projectile.oldPos[num271] + value36;
                if (!(vector54 == value36))
                {
                    Vector2 vector55 = Projectile.oldPos[num271 - 1] + value36;
                    float rotation25 = (vector55 - vector54).ToRotation() - 1.57079637f;
                    Vector2 scale10 = new Vector2(1f, Vector2.Distance(vector54, vector55) / (float)texture2D32.Height);
                    Color color53 = white2 * (1f - (float)num271 / (float)Projectile.oldPos.Length);
                    Texture2D arg_D4F2_1 = texture2D32;
                    Vector2 arg_D4F2_2 = vector54 - Main.screenPosition;
                    Rectangle? sourceRectangle2 = null;
                    EAU.Sb.Draw(arg_D4F2_1, arg_D4F2_2, sourceRectangle2, color53, rotation25, origin9, scale10, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            Projectile.position.X = player.Center.X;
            Projectile.position.Y = player.Center.Y + 5;
            if (!player.active || !modPlayer.nyanBoots)
            {
                Projectile.Kill();
            }
        }
    }
}