using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions.AqueousMinion
{
    public class AqueousMinionFriendly4 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 66;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 66;
        }
        public override void AI()
        {
            Projectile.position.X = Main.player[Projectile.owner].Center.X + -100;
            Projectile.position.Y = Main.player[Projectile.owner].Center.Y + 50;
            bool flag64 = Projectile.type == ModContent.ProjectileType<AqueousMinionFriendly4>();
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.aqueousMinions = false;
                }
                if (modPlayer.aqueousMinions)
                {
                    Projectile.timeLeft = 2;
                }
            }
            if (player.FindBuffIndex(ModContent.BuffType<AqueousMinions>()) == -1)
            {
                Projectile.Kill();
            }
            if (Projectile.owner == Main.myPlayer)
            {
                if (Projectile.ai[0] != 0f)
                {
                    Projectile.ai[0] -= 1f;
                    return;
                }
                float num396 = Projectile.position.X;
                float num397 = Projectile.position.Y;
                float num398 = 700f;
                bool flag11 = false;
                for (int nPC = 0; nPC < 200; nPC++)
                {
                    if (Main.npc[nPC].CanBeChasedBy(Projectile, true))
                    {
                        float num400 = Main.npc[nPC].position.X + (float)(Main.npc[nPC].width / 2);
                        float num401 = Main.npc[nPC].position.Y + (float)(Main.npc[nPC].height / 2);
                        float num402 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num400) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num401);
                        if (num402 < num398 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[nPC].position, Main.npc[nPC].width, Main.npc[nPC].height))
                        {
                            num398 = num402;
                            num396 = num400;
                            num397 = num401;
                            flag11 = true;
                        }
                        Vector2 direction = Main.npc[nPC].Center - Projectile.Center;
                        Projectile.rotation = direction.ToRotation();
                    }
                }
                if (flag11)
                {
                    float num403 = 30f;
                    Vector2 vector29 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num404 = num396 - vector29.X;
                    float num405 = num397 - vector29.Y;
                    float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
                    num406 = num403 / num406;
                    num404 *= num406;
                    num405 *= num406;
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X - 4f, Projectile.Center.Y, num404, num405, ModContent.ProjectileType<WaterballFriendly>(), 60, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    Projectile.ai[0] = 50f;
                    return;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}