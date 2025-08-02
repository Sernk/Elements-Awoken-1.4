using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AsteroxShieldBase : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
            if (Projectile.ai[0] == 0)
            {
                int swirlCount = 5;
                int orbital = Projectile.whoAmI;
                Projectile.ai[1] = Projectile.whoAmI;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 59;
                    orbital = Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<AsteroxShieldSwirl>(), Projectile.damage, Projectile.knockBack, Projectile.owner, l * distance, Projectile.whoAmI);
                }
                Projectile.ai[0] = 1;
            }
        }
    }
}