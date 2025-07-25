using ElementsAwoken.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ChaosRingShield : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10000;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ring of Chaos");
        }
        public override void AI()
        {
            Player P = Main.player[Projectile.owner];

            Projectile.position.X = P.position.X;
            Projectile.position.Y = P.position.Y;
            if (Projectile.ai[0] == 0)
            {
                int swirlCount = 2;
                int orbital = Projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 16;

                    orbital = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<ChaosRingSwirl>(), Projectile.damage, Projectile.knockBack, Projectile.owner, l * distance, Projectile.whoAmI);
                }
                Projectile.ai[0] = 1;
            }
            if (P.FindBuffIndex(ModContent.BuffType<ChaosShield>()) == -1 || P.dead)
            {
                Projectile.active = false;
            }
        }
    }
}