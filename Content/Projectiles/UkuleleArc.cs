using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class UkuleleArc : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.friendly = true;
            Projectile.tileCollide = false;

            Projectile.penetrate = 1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Strange Ukulele");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.whoAmI != (int)Projectile.ai[0])
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            if (Projectile.velocity.X != Projectile.velocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = -Projectile.velocity.X;
            }
            if (Projectile.velocity.Y != Projectile.velocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = -Projectile.velocity.Y;
            }
            int dustLength = 4;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
            }

            NPC target = Main.npc[(int)Projectile.ai[0]]; 
            if (target != null)
            {
                float speed = 5.5f;
                float num4 = target.Center.X - Projectile.Center.X;
                float num5 = target.Center.Y - Projectile.Center.Y;
                float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
                num6 = speed / num6;
                num4 *= num6;
                num5 *= num6;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + num4) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num5) / 21f;
                return;
            }
        }
    }
}