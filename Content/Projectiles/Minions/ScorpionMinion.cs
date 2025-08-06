using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class ScorpionMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.OneEyedPirate);
            AIType = ProjectileID.OneEyedPirate;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.minionSlots = 1;
            Projectile.timeLeft = 18000;
            Main.projFrames[Projectile.type] = 15;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.scale *= 0.8f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
        public override void AI()
        {
            bool flag64 = Projectile.type == ModContent.ProjectileType<ScorpionMinion>();
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<ScorpionMinionBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.scorpionMinion = false;
            }
            if (modPlayer.scorpionMinion)
            {
                Projectile.timeLeft = 2;
            }

            Projectile.localAI[0] = 0; // responsible for pooping
            // platform collision
            Vector2 platform = Projectile.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.TileType] && player.Center.Y < Projectile.Center.Y && platformTile.HasTile && Projectile.ai[0] != 1) Projectile.velocity.Y = 0;
        }
    }
}