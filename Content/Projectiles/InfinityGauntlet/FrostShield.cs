using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.InfinityGauntlet
{
    public class FrostShield : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 204;
            Projectile.height = 204;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 60000;
            Projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frost Shield");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 180, false);
            if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage && !target.boss)
            {
                Vector2 knockBack = (target.Center - Projectile.Center);
                target.velocity = (target.velocity + knockBack) / 5;
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            Projectile.rotation += 0.01f;
            if (player.FindBuffIndex(ModContent.BuffType<Buffs.FrostShield>()) == -1)
            {
                Projectile.Kill();
            }
        }
    }
}