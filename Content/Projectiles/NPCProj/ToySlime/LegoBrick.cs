using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.ToySlime
{
    public class LegoBrick : ModProjectile
    {
        public int type = 0;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 600;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 3;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = type;
            return true;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                type = Main.rand.Next(0, 3);
                Projectile.localAI[0] = 1;
            }
            Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
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
                        if (Main.tile[num191, num192] != null && Main.tile[num191, num192].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num191, num192].TileType] || (Main.tileSolidTop[(int)Main.tile[num191, num192].TileType] && Main.tile[num191, num192].TileFrameY == 0)))
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
        }
    }
}