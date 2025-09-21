using ElementsAwoken.Content.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class ShadeOfLightP : ModProjectile
    {
        public float shootTimer = 0f;

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 6;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 1600;
            AIType = 52;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shade Of Light");
        }
        public override void AI()
        {
            shootTimer--;
            if (Projectile.owner == Main.myPlayer)
            {
                float max = 400f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 4f;
                        float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                        if (shootTimer <= 0)
                        {
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
                            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<ShadeOfLightBall>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
                            shootTimer = Main.rand.Next(30,90);
                        }
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Glowing>(), 180);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] += 0.1f;
            if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X;
            if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }     
    }
}