using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    public class FireStorm : ModItem
    {
        private int shotNum = 0;
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 18;
            Item.useStyle = 5;
            Item.useAnimation = 15;
            Item.useTime = 5;
            Item.reuseDelay = 19;
            Item.damage = 15;
            Item.shootSpeed = 7.75f;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.rare = 4;
            Item.shoot = 10;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item11, player.position);
            if (shotNum >= 3) shotNum = 0;
            shotNum++;
            if (shotNum == 3)
            {
                type = ModContent.ProjectileType<FirestormShot>();
                damage = (int)(damage * 1.5f);
            }
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.FireEssence, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}