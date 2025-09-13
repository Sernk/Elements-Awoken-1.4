using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Radia
{
    public class RadiantConcentrator : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 18;
            Item.damage = 490;
            Item.knockBack = 2;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item109;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = RarityType<EARarity.Rarity13>();
            Item.shoot = ProjectileType<RadiantBeam>();
            Item.shootSpeed = 16f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 30f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            float pi = 0.314159274f;
            int numProjectiles = 3;
            Vector2 vector14 = new(speed.X, speed.Y);
    
            vector14.Normalize();
            vector14 *= 40f;
            bool flag11 = Collision.CanHit(position, 0, 0, position + vector14, 0, 0);
            for (int num123 = 0; num123 < numProjectiles; num123++)
            {
                float num124 = (float)num123 - ((float)numProjectiles - 1f) / 2f;
                Vector2 vector15 = vector14.RotatedBy((double)(pi * num124), default);
                if (!flag11)
                {
                    vector15 -= vector14;
                }
                if (num123 == 1) type = ProjectileType<RadiantBeam>();
                else type = ProjectileType<RadiantBeamFocused>();
                int num125 = Projectile.NewProjectile(source, position.X + vector15.X, position.Y + vector15.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[num125].noDropItem = true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Radia>(), 16);
            recipe.AddTile(EAU.ChaoticCrucible);
            recipe.Register();
        }
    }
}