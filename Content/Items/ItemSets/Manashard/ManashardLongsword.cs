using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class ManashardLongsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;         
            Item.damage = 51;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileType<Projectiles.Manaspike>();
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Manashard>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}