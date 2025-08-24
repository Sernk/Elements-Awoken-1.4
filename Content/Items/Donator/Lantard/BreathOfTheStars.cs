using ElementsAwoken.Content.Items.BossDrops.zVanilla;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.Lantard
{
    public class BreathOfTheStars : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 58;
            Item.damage = 72;
            Item.mana = 14;
            Item.knockBack = 2.25f;
            Item.DamageType = DamageClass.Magic;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 11;
            Item.UseSound = SoundID.Item43;
            Item.shoot = ModContent.ProjectileType<StarLunar>();
            Item.shootSpeed = 26f;
            Item.GetGlobalItem<EATooltip>().donator = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Starthrower>(), 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 12);
            recipe.AddIngredient(ItemID.FragmentSolar, 12);
            recipe.AddIngredient(ItemID.FragmentStardust, 12);
            recipe.AddIngredient(ItemID.FragmentVortex, 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float numberProjectiles = 4;
            float rotation = MathHelper.ToRadians(10);
            position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI, i);
            }
            return false;
        }
    }
}