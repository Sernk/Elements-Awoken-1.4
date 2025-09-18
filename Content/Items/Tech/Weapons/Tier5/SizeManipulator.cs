using ElementsAwoken.Content.Items.Tech.Materials;
using ElementsAwoken.Content.Items.Tech.Weapons.Tier4;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Weapons.Tier5
{
    public class SizeManipulator : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 10;
            Item.knockBack = 1f;
            Item.GetGlobalItem<ItemEnergy>().energy = 25;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item12;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.shootSpeed = 6f;
            Item.shoot = ModContent.ProjectileType<GrowinatorP>();
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2) type = ModContent.ProjectileType<ShrinkinatorP>();
            else type = ModContent.ProjectileType<GrowinatorP>();
            Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofSight, 4);
            recipe.AddIngredient(ItemID.HallowedBar, 2);
            recipe.AddIngredient(ModContent.ItemType<Transistor>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Shrinkinator>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Growinator>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}