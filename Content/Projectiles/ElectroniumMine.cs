using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ElectroniumMine : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if ((Projectile.velocity.X > 0.5f || Projectile.velocity.X < -0.5f) || (Projectile.velocity.Y > 0.5f || Projectile.velocity.Y < -0.5f))
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / 6f * (float)i;
                    dust.noGravity = true;
                    dust.scale = 1f;
                }
            }
            try
            {
                int num187 = (int)(Projectile.position.X / 16f) - 1;
                int num188 = (int)((Projectile.position.X + (float)Projectile.width) / 16f) + 2;
                int num189 = (int)(Projectile.position.Y / 16f) - 1;
                int num190 = (int)((Projectile.position.Y + (float)Projectile.height) / 16f) + 2;
                if (num187 < 0)
                {
                    num187 = 0;
                }
                if (num188 > Main.maxTilesX)
                {
                    num188 = Main.maxTilesX;
                }
                if (num189 < 0)
                {
                    num189 = 0;
                }
                if (num190 > Main.maxTilesY)
                {
                    num190 = Main.maxTilesY;
                }
                int num3;
                for (int num191 = num187; num191 < num188; num191 = num3 + 1)
                {
                    for (int num192 = num189; num192 < num190; num192 = num3 + 1)
                    {
                        if (Main.tile[num191, num192] != null && Main.tile[num191, num192].HasUnactuatedTile && !TileID.Sets.Platforms[Main.tile[num191, num192].TileType] &&  (Main.tileSolid[(int)Main.tile[num191, num192].TileType] || (Main.tileSolidTop[(int)Main.tile[num191, num192].TileType] && Main.tile[num191, num192].TileFrameY == 0)))
                        {
                            Vector2 vector18;
                            vector18.X = (float)(num191 * 16);
                            vector18.Y = (float)(num192 * 16);
                            if (Projectile.position.X + (float)Projectile.width > vector18.X && Projectile.position.X < vector18.X + 16f && Projectile.position.Y + (float)Projectile.height > vector18.Y && Projectile.position.Y < vector18.Y + 16f)
                            {
                                Projectile.velocity.X = 0f;
                                Projectile.velocity.Y = -0.2f;
                            }
                        }
                        num3 = num192;
                    }
                    num3 = num191;
                }
            }
            catch
            {
            }
            if (Projectile.ai[1] != 0)
            {
                NPC stick = Main.npc[(int)Projectile.ai[0]];
                if (stick.active)
                {
                    Projectile.Center = stick.Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = stick.gfxOffY;
                }
                else Projectile.Kill();
            }
            else
            {
                Projectile.velocity.Y += 0.13f;
                Projectile.rotation += 0.2f * (Projectile.velocity.X * 0.2f);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] == 1) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = target.whoAmI;
            Projectile.ai[1] = 1;
            Projectile.velocity =(target.Center - Projectile.Center) * 0.75f;
            Projectile.netUpdate = true;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 6 }, Projectile.damage, "ranged");
        }
    }
}