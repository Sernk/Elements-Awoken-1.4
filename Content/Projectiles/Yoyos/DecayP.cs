using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class DecayP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 390f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 17f;
        }
        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 62, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

            int[] array = new int[20];
            int num428 = 0;
            float num429 = 300f;
            bool flag14 = false;
            for (int num430 = 0; num430 < 200; num430++)
            {
                if (Main.npc[num430].CanBeChasedBy(Projectile, false))
                {
                    float num431 = Main.npc[num430].position.X + (float)(Main.npc[num430].width / 2);
                    float num432 = Main.npc[num430].position.Y + (float)(Main.npc[num430].height / 2);
                    float num433 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num431) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num432);
                    if (num433 < num429 && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num430].Center, 1, 1))
                    {
                        if (num428 < 20)
                        {
                            array[num428] = num430;
                            num428++;
                        }
                        flag14 = true;
                    }
                }
            }
            if (flag14)
            {
                int num434 = Main.rand.Next(num428);
                num434 = array[num434];
                float num435 = Main.npc[num434].position.X + (float)(Main.npc[num434].width / 2);
                float num436 = Main.npc[num434].position.Y + (float)(Main.npc[num434].height / 2);
                Projectile.localAI[0] += 1f;
                if (Projectile.localAI[0] > 16f)
                {
                    Projectile.localAI[0] = 0f;
                    float num437 = 6f;
                    Vector2 value10 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    value10 += Projectile.velocity * 8f;
                    float num438 = num435 - value10.X;
                    float num439 = num436 - value10.Y;
                    float num440 = (float)Math.Sqrt((double)(num438 * num438 + num439 * num439));
                    num440 = num437 / num440;
                    num438 *= num440;
                    num439 *= num440;
                    Projectile.NewProjectile(EAU.Proj(Projectile), value10.X, value10.Y, num438, num439, ModContent.ProjectileType<MortemiteFlames>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                    return;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 120);
        }
    }
}