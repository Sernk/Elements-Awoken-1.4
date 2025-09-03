using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class CarapaceSlicerP : ModProjectile
    {
        public int stickID = -1;
        public float stickOffX = 0;
        public float stickOffY = 0;
        public float stickTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(stickID);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            stickID = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 1600;
            AIType = 52;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Carapace Slicer");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] = 1;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            return false;
        }
        public override void AI()
        {
            if (stickID > -1)
            {
                stickTimer++;
                NPC stick = Main.npc[stickID];
                if (stick.active)
                {
                    Projectile.Center = stick.Center - new Vector2(stickOffX,stickOffY);
                    Projectile.gfxOffY = stick.gfxOffY;
                }
                else stickID = -2;
                if (stickTimer > 30) stickID = -2;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (stickID == -1)
            {
                stickID = target.whoAmI;
                stickOffX = target.Center.X - Projectile.Center.X + Projectile.velocity.X * 3;
                stickOffY = target.Center.Y - Projectile.Center.Y + Projectile.velocity.Y * 3;
            }
        }
    }
}