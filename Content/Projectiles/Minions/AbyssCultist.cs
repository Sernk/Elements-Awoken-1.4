using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class AbyssCultist : ModProjectile
    {
        public int shootTimer = 30;

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 5f;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 317;
            Projectile.aiStyle = 54;
            Projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 20)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 7)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.3f, 0.3f);

            Projectile.rotation += Projectile.velocity.X * 0.04f;
            bool flag64 = Projectile.type == ModContent.ProjectileType<AbyssCultist>();
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<AbyssCultistBuff>(), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.abyssCultist = false;
                }
                if (modPlayer.abyssCultist)
                {
                    Projectile.timeLeft = 2;
                }
            }
            ProjectileUtils.PushOtherEntities(Projectile);

            shootTimer--;
            float max = 400f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max && Main.npc[i].CanBeChasedBy(Projectile, false))
                {
                    int numberProjectiles = Main.rand.Next(4,6);
                    Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
                    int type = ModContent.ProjectileType<AbyssCultistBolt>();
                    float Speed = 12f;
                    float rotation = (float)Math.Atan2(vector8.Y - (nPC.position.Y + (nPC.height * 0.5f)), vector8.X - (nPC.position.X + (nPC.width * 0.5f)));
                    int damage = Projectile.damage / 3;
                    if (shootTimer <= 0)
                    {
                        for (int l = 0; l < numberProjectiles; l++)
                        {
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(90));
                            Projectile.NewProjectile(EAU.Proj(Projectile), vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 0f, Main.myPlayer, 0f, 0f);
                        }
                        shootTimer = 30;
                    }
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