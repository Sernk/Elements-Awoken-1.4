using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class TheUrchinUrchin : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.MaxUpdates = 2;
            Projectile.timeLeft = 500;
            Projectile.scale *= 0.9f;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] >= 60)
            {
                Projectile.alpha += 5;
            }
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
            Projectile.localAI[1]++;
            if (Projectile.localAI[0] >= 10)
            {
                Projectile.tileCollide = true;
            }
            Projectile.velocity.Y += 0.2f;
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            // stick to enemies code
            Rectangle myRect = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
            for (int i = 0; i < 200; i++)
            {
                bool flag = (!Projectile.usesLocalNPCImmunity && !Projectile.usesIDStaticNPCImmunity) || (Projectile.usesLocalNPCImmunity && Projectile.localNPCImmunity[i] == 0) || (Projectile.usesIDStaticNPCImmunity /*&& Projectile.IsNPCImmune(Projectile.type, i)*/);
                if (((Main.npc[i].active && !Main.npc[i].dontTakeDamage) & flag))
                {
                    bool flag2 = false;
                    if (Main.npc[i].trapImmune && Projectile.trap)
                    {
                        flag2 = true;
                    }
                    else if (Main.npc[i].immortal && Projectile.npcProj)
                    {
                        flag2 = true;
                    }

                    bool flag3;
                    {
                        flag3 = Projectile.Colliding(myRect, Main.npc[i].getRect());
                    }
                    if (!flag2 && (Main.npc[i].noTileCollide || !Projectile.ownerHitCheck /*|| Projectile.CanHit(Main.npc[i])*/))
                    {
                        if (flag3)
                        {
                            Projectile.ai[0] = 1f;
                            Projectile.ai[1] = (float)i;
                            Projectile.velocity = (Main.npc[i].Center - Projectile.Center) * 0.75f;
                            Projectile.netUpdate = true;
                        }
                    }
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 111, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                Main.dust[dust].velocity *= 0.6f;
            }
        }
    }
}