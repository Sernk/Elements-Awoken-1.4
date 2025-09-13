using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.Held.Staffs
{
    public class WretchedStaffHeld : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wretched Staff");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            int chargeDur = 70;
            if (Main.myPlayer == Projectile.owner)
            {
                if ((player.channel && player.HeldItem.mana > 0 && player.CheckMana(player.inventory[player.selectedItem].mana, false, false)) && !player.noItems && !player.CCed)
                {
                    Projectile.ai[1] += 1f;
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.Item43, Projectile.position);
                    int numProj = (int)MathHelper.Lerp(1, 10, MathHelper.Clamp(Projectile.ai[1] / chargeDur, 0, 1));
                    float rotation = (float)Math.Atan2(Projectile.Center.Y -Main.MouseWorld.Y, Projectile.Center.X - Main.MouseWorld.X);
                    for (int i = 0; i < numProj; i++)
                    {
                        float speed = Main.rand.NextFloat(12, 15);
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<PutridSpike>(), Projectile.damage, 0f, 0);
                    }
                    Projectile.Kill();
                }
                if (Projectile.ai[1] <= chargeDur)
                {
                    int num = (int)MathHelper.Lerp(14, 1, MathHelper.Clamp(Projectile.ai[1] / chargeDur, 0, 1));
                    float vel = MathHelper.Lerp(2, 6, MathHelper.Clamp(Projectile.ai[1] / chargeDur, 0, 1));
                    if (Projectile.ai[1] % num == 0)
                    {
                        ProjectileUtils.OutwardsCircleDust(Projectile, 46, 36, vel, true, 150);
                    }

                    int soundDelay = (int)MathHelper.Lerp(12, 2, MathHelper.Clamp(Projectile.ai[1] / chargeDur, 0, 1));
                    if (Projectile.soundDelay <= 0)
                    {
                        Projectile.soundDelay = 2 + soundDelay;
                        Projectile.soundDelay *= 2;
                        SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
                    }
                }
                ProjectileUtils.HeldWandPos(Projectile, player, MathHelper.ToRadians(25));
            }
        }
    }
}
 