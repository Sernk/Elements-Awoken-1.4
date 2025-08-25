using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Elemental
{
    public class ElementalBurst : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 92;
            Item.height = 28;
            Item.damage = 210;
            Item.knockBack = 1.75f;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.UseSound = SoundID.Item91;
            Item.shoot = ModContent.ProjectileType<Projectiles.ElementalBurst>();
            Item.shootSpeed = 24f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 7 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(40));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElementalEssence>(), 5);
            recipe.AddIngredient(ModContent.ItemType<VoiditeBar>(), 8);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}