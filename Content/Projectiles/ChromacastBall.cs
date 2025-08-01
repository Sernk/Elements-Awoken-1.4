using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.Content.Projectiles.Explosions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ChromacastBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        int startingDamage = 100;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                startingDamage = Projectile.damage;
                Projectile.localAI[0]++;
            }
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
            int dustID = ModContent.DustType<AncientRed>();
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 20 && Projectile.ai[0] < 40) dustID = ModContent.DustType<AncientGreen>();
            else if (Projectile.ai[0] >= 40 && Projectile.ai[0] < 60) dustID = ModContent.DustType<AncientBlue>();
            else if (Projectile.ai[0] >= 60 && Projectile.ai[0] < 80) dustID = ModContent.DustType<AncientPink>();
            else if (Projectile.ai[0] >= 80) Projectile.ai[0] = 0;
            for (int i = 0; i < 2; i++)
            {
                int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Main.rand.Next(3) <= 1 ? 264 : 31);
                Main.dust[dust1].velocity = Projectile.velocity * 0.2f;
                Main.dust[dust1].scale *= 1.5f;
                Main.dust[dust1].noGravity = true;

                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustID);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[dust].velocity = Projectile.velocity * Main.rand.NextFloat(0.7f,0.9f);
                    Main.dust[dust].scale *= 1.4f;
                }
                else
                {
                    Main.dust[dust].velocity *= 0.6f;
                }
                Main.dust[dust].noGravity = true;
            }

            Projectile.damage = (int)(startingDamage * MathHelper.Lerp(0.05f, 2, (float)Projectile.timeLeft / 300f));
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.townNPC) return false;
            return base.CanHitNPC(target);
        }
        public override void OnKill(int timeLeft)
        {
            Projectile proj = Main.projectile[Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<BigExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f)];
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int num369 = 0; num369 < 30; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.7f;
            }
            for (int num371 = 0; num371 < 20; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, GetDustID(), 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 7f;
                num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, GetDustID(), 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num372].velocity *= 5f;
            }
            int num373 = Gore.NewGore(Const.Proj(Projectile), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(Const.Proj(Projectile), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(Const.Proj(Projectile), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(Const.Proj(Projectile), new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
        private int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return ModContent.DustType<AncientRed>();
                case 1:
                    return ModContent.DustType<AncientGreen>();
                case 2:
                    return ModContent.DustType<AncientBlue>();
                case 3:
                    return ModContent.DustType<AncientPink>();
                default:
                    return ModContent.DustType<AncientRed>();
            }
        }
    }
}