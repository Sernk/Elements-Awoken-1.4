using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class RetinasmP : ModProjectile
    {
        public float timer = 30;
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.extraUpdates = 0;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 270f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 11f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 5f;
        }
        public override void AI()
        {
            timer--;
            float max = 400f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                {
                    int type = ProjectileID.MiniRetinaLaser;
                    SoundStyle sound = SoundID.Item11;
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            type = ProjectileID.MiniRetinaLaser;
                            sound = SoundID.Item33;
                            break;
                        case 1:
                            type = ProjectileID.CursedFlameFriendly;
                            sound = SoundID.Item20;
                            break;
                        default: break;
                    }
                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                    Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    if (timer <= 0)
                    {
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(sound, Projectile.position);
                        timer = Main.rand.Next(15, 40);
                    }
                }
            }            
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 120);
        }
    }
}