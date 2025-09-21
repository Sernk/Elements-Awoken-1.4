using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class CorruptPenguin : ModProjectile
    {
        public override void SetDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.netImportant = true;
            Projectile.minion = true;
            Projectile.width = 24;
            Projectile.height = 42;
            Projectile.aiStyle = 26;
            AIType = 266;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minionSlots = 1f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity.X == 0)
            {
                Projectile.frame = 0;
            }
            return true;
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
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<CorruptPenguinBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.corruptPenguin = false;
            }
            if (modPlayer.corruptPenguin)
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}