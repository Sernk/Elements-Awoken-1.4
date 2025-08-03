using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class IceWrathArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var p = Const.Proj(Projectile);
            var damage = hit.Damage;
            Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
            int type = ModContent.ProjectileType<IceWrathBeam>();
            //up
            Projectile.NewProjectile(p, vector8.X, vector8.Y, -1, -8, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, 1, -8, type, damage, 0f, 0);

            Projectile.NewProjectile(p, vector8.X, vector8.Y, 8, 0, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, -8, 0, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, 8, 8, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, -8, -8, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, -8, 8, type, damage, 0f, 0);
            Projectile.NewProjectile(p, vector8.X, vector8.Y, 8, -8, type, damage, 0f, 0);
            target.AddBuff(BuffID.Frostburn, 200);
        }
    }
}