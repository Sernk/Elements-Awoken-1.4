using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class CosmicWrathP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] != 0)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage *= 3;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 160);
            target.immune[Projectile.owner] = 7;

            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] = 0f;
                int num14 = -target.whoAmI - 1;
                Projectile.ai[0] = (float)num14;
                Projectile.velocity = target.Center - Projectile.Center;
            }
        }
        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                }
            }
            if (Projectile.velocity.Y > 18f)
            {
                Projectile.velocity.Y = 18f;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] > 20f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
                    Projectile.velocity.X = Projectile.velocity.X * 0.992f;
                }
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                return;
            }
            Projectile.tileCollide = false;
            if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                Projectile.velocity *= 0.6f;
            }
            else
            {
                Projectile.tileCollide = false;
                int num895 = (int)(-(int)Projectile.ai[0]);
                num895--;
                Projectile.position = Main.npc[num895].Center - Projectile.velocity;
                Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
                if (!Main.npc[num895].active || Main.npc[num895].life < 0)
                {
                    Projectile.tileCollide = true;
                    Projectile.ai[0] = 0f;
                    Projectile.ai[1] = 20f;
                    Projectile.velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= 6f;
                    Projectile.netUpdate = true;
                }
                else if (Projectile.velocity.Length() > (float)((Main.npc[num895].width + Main.npc[num895].height) / 3))
                {
                    Projectile.velocity *= 0.99f;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            var s = Const.Proj(Projectile);
            Projectile.NewProjectile(s, Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<CosmicWrathExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.4f;
            }
            for (int num371 = 0; num371 < 10; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Const.PinkFlame, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 5f;
                num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Const.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num372].velocity *= 3f;
            }
            int num373 = Gore.NewGore(s, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(s,new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(s,new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(s,new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
    }
}