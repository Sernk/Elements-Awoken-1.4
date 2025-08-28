using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class IceWave : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.direction = (int)Projectile.ai[1];
            Projectile.spriteDirection = (int)Projectile.ai[1];
            if (Projectile.frame < 3)
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 3)
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                }
            }
            return true;
        }
        public override void AI()
        {
            //projectile.ai[0]; is the strength of the shockwave
            //projectile.ai[1]; is the direction
            Point projPos = Projectile.Center.ToTileCoordinates();

            Point nextUnderTilePoint = new Point(projPos.X + (int)Projectile.ai[1], projPos.Y + 1);
            Tile nextUnderTile = Framing.GetTileSafely(nextUnderTilePoint.X, nextUnderTilePoint.Y);

            Point nextTilePoint = new Point(projPos.X + (int)Projectile.ai[1], projPos.Y);
            Tile nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);

            if (Projectile.ai[0] > 0)
            {
                if (Main.tileSolid[nextTile.TileType] && nextTile.HasTile && !TileID.Sets.Platforms[nextTile.TileType])
                {
                    nextTilePoint.Y -= 1;
                    nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);
                    if (Main.tileSolid[nextTile.TileType] && nextTile.HasTile && !TileID.Sets.Platforms[nextTile.TileType])
                    {
                        return;
                    }
                }
                if ((!Main.tileSolid[nextUnderTile.TileType] && nextUnderTile.HasTile) || !nextUnderTile.HasTile)
                {
                    nextUnderTilePoint.Y += 1;
                    nextUnderTile = Framing.GetTileSafely(nextUnderTilePoint.X, nextUnderTilePoint.Y);
                    nextTilePoint.Y += 1;
                    nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);
                    if ((!Main.tileSolid[nextUnderTile.TileType] && nextUnderTile.HasTile) || !nextUnderTile.HasTile)
                    {
                        return;
                    }
                }
                Projectile.localAI[0]++;
                if (Projectile.localAI[0] == 3)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.Proj(Projectile), nextTilePoint.X * 16 + 8, nextTilePoint.Y * 16, 0f, 0f, Projectile.type, Projectile.damage, 0f, Projectile.owner, Projectile.ai[0] - 1, Projectile.ai[1])];
                    for (int i = 0; i < 4; i++)
                    {
                        Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(nextUnderTilePoint.X, nextUnderTilePoint.Y, nextUnderTile)];
                        dust.velocity.Y = Main.rand.NextFloat(-0.2f, -3f);
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.velocity.Y -= Main.rand.NextFloat(5f,12f) * target.knockBackResist;
        }
    }
}