using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class VoxusP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 200;
            Projectile.light = 2f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numberProjectiles = 1;
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                int num1 = Main.rand.Next(-30, 30);
                int num2 = Main.rand.Next(300, 500);
                int type = ModContent.ProjectileType<Voxus1>();
                switch (Main.rand.Next(4))
                {
                    case 0: type = ModContent.ProjectileType<Voxus1>(); break;
                    case 1: type = ModContent.ProjectileType<Voxus2>(); break;
                    case 2: type = ModContent.ProjectileType<Voxus3>(); break;
                    case 3: type = ModContent.ProjectileType<Voxus4>(); break;
                    default: break;
                }
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + num1, Projectile.Center.Y - num2, 0, 20, type, Projectile.damage, 0, Projectile.owner);
                int num3 = Main.rand.Next(-500, -300);
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + num1, Projectile.Center.Y - num3, 0, -20, type, Projectile.damage, 0, Projectile.owner);
            }
        }
    }
}