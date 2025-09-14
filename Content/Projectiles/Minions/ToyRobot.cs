using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class ToyRobot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 40;
            Projectile.aiStyle = 67;
            AIType = ProjectileID.OneEyedPirate;
            Projectile.netImportant = true;
            Projectile.minion = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.timeLeft *= 5;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
        }
        public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 15;
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0) Projectile.Kill();
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<ToyRobotBuff>(), 3600);
            if (player.dead) modPlayer.toyRobot = false;
            if (modPlayer.toyRobot) Projectile.timeLeft = 2;
            Projectile.localAI[0] = 0;
            Vector2 platform = Projectile.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.TileType] && player.Center.Y < Projectile.Center.Y && platformTile.HasTile && Projectile.ai[0] != 1) Projectile.velocity.Y = 0;
        }
    }
}