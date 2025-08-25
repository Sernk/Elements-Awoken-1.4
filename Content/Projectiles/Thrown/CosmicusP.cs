using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class CosmicusP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Wrath");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] != 0)
            {
                return false;
            }
            return true;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage *= 3; // илм 3f
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 200);

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
            ProjectileUtils.Explosion(Projectile, new int[] { 242, 197, 6 }, damageType: "thrown");
        }
    }   
}