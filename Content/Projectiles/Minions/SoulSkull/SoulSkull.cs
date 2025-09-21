using ElementsAwoken.EASystem.EAMinion;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions.SoulSkull
{
    public class SoulSkull : SoulSkullINFO
    {
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Main.projPet[Projectile.type] = true;
            Projectile.minion = true;
            Projectile.netImportant = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            inertia = 30f;
            shoot = ModContent.ProjectileType<Soulflames>();
            shootSpeed = 18f;
            shootCool = 22f;
            ProjectileID.Sets.LightPet[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
        }
        public override void CheckActive()
        {
            bool flag64 = Projectile.type == ModContent.ProjectileType<SoulSkull>();
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<Buffs.MinionBuffs.SoulSkull>(), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.soulSkull = false;
                }
                if (modPlayer.soulSkull)
                {
                    Projectile.timeLeft = 2;
                }
            }
            if (Projectile.direction == -1)
            {
                int num154 = Dust.NewDust(new Vector2(Projectile.Center.X - 6, Projectile.Center.Y + 4), 6, 6, 173, Main.rand.Next(2, 10), -15, 100, default(Color), 1f);
                Main.dust[num154].velocity *= 0.6f;
                Main.dust[num154].scale *= 1.4f;
                Main.dust[num154].noGravity = true;
            }
            if (Projectile.direction == 1)
            {
                int num154 = Dust.NewDust(new Vector2(Projectile.Center.X + 14, Projectile.Center.Y + 4), 6, 6, 173, -Main.rand.Next(2, 10), -15, 100, default(Color), 1f);
                Main.dust[num154].velocity *= 0.6f;
                Main.dust[num154].scale *= 1.4f;
                Main.dust[num154].noGravity = true;
            }
        }
        public override void CreateDust()
        {
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }
    }
}