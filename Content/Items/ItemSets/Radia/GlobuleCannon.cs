using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Radia
{
    public class GlobuleCannon : ModItem
    {
        private int shotCount = 0;
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 18;
            Item.damage = 360;
            Item.knockBack = 15;
            Item.useTime = 12;
            Item.useAnimation = 36;
            Item.reuseDelay = 24;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.shoot = ProjectileType<RadiantGlobule>();
            Item.shootSpeed = 24f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 40f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) position += muzzleOffset;

            shotCount++;
            SoundEngine.PlaySound(SoundID.Item95.WithPitchOffset(-0.35f), position);
            float angle = 4 * shotCount;
            int numberProjectiles = 3 * shotCount;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(angle));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            if (shotCount >= 3) shotCount = 0;
            return false;
        }
    }
}