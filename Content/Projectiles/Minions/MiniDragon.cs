using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class MiniDragon : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 38;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.minion = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.minionSlots = 1;
            Projectile.timeLeft = 18000;
            Main.projFrames[Projectile.type] = 6;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.aiStyle = 62;
            AIType = 375;
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
            player.AddBuff(ModContent.BuffType<MiniDragonBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.miniDragon = false;
            }
            if (modPlayer.miniDragon)
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}