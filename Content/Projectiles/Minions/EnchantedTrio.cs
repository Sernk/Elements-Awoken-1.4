using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public abstract class EnchantedTrioBase : ModProjectile
    {
        public bool hasGivenBuff = false;
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 0.33f;
            Projectile.light = 0.5f;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 388;
            Projectile.aiStyle = 66;
            Projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Enchanted Trio");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 9;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (!hasGivenBuff)
            {
                player.AddBuff(ModContent.BuffType<EnchantedTrio>(), 3600);

                hasGivenBuff = true;
            }
            if (player.dead)
            {
                modPlayer.enchantedTrio = false;
            }
            if (modPlayer.enchantedTrio)
            {
                Projectile.timeLeft = 2;
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

    public class EnchantedTrio0 : EnchantedTrioBase { }
    public class EnchantedTrio1 : EnchantedTrioBase { }
    public class EnchantedTrio2 : EnchantedTrioBase { }
}