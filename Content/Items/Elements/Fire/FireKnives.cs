using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Fire
{
    public class FireKnives : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.damage = 18;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 35;
            Item.useStyle = 1;
            Item.useTime = 35;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 7, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<FireKnife>();
            Item.shootSpeed = 12f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.type == Item.shoot && proj.alpha < 150)
                    {

                        proj.Kill();
                    }
                }
            }
            else
            {
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(5);
                position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 5f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}