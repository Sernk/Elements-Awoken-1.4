using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.CosmicObserver
{
    public class EnergyFork : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.damage = 49;
            Item.knockBack = 3f;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.useTime = 16;
            Item.UseSound = SoundID.Item39;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.EnergyFork>();
            Item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CosmicShard>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}