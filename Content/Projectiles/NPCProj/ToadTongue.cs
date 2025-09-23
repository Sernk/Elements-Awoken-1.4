using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class ToadTongue : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[0] <= 0f)
            {
                Projectile.alpha -= 200;
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    Projectile.ai[0] = 1f;
                    if (Projectile.ai[1] == 0f)
                    {
                        Projectile.ai[1] += 1f;
                        Projectile.position += Projectile.velocity * 1f;
                    }
                    if (Main.myPlayer == Projectile.owner)
                    {
                        int num47 = Projectile.type;
                        float mult = 1;
                        if (Projectile.ai[1] >= 30f + Main.rand.Next(0,6))
                        {
                            num47 = ProjectileType<ToadTongueTip>();
                            mult = 1.2f;
                        }
                        int num48 = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + Projectile.velocity.X * mult * (Projectile.scale * 0.98f), Projectile.Center.Y + Projectile.velocity.Y * mult * (Projectile.scale * 0.98f), Projectile.velocity.X, Projectile.velocity.Y, num47, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, Projectile.ai[1] + 1f);
                        NetMessage.SendData(27, -1, -1, null, num48, 0f, 0f, 0f, 0, 0, 0);
                        Main.projectile[num48].localAI[1] = Projectile.localAI[1];
                        Main.projectile[num48].localAI[0] = Projectile.localAI[0] - 2;
                        Main.projectile[num48].scale= Projectile.scale * 0.98f;
                        return;
                    }
                }
            }
            else
            {
                Projectile.ai[0]++;
                if (Projectile.ai[0] > Projectile.localAI[0])
                {
                    Projectile.alpha += 60;
                    if (Projectile.alpha >= 255)
                    {
                        Projectile.Kill();
                        return;
                    }
                }
            }
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.Venom, 300);
        }
    }
}