using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Other
{
    public class HandsOfDespair : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 76;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60000;
            Projectile.light = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hands of Despair");
            Main.projFrames[Projectile.type] = 4;
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
        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return false;
        }
        public override void AI()
        {
            Player parent = Main.player[(int)Projectile.ai[1]];
            NPC parent1 = Main.npc[(int)Projectile.ai[1]];

            //Player player = Main.player[projectile.owner];

            if (Projectile.ai[0] > 0)
            {
                Projectile.position.X = parent1.Center.X - (Projectile.width / 2);
                Projectile.position.Y = parent1.position.Y + 5;
                if (parent1.FindBuffIndex(ModContent.BuffType<Buffs.Debuffs.HandsOfDespair>()) == -1)
                {
                    Projectile.alpha++;
                }
                if (!parent1.active)
                {
                    Projectile.Kill();
                }
                Projectile.hostile = true;
                Projectile.friendly = false;
            }
            else
            {
                Projectile.position.X = parent.Center.X - (Projectile.width / 2);
                Projectile.position.Y = parent.position.Y + 5;
                if (parent.FindBuffIndex(ModContent.BuffType<Buffs.Debuffs.HandsOfDespair>()) == -1)
                {
                    Projectile.alpha += 10;
                }
                if (!parent.active)
                {
                    Projectile.Kill();
                }
                Projectile.hostile = false;
                Projectile.friendly = true;
            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}