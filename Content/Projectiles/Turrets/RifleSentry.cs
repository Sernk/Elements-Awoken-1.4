using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ElementsAwoken.Content.Projectiles.Turrets
{
    public class RifleSentry : TurretBase
    {
        public int energyDrainCD = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(energyDrainCD);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            energyDrainCD = reader.ReadInt32();
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 28;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.timeLeft *= 10;
            Projectile.manualDirectionChange = true;
            Projectile.netImportant = true;
            Projectile.sentry = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            shootCDAmount = 60;
            maxRange = 900f;
            baseTex = "ElementsAwoken/Content/Projectiles/Turrets/RifleSentryBase";
        }
        public override void Shoot(NPC target)
        {
            Vector2 shootVel = target.Center - Projectile.Center;
            if (shootVel == Vector2.Zero) shootVel = new Vector2(0f, 1f);
            shootVel.Normalize();
            shootVel *= 7f;

            SoundEngine.PlaySound(SoundID.Item11, Projectile.position);
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center - new Vector2(0,14), shootVel, ProjectileID.Bullet, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
        public override void ExtraAI()
        {
            Player player = Main.player[Projectile.owner];
            PlayerEnergy energyPlayer = player.GetModPlayer<PlayerEnergy>();
            energyDrainCD--;
            if (energyPlayer.energy < 1)
            {
                Projectile.ai[0] = 0;
                Projectile.rotation = 0;
                Projectile.spriteDirection = 1;
            }
            else if (energyDrainCD <= 0)
            {
                energyPlayer.energy--;
                energyDrainCD = 120;
            }
        }
    }
}