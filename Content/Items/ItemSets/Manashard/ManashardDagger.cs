using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class ManashardDagger : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.damage = 45;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.useStyle = 1;
            Item.useTime = 12;
            Item.knockBack = 3f;
            Item.UseSound = SoundID.Item39;
            Item.autoReuse = true;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.shoot = ModContent.ProjectileType<ManashardDaggerP>();
            Item.shootSpeed = 8f;
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