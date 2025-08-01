using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan
{
    public class BarrenBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 3600;
            Projectile.tileCollide = false;
        }
		internal const float charge = 220f;
        public float LaserLength { get { return Projectile.localAI[1]; } set { Projectile.localAI[1] = value; } }
        public const float LaserLengthMax = 5000f;
		int multiplier = 1;
		public override bool ShouldUpdatePosition ()
		{
			return false;
		}
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.ai[1] += 6f * multiplier;
            if (Projectile.ai[1] >= charge + 60f && multiplier == 1)
            {
                multiplier = -1;
            }
            if (multiplier == -1 && Projectile.ai[1] <= 0)
                Projectile.Kill();
            Projectile.gfxOffY = player.gfxOffY;

            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
            Projectile.velocity = Vector2.Normalize(Projectile.velocity);

            float[] sampleArray = new float[2];
            Collision.LaserScan(Projectile.Center, Projectile.velocity, 0, LaserLengthMax, sampleArray);
            float sampledLength = 0f;
            for (int i = 0; i < sampleArray.Length; i++)
            {
                sampledLength += sampleArray[i];
            }
            sampledLength /= sampleArray.Length;
            float amount = 0.75f;
            LaserLength = MathHelper.Lerp(LaserLength, sampledLength, amount);

            #region Dusts
            Vector2 endPoint = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
            for (int i = 0; i < 2; i++)
            {
                float num809 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num810 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector79 = new Vector2((float)Math.Cos((double)num809) * num810, (float)Math.Sin((double)num809) * num810);
                int num811 = Dust.NewDust(endPoint, 0, 0, DustID.Firework_Pink, vector79.X, vector79.Y, 0, default(Color), 1f);
                Main.dust[num811].noGravity = true;
                Main.dust[num811].scale = 1.7f;
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 value29 = Projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)Projectile.width;
                int num812 = Dust.NewDust(endPoint + value29 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust3 = Main.dust[num812];
                dust3.velocity *= 0.5f;
                Main.dust[num812].velocity.Y = -Math.Abs(Main.dust[num812].velocity.Y);
            }
            #endregion

            Projectile.ai[0]++;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint = 0f;
            return (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * LaserLength, projHitbox.Width, ref collisionPoint));
        }
        public override bool? CanCutTiles()
        {
            DelegateMethods.tilecut_0 = Terraria.Enums.TileCuttingContext.AttackProjectile;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * LaserLength, (float)Projectile.width * Projectile.scale * 2, CutTilesAndBreakWalls);
            return true;
        }

        private bool CutTilesAndBreakWalls(int x, int y)
        {
            return DelegateMethods.CutTiles(x, y);
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.ai[1] < charge) return false;
            return base.CanHitPlayer(target);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D tailTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/VoidLeviathan/BarrenBeamTail").Value;
            Texture2D beamTex = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D headTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/VoidLeviathan/BarrenBeamHead").Value;
            float num228 = LaserLength;
            Color color44 = Color.White * 0.8f * (Projectile.ai[1] >= charge ? 1f : 0.5f) ;
            Vector2 arg_AF99_2 = Projectile.Center + new Vector2(0, Projectile.gfxOffY) - Main.screenPosition;
            Const.Sb.Draw(tailTex, arg_AF99_2, null, color44, Projectile.rotation, tailTex.Size() / 2f, new Vector2(Math.Min(Projectile.ai[1], charge) / charge, 1f), SpriteEffects.None, 0f);
            num228 -= (float)(tailTex.Height / 2 + headTex.Height) * Projectile.scale;
            Vector2 value20 = Projectile.Center + new Vector2(0, Projectile.gfxOffY);
            value20 += Projectile.velocity * Projectile.scale * (float)tailTex.Height / 2f;
            if (num228 > 0f)
            {
                float num229 = 0f;
                Rectangle rectangle7 = new Rectangle(0, 16 * (Projectile.timeLeft / 3 % 5), beamTex.Width, 16);
                while (num229 + 1f < num228)
                {
                    if (num228 - num229 < (float)rectangle7.Height)
                    {
                        rectangle7.Height = (int)(num228 - num229);
                    }
                    Const.Sb.Draw(beamTex, value20 - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(rectangle7), color44, Projectile.rotation, new Vector2((float)(rectangle7.Width / 2), 0f), new Vector2(Math.Min(Projectile.ai[1], charge) / charge, 1f), SpriteEffects.None, 0f);
                    num229 += (float)rectangle7.Height * Projectile.scale;
                    value20 += Projectile.velocity * (float)rectangle7.Height * Projectile.scale;
                    rectangle7.Y += 16;
                    if (rectangle7.Y + rectangle7.Height > beamTex.Height)
                    {
                        rectangle7.Y = 0;
                    }
                }
            }
            Const.Sb.Draw(headTex, value20 - Main.screenPosition, null, color44, Projectile.rotation, headTex.Frame(1, 1, 0, 0).Top(), new Vector2(Math.Min(Projectile.ai[1], charge) / charge, 1f), SpriteEffects.None, 0f);
            return false;
        }
    }
}