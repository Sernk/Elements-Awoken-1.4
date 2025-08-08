using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Azana
{
    public class AzanaGiantCloud : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 420;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.96f;

            Projectile.ai[1]--;
            if (Projectile.ai[1] <= 0)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center, new Vector2(Main.rand.NextFloat(-3.5f, 3.5f), Main.rand.NextFloat(-1.5f, 1.5f)), ModContent.ProjectileType<AzanaCloud>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
                Projectile.ai[1] = Main.rand.Next(2, 12);
            }
            if (MathHelper.Distance(Projectile.velocity.X, 0) < 0.1f && MathHelper.Distance(Projectile.velocity.Y, 0) < 0.1f) Projectile.velocity = Vector2.Zero;
            if (Projectile.velocity == Vector2.Zero)
            {
                Projectile.ai[0]++;
                Vector2 offset = new Vector2(400, 0);
                float rotateSpeed = 0.005f;
                Projectile.ai[0] += 1;
                float spinAI = Projectile.ai[0] / rotateSpeed;

                if (Projectile.ai[0] % 60 == 0)
                {
                    if (Projectile.ai[0] % 120 == 0) SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/GiantLaser"));

                    float speed = 3f;
                    Vector2 shootTarget1 = Projectile.Center + offset.RotatedBy(spinAI);
                    float rotation = (float)Math.Atan2(Projectile.Center.Y - shootTarget1.Y, Projectile.Center.X - shootTarget1.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center + projSpeed * 20, projSpeed, ModContent.ProjectileType<AzanaBeam>(), Projectile.damage, 0f, Main.myPlayer);
                }
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<ChaosBurn>(), 180);
        }
    }
}