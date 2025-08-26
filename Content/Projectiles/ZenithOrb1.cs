using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ZenithOrb1 : ModProjectile
    {
        public int shootTimer = 5;
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 400;
            Projectile.light = 2f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zenith");
            Main.projFrames[Projectile.type] = 5;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.997f;
            shootTimer--;
            if (Projectile.owner == Main.myPlayer)
            {
                float num396 = Projectile.position.X;
                float num397 = Projectile.position.Y;
                float num398 = 300f;
                float max = 400f;
                bool flag11 = false;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(Projectile, true) && Vector2.Distance(Projectile.Center, Main.npc[i].Center) <= max)
                    {
                        float num400 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
                        float num401 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
                        float num402 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num400) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num401);
                        if (num402 < num398 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                        {
                            num398 = num402;
                            num396 = num400;
                            num397 = num401;
                            flag11 = true;
                        }
                    }
                }
                if (flag11)
                {
                    float num403 = 30f; //modify the speed the projectile are shot.  Lower number = slower projectile.
                    Vector2 vector29 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num404 = num396 - vector29.X;
                    float num405 = num397 - vector29.Y;
                    float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
                    num406 = num403 / num406;
                    num404 *= num406;
                    num405 *= num406;
                    if (shootTimer <= 0)
                    {
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X - 4f, Projectile.Center.Y, num404, num405, ModContent.ProjectileType<ZenithBeam1>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, 0f);
                        shootTimer = 8;
                    }
                    return;
                }
            }
        }
    }
}