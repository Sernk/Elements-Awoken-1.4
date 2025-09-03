using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAMinion;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{   
    public class CorroderMinion : FloatyMinionINFO
    {
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 30;
            Main.projFrames[Projectile.type] = 3;
            Projectile.friendly = true;
            Main.projPet[Projectile.type] = true;
            Projectile.minion = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            Projectile.minionSlots = 0.75f;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 18000;
            inertia = 15f;
            sitType = 1;
            shootType = 1;
        }
        public override void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<CorroderBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.corroder = false;
            }
            if (modPlayer.corroder)
            {
                Projectile.timeLeft = 2;
            }
        }
        public override void Shoot(Vector2 targetPos)
        {
            Vector2 shootVel = targetPos - Projectile.Center;
            if (shootVel == Vector2.Zero)
            {
                shootVel = new Vector2(0f, 1f);
            }
            shootVel.Normalize();
            shootVel *= 12f;
            Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<CorroderSpit>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f)];
            proj.timeLeft = 300;
            proj.netUpdate = true;
            Projectile.netUpdate = true;
        }
        public override void CreateDust()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }
        public override void SelectFrame()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 3;
            }
        }
    }
}