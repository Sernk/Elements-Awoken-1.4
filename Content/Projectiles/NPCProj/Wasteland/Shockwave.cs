using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Wasteland
{
    public class Shockwave : ModProjectile
    {
        public int whichSand = 0;
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 20;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 15;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
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
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D projTexture = TextureAssets.Projectile[Projectile.type].Value;
            int width = projTexture.Width / 4;
            int height = projTexture.Height / Main.projFrames[Projectile.type];
            int drawX = width * whichSand;

            Rectangle rectangle = new Rectangle(drawX, height * Projectile.frame, width, height);
            Vector2 origin = rectangle.Size() / 4f - new Vector2(4,4);

            Main.spriteBatch.Draw(projTexture, Projectile.position - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Point projPos = Projectile.Center.ToTileCoordinates();
            Tile currTile = Framing.GetTileSafely(projPos.X, projPos.Y);
            Tile currTileUnder = Framing.GetTileSafely(projPos.X, projPos.Y + 1);

            Point nextUnderTilePoint = new Point(projPos.X + (int)Projectile.ai[1], projPos.Y + 1);
            Tile nextUnderTile = Framing.GetTileSafely(nextUnderTilePoint.X, nextUnderTilePoint.Y);

            Point nextTilePoint = new Point(projPos.X + (int)Projectile.ai[1], projPos.Y);
            Tile nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);

            if (currTileUnder.TileType == TileID.Sand)
            {
                whichSand = 0;
            }
            else if (currTileUnder.TileType == TileID.Ebonsand)
            {
                whichSand = 1;
            }
            else if (currTileUnder.TileType == TileID.Crimsand)
            {
                whichSand = 2;
            }
            else if (currTileUnder.TileType == TileID.Pearlsand)
            {
                whichSand = 3;
            }
            else
            {
                whichSand = 2;
            }
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
                if (!Main.tileSolid[nextUnderTile.TileType] && nextUnderTile.HasTile || !nextUnderTile.HasTile)
                {
                    nextUnderTilePoint.Y += 1;
                    nextUnderTile = Framing.GetTileSafely(nextUnderTilePoint.X, nextUnderTilePoint.Y);
                    nextTilePoint.Y += 1;
                    nextTile = Framing.GetTileSafely(nextTilePoint.X, nextTilePoint.Y);
                    if (!Main.tileSolid[nextUnderTile.TileType] && nextUnderTile.HasTile || !nextUnderTile.HasTile)
                    {
                        return;
                    }
                }
                Projectile.localAI[0]++;
                if (Projectile.localAI[0] == 5)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), nextTilePoint.X * 16 + 8, nextTilePoint.Y * 16 + 8, 0f, 0f, ModContent.ProjectileType<Shockwave>(), 0, 0f, Projectile.owner, Projectile.ai[0] - 1, Projectile.ai[1]);
                    for (int i = 0; i < 4; i++)
                    {
                        Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(nextUnderTilePoint.X, nextUnderTilePoint.Y, nextUnderTile)];
                        dust.velocity.Y = Main.rand.NextFloat(-0.2f, -3f);
                    }
                }
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.velocity.Y -= 15f;
        }
    }
}