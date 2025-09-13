using ElementsAwoken.Content.Projectiles.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Manashard
{
    public class ManashardPike : ModItem
    {
        public override void SetDefaults()
        {
            Item.height = 60;
            Item.width = 60;
            Item.damage = 45;
            Item.knockBack = 8.75f;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.useTime = 18;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 5;
            Item.shoot = ModContent.ProjectileType<ManashardPikeP>();
            Item.shootSpeed = 9f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
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