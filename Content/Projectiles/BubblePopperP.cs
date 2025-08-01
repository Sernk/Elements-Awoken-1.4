using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BubblePopperP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1, 1, 1);

            Projectile.localAI[1]++;
            if (Projectile.localAI[1] > 4)
            {
                for (int num121 = 0; num121 < 6; num121++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 111)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / 6f * (float)num121;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int numberProjectiles = 1;
            for (int i = 0; i < numberProjectiles; i++)
            {
                int proj = Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.FlaironBubble, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numberProjectiles = 1;
            for (int i = 0; i < numberProjectiles; i++)
            {
                int proj = Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.FlaironBubble, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
            }
        }
    }
}