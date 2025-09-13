using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class WyvernBreath : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.staff[Item.type] = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.mana = 5;
            Item.UseSound = SoundID.Item42;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<WyvernBreath1>();
            Item.shootSpeed = 13f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Manashard>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}