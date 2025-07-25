using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.EASystem;
namespace ElementsAwoken.Content.Projectiles
{
    public class ShackledBase : ModProjectile
    {
        float timeFlinging = 0;
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
        }
        public override void AI()
        {
            Player parent = Main.player[(int)Projectile.ai[1]];
            MyPlayer modPlayer = parent.GetModPlayer<MyPlayer>();

            if (modPlayer.forgedShackled <= 0)
            {
                Projectile.Kill();
            }
            Vector2 toTarget = new Vector2(Projectile.Center.X - parent.Center.X, Projectile.Center.Y - parent.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(parent.Center, Projectile.Center) > 250)
            {
                parent.velocity += toTarget * 1.75f;
            }
            if (Vector2.Distance(parent.Center, Projectile.Center) > 350)
            {
                parent.velocity += toTarget * 3;
            }
            if (Vector2.Distance(parent.Center, Projectile.Center) > 450)
            {
                parent.velocity = toTarget * 10;
            }
            if (modPlayer.flingToShackle)
            {
                parent.velocity = toTarget * 20;
                if (Vector2.Distance(parent.Center, Projectile.Center) < 10)
                {
                    modPlayer.flingToShackle = false;
                    parent.velocity = Vector2.Zero;
                }
                timeFlinging++;
                if (timeFlinging > 60)
                {
                    modPlayer.flingToShackle = false;
                }
            }
            else
            {
                timeFlinging = 0;
            }
            if (!parent.active)
            {
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player parent = Main.player[(int)Projectile.ai[1]];

            Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/ShackledBaseChain").Value;

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = parent.MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item37, Projectile.position);
        }
    }
}
    