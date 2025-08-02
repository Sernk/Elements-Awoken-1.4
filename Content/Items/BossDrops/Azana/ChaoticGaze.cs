using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.Bullets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class ChaoticGaze : ModItem
    {
        public int hitCount = 0;
        public int hitTimer = 0;
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 14;
            Item.damage = 280;
            Item.knockBack = 4;
            Item.crit = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 4;
            Item.useAnimation = 16;
            Item.reuseDelay = 18;
            Item.useStyle = 5;
            Item.rare = RarityType<Rarity13>();
            Item.value = Item.sellPrice(0, 35, 0, 0);
            Item.UseSound = SoundID.Item36;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (type == ProjectileID.Bullet) type = ProjectileType<OutbreakDart>();
            damage = (int)(damage * (1 + MathHelper.Clamp(hitCount / 60, 0, 1)));

            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(4));
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            if (Main.rand.Next(6) == 0)
            {
                SoundEngine.PlaySound(SoundID.Item95, position);
                Projectile.NewProjectile(source, position.X, position.Y, speed.X * 0.75f, speed.Y * 0.75f, ProjectileType<ChaosGazer>(), damage * 3, 0, player.whoAmI, 0f, 0f);
            }
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            hitTimer--;
            if (hitTimer <= 0)
            {
                hitCount = 0;
            }
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .50f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
    }
}
