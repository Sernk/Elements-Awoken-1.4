using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class IcicleMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            Projectile.alpha = 100;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 388;
            Projectile.aiStyle = 66;
        }
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.04f;
            bool flag64 = Projectile.type == ModContent.ProjectileType<IcicleMinion>();
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<IcicleBuff>(), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.icicleMinion = false;
                }
                if (modPlayer.icicleMinion)
                {
                    Projectile.timeLeft = 2;
                }
            }
            ProjectileUtils.PushOtherEntities(Projectile);

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}