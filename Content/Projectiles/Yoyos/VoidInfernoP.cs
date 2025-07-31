using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class VoidInfernoP : ModProjectile
    {
        public float timer = 0;
        public float cooldown = 0;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 450f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 18f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("ExtinctionCurse").Type, 200);
        }
        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, Const.PinkFlame, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            if (cooldown <= 0)
            {
                cooldown = 45;
            }
            cooldown--;
            timer--;
            float max = 600f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                    Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    if (timer <= 0 && cooldown <= 18)
                    {
                        Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<VoidInfernoBlast>(), Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                        timer = 6;
                    }
                }
            }
        }
    }
}