using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class WaternadoBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = false;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 250;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.1f;
            if (Projectile.wet)
            {
                Projectile.Kill();
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath19, Projectile.Center);
            int num326 = 36;
            int num3;
            for (int num327 = 0; num327 < num326; num327 = num3 + 1)
            {
                Vector2 vector16 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
                vector16 = vector16.RotatedBy((double)((float)(num327 - (num326 / 2 - 1)) * 6.28318548f / (float)num326), default(Vector2)) + Projectile.Center;
                Vector2 vector17 = vector16 - Projectile.Center;
                int num328 = Dust.NewDust(vector16 + vector17, 0, 0, 172, vector17.X * 2f, vector17.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[num328].noGravity = true;
                Main.dust[num328].noLight = true;
                Main.dust[num328].velocity = vector17;
                num3 = num327;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                if (Projectile.ai[1] < 1f)
                {
                    int num329 = Main.expertMode ? 25 : 40;
                    int num330 = Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X - (float)(Projectile.direction * 30), Projectile.Center.Y - 4f, (float)(-(float)Projectile.direction) * 0.01f, 0f, Mod.Find<ModProjectile>("Waternado").Type, num329, 4f, Projectile.owner, 16f, 15f);
                    Main.projectile[num330].netUpdate = true;
                }
                else
                {
                    int num331 = (int)(Projectile.Center.Y / 16f);
                    int num332 = (int)(Projectile.Center.X / 16f);
                    int num333 = 100;
                    if (num332 < 10)
                    {
                        num332 = 10;
                    }
                    if (num332 > Main.maxTilesX - 10)
                    {
                        num332 = Main.maxTilesX - 10;
                    }
                    if (num331 < 10)
                    {
                        num331 = 10;
                    }
                    if (num331 > Main.maxTilesY - num333 - 10)
                    {
                        num331 = Main.maxTilesY - num333 - 10;
                    }
                    for (int num334 = num331; num334 < num331 + num333; num334 = num3 + 1)
                    {
                        Tile tile = Main.tile[num332, num334];
                        if (tile.HasTile && (Main.tileSolid[(int)tile.TileType] || tile.LiquidAmount != 0))
                        {
                            num331 = num334;
                            break;
                        }
                        num3 = num334;
                    }
                    int num335 = Main.expertMode ? 50 : 80;
                    int num336 = Projectile.NewProjectile(Const.Proj(Projectile), (float)(num332 * 16 + 8), (float)(num331 * 16 - 24), 0f, 0f, ModContent.ProjectileType<Waternado>(), num335, 4f, Main.myPlayer, 16f, 24f);
                    Main.projectile[num336].netUpdate = true;
                }
            }
        }
    }
}