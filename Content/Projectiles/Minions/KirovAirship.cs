using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAMinion;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{   
    public class KirovAirship : MinionINFO
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            inertia = 50f;
            shoot = ProjectileID.RocketI;
            shootCool = 45;
            shootSpeed = 12f;
        }
        public override void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<KirovAirshipBuff>(), 3600);
            if (player.dead)
            {
                modPlayer.kirovAirship = false;
            }
            if (modPlayer.kirovAirship)
            {
                Projectile.timeLeft = 2;
            }
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