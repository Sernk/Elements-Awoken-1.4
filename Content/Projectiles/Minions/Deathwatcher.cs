using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class Deathwatcher : ModProjectile
    {
        public int shootTimer = 30;
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 1f;
            Projectile.aiStyle = 54;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 317;
            Projectile.tileCollide = false;
            Projectile.scale *= 1f;
        }
        public override void AI()
        {
            bool flag64 = Projectile.type == ModContent.ProjectileType<Deathwatcher>();
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<DeathwatcherBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.deathwatcher = false;
            }
            if (modPlayer.deathwatcher)
            {
                Projectile.timeLeft = 2;
            }
            shootTimer--;
            float max = 400f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                {
                    int numberProjectiles = 4;
                    Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
                    int type = ModContent.ProjectileType<DeathwatcherBolt>();
                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(vector8.Y - (nPC.position.Y + (nPC.height * 0.5f)), vector8.X - (nPC.position.X + (nPC.width * 0.5f)));
                    int damage = Projectile.damage / 3;
                    if (shootTimer <= 0)
                    {
                        for (int l = 0; l < numberProjectiles; l++)
                        {
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                            Projectile.NewProjectile(EAU.Proj(Projectile), vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 0f, Main.myPlayer, 0f, 0f);
                        }
                        shootTimer = 30;
                    }
                }
            }
            ProjectileUtils.PushOtherEntities(Projectile);
        }
    }
}